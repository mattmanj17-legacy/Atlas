using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC
{
    public enum CBaseType { }
    public enum ValueCatagory { LValue,  RValue }
    
    public class ExpressionInfo
    {
        public ExpressionInfo(CTypeInfo type, ValueCatagory valueCat, bool isConstant)
        {
            Type = type;
            valueCatagory = valueCat;
            Constant = isConstant;
        }
        
        public readonly CTypeInfo Type;
        public readonly ValueCatagory valueCatagory;
        public readonly bool Constant;
    }
}
