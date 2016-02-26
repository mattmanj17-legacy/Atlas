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
            EmitLine("pop");
        }
        
        public void EmitAssinmentOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();

            if(lhs.valueCatagory != ValueCatagory.LValue)
            {
                CodeGenError("cannot assign to non lvalue");
            }

            //todo: change lb, lh etc so that they push the value they set memory to onto the stack
            EmitLine("=");

            Expressions.Push(new .....);
        }

        public void EmitCompoundAssinmentMultiply()
        {
            //fix tree walk so we dont have to use inline functions
            //emit compound assignment as inline functions
            CodeGenError("not implimented");
        }

        public void EmitCompoundAssinmentDivide()
        {
            CodeGenError("not implimented");
        }

        public void EmitCompoundAssinmentMod()
        {
            CodeGenError("not implimented");
        }

        public void EmitCompoundAssinmentAdd()
        {
            CodeGenError("not implimented");
        }

        public void EmitCompoundAssinmentSub()
        {
            CodeGenError("not implimented");
        }

        public void EmitCompoundAssinmentShiftLeft()
        {
            CodeGenError("not implimented");
        }

        public void EmitCompoundAssinmentShiftRight()
        {
            CodeGenError("not implimented");
        }

        public void EmitCompoundAssinmentAnd()
        {
            CodeGenError("not implimented");
        }

        public void EmitCompoundAssinmentXor()
        {
            CodeGenError("not implimented");
        }

        public void EmitCompoundAssinmentOr()
        {
            CodeGenError("not implimented");
        }

        public void EmitCoditionalExpresionHeader()
        {
            EmitLine("not");
            PushLabel();
            Emit("push ");
            EmitLine(GetCurrentLabel());
            EmitLine("jif");
        }

        public void EmitCoditionalExpresionBody()
        {
            string oldLabel = GetCurrentLabel();
            PopLabel();

            PushLabel();
            Emit("push ");
            EmitLine(GetCurrentLabel());
            EmitLine("jmp");
            EmitLine(oldLabel + ":");
            throw new NotImplementedException();
        }

        public void EmitCoditionalExpresionFooter()
        {
            EmitLine(GetCurrentLabel() + ":");
            PopLabel();
        }

        public void EmitLogicalOrBody()
        {
            PushLabel();
            Emit("push ");
            EmitLine(GetCurrentLabel());
            EmitLine("jif");
            EmitLine("pop");
        }

        public void EmitLogicalOrFooter()
        {
            EmitLine(GetCurrentLabel() + ":");
            PopLabel();
        }

        public void EmitLogicalAndBody()
        {
            EmitLine("not");
            PushLabel();
            Emit("push ");
            EmitLine(GetCurrentLabel());
            EmitLine("jif");
            EmitLine("pop");
        }

        public void EmitLogicalAndFooter()
        {
            EmitLine(GetCurrentLabel() + ":");
            PopLabel();
        }

        public void EmitOrOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();
            EmitLine("|");
        }

        public void EmitXorOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();
            EmitLine("^");
        }

        public void EmitAndOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();
            EmitLine("&");
        }

        public void EmitAreEqualOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();
            EmitLine("==");
        }

        public void EmitNotEqualOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();
            EmitLine("!=");
        }

        public void EmitLessThanOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();
            EmitLine("<"); 
        }

        public void EmitGreaterThanOperation()
        {
            //emit other compare ops as inline functions
            CodeGenError("not implimented");
        }

        public void EmitLessThanOrEqualOperation()
        {
            CodeGenError("not implimented");
        }

        public void EmitGreaterThanOrEqualOperation()
        {
            CodeGenError("not implimented");
        }

        public void EmitLeftShiftOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();
            EmitLine("<<");
        }

        public void EmitRightShiftOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();
            EmitLine(">>");
        }

        public void EmitAddOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();
            EmitLine("+");
        }

        public void EmitSubOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();
            EmitLine("-");
        }

        public void EmitMultiplyOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            ExpressionInfo lhs = Expressions.Pop();
            EmitLine("*");
        }

        public void EmitDivideOperation()
        {
            //handled by inline function probably
            CodeGenError("not implimented");
        }

        public void EmitModOperation()
        {
            //handled by inline function probably
            CodeGenError("not implimented");
        }

        public void EmitCastExpresion(CTypeInfo type)
        {
            //handled by inline function probably
            ExpressionInfo rhs = Expressions.Pop();
            EmitLine("Cast("+type.ToString()+")");
        }

        public void EmitSizeOfType(CTypeInfo type)
        {
            EmitLine("push " + type.SizeOf);
        }

        public void EmitSizeOfValue()
        {
            ExpressionInfo rhs = Expressions.Pop();
            EmitLine("push " + rhs.Type.SizeOf);
        }

        //above this, all expressions emit rvalues
        
        //below this, emit lvalus whenever possible

        public void EmitPreDecrement()
        {
            //handled by inline function probably
            CodeGenError("not implimented");
        }

        public void EmitPreIncrement()
        {
            //handled by inline function probably
            CodeGenError("not implimented");
        }

        public void EmitAddressOfOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            //change expression from lvalue to rvalue
        }

        public void EmitDereferenceOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            EmitLine("DeRef");
            //make sure to return an lvalue (say the value on top of stack is a pointer)
        }

        public void EmitPositiveOperation()
        {
            //cast to signed
            CodeGenError("not implimented");
        }

        public void EmitNegativeOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            //cast to signed (later)
            //invert sign
            EmitLine("Neg");
        }

        public void EmitBitwiseNotOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            EmitLine("~");
        }

        public void EmitLogicalNotOperation()
        {
            ExpressionInfo rhs = Expressions.Pop();
            EmitLine("!");
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
            //handled by inline function probably
            CodeGenError("not implimented");
        }

        public void EmitPostIncrement()
        {
            //handled by inline function probably
            CodeGenError("not implimented");
        }

        public void EmitMemberAccess(LabelInfo label)
        {
            throw new NotImplementedException();
        }

        public void EmitPointerAccess(LabelInfo label)
        {
            throw new NotImplementedException();
        }

        public void EmitFunctionCall(int numPassedArguments)
        {
            //pop the function addres off the stack
            //check the types of the arguments

            throw new NotImplementedException();
        }

        public void EmitArrayAccess()
        {
            throw new NotImplementedException();
        }

        public void EmitStringLiteral(IEnumerable<string> literals)
        {
            throw new NotImplementedException();
        }

        public void EmitIdentifierReference(LabelInfo label)
        {
            throw new NotImplementedException();
        }

        public void EmitConstant(string literal)
        {
            throw new NotImplementedException();
        }

        public ExpressionInfo CurrentExpresion
        {
            get
            {
                return Expressions.Peek();
            }
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
