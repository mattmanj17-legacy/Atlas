using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Numerics;
using Atlas.Architecture;

namespace Atlas.Assembler
{
    public class AtlasAssembler : AtlasBaseListener, IAntlrErrorListener<IToken>
    {
        //errors and warnings are logged to stdout
        public AtlasAssembler()
        {
            m_outStream = Console.Out;
        }

        //point the assembler output (errors and warnings) to a custom locations
        public AtlasAssembler(TextWriter outStream)
        {
            m_outStream = outStream;
        }

        //lax/parse/assemble inputfile, and write flat binary out to outputFile
        public byte[] Assemble(ICharStream input)
        {
            try
            {
                //Antlr boilerplate
                AtlasLexer lexer = new AtlasLexer(input);
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                AtlasParser parser = new AtlasParser(tokens);
                parser.RemoveErrorListeners();
                parser.AddErrorListener(this);
                AtlasParser.RootContext root = parser.root();
                ParseTreeWalker walker = new ParseTreeWalker();

                //reset assembler state
                m_currentPass = AssemblerSemanticAnalysisPass.RecordLabels;
                m_fileSize = 0;
                m_labels.Clear();
                m_byteBuffer.Clear();
                
                //perform all passes
                while(m_currentPass != AssemblerSemanticAnalysisPass.Done)
                {
                    //parse the input file
                    //this will trigger listeners for the current pass
                    //TODO fix it so we dont lex/parse on every single pass
                    walker.Walk(this, root);
                }

                return m_byteBuffer.ToArray(); 
            }
            catch (AssemblerException e)
            {
                m_outStream.WriteLine("Assembly Failed: " + e.Message);
                return null;
            }
        }

        //state of the assembler
        private enum AssemblerSemanticAnalysisPass
        {
            RecordLabels,
            CodeGen,
            Done
        }
        private AssemblerSemanticAnalysisPass m_currentPass;

        //AST litener functions (Enter* / Exit*)
        //transistion to the correct pass
        public override void ExitRoot(AtlasParser.RootContext context)
        {
            if (m_currentPass == AssemblerSemanticAnalysisPass.RecordLabels) { m_currentPass = AssemblerSemanticAnalysisPass.CodeGen; }
            else if (m_currentPass == AssemblerSemanticAnalysisPass.CodeGen) { m_currentPass = AssemblerSemanticAnalysisPass.Done; }
        }

        public override void ExitLabelDecl(AtlasParser.LabelDeclContext context)
        {
            if (m_currentPass == AssemblerSemanticAnalysisPass.RecordLabels)
            {
                //m_fileSize++;
                RecordLabel(context.ID().Symbol);

                //Do not add to file size, decause labels do not appeare in the final binary
            }
            if(m_currentPass == AssemblerSemanticAnalysisPass.CodeGen)
            {
                //EmitOpCode(OpCode.NOP);
            }
        }
        
        public override void ExitVariableDecl(AtlasParser.VariableDeclContext context)
        {
            if (m_currentPass == AssemblerSemanticAnalysisPass.RecordLabels)
            {
                RecordLabel(context.ID().Symbol);

                m_fileSize += SizeOfType(context.type());
            }
            else if (m_currentPass == AssemblerSemanticAnalysisPass.CodeGen)
            {
                EmitLiteral(context.literal(), SizeOfType(context.type()));
            }
        }

        public override void ExitArrayDecl(AtlasParser.ArrayDeclContext context)
        {
            if (m_currentPass == AssemblerSemanticAnalysisPass.RecordLabels)
            {
                RecordLabel(context.ID().Symbol);

                m_fileSize += GetArrayInitilizationElementCount(context.arrayInitilizer()) * SizeOfType(context.type());
            }
            else if (m_currentPass == AssemblerSemanticAnalysisPass.CodeGen)
            {
                EmitArray(context.arrayInitilizer(), SizeOfType(context.type()));
            }
        }

        public override void ExitStringDecl(AtlasParser.StringDeclContext context)
        {
            //TODO change parser so we dont have to do this
            string s = context.STRING().GetText().Trim('"');
            
            if (m_currentPass == AssemblerSemanticAnalysisPass.RecordLabels)
            {
                // space for string is number of characters plus null character
                RecordLabel(context.ID().Symbol);
                m_fileSize += (uint)s.Length + 1;
            }
            else if (m_currentPass == AssemblerSemanticAnalysisPass.CodeGen)
            {
                EmitBytes(s);
                //null terminate strings
                EmitByte('\0');
            }
        }

