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
        public override void EnterExpression(CParser.ExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            
            if (context.parent.GetType() == typeof(CParser.ConditionalExpressionContext))
            {
                // conditional expresion befor first value expresion
                m_codeGen.EmitCoditionalExpresionHeader();
            }
            else if (context.parent.GetType() == typeof(CParser.IterationStatementContext))
            {
                string parentText = context.parent.GetText();
                if (parentText.StartsWith("while"))
                {
                    //while befor expression
                }
                else if (parentText.StartsWith("do"))
                {
                    //do while befor expression
                }
                else if (parentText.StartsWith("for"))
                {
                    if ((context.parent as CParser.IterationStatementContext).declaration() != null)
                    {
                        //for loop with declaration befor expression
                    }
                    else
                    {
                        //for loop without declaration befor expression
                    }
                }
            }
        }
        
        public override void ExitExpression(CParser.ExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            
            if(context.parent.GetType() == typeof(CParser.ExpressionContext))
            {
                //this expression was the first operand to a comma operator, diregard it
                m_codeGen.DisregardLastExpression();
            }
            else if (context.parent.GetType() == typeof(CParser.ExpressionStatementContext))
            {
                //this expression was the an expression statment, disregard it
                m_codeGen.DisregardLastExpression();
            }
            else if (context.parent.GetType() == typeof(CParser.SelectionStatementContext))
            {
                //TODO need to do befor expression
                if (context.parent.GetText().StartsWith("if"))
                {
                    //if statment end expression
                }
                else
                {
                    //switch statment end expression
                }
            }
            else if(context.parent.GetType() == typeof(CParser.IterationStatementContext))
            {
                string parentText = context.parent.GetText();
                if(parentText.StartsWith("while"))
                {
                    //while
                }
                else if (parentText.StartsWith("do"))
                {
                    //do while
                }
                else if (parentText.StartsWith("for"))
                {
                    if((context.parent as CParser.IterationStatementContext).declaration() != null)
                    {
                        //for loop with declaration
                    }
                    else
                    {
                        //for loop without declaration
                    }
                }
            }
            else if(context.parent.GetType() == typeof(CParser.ConditionalExpressionContext))
            {
                // after conditional first value expresion
                m_codeGen.EmitCoditionalExpresionBody();
            }
            else
            {
                //array index
                //do we need to do anything special here?
            }
        }

        //check that an expression is actually constant
        public override void ExitConstantExpression(CParser.ConstantExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            
            if (!m_codeGen.CurrentExpresion.Constant)
            {
                if (context.parent.GetType() == typeof(CParser.LabeledStatementContext))
                {
                    SematicError(context, "value of case statment must be constant");
                }
                else if(context.parent.GetType() == typeof(CParser.EnumeratorContext))
                {
                    SematicError(context, "enumeration value must be constant");
                }
                else if(context.parent.GetType() == typeof(CParser.DesignatorContext))
                {
                    SematicError(context, "Array element designator must be constant expression");
                }
                else
                {
                    //__typeof__, struct declarator, __alignas, _static_assert
                    //these are unsupported, so the parent will produce an error
                    return;
                }
            }

            //todo optimise so constant expressions are not actually calculated
        }

        //TODO : !!!! handle only resolving the lhs to an lvalu, not an rvalue also handel ++ --
        public override void ExitAssignmentExpression(CParser.AssignmentExpressionContext context)
        {
            //handle when parent is a declerator or an initilizer, (do we need to do anything special?)

            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            
            if (context.conditionalExpression() == null)
            {
                string operation = context.assignmentOperator().GetText();

                if(operation == "=")
                {
                    m_codeGen.EmitAssinmentOperation();
                }
                else if (operation == "*=")
                {
                    m_codeGen.EmitCompoundAssinmentMultiply();
                }
                else if (operation == "/=")
                {
                    m_codeGen.EmitCompoundAssinmentDivide();
                }
                else if (operation == "%=")
                {
                    m_codeGen.EmitCompoundAssinmentMod();
                }
                else if (operation == "+=")
                {
                    m_codeGen.EmitCompoundAssinmentAdd();
                }
                else if (operation == "-=")
                {
                    m_codeGen.EmitCompoundAssinmentSub();
                }
                else if (operation == "<<=")
                {
                    m_codeGen.EmitCompoundAssinmentShiftLeft();
                }
                else if (operation == ">>=")
                {
                    m_codeGen.EmitCompoundAssinmentShiftRight();
                }
                else if (operation == "&=")
                {
                    m_codeGen.EmitCompoundAssinmentAnd();
                }
                else if (operation == "^=")
                {
                    m_codeGen.EmitCompoundAssinmentXor();
                }
                else if (operation == "|=")
                {
                    m_codeGen.EmitCompoundAssinmentOr();
                }
            }
        }

        public override void ExitConditionalExpression(CParser.ConditionalExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            
            if(context.parent.GetType() == typeof(CParser.ConditionalExpressionContext))
            {
                //this is the second value expression of a conditional expression
                m_codeGen.EmitCoditionalExpresionFooter();
            }
        }

        //TODO add short circuting
        public override void ExitLogicalOrExpression(CParser.LogicalOrExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.parent.GetType() == typeof(CParser.LogicalOrExpressionContext))
            {
                m_codeGen.EmitLogicalOrBody();
            }
        }

        //TODO add short circuting
        public override void ExitLogicalAndExpression(CParser.LogicalAndExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.parent.GetType() == typeof(CParser.LogicalOrExpressionContext))
            {
                if((context.parent as CParser.LogicalOrExpressionContext).logicalOrExpression() != null)
                {
                    m_codeGen.EmitLogicalOrFooter();
                }
            }
            else if(context.parent.GetType() == typeof(CParser.LogicalAndExpressionContext))
            {
                m_codeGen.EmitLogicalAndBody();
            }
        }

        public override void ExitInclusiveOrExpression(CParser.InclusiveOrExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.inclusiveOrExpression() != null)
            {
                m_codeGen.EmitOrOperation();
            }

            if(context.parent.GetType() == typeof(CParser.LogicalAndExpressionContext))
            {
                if ((context.parent as CParser.LogicalAndExpressionContext).logicalAndExpression() != null)
                {
                    m_codeGen.EmitLogicalAndFooter();
                }
            }
        }

        public override void ExitExclusiveOrExpression(CParser.ExclusiveOrExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.exclusiveOrExpression() != null)
            {
                m_codeGen.EmitXorOperation();
            }
        }

        public override void ExitAndExpression(CParser.AndExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.andExpression() != null)
            {
                m_codeGen.EmitAndOperation();
            }
        }

        public override void ExitEqualityExpression(CParser.EqualityExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.equalityExpression() != null)
            {
                string operation = context.GetText().Substring(context.equalityExpression().GetText().Length, 2);

                if (operation == "==")
                {
                    m_codeGen.EmitAreEqualOperation();
                }
                else
                {
                    m_codeGen.EmitNotEqualOperation();
                }
            }
        }

        public override void ExitRelationalExpression(CParser.RelationalExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.relationalExpression() != null)
            {
                int contextLength = context.GetText().Length;
                int lhsLength = context.relationalExpression().GetText().Length;
                int rhsLength = context.shiftExpression().GetText().Length;

                string operation = context.GetText().Substring(lhsLength, contextLength - (lhsLength + rhsLength));

                if(operation == "<")
                {
                    m_codeGen.EmitLessThanOperation();
                }
                else if(operation == ">")
                {
                    m_codeGen.EmitGreaterThanOperation();
                }
                else if (operation == "<=")
                {
                    m_codeGen.EmitLessThanOrEqualOperation();
                }
                else if (operation == ">=")
                {
                    m_codeGen.EmitGreaterThanOrEqualOperation();
                }
            }
        }

        public override void ExitShiftExpression(CParser.ShiftExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.shiftExpression() == null)
            {
                //this is a additive expresssion, its value is already on the stack
                return;
            }
            else
            {
                string operation = context.GetText().Substring(context.shiftExpression().GetText().Length, 2);

                if(operation == "<<")
                {
                    m_codeGen.EmitLeftShiftOperation();
                }
                else if(operation == ">>")
                {
                    m_codeGen.EmitRightShiftOperation();
                }
            }
        }

        public override void ExitAdditiveExpression(CParser.AdditiveExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.additiveExpression() != null)
            {
                string operation = context.GetText().Substring(context.additiveExpression().GetText().Length, 1);

                if (operation == "+")
                {
                    m_codeGen.EmitAddOperation();
                }
                else if (operation == "-")
                {
                    m_codeGen.EmitSubOperation();
                }
            }
        }

        public override void ExitMultiplicativeExpression(CParser.MultiplicativeExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.multiplicativeExpression() != null)
            {
                string operation = context.GetText().Substring(context.multiplicativeExpression().GetText().Length, 1);

                if (operation == "*")
                {
                    m_codeGen.EmitMultiplyOperation();
                }
                else if (operation == "/")
                {
                    m_codeGen.EmitDivideOperation();
                }
                else if (operation == "%")
                {
                    m_codeGen.EmitModOperation();
                }
            }
        }

        public override void ExitCastExpression(CParser.CastExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.typeName() != null)
            {
                m_codeGen.EmitCastExpresion(CTypeFromName(context.typeName().GetText()));
            }

            if(context.parent.GetType() == typeof(CParser.UnaryExpressionContext))
            {
                //handl lvalue stuff .. hmm ... we will se if anything needs to happen hear
            }
        }

        public override void ExitUnaryExpression(CParser.UnaryExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.postfixExpression() != null)
            {
                //this is a postfix expression, this expression has already been emited
            }
            else if (context.unaryOperator() != null)
            {
                string operation = context.unaryOperator().GetText();
                
                if(operation == "&")
                {
                    m_codeGen.EmitAddressOfOperation();
                }
                else if (operation == "*")
                {
                    m_codeGen.EmitDereferenceOperation();
                }
                else if (operation == "+")
                {
                    m_codeGen.EmitPositiveOperation();
                }
                else if (operation == "-")
                {
                    m_codeGen.EmitNegativeOperation();
                }
                else if (operation == "~")
                {
                    m_codeGen.EmitBitwiseNotOperation();
                }
                else if (operation == "!")
                {
                    m_codeGen.EmitLogicalNotOperation();
                }
            }
            else if (context.GetText().Substring(0, 2) == "++")
            {
                m_codeGen.EmitPreIncrement();
            }
            else if (context.GetText().Substring(0, 2) == "--")
            {
                //prefix decrement
                m_codeGen.EmitPreDecrement();
            }
            else if (context.GetText().Substring(0, 6) == "sizeof")
            {
                if (context.unaryExpression() != null)
                {
                    m_codeGen.EmitSizeOfValue();
                }
                else
                {
                    m_codeGen.EmitSizeOfType(CTypeFromName(context.typeName().GetText()));
                }
            }
            else
            {
                //alingof or &&
                SematicError(context, "alignof and && not supported");
            }

            if(context.parent.GetType() == typeof(CParser.AssignmentExpressionContext))
            {
                m_codeGen.EmitCloneExpression();

                if((context.parent as CParser.AssignmentExpressionContext).assignmentOperator().GetText() != "=")
                {
                    m_codeGen.EmitCloneExpression();
                    m_codeGen.MakeCurrentExpressionRValue();
                }
            }
        }

        public override void EnterPostfixExpression(CParser.PostfixExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.parent.GetType() == typeof(CParser.PostfixExpressionContext))
            {
                if ((context.parent as CParser.PostfixExpressionContext).argumentExpressionList() != null)
                {
                    //handle case whre we need to defer emiting this value untill the end of the function call
                    m_codeGen.BeginDeferFunctionName();
                }
            }
        }
        
        public override void ExitPostfixExpression(CParser.PostfixExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.primaryExpression() != null)
            {
                //this is a primary expression, so its value is already on the stack
                return;
            }
            else if (context.expression() != null)
            {
                //this is an array accses operation
                m_codeGen.EmitArrayAccess();
            }
            else if (context.argumentExpressionList() != null)
            {
                //this is a function call

                m_codeGen.EmitDeferedFunctionName();

                m_codeGen.EmitFunctionCall(numArguments(context.argumentExpressionList()));
            }
            else if (context.Identifier() != null)
            {
                //this is a member accses, either '.' or '->'

                int objStringLength = context.postfixExpression().GetText().Length;
                int idStringLength = context.Identifier().GetText().Length;
                int contextStringSize = context.GetText().Length;

                int operationStringSize = contextStringSize - (objStringLength + idStringLength);

                if (operationStringSize == 1)
                {
                    m_codeGen.EmitMemberAccess(LabelInfoFromName(context.Identifier().GetText(), m_codeGen.CurrentExpresion.Type));
                }
                else
                {
                    m_codeGen.EmitPointerAccess(LabelInfoFromName(context.Identifier().GetText(), m_codeGen.CurrentExpresion.Type));
                }
            }
            else if (context.postfixExpression() != null)
            {
                //this is a ++ or -- postfix
                int expresionStringLength = context.postfixExpression().GetText().Length;
                string op = context.GetText().Substring(expresionStringLength);

                if (op == "++")
                {
                    m_codeGen.EmitPostIncrement();
                }
                else
                {
                    m_codeGen.EmitPostDecrement();
                }
            }
            else
            {
                //one of the weird cases with an initilizer list
                //not sure what these are supposed to do
                SematicError(context, "typed initilizer list postfix expressions not supported");
            }

            if (context.parent.GetType() == typeof(CParser.PostfixExpressionContext))
            {
                if ((context.parent as CParser.PostfixExpressionContext).argumentExpressionList() != null)
                {
                    //handle case whre we need to defer emiting this value untill the end of the function call
                    m_codeGen.EndDeferFunctionName();
                }
            }
        }

        int numArguments(CParser.ArgumentExpressionListContext list)
        {
            if(list.argumentExpressionList() != null)
            {
                return numArguments(list.argumentExpressionList()) + 1;
            }
            else
            {
                return 1;
            }
        }

        public override void ExitPrimaryExpression(CParser.PrimaryExpressionContext context)
        {
            m_codeGen.CodeGenErrorHandler = (s) => SematicError(context, s);
            if (context.expression() != null)
            {
                //this is just an expression, its value of is already on the stack
                return;
            }
            else if (context.Identifier() != null)
            {
                LabelInfo label = LabelInfoFromName(context.Identifier().GetText());

                if (label == null)
                    SematicError(context, "symbol \"" + context.Identifier().GetText() + "\" doe not exist in this scope");

                m_codeGen.EmitIdentifierReference(label);
            }
            else if (context.Constant() != null)
            {
                m_codeGen.EmitConstant(context.Constant().GetText());
            }
            else if (context.StringLiteral().Count > 0)
            {
                m_codeGen.EmitStringLiteral(context.StringLiteral().Select(lit => lit.GetText()));
            }

            else if (context.compoundStatement() != null)
            {
                SematicError(context, "GCC blocks extention not supported");
            }
            else if (context.GetText().Contains("__builtin_va_arg"))
            {
                SematicError(context, "var args not supported (yet)");
            }
            else
            {
                //either genericslection, or __builtin_offsetof. offsetof is an interesting case, because it is actually an example of basic reflection
                SematicError(context, "genericslection and  offsetof not supported");
            }
        }
    }
}
