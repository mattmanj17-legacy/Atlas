using Atlas.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC
{
    //A modifiable lvalue is any lvalue expression of complete, non-array type which is not const-qualified, and, if it's a struct/union, has no members that are const-qualified, recursively.
    //Only modifiable lvalue expressions may be used as arguments to increment/decrement, and as left-hand arguments of assignment and compound assignment operators.
    public enum ValueCatagory 
    { 
        LValue, // this expression is a pointer to where its value is 
        RValue // this expression is its literal value
    }
    
    //TODO handle const expressions (evaluating expressions at compile time)
    //http://en.cppreference.com/w/c/language/constant_expression
    public class CExpression : EmitterList
    {
        public CExpression(CType type, ValueCatagory valueCat)
        {
            Type = type;
            valueCatagory = valueCat;
        }

        public CType Type;
        public readonly ValueCatagory valueCatagory;

        //A modifiable lvalue is any lvalue expression of complete, non-array type which is not const-qualified, and, if it's a struct, has no members that are const-qualified, recursively.
        //Only modifiable lvalue expressions may be used as arguments to increment/decrement, and as left-hand arguments of assignment and compound assignment operators.
        public bool IsModifiableLValue 
        { 
            get 
            {
                return valueCatagory == ValueCatagory.LValue && Type.Complete && !Type.IsArray && !Type.IsConst && !Type.HasConstMember;
            } 
        }

        /* Lvalue to value
         * Any lvalue expression of any non-array type, when used in any context other than
         *  as the operand of the address-of operator (if allowed)
         *  as the operand of the pre/post increment and decrement operators.
         *  as the left-hand operand of the member access (dot) operator.
         *  as the left-hand operand of the assignment and compound assignment operators.
         *  as the operand of sizeof
         * undergoes lvalue conversion: the type remains the same,  The value remains the same, but loses its lvalue properties (the address may no longer be taken).
         * If the lvalue has incomplete type, the behavior is undefined
         */ 
        public CExpression ToRValue()
        {
            //todo handle arrays
            
            CExpression converted;

            if (valueCatagory != ValueCatagory.RValue)
            {
                converted = new CExpression(Type, ValueCatagory.RValue);
                converted.Add(this);
                converted.Add(new OpCodeEmitter(GetLoadOperation()));
                return converted;
            }
            else
            {
                converted = this;
            }

            return converted;
        }

        public OpCode GetLoadOperation()
        {
            switch(Type.Size)
            {
                case 1: return Type.IsUnsigned ? OpCode.LUB : OpCode.LB;
                case 2: return Type.IsUnsigned ? OpCode.LUH : OpCode.LH;
                case 4: return OpCode.LW;
                default: throw new InvalidOperationException("cannot load this type of expression");
            }
        }

        public OpCode GetStoreOperation()
        {
            switch (Type.Size)
            {
                case 1: return OpCode.SB;
                case 2: return OpCode.SH;
                case 4: return OpCode.SW;
                default: throw new InvalidOperationException("cannot store this type of expression");
            } 
        }
        
        public bool CanConvertImplicitlyToType(CType targetType)
        {
            //When an expression is used in the context where a value of a different type is expected, conversion may occur
            // Conversions take place in the following situations
            // assignment
            // Usual arithmetic conversions (arithmetic operators)
            
            /* A value of any integer type can be implicitly converted to any other integer type. Except where covered by promotions and boolean conversions above, the rules are:
             *  if the target type can represent the entire range of values of the source type, the value is unchanged
             *  
             *  otherwise, if the target type is unsigned, the value 2b, 
             *  where b is the number of bits in the target type, 
             *  is repeatedly subtracted or added to the source value until the result fits in the target type. 
             *  In other words, unsigned integers implement modulo arithmetic.
             *  
             *  otherwise, if the target type is signed, the behavior is implementation-defined (which may include raising a signal)
             */
            //A value of any scalar type can be implicitly converted to _Bool. The values that compare equal to zero are converted to ​0​, all other values are converted to 1 (NOT IMPLIMENTERD YET)
            //A pointer to void can be implicitly converted to and from any pointer to object type

            CType sourceType = Type;

            if(sourceType.CompatableWith(targetType))
            {
                return true;
            }
            else if (sourceType.IsInteger && targetType.IsInteger)
            {
                return true;
            }
            else if(sourceType.IsVoidPointer && targetType.IsPointer && !targetType.IsFunctionPointer)
            {
                return true;
            }
            else if (targetType.IsVoidPointer && sourceType.IsPointer && !sourceType.IsFunctionPointer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool CanConvertToType(CType targetType)
        {
            /* If type-name is void, then expression is evaluated for its side-effects and its returned value is discarded, same as when expression is used on its own, as an expression statement.
             * Otherwise, if type-name is exactly the type of expression, nothing is done (except that if expression has floating type and is represented with greater range and precision than its type indicates -- see below)
             * Otherwise, the value of expression is converted to the type named by type-name, as follows:
             * Every implicit conversion as if by assignment is allowed.
             * In addition to the implicit conversions, the following conversions are allowed:
             *  Any integer can be cast to any pointer type. 
             *  Any pointer type can be cast to any integer type.
             *  Any pointer to object can be cast to any other pointer to object. 
             *  Any pointer to function can be cast to a pointer to any other function type.
             *  When casting between pointers (either object or function), if the original value is a null pointer value of its type, the result is the correct null pointer value for the target type.
             */
            
            if(targetType.TypeClass == CTypeClass.CVoid)
            {
                return true;
            }
            else if(CanConvertImplicitlyToType(targetType))
            {
                return true;
            }
            else if(Type.IsInteger || targetType.IsPointer)
            {
                return true;
            }
            else if(Type.IsPointer || targetType.IsInteger)
            {
                return true;
            }
            else if(Type.IsPointer && !Type.IsFunctionPointer && targetType.IsPointer && !targetType.IsFunctionPointer)
            {
                return true;
            }
            else if (Type.IsFunctionPointer && targetType.IsFunctionPointer)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CExpression ConvertToType(CType targetType)
        {
            if(!CanConvertToType(targetType))
            {
                throw new InvalidOperationException("invalid type conversion");
            }
            else
            {
                //todo warn about unsigned to signed conversion
                //hmmm ... does this cause bugs?
                Type = targetType;
            }

            return this;
        }

        internal void ToUnsigned()
        {
            throw new NotImplementedException();
        }
    }
}
