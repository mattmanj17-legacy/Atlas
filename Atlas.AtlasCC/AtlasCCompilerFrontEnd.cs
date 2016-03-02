using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.IO;

namespace Atlas.AtlasCC
{
    public class SemanticException : Exception
    {
        public SemanticException(string msg) : base(msg) { }
    }
    
    public class AtlasCCompilerFrontEnd : CBaseListener, IAntlrErrorListener<IToken>
    {
        //TOP LEVEL INTERFACE

        //by default errors and warnings are logged to stdout
        public AtlasCCompilerFrontEnd() : this(Console.Out){}

        //point the assembler output (errors and warnings) to a custom locations
        public AtlasCCompilerFrontEnd(TextWriter outStream)
        {
            m_outStream = outStream;
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

                walker.Walk(this, compilationUnit);

                string exprs = "";
                foreach(CExpression expr in COperator.Expressions)
                {
                    exprs += expr.Emit();
                }

                foreach(CIdentifier ident in CIdentifier.StaticIdentifiers)
                {
                    exprs += ident.Emit();
                }

                return exprs;
            }
            catch (SemanticException e)
            {
                m_outStream.WriteLine("Compilation Failed: " + e.Message);
                return null;
            }
        }

        public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            m_outStream.Write("Error on line " + line + ": ");
            m_outStream.WriteLine("Syntax Error at " + line + ":" + charPositionInLine);
            m_outStream.WriteLine(msg);

