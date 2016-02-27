using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    partial class AtlasCodeGen
    {
        public void DisregardLastExpression()
        {
            Expressions.Pop();
            EmitLine("POPW");
        }

        public void EmitAssinmentOperation()
        {
            //pop arguments to store
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert right hand expression to rvalue (EmitAssinmentOperation)");
            }

            if (lhs.valueCatagory != ValueCatagory.LValue)
            {
                CodeGenError("cannot assign to non lvalue");
            }

            if (!lhs.Type.CompatableWith(rhs.Type))
            {
                if (rhs.Type.CanImplicentlyConvertTo(lhs.Type))
                {
                    EmitCastExpresion(lhs.Type);
                }
                else
                {
                    CodeGenError("cannot assign expresion of type " + rhs.Type.ToString() + " to expression of type " + lhs.Type.ToString());
                }
            }

            EmitLine("SW");

            //load the result of the assignment onto stack
            ExpressionInfo adrress = Expressions.Pop();
            EmitLine("LW");

            Expressions.Push(new ExpressionInfo(rhs.Type, ValueCatagory.RValue, false));
        }

        public void EmitCompoundAssinmentMultiply()
        {
            EmitMultiplyOperation();
            EmitAssinmentOperation();
        }

        public void EmitCompoundAssinmentDivide()
        {
            EmitDivideOperation();
            EmitAssinmentOperation();
        }

        public void EmitCompoundAssinmentMod()
        {
            EmitModOperation();
            EmitAssinmentOperation();
        }

        public void EmitCompoundAssinmentAdd()
        {
            EmitAddOperation();
            EmitAssinmentOperation();
        }

        public void EmitCompoundAssinmentSub()
        {
            EmitSubOperation();
            EmitAssinmentOperation();
        }

        public void EmitCompoundAssinmentShiftLeft()
        {
            EmitLeftShiftOperation();
            EmitAssinmentOperation();
        }

        public void EmitCompoundAssinmentShiftRight()
        {
            EmitRightShiftOperation();
            EmitAssinmentOperation();
        }

        public void EmitCompoundAssinmentAnd()
        {
            EmitAndOperation();
            EmitAssinmentOperation();
        }

        public void EmitCompoundAssinmentXor()
        {
            EmitXorOperation();
            EmitAssinmentOperation();
        }

        public void EmitCompoundAssinmentOr()
        {
            EmitOrOperation();
            EmitAssinmentOperation();
        }

        public void EmitCoditionalExpresionHeader()
        {
            MakeCurrentExpressionRValue();
            
            EmitLine("NOT");
            PushLabel();
            Emit("PUSHW ");
            EmitLine(GetCurrentLabel());
            EmitLine("JIF");

            Expressions.Pop();
        }

        public void EmitCoditionalExpresionBody()
        {
            MakeCurrentExpressionRValue();
            
            string oldLabel = GetCurrentLabel();
            PopLabel();

            PushLabel();
            Emit("PUSHW ");
            EmitLine(GetCurrentLabel());
            EmitLine("JMP");
            EmitLine(oldLabel + ":");

            Expressions.Pop();
        }

        public void EmitCoditionalExpresionFooter()
        {
            MakeCurrentExpressionRValue();

            EmitLine(GetCurrentLabel() + ":");
            PopLabel();

            Expressions.Pop();

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, false));
        }

        public void EmitLogicalOrBody()
        {
            MakeCurrentExpressionRValue();
            
            PushLabel();
            Emit("PUSHW ");
            EmitLine(GetCurrentLabel());
            EmitLine("JIF");
            EmitLine("POPW");

            Expressions.Pop();
        }

        public void EmitLogicalOrFooter()
        {
            MakeCurrentExpressionRValue();
            
            EmitLine(GetCurrentLabel() + ":");
            PopLabel();

            Expressions.Pop();

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, false));
        }

        public void EmitLogicalAndBody()
        {
            MakeCurrentExpressionRValue();

            EmitLine("NOT");
            PushLabel();
            Emit("PUSHW ");
            EmitLine(GetCurrentLabel());
            EmitLine("JIF");
            EmitLine("POPW");

            Expressions.Pop();
        }

        public void EmitLogicalAndFooter()
        {
            MakeCurrentExpressionRValue();
            
            EmitLine(GetCurrentLabel() + ":");
            PopLabel();

            Expressions.Pop();

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, false));
        }

        public void EmitOrOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitOrOperation");
            }

            EmitLine("OR");

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitXorOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitXorOperation");
            }

            EmitLine("XOR");

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitAndOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitAndOperation");
            }

            EmitLine("AND");

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitAreEqualOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitAreEqualOperation");
            }

            //todo implimet with basic operations
            EmitLine("TEMP==");
            CodeGenError("EmitAreEqualOperation not implimented");

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitNotEqualOperation()
        {
            EmitAreEqualOperation();
            EmitLogicalNotOperation();
        }

        public void EmitLessThanOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitLessThanOperation");
            }

            EmitLine("LESS");

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitGreaterThanOperation()
        {
            //assume args are pased in corect order
            EmitLessThanOperation();
            CodeGenError("EmitGreaterThanOperation not implimented");
        }

        public void EmitLessThanOrEqualOperation()
        {
            EmitLine("PUSHW 1");
            EmitLine("ADD");
            EmitLessThanOperation();
        }

        public void EmitGreaterThanOrEqualOperation()
        {
            //assume args are pased in corect order
            EmitLessThanOrEqualOperation();
            CodeGenError("EmitGreaterThanOrEqualOperation not implimented");
        }

        public void EmitLeftShiftOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitLeftShiftOperation");
            }

            if (rhs.Type.IsNaturalNumber)
            {
                CodeGenError("right hand argument to shift must be natural number");
            }

            EmitLine("SLL");

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitRightShiftOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitRightShiftOperation");
            }

            if (rhs.Type.IsNaturalNumber)
            {
                CodeGenError("right hand argument to shift must be natural number");
            }

            EmitLine("SRL");

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant)); Expressions.Push(new ExpressionInfo(lhs.Type, ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitAddOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitAddOperation");
            }

            if (lhs.Type.IsNumber && rhs.Type.IsNumber)
            {
                EmitLine("ADD");
            }
            else if (lhs.Type.IsPointer && rhs.Type.IsNumber)
            {
                EmitSizeOfType(lhs.Type.TypePointedTo);
                EmitLine("MUL");
                EmitLine("ADD");
            }
            else
            {
                CodeGenError("invalid operands to Add");
            }

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitSubOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitSubOperation");
            }

            if (lhs.Type.IsNumber && rhs.Type.IsNumber)
            {
                EmitLine("SUB");
            }
            else if (lhs.Type.IsPointer && rhs.Type.IsNumber)
            {
                EmitSizeOfType(lhs.Type.TypePointedTo);
                EmitLine("MUL");
                EmitLine("SUB");
            }
            else
            {
                CodeGenError("invalid operands to sub");
            }

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitMultiplyOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitMultiplyOperation");
            }

            if (lhs.Type.IsNaturalNumber && rhs.Type.IsNaturalNumber)
            {
                EmitLine("MUL");
            }
            else
            {
                CodeGenError("invalid operands to mul");
            }

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitDivideOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitDivideOperation");
            }

            if (lhs.Type.IsNaturalNumber && rhs.Type.IsNaturalNumber)
            {
                EmitLine("DIV");
                CodeGenError("DIV not implimented");
            }
            else
            {
                CodeGenError("invalid operands to div");
            }

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitModOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if (lhs.valueCatagory != ValueCatagory.RValue || rhs.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: failed to convert arithmetic operand to rvalue EmitModOperation");
            }

            if (lhs.Type.IsNaturalNumber && rhs.Type.IsNaturalNumber)
            {
                EmitLine("MOD");
                CodeGenError("DIV not implimented");
            }
            else
            {
                CodeGenError("invalid operands to mod");
            }

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, lhs.Constant && rhs.Constant));
        }

        public void EmitCastExpresion(CTypeInfo type)
        {
            //handled by inline function probably
            ExpressionInfo rhs = Expressions.Pop();

            if (rhs.Type.CompatableWith(type))
            {
                Expressions.Push(rhs);
                return;
            }

            EmitLine("TEMPCast(" + type.ToString() + ")");
            CodeGenError("EmitCastExpresion not implimented");

            Expressions.Push(new ExpressionInfo(type, ValueCatagory.RValue, rhs.Constant));
        }

        public void EmitSizeOfType(CTypeInfo type)
        {
            EmitLine("PUSHW " + type.SizeOf);
            CodeGenError("not imp EmitSizeOfType");
        }

        public void EmitSizeOfValue()
        {
            ExpressionInfo rhs = Expressions.Pop();
            EmitLine("PUSHW " + rhs.Type.SizeOf);
            CodeGenError("not imp EmitSizeOfValue");
        }

        public void EmitPreDecrement()
        {
            CodeGenError("not implimented EmitPreDecrement");
        }

        public void EmitPreIncrement()
        {
            CodeGenError("not implimented EmitPreIncrement");
        }

        public void EmitAddressOfOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();

            if (rhs.valueCatagory != ValueCatagory.LValue)
            {
                CodeGenError("cannot take the addres of a non lvalue");
            }

            Expressions.Push(new ExpressionInfo(rhs.Type.GetPointerType(), ValueCatagory.RValue, false));
        }

        public void EmitDereferenceOperation()
        {
            MakeCurrentExpressionRValue();
            
            ExpressionInfo rhs = Expressions.Pop();

            if (!rhs.Type.IsPointer)
            {
                CodeGenError("cannot dereference non pointer type");
            }

            EmitLine("LW");

            Expressions.Push(new ExpressionInfo(rhs.Type.TypePointedTo, ValueCatagory.LValue, false));
        }

        public void EmitPositiveOperation()
        {
            MakeCurrentExpressionRValue();

            ExpressionInfo rhs = Expressions.Peek();

            CTypeInfo promotedType = rhs.Type.ToSigned();

            if (promotedType == null)
            {
                CodeGenError("unary + cannot be applyed to expression of type " + rhs.Type.ToString());
            }

            EmitCastExpresion(promotedType);
        }

        public void EmitNegativeOperation()
        {
            EmitPositiveOperation();
            EmitLine("NEG");
        }

        public void EmitBitwiseNotOperation()
        {
            MakeCurrentExpressionRValue();
            
            ExpressionInfo rhs = Expressions.Pop();

            EmitLine("NOT");

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, rhs.Constant));
        }

        public void EmitLogicalNotOperation()
        {
            MakeCurrentExpressionRValue();
            ExpressionInfo rhs = Expressions.Pop();
            EmitLine("TEMPLOGICALNOT");
            CodeGenError("EmitLogicalNotOperation not implimented");

            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, rhs.Constant));
        }

        //TODO right now this is tighly bound to the atlas calling convention
        // maybe later this will be made more general
        // also, right now there isnt any special way to handle function arguments (again, making calling convention assumptions)
        public void BeginDeferFunctionName()
        {
            writeToDefered = true;
            EmitLine("BEGINARGS");
        }

        public void EndDeferFunctionName()
        {
            writeToDefered = false;
        }

        public void EmitDeferedFunctionName()
        {
            Emit(getfunctionName.ToString());
        }

        public void EmitPostDecrement()
        {
            CodeGenError("not implimented");
        }

        public void EmitPostIncrement()
        {
            CodeGenError("not implimented");
        }

        public void EmitMemberAccess(LabelInfo label)
        {
            ExpressionInfo obj = Expressions.Pop();

            if (obj.valueCatagory != ValueCatagory.LValue)
            {
                CodeGenError("cannot get member from rvalue expression");
            }

            if (obj.Type.HasMembers)
            {
                if (obj.Type.HasMember(label))
                {
                    EmitLine("PUSHW " + obj.Type.GetMemberOffset(label));
                    EmitLine("ADD");

                    Expressions.Push(new ExpressionInfo(obj.Type.GetMemberType(label), ValueCatagory.LValue, false));
                }
                else
                {
                    CodeGenError("type " + obj.Type.ToString() + " has no member named \"" + label.ToString() + "\"");
                }
            }
            else
            {
                CodeGenError("type " + obj.Type.ToString() + " has no members");
            }
        }

        public void EmitPointerAccess(LabelInfo label)
        {
            EmitDereferenceOperation();
            EmitMemberAccess(label);
        }

        public void EmitFunctionCall(int numPassedArguments)
        {
            MakeCurrentExpressionRValue();

            ExpressionInfo func = Expressions.Pop();

            //check that they are rvalues????
            List<ExpressionInfo> args = new List<ExpressionInfo>();

            for (int i = 0; i < numPassedArguments; i++)
            {
                args.Add(Expressions.Pop());
            }

            if (!func.Type.IsFunction || !func.Type.CheckFuncArguments(args))
            {
                CodeGenError("invalid arguments passed to function, or expresion not of function type");
            }

            EmitLine("CALL");

            Expressions.Push(new ExpressionInfo(func.Type.ReturnType, ValueCatagory.RValue, false));
        }

        public void EmitArrayAccess()
        {
            ExpressionInfo index = Expressions.Pop();
            ExpressionInfo array = Expressions.Pop();

            if (index.valueCatagory != ValueCatagory.RValue)
            {
                CodeGenError("internal error: forgot to chang to rvalue EmitArrayAccess");
            }

            if (array.valueCatagory != ValueCatagory.LValue)
            {
                CodeGenError("cannot use array subscript operator on non lvalue expresion");
            }

            if (!array.Type.IsPointer)
            {
                CodeGenError("cannot use array subscript operator on non pointer type");
            }

            EmitSizeOfType(array.Type.TypePointedTo);
            EmitLine("MUL");
            EmitLine("ADD");

            Expressions.Push(new ExpressionInfo(array.Type.TypePointedTo, ValueCatagory.LValue, false));
        }

        public void EmitStringLiteral(IEnumerable<string> literals)
        {
            throw new NotImplementedException();
        }

        public void EmitIdentifierReference(LabelInfo label)
        {
            if (label.IsMember)
            {
                CodeGenError("symbol \"" + label.ToString() + "\" does not exist in this scope");
            }

            if (label.IsLocal)
            {
                EmitLine("PUSHBP");
                EmitLine("PUSHW " + label.Offset);
                EmitLine("ADD");
            }
            else
            {
                EmitLine("PUSHW " + label.ToString());
            }

            Expressions.Push(new ExpressionInfo(label.Type, ValueCatagory.LValue, false));
        }

        public void EmitConstant(string literal)
        {
            EmitLine("PUSHW " + literal);
            Expressions.Push(new ExpressionInfo(CTypeInfo.FromFundamentalType(FundamentalType.uint32), ValueCatagory.RValue, true));
        }

        public ExpressionInfo CurrentExpresion
        {
            get
            {
                return Expressions.Peek();
            }
        }

        public void MakeCurrentExpressionRValue()
        {
            ExpressionInfo top = Expressions.Pop();

            if (top.valueCatagory == ValueCatagory.RValue)
            {
                Expressions.Push(top);
                return;
            }

            EmitLine("LW");

            Expressions.Push(new ExpressionInfo(top.Type, ValueCatagory.RValue, false));
        }

        public void EmitCloneExpression()
        {
            EmitLine("PUSHSP");
            EmitLine("PUSHW 4");
            EmitLine("SUB");
            EmitLine("LW");
            Expressions.Push(Expressions.Peek());
        }

        private void PushLabel()
        {
            labels.Push(num_gen_labels);
            num_gen_labels++;
        }

        private string GetCurrentLabel()
        {
            return "__gen__expression__label" + labels.Peek();
        }

        private void PopLabel()
        {
            labels.Pop();
        }

        private int num_gen_labels = 0;

        private Stack<int> labels = new Stack<int>();

        private Stack<ExpressionInfo> Expressions = new Stack<ExpressionInfo>();
    }
}
