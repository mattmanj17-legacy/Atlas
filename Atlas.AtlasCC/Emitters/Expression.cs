using Atlas.Architecture;
using Atlas.AtlasCC.Emitters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC
{
    public enum ValueCatagory 
    { 
        LValueModifiable, // this expression is a pointer to where its value is 
        LValueConst, // this expression is a pointer to where its value is, and we cant set that value
        RValue // this expression is its literal value
    }
    
    //TODO handle const expressions (evaluating expressions at compile time)
    public class Expression : EmitterList
    {
        public Expression(CType type, ValueCatagory valueCat)
        {
            Type = type;
            valueCatagory = valueCat;
        }

        public readonly CType Type;
        public readonly ValueCatagory valueCatagory;

        public Expression ToRValue()
        {
            Expression converted;

            if (valueCatagory != ValueCatagory.RValue)
            {
                converted = new Expression(Type, ValueCatagory.RValue);
                converted.Add(this);
                converted.Add(new OpCodeEmitter(OpCode.LW));
                return converted;
            }
            else
            {
                converted = this;
            }

            return converted;
        }
        
        public bool CanConvertImplicitlyToType(CType type)
        {
            throw new NotImplementedException();
        }
        
        public bool CanConvertToType(CType type)
        {
            throw new NotImplementedException();
        }

        public Expression ConvertToType(CType type)
        {
            throw new NotImplementedException();
        }
    }
}
