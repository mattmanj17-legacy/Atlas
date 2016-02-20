using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Numerics;

//using Atlas.Assembler.

namespace Atlas.Assembler
{
    enum OpCode : byte
    {
        ADD,
        SUB,
        NEG,
        LESS,
        MUL,
        SLL,
        SRL,
        SRA,
        ROR,
        NOT,
        AND,
        OR,
        XOR,
        LB,
        LUB,
        LH,
        LUH,
        LW,
        SB,
        SH,
        SW,
        JMP,
        JIF,
        PUSHBP,
        PUSHB,
        PUSHH,
        PUSHW,
        POPB,
        POPH,
        POPW,
        BEGINARGS,
        CALL,
        RETV,
        RET
    }

    class Assembler : AtlasBaseListener, IAntlrErrorListener<IToken>
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                printUsage();
                Environment.Exit(1);
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("File " + args[0] + " does not exits!");
                Environment.Exit(2);
            }

            try
            {
                AntlrFileStream fileStream = new AntlrFileStream(args[0]);
                AtlasLexer lexer = new AtlasLexer(fileStream);
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                AtlasParser parser = new AtlasParser(tokens);
                Assembler asm = new Assembler();

                parser.RemoveErrorListeners();
                parser.AddErrorListener(asm);

                parser.RemoveParseListeners();
                parser.AddParseListener(asm);

                //first pass: syntax check
                parser.source();
                parser.Reset();
                
                //second pass: label resolution
                parser.source();
                parser.Reset();

                //third pass: code gen
                parser.source();

                asm.BytesToFile(args[1]);
            }
            catch (IOException)
            {
                Console.WriteLine("Could not open file: " + args[0]);
                Environment.Exit(2);
            }
        }

        static void printUsage()
        {
            Console.WriteLine("AtlasAssembler is used to translate a sigle Atlas assembly language file into an object file \n Proper usage is: AtlasAssembler inFileName.extention outFileName.extention");
        }

        public void BytesToFile(string filePath)
        {
            if (File.Exists(filePath)) { File.Delete(filePath); }
            
            using(var stream = File.OpenWrite(filePath))
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.Write(text.ToArray(), 0, text.Count);
            }
        }

        public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            Console.Write("Error on line " + line + ": ");
            Console.WriteLine("Syntax Error at " + line + ":" + charPositionInLine);
            Console.WriteLine("Antlr message: " + msg);

            List<string> stack = ((Parser)recognizer).GetRuleInvocationStack().Reverse().ToList();
            Console.WriteLine("rule stack: ");
            foreach(string s in stack)
            {
                Console.WriteLine("\t" + s);
            }
            Environment.Exit(3);
        }

        private void SematicError(ParserRuleContext ctx, String msg)
        {
            printError(ctx.start, msg);
        }

        private void printError(IToken ctx, String msg)
        {
            Console.WriteLine("Error on line " + ctx.Line + ": " + msg);
            Environment.Exit(4);
        }

        private List<byte> text = new List<byte>();
        private Dictionary<string, UInt32> labels = new Dictionary<string, UInt32>();
        private UInt32 line = 0;
        private int walk = -1;

        public override void EnterSource(AtlasParser.SourceContext context)
        {
            line = 0;
            if (walk > 0)
            {
                EmitBytes("HEADER");
            }
        }

        public override void ExitSource(AtlasParser.SourceContext context)
        {
            if (walk > 0)
            {
                EmitBytes("FOOTER");
            }
            walk++;
        }

        private void AddLabel(IToken label)
        {
            if (labels.ContainsKey(label.Text))
            {
                printError(label, "The label " + label + " was already used!");
            }
            else
            {
                labels[label.Text] = line;
            }
        }

        public override void ExitLabel(AtlasParser.LabelContext context)
        {
            if (walk == 0)
            {
                AddLabel(context.ID().Symbol);
            }
        }

        public override void ExitVariable(AtlasParser.VariableContext context)
        {
            if (walk == 0)
            {
                AddLabel(context.ID().Symbol);

                if (context.type().BYTE() != null)
                {
                    line++;
                }
                else if (context.type().HALF() != null)
                {
                    line += 2;
                }
                else if (context.type().WORD() != null)
                {
                    line += 4;
                }
            }
            else if (walk > 0)
            {
                UInt32 bytes = 0;
                if (context.type().BYTE() != null)
                {
                    bytes = 1;
                }
                else if (context.type().HALF() != null)
                {
                    bytes = 2;
                }
                else if (context.type().WORD() != null)
                {
                    bytes = 4;
                }

                EmitLiteral(context.literal(), bytes);
            }
        }

        private UInt32 UInt32FromBytes(byte b3, byte b2, byte b1, byte b0)
        {
            return (UInt32)((b3 << (8 * 3)) + (b2 << (8 * 2)) + (b1 << (8 * 1)) + (b0 << (8 * 0)));
        }
        
        private UInt32 GetLiteralValue(AtlasParser.LiteralContext literal)
        {
            UInt32 val = 0;
            if (literal.INT() != null)
            {
                var bytes = BigInteger.Parse(literal.INT().GetText()).ToByteArray();
                val = UInt32FromBytes(bytes.ElementAtOrDefault(3), bytes.ElementAtOrDefault(2), bytes.ElementAtOrDefault(1), bytes.ElementAtOrDefault(0));
            }
            else if (literal.HEX() != null)
            {
                var bytes = BigInteger.Parse(literal.HEX().GetText(), System.Globalization.NumberStyles.AllowHexSpecifier).ToByteArray();
                val = UInt32FromBytes(bytes.ElementAtOrDefault(3), bytes.ElementAtOrDefault(2), bytes.ElementAtOrDefault(1), bytes.ElementAtOrDefault(0));
            }
            else if (literal.CHAR() != null)
            {
                val = (UInt32)Convert.ToByte(Convert.ToChar(literal.CHAR().GetText().Trim('\'')));
            }
            else if (literal.BOOL() != null)
            {
                string boolean = literal.BOOL().GetText();
                bool litVal = false;
                if (boolean == "true")
                {
                    litVal = true;
                }
                else if (boolean == "false")
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
        
        private void EmitBytes(UInt32 value, UInt32 bytes)
        {
            for (int i = 0; i < bytes; i++)
            {
                text.Add(BitConverter.GetBytes(value)[i]);
            }
        }

        private void EmitBytes(string value)
        {
            foreach(char c in value)
            {
                EmitByte(c);
            }
        }

        private void EmitByte(char c)
        {
            text.Add((byte)c);
        }
        
        private void EmitLiteral(AtlasParser.LiteralContext literal, UInt32 bytes)
        {
            EmitBytes(GetLiteralValue(literal), bytes);
        }

        private UInt32 GetArraySize(AtlasParser.ArrayContext context)
        {
            if(context.OBRACE() != null)
            {
                if (context.INT() != null)
                {
                    return (UInt32)BigInteger.Parse(context.INT().GetText());
                }
                else
                {
                    return (UInt32)BigInteger.Parse(context.HEX().GetText(), System.Globalization.NumberStyles.AllowHexSpecifier);
                }
            }
            else
            {
                return (UInt32)context.literal().Count;
            }
        }

        private void EmitArray(AtlasParser.ArrayContext context, UInt32 bytes)
        {
            if (context.OBRACE() != null)
            {
                UInt32 size = GetArraySize(context);
                for(int i = 0; i < size; i++)
                {
                    EmitBytes(0, bytes);
                }
            }
            else
            {
                foreach(var literal in context.literal())
                {
                    EmitLiteral(literal, bytes);
                }
            }
        }
        
        public override void ExitArrayDec(AtlasParser.ArrayDecContext context)
        {
            if (walk == 0)
            {
                AddLabel(context.ID().Symbol);
                
                if (context.type().BYTE() != null)
                {
                    line += GetArraySize(context.array());
                }
                else if (context.type().HALF() != null)
                {
                    line += GetArraySize(context.array()) * 2;
                }
                else if (context.type().WORD() != null)
                {
                    line += GetArraySize(context.array()) * 4;
                }
            }
            else if (walk > 0)
            {
                UInt32 bytes = 0;
                if (context.type().BYTE() != null)
                {
                    bytes = 1;
                }
                else if (context.type().HALF() != null)
                {
                    bytes = 2;
                }
                else if (context.type().WORD() != null)
                {
                    bytes = 4;
                }

                EmitArray(context.array(), bytes);
            } 
        }

        public override void ExitString(AtlasParser.StringContext context)
        {
            string s = context.STRING().GetText().Trim('"');
            if(walk == 0)
            {
                AddLabel(context.ID().Symbol);
                // space for string is number of characters plus null character
                line += (uint)s.Length + 1;
            }
            else if(walk > 0)
            {
                EmitBytes(s);
                //null terminate strings
                EmitByte('\0');
            }
        }

        private void EmitOpCode(OpCode code)
        {
            text.Add((byte)code);
        }
        
        public override void ExitInstruction(AtlasParser.InstructionContext context)
        {
            AtlasParser.InstructioncodeContext instructionCode = context.instructioncode();
            OpCode code = OpCode.ADD;

            if (walk == 0)
            {
                //space for opcode
                line++;;
            }
            else if (walk > 0)
            {
                EmitOpCode(code);
            }
        }

        public override void ExitArginstruction(AtlasParser.ArginstructionContext context)
        {
            AtlasParser.ArginstructioncodeContext instructionCode = context.arginstructioncode();
            OpCode code = OpCode.PUSHB;
            UInt32 argSize = 1;

            if(instructionCode.PUSHB() != null)
            {
                code = OpCode.PUSHB;
                argSize = 1;
            }
            else if (instructionCode.PUSHH() != null)
            {
                code = OpCode.PUSHB;
                argSize = 2;
            }
            else if (instructionCode.PUSHW() != null)
            {
                code = OpCode.PUSHB;
                argSize = 4;
            }

            if(walk == 0)
            {
                //space for opcode
                line++;
                //space for arg
                line += argSize;
            }
            else if(walk > 0)
            {
                UInt32 argVal = 0;

                if (context.ID() != null)
                {
                    if (!labels.ContainsKey(context.ID().Symbol.Text))
                    {
                        SematicError(context, "Label " + context.ID().GetText() + " is undefined");
                    }

                    argVal = labels[context.ID().Symbol.Text];
                }
                else if (context.literal() != null)
                {
                    argVal = GetLiteralValue(context.literal());
                }
                
                EmitOpCode(code);
                EmitBytes(argVal, argSize);
            }
        }
    }
}
