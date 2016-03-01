using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    partial class AtlasCCompiler : CBaseListener, IAntlrErrorListener<IToken>
    {
        public override void ExitExpression(CParser.ExpressionContext context)
        {
            if (context.expression() != null)
            {
                // expression ',' assignmentExpression
                SafeCall(context, m_codeGen.CommaOperator);
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
                    SafeCall(context, m_codeGen.BasicAssignmentOperator);
                }
                else if (operation == "*=")
                {
                    SafeCall(context, m_codeGen.MultiplicationAssignmentOperator);
                }
                else if (operation == "/=")
                {
                    SafeCall(context, m_codeGen.DivisionAssignmentOperator);
                }
                else if (operation == "%=")
                {
                    SafeCall(context, m_codeGen.ModuloAssignmentOperator);
                }
                else if (operation == "+=")
                {
                    SafeCall(context, m_codeGen.AdditionAssignmentOperator);
                }
                else if (operation == "-=")
                {
                    SafeCall(context, m_codeGen.SubtractionAssignmentOperator);
                }
                else if (operation == "<<=")
                {
                    SafeCall(context, m_codeGen.BitwiseLeftShiftAssignmentOperator);
                }
                else if (operation == ">>=")
                {
                    SafeCall(context, m_codeGen.BitwiseRightShiftAssignmentOperator);
                }
                else if (operation == "&=")
                {
                    SafeCall(context, m_codeGen.BitwiseAndAssignmentOperator);
                }
                else if (operation == "^=")
                {
                    SafeCall(context, m_codeGen.BitwiseXorAssignmentOperator);
                }
                else if (operation == "|=")
                {
                    SafeCall(context, m_codeGen.BitwiseOrAssignmentOperator);
                }
            }
        }

        public override void ExitConditionalExpression(CParser.ConditionalExpressionContext context)
        {
            if (context.conditionalExpression() != null)
            {
                // logicalOrExpression ('?' expression ':' conditionalExpression)?

                SafeCall(context, m_codeGen.CoditionalOperator);
            }
        }

        public override void ExitLogicalOrExpression(CParser.LogicalOrExpressionContext context)
        {
            if (context.logicalOrExpression() != null)
            {
                // logicalOrExpression '||' logicalAndExpression
                SafeCall(context, m_codeGen.LogicalOrOperator);
            }
        }

        public override void ExitLogicalAndExpression(CParser.LogicalAndExpressionContext context)
        {
            if (context.logicalAndExpression() != null)
            {
                // logicalAndExpression '&&' inclusiveOrExpression
                SafeCall(context, m_codeGen.LogicalAndOperator);
            }
        }

        public override void ExitInclusiveOrExpression(CParser.InclusiveOrExpressionContext context)
        {
            if (context.inclusiveOrExpression() != null)
            {
                // inclusiveOrExpression '|' exclusiveOrExpression
                SafeCall(context, m_codeGen.BitwiseOrOperator);
            }
        }

        public override void ExitExclusiveOrExpression(CParser.ExclusiveOrExpressionContext context)
        {
            if (context.exclusiveOrExpression() != null)
            {
                // exclusiveOrExpression '^' andExpression
                SafeCall(context, m_codeGen.BitwiseXorOperator);
            }
        }

        public override void ExitAndExpression(CParser.AndExpressionContext context)
        {
            if (context.andExpression() != null)
            {
                // andExpression '&' equalityExpression
                SafeCall(context, m_codeGen.BitwiseAndOperator);
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
                    SafeCall(context, m_codeGen.EqualToOperator);
                }
                else
                {
                    //equalityExpression '!=' relationalExpression
                    SafeCall(context, m_codeGen.NotEqualToOperator);
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
                    SafeCall(context, m_codeGen.LessThanOperator);
                }
                else if (operation == ">")
                {
                    //relationalExpression '>' shiftExpression
                    SafeCall(context, m_codeGen.GreaterThanOperator);
                }
                else if (operation == "<=")
                {
                    //relationalExpression '<=' shiftExpression
                    SafeCall(context, m_codeGen.LessThanOrEqualOperator);
                }
                else if (operation == ">=")
                {
                    //relationalExpression '>=' shiftExpression
                    SafeCall(context, m_codeGen.GreaterThanOrEqualOperator);
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
                    SafeCall(context, m_codeGen.BitwiseLeftShiftOperator);
                }
                else if (operation == ">>")
                {
                    //shiftExpression '>>' additiveExpression
                    SafeCall(context, m_codeGen.BitwiseRightShiftOperator);
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
                    SafeCall(context, m_codeGen.AdditionOperator);
                }
                else if (operation == "-")
                {
                    //additiveExpression '-' multiplicativeExpression
                    SafeCall(context, m_codeGen.SubtractionOperator);
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
                    SafeCall(context, m_codeGen.MultiplicationOperator);
                }
                else if (operation == "/")
                {
                    //multiplicativeExpression '/' castExpression
                    SafeCall(context, m_codeGen.DivisionOperator);
                }
                else if (operation == "%")
                {
                    //multiplicativeExpression '%' castExpression
                    SafeCall(context, m_codeGen.ModuloOperator);
                }
            }
        }

        public override void ExitCastExpression(CParser.CastExpressionContext context)
        {
            if (context.typeName() != null)
            {
                //'(' typeName ')' castExpression
                SafeCall(context, () => m_codeGen.TypeCast(context.typeName().GetText()));
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
                    SafeCall(context, m_codeGen.AddressOfOperator);
                }
                else if (operation == "*")
                {
                    SafeCall(context, m_codeGen.DereferenceOperator);
                }
                else if (operation == "+")
                {
                    SafeCall(context, m_codeGen.UnaryPlusOperator);
                }
                else if (operation == "-")
                {
                    SafeCall(context, m_codeGen.UnaryMinusOperator);
                }
                else if (operation == "~")
                {
                    SafeCall(context, m_codeGen.BitwiseNotOperator);
                }
                else if (operation == "!")
                {
                    SafeCall(context, m_codeGen.LogicalNotOperator);
                }
            }
            else if (context.GetText().StartsWith("++"))
            {
                //'++' unaryExpression
                SafeCall(context, m_codeGen.PrefixIncrementOperator);
            }
            else if (context.GetText().StartsWith("--"))
            {
                //'--' unaryExpression
                SafeCall(context, m_codeGen.PrefixDecrementOperator);
            }
            else if (context.GetText().StartsWith("sizeof"))
            {
                if (context.unaryExpression() != null)
                {
                    //'sizeof' unaryExpression
                    SafeCall(context, m_codeGen.SizeOf);
                }
                else
                {
                    //'sizeof' '(' typeName ')'
                    SafeCall(context, () => m_codeGen.SizeOf(context.typeName().GetText()));
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
                SafeCall(context, m_codeGen.SubscriptOperator);
            }
            else if (context.postfixExpression() != null && context.GetText().EndsWith(")"))
            {
                //postfixExpression '(' argumentExpressionList? ')'
                int numArgs = GetArgumentListLength(context.argumentExpressionList());
                SafeCall(context, () => m_codeGen.FunctionCall(numArgs));
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
                    SafeCall(context, () => m_codeGen.MemberAccess(idString));
                }
                //'->'
                else
                {
                    //postfixExpression '->' Identifier
                    SafeCall(context, () => m_codeGen.MemberAccessThroughPointer(idString));
                }
            }
            else if (context.GetText().EndsWith("++"))
            {
                //postfixExpression '++'
                SafeCall(context, m_codeGen.PostfixIncrementOperator);
            }
            else if (context.GetText().EndsWith("--"))
            {
                //postfixExpression '--'
                SafeCall(context, m_codeGen.PostfixDecrementOperator);
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

                SafeCall(context, () => m_codeGen.PushIdentifier(context.Identifier().GetText()));
            }
            else if (context.Constant() != null)
            {
                //Constant
                SafeCall(context, () => m_codeGen.PushConstant(context.Constant().GetText()));
            }
            else if (context.StringLiteral().Count > 0)
            {
                //StringLiteral+
                var literals = context.StringLiteral().Select(lit => lit.GetText());
                SafeCall(context, () => m_codeGen.PushString(literals));
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
            if(list == null)
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
