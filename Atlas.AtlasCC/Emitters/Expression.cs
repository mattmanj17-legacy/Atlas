using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC
{
    public enum ValueCatagory 
    { 
        LValue, // this expression is a pointer to where its value is 
        RValue // this expression is its literal value
    }
    
    public class Expression : EmitterList
    {
        public Expression(CTypeInfo type, ValueCatagory valueCat, int constantValue, bool isConstant)
        {
            Type = type;
            valueCatagory = valueCat;
            ConstantValue = constantValue;
            IsConstant = isConstant;
        }

        public readonly bool Modifiable;
        public readonly CTypeInfo Type;
        public readonly ValueCatagory valueCatagory;
        public readonly int ConstantValue;
        public readonly bool IsConstant;

        internal Expression PromoteInteger()
        {
            //hack, asuming all same type
            return this;
        }

        internal bool CanCastTo(CTypeInfo type)
        {
            //hack, asuming all same type
            return true;
        }

        internal Expression CastTo(CTypeInfo type)
        {
            //hack, asuming all same type
            return this;
        }

        internal Expression InvertSign()
        {
            //hack, asuming all same type
            return this;
        }
    }
}