        public override void ExitInstruction(AtlasParser.InstructionContext context)
        {
            OpCode opCode = OpcodeFromInstruction(context);
            UInt32 argSize = ArgSizeFromOpCode(opCode);
            bool needsArg = argSize != 0;

            if (m_currentPass == AssemblerSemanticAnalysisPass.RecordLabels)
            {
                //space for opcode
                m_fileSize++;

                //does this instruction take an argument?
                if (!needsArg) { return; }

                //space for arg
                m_fileSize += argSize;
            }
            else if (m_currentPass == AssemblerSemanticAnalysisPass.CodeGen)
            {
                EmitOpCode(opCode);

                //does this instruction take an argument?
                if (!needsArg) { return; }
                
                //if it does, emit that value
                
                UInt32 argVal = 0;

                //argument is a label
                if (context.ID() != null)
                {
                    //check that the label exists
                    if (!m_labels.ContainsKey(context.ID().Symbol.Text))
                    {
                        SematicError(context, "Label " + context.ID().GetText() + " is undefined");
                    }

                    argVal = m_labels[context.ID().Symbol.Text];
                }
                //argument is a literal
                else if (context.literal() != null)
                {
                    argVal = GetLiteralValue(context.literal());
                }

                EmitBytes(argVal, argSize);
            }
        }
        
        //recordLabels pass
        private void RecordLabel(IToken label)
        {
            if (m_labels.ContainsKey(label.Text))
            {
                SematicError(label, "The label " + label + " was already used!");
            }
            else
            {
                m_labels[label.Text] = m_fileSize;
            }
        }

        private UInt32 m_fileSize = 0;
        private readonly Dictionary<string, UInt32> m_labels = new Dictionary<string, UInt32>();

        //codegen Pass
        private void EmitArray(AtlasParser.ArrayInitilizerContext context, UInt32 bytes)
        {
            if (context.OSQUAREBRACE() != null)
            {
                //emit zeros eqal to the number of elements in the array
                UInt32 size = GetArrayInitilizationElementCount(context);
                for (int i = 0; i < size; i++)
                {
                    EmitBytes(0, bytes);
                }
            }
            else
            {
                foreach (var literal in context.literal())
                {
                    EmitLiteral(literal, bytes);
                }
            }
        }

        private void EmitLiteral(AtlasParser.LiteralContext literal, UInt32 bytes)
        {
            EmitBytes(GetLiteralValue(literal), bytes);
        }

        private void EmitOpCode(OpCode code)
        {
            m_byteBuffer.Add((byte)code);
        }
        
        private void EmitBytes(UInt32 value, UInt32 bytes)
        {
            for (int i = 0; i < bytes; i++)
            {
                m_byteBuffer.Add(BitConverter.GetBytes(value)[i]);
            }
        }

        private void EmitBytes(string value)
        {
            foreach (char c in value)
            {
                EmitByte(c);
            }
        }

        private void EmitByte(char c)
        {
            m_byteBuffer.Add((byte)c);
        }

        private readonly List<byte> m_byteBuffer = new List<byte>();

        //utility functions for working with literals
        private UInt32 GetLiteralValue(AtlasParser.LiteralContext literal)
        {
            UInt32 val = 0;
            if (literal.INT() != null)
            {
                var bytes = BigInteger.Parse(literal.INT().GetText()).ToByteArray();
                val = (UInt32)AtlasCPU.IntFromBytes(bytes.ElementAtOrDefault(3), bytes.ElementAtOrDefault(2), bytes.ElementAtOrDefault(1), bytes.ElementAtOrDefault(0));
            }
            else if (literal.HEX() != null)
            {
                var bytes = BigInteger.Parse(literal.HEX().GetText().TrimStart('0').TrimStart('x'), System.Globalization.NumberStyles.AllowHexSpecifier).ToByteArray();
                val = (UInt32)AtlasCPU.IntFromBytes(bytes.ElementAtOrDefault(3), bytes.ElementAtOrDefault(2), bytes.ElementAtOrDefault(1), bytes.ElementAtOrDefault(0));
            }
            else if (literal.CHAR() != null)
            {
                val = (UInt32)Convert.ToByte(Convert.ToChar(literal.CHAR().GetText().Trim('\'')));
            }
            else if (literal.boolean() != null)
            {
                bool litVal = false;
                if (literal.boolean().TRUE() != null)
                {
                    litVal = true;
                }
                else if (literal.boolean().FALSE() != null)
                {
                    litVal = false;
                }

                val = litVal ? (UInt32)0xFFFFFFFF : (UInt32)0x00000000;
            }
            else if (literal.NULL() != null)
            {
                val = (UInt32)0x00000000;
            }

            return val;
        }

