using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC
{
    public enum CBaseType { }
    public enum ValueCatagory { LValue,  RValue }
    
    public abstract class ExpressionInfo
    {
        public CTypeInfo Type;
        public ValueCatagory valueCatagory;
        public bool Constant;
    }
}
