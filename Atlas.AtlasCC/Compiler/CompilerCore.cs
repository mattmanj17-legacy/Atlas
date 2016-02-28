﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.IO;

namespace Atlas.AtlasCC
{
    public class CompilerExcepion : Exception
    {
        public CompilerExcepion(string msg) : base(msg) { }
    }
    
    public partial class AtlasCCompiler : CBaseListener, IAntlrErrorListener<IToken>
    {
        //errors and warnings are logged to stdout
        public AtlasCCompiler(AtlasCodeGen codeGen)
        {
            m_outStream = Console.Out;
            m_codeGen = codeGen;

            InitDeclarations();
        }

        //point the assembler output (errors and warnings) to a custom locations
        public AtlasCCompiler(AtlasCodeGen codeGen, TextWriter outStream)
        {
            m_outStream = outStream;
            m_codeGen = codeGen;

            InitDeclarations();
        }
        
        public string Compile(ICharStream cSource)
        {
            CLexer lexer = new CLexer(cSource);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CParser parser = new CParser(tokens);
            parser.RemoveErrorListeners();

            parser.AddErrorListener(this);

            try
            {
                CParser.CompilationUnitContext compilationUnit = parser.compilationUnit();
                ParseTreeWalker walker = new ParseTreeWalker();

                CreateVariable("x", new CVariable(CType.FromTypeClass(CTypeClass.CInt),"x"));
                CreateVariable("true", new CVariable(CType.FromTypeClass(CTypeClass.CBool), "true"));
                CreateVariable("false", new CVariable(CType.FromTypeClass(CTypeClass.CBool), "false"));

                walker.Walk(this, compilationUnit);

                return m_codeGen.Emit() + "x : WORD 0\ntrue : BYTE TRUE\nfalse : BYTE FALSE";
            }
            catch (CompilerExcepion e)
            {
                m_outStream.WriteLine("Compilation Failed: " + e.Message);
                return null;
            }
        }

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
            throw new CompilerExcepion("Syntax Error");
        }

        private void SematicError(ParserRuleContext ctx, String msg)
        {
            SematicError(ctx.start, msg);
        }

        private void SematicError(IToken ctx, String msg)
        {
            m_outStream.WriteLine("Error on line " + ctx.Line + ": " + msg);
            throw new CompilerExcepion("Sematic Error");
        }

        private void SafeCall(ParserRuleContext rule, Action func)
        {
            try
            {
                func();
            }
            catch (CompilerExcepion e)
            {
                SematicError(rule, e.Message);
            }
        }

        //destination for errors and warnings
        private readonly TextWriter m_outStream;
        private readonly AtlasCodeGen m_codeGen;
    }
}