        //utility functions for working with opcodes
        public UInt32 ArgSizeFromOpCode(OpCode code)
        {
            switch (code)
            {
                case OpCode.PUSH:
                    return 4;
                default:
                    return 0;
            }
        }

        public OpCode OpcodeFromString(string s)
        {
            OpCode result;
            if (!Enum.TryParse(s, out result))
            {
                throw new AssemblerException("unrecognized instruction " + s);
            }

            return result;
        }

        private OpCode OpcodeFromInstruction(AtlasParser.InstructionContext context)
        {
            AtlasParser.InstructionCodeContext instructionCode = context.instructionCode();
            AtlasParser.InstructionCodeNeedsArgContext instructionNeedsArgumentCode = context.instructionCodeNeedsArg();

            string s = instructionCode != null ? instructionCode.GetText() : (instructionNeedsArgumentCode != null ? instructionNeedsArgumentCode.GetText() : "MISSINGINSTRUCTIONERROR");
            return OpcodeFromString(s);
        }

        //utility functions for working with types
        private UInt32 SizeOfType(AtlasParser.TypeContext type)
        {
            if (type.BYTE() != null)
            {
                return 1;
            }
            else if (type.HALF() != null)
            {
                return 2;
            }
            else if (type.WORD() != null)
            {
                return 4;
            }
            else
            {
                SematicError(type, "Unrecognized Type " + type.GetText());
                return 0;
            }
        }

        //utility functions for working with array initilizations
        private UInt32 GetArrayInitilizationElementCount(AtlasParser.ArrayInitilizerContext context)
        {
            if (context.OSQUAREBRACE() != null)
            {
                if (context.INT() != null)
                {
                    var bytes = BigInteger.Parse(context.INT().GetText()).ToByteArray();
                    return (UInt32)AtlasCPU.IntFromBytes(bytes.ElementAtOrDefault(3), bytes.ElementAtOrDefault(2), bytes.ElementAtOrDefault(1), bytes.ElementAtOrDefault(0));
                }
                else
                {
                   //TODO i think there are some bugs related to hex literals
                    var bytes = BigInteger.Parse(context.HEX().GetText().TrimStart('0').TrimStart('x'),System.Globalization.NumberStyles.AllowHexSpecifier).ToByteArray();
                    return (UInt32)AtlasCPU.IntFromBytes(bytes.ElementAtOrDefault(3), bytes.ElementAtOrDefault(2), bytes.ElementAtOrDefault(1), bytes.ElementAtOrDefault(0));
                }
            }
            else
            {
                return (UInt32)context.literal().Count;
            }
        }

        //Error handeling
        public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            m_outStream.Write("Error on line " + line + ": ");
            m_outStream.WriteLine("Syntax Error at " + line + ":" + charPositionInLine);
            m_outStream.WriteLine("Antlr message: " + msg);

            List<string> stack = ((Parser)recognizer).GetRuleInvocationStack().Reverse().ToList();
            m_outStream.WriteLine("rule stack: ");
            foreach (string s in stack)
            {
                m_outStream.WriteLine("\t" + s);
            }
            throw new AssemblerException("Syntax Error");
        }

        private void SematicError(ParserRuleContext ctx, String msg)
        {
            SematicError(ctx.start, msg);
        }

        private void SematicError(IToken ctx, String msg)
        {
            m_outStream.WriteLine("Error on line " + ctx.Line + ": " + msg);
            throw new AssemblerException("Sematic Error");
        }

        private class AssemblerException : Exception
        {
            public AssemblerException(string msg) : base(msg) { }
        }

        //destination for errors and warnings
        private readonly TextWriter m_outStream;
    }
}