            throw new SemanticException("Syntax Error");
        }

        //error handleing

        private void SematicError(ParserRuleContext ctx, String msg)
        {
            m_outStream.WriteLine("Error on line " + ctx.start.Line + ": \n" + msg);
            m_outStream.WriteLine("Offending Code: ");
            m_outStream.WriteLine("\b\"" + ctx.GetText() + "\"");
            throw new SemanticException("Sematic Error");
        }

        private void SafeCall(ParserRuleContext rule, Action func)
        {
            try
            {
                func();
            }
            catch (SemanticException e)
            {
                SematicError(rule, e.Message);
            }
        }

        //destination for errors and warnings
        private readonly TextWriter m_outStream;

        //expressions
        public override void ExitExpression(CParser.ExpressionContext context)
        {
            if (context.expression() != null)
            {
                // expression ',' assignmentExpression
                SafeCall(context, COperator.CommaOperator);
            }
        }

        public override void ExitConstantExpression(CParser.ConstantExpressionContext context)
        {
            //TODO constant expression optimization
            //constant expressions of floating-point types are always evaluated during run-time
            /*
             * address constant expression
                a null pointer
                lvalue designating an object of static storage duration or a function designator, converted to a pointer either
                    by using the unary address-of operator
                    by casting an integer constant to a pointer
                    by array-to-pointer or function-to-pointer implicet conversion
             */
            //Integer constant expressions are evaluated at compile time
            throw new NotSupportedException("constant expressions not implimented yet");
        }

        public override void ExitAssignmentExpression(CParser.AssignmentExpressionContext context)
        {
            if (context.assignmentExpression() != null)
            {
                // unaryExpression assignmentOperator assignmentExpression

                /*
                    assignmentOperator
                        :   '=' | '*=' | '/=' | '%=' | '+=' | '-=' | '<<=' | '>>=' | '&=' | '^=' | '|='
                        ;
                 */

                string operation = context.assignmentOperator().GetText();

                if (operation == "=")
                {
                    SafeCall(context, COperator.BasicAssignmentOperator);
                }
                else if (operation == "*=")
                {
                    SafeCall(context, COperator.MultiplicationAssignmentOperator);
                }
                else if (operation == "/=")
                {
                    SafeCall(context, COperator.DivisionAssignmentOperator);
                }
                else if (operation == "%=")
                {
                    SafeCall(context, COperator.ModuloAssignmentOperator);
                }
                else if (operation == "+=")
                {
                    SafeCall(context, COperator.AdditionAssignmentOperator);
                }
                else if (operation == "-=")
                {
                    SafeCall(context, COperator.SubtractionAssignmentOperator);
                }
                else if (operation == "<<=")
                {
                    SafeCall(context, COperator.BitwiseLeftShiftAssignmentOperator);
                }
                else if (operation == ">>=")
                {
                    SafeCall(context, COperator.BitwiseRightShiftAssignmentOperator);
                }
                else if (operation == "&=")
                {
                    SafeCall(context, COperator.BitwiseAndAssignmentOperator);
                }
                else if (operation == "^=")
                {
                    SafeCall(context, COperator.BitwiseXorAssignmentOperator);
                }
                else if (operation == "|=")
                {
                    SafeCall(context, COperator.BitwiseOrAssignmentOperator);
                }
            }
        }

        public override void ExitConditionalExpression(CParser.ConditionalExpressionContext context)
        {
            if (context.conditionalExpression() != null)
            {
                // logicalOrExpression ('?' expression ':' conditionalExpression)?

                SafeCall(context, COperator.CoditionalOperator);
            }
        }

        public override void ExitLogicalOrExpression(CParser.LogicalOrExpressionContext context)
        {
            if (context.logicalOrExpression() != null)
            {
                // logicalOrExpression '||' logicalAndExpression
                SafeCall(context, COperator.LogicalOrOperator);
            }
        }

        public override void ExitLogicalAndExpression(CParser.LogicalAndExpressionContext context)
        {
            if (context.logicalAndExpression() != null)
            {
                // logicalAndExpression '&&' inclusiveOrExpression
                SafeCall(context, COperator.LogicalAndOperator);
            }
        }

        public override void ExitInclusiveOrExpression(CParser.InclusiveOrExpressionContext context)
        {
            if (context.inclusiveOrExpression() != null)
            {
                // inclusiveOrExpression '|' exclusiveOrExpression
                SafeCall(context, COperator.BitwiseOrOperator);
            }
        }

        public override void ExitExclusiveOrExpression(CParser.ExclusiveOrExpressionContext context)
        {
            if (context.exclusiveOrExpression() != null)
            {
                // exclusiveOrExpression '^' andExpression
                SafeCall(context, COperator.BitwiseXorOperator);
            }
        }

        public override void ExitAndExpression(CParser.AndExpressionContext context)
        {
            if (context.andExpression() != null)
            {
                // andExpression '&' equalityExpression
                SafeCall(context, COperator.BitwiseAndOperator);
            }
        }

        public override void ExitEqualityExpression(CParser.EqualityExpressionContext context)
        {
            if (context.equalityExpression() != null)
            {
                string operation = context.GetText().Substring(context.equalityExpression().GetText().Length, 2);

                if (operation == "==")
                {
                    //equalityExpression '==' relationalExpression
                    SafeCall(context, COperator.EqualToOperator);
                }
                else
                {
                    //equalityExpression '!=' relationalExpression
                    SafeCall(context, COperator.NotEqualToOperator);
                }
            }
        }

        public override void ExitRelationalExpression(CParser.RelationalExpressionContext context)
        {
            if (context.relationalExpression() != null)
            {
                int contextLength = context.GetText().Length;
                int lhsLength = context.relationalExpression().GetText().Length;
                int rhsLength = context.shiftExpression().GetText().Length;

                string operation = context.GetText().Substring(lhsLength, contextLength - (lhsLength + rhsLength));

                if (operation == "<")
                {
                    //relationalExpression '<' shiftExpression
                    SafeCall(context, COperator.LessThanOperator);
                }
                else if (operation == ">")
                {
                    //relationalExpression '>' shiftExpression
                    SafeCall(context, COperator.GreaterThanOperator);
                }
                else if (operation == "<=")
                {
                    //relationalExpression '<=' shiftExpression
                    SafeCall(context, COperator.LessThanOrEqualOperator);
                }
                else if (operation == ">=")
                {
                    //relationalExpression '>=' shiftExpression
                    SafeCall(context, COperator.GreaterThanOrEqualOperator);
                }
            }
        }

        public override void ExitShiftExpression(CParser.ShiftExpressionContext context)
        {
            if (context.shiftExpression() != null)
            {
                string operation = context.GetText().Substring(context.shiftExpression().GetText().Length, 2);

                if (operation == "<<")
                {
                    //shiftExpression '<<' additiveExpression
                    SafeCall(context, COperator.BitwiseLeftShiftOperator);
                }
                else if (operation == ">>")
                {
                    //shiftExpression '>>' additiveExpression
                    SafeCall(context, COperator.BitwiseRightShiftOperator);
                }
            }
        }

        public override void ExitAdditiveExpression(CParser.AdditiveExpressionContext context)
        {
            if (context.additiveExpression() != null)
            {
                string operation = context.GetText().Substring(context.additiveExpression().GetText().Length, 1);

                if (operation == "+")
                {
                    //additiveExpression '+' multiplicativeExpression
                    SafeCall(context, COperator.AdditionOperator);
                }
                else if (operation == "-")
                {
                    //additiveExpression '-' multiplicativeExpression
                    SafeCall(context, COperator.SubtractionOperator);
                }
            }
        }

        public override void ExitMultiplicativeExpression(CParser.MultiplicativeExpressionContext context)
        {
            if (context.multiplicativeExpression() != null)
            {
                string operation = context.GetText().Substring(context.multiplicativeExpression().GetText().Length, 1);

                if (operation == "*")
                {
                    //multiplicativeExpression '*' castExpression
                    SafeCall(context, COperator.MultiplicationOperator);
                }
                else if (operation == "/")
                {
                    //multiplicativeExpression '/' castExpression
                    SafeCall(context, COperator.DivisionOperator);
                }
                else if (operation == "%")
                {
                    //multiplicativeExpression '%' castExpression
                    SafeCall(context, COperator.ModuloOperator);
                }
            }
        }

        public override void ExitCastExpression(CParser.CastExpressionContext context)
        {
            if (context.typeName() != null)
            {
                //'(' typeName ')' castExpression
                SafeCall(context, () => COperator.TypeCast(context.typeName().GetText()));
            }
        }

        public override void ExitUnaryExpression(CParser.UnaryExpressionContext context)
        {
            if (context.unaryOperator() != null)
            {
                // unaryOperator castExpression

                /*
                   unaryOperator
                    :   '&' | '*' | '+' | '-' | '~' | '!'
                    ;
                 */

                string operation = context.unaryOperator().GetText();

                if (operation == "&")
                {
                    SafeCall(context, COperator.AddressOfOperator);
                }
                else if (operation == "*")
                {
                    SafeCall(context, COperator.DereferenceOperator);
                }
                else if (operation == "+")
                {
                    SafeCall(context, COperator.UnaryPlusOperator);
                }
                else if (operation == "-")
                {
                    SafeCall(context, COperator.UnaryMinusOperator);
                }
                else if (operation == "~")
                {
                    SafeCall(context, COperator.BitwiseNotOperator);
                }
                else if (operation == "!")
                {
                    SafeCall(context, COperator.LogicalNotOperator);
                }
            }
            else if (context.GetText().StartsWith("++"))
            {
                //'++' unaryExpression
                SafeCall(context, COperator.PrefixIncrementOperator);
            }
            else if (context.GetText().StartsWith("--"))
            {
                //'--' unaryExpression
                SafeCall(context, COperator.PrefixDecrementOperator);
            }
            else if (context.GetText().StartsWith("sizeof"))
            {
                if (context.unaryExpression() != null)
                {
                    //'sizeof' unaryExpression
                    SafeCall(context, COperator.SizeOf);
                }
                else
                {
                    //'sizeof' '(' typeName ')'
                    SafeCall(context, () => COperator.SizeOf(context.typeName().GetText()));
                }
            }
            else if (context.Identifier() != null)
            {
                //'&&' Identifier
                SematicError(context, "GCC extension && not supported");
            }
            else if (context.GetText().StartsWith("_Alignof"))
            {
                //'_Alignof' '(' typeName ')'
                SematicError(context, "alignof not supported");
            }
        }

        public override void ExitPostfixExpression(CParser.PostfixExpressionContext context)
        {
            if (context.expression() != null)
            {
                //postfixExpression '[' expression ']'
                SafeCall(context, COperator.SubscriptOperator);
            }
            else if (context.postfixExpression() != null && context.GetText().EndsWith(")"))
            {
                //postfixExpression '(' argumentExpressionList? ')'
                int numArgs = GetArgumentListLength(context.argumentExpressionList());
                SafeCall(context, () => COperator.FunctionCall(numArgs));
            }
            else if (context.Identifier() != null)
            {
                int objStringLength = context.postfixExpression().GetText().Length;
                int idStringLength = context.Identifier().GetText().Length;
                int contextStringSize = context.GetText().Length;

                int operationStringSize = contextStringSize - (objStringLength + idStringLength);

                string idString = context.Identifier().GetText();

                //'.'
                if (operationStringSize == 1)
                {
                    //postfixExpression '.' Identifier
                    SafeCall(context, () => COperator.MemberAccess(idString));
                }
                //'->'
                else
                {
                    //postfixExpression '->' Identifier
                    SafeCall(context, () => COperator.MemberAccessThroughPointer(idString));
                }
            }
            else if (context.GetText().EndsWith("++"))
            {
                //postfixExpression '++'
                SafeCall(context, COperator.PostfixIncrementOperator);
            }
            else if (context.GetText().EndsWith("--"))
            {
                //postfixExpression '--'
                SafeCall(context, COperator.PostfixDecrementOperator);
            }
            else if (context.initializerList() != null)
            {
                /*
                        '(' typeName ')' '{' initializerList '}'
                    |   '(' typeName ')' '{' initializerList ',' '}'
                    |   '__extension__' '(' typeName ')' '{' initializerList '}'
                    |   '__extension__' '(' typeName ')' '{' initializerList ',' '}'
                 */
                SematicError(context, "compound literals not supported");
            }
        }

        public override void ExitPrimaryExpression(CParser.PrimaryExpressionContext context)
        {
            if (context.Identifier() != null)
            {
                //Identifier

                SafeCall(context, () => COperator.PushIdentifier(context.Identifier().GetText()));
            }
            else if (context.Constant() != null)
            {
                //Constant
                SafeCall(context, () => COperator.PushConstant(context.Constant().GetText()));
            }
            else if (context.StringLiteral().Count > 0)
            {
                //StringLiteral+
                var literals = context.StringLiteral().Select(lit => lit.GetText());
                SafeCall(context, () => COperator.PushString(literals));
            }
            else if (context.compoundStatement() != null)
            {
                //'__extension__'? '(' compoundStatement ')'
                SematicError(context, "GCC blocks extention not supported");
            }
            else if (context.GetText().Contains("__builtin_va_arg"))
            {
                //'__builtin_va_arg' '(' unaryExpression ',' typeName ')'
                SematicError(context, "var args not supported");
            }
            else if (context.genericSelection() != null)
            {
                //genericSelection
                SematicError(context, "generic selection not supported");
            }
            else if (context.GetText().StartsWith("__builtin_offsetof"))
            {
                //'__builtin_offsetof' '(' typeName ',' unaryExpression ')'
                SematicError(context, "offsetof not supported");
            }
        }

        private int GetArgumentListLength(CParser.ArgumentExpressionListContext list)
        {
            if (list == null)
            {
                return 0;
            }

            if (list.argumentExpressionList() != null)
            {
                return GetArgumentListLength(list.argumentExpressionList()) + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}
