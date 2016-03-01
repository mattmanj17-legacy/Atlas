using Atlas.Architecture;
using Atlas.AtlasCC.Emitters;
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

        public readonly CType Type;
        public readonly ValueCatagory valueCatagory;

        public bool IsModifiable 
        { 
            get 
            {
                throw new NotImplementedException();
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
                converted.Add(new OpCodeEmitter(OpCode.LW)); // todo fix this so it is size dependent
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
            //When an expression is used in the context where a value of a different type is expected, conversion may occur
            // Conversions take place in the following situations
            // assignment
            // Usual arithmetic conversions (arithmetic operators)
            
            /*A value of any integer type can be implicitly converted to any other integer type. Except where covered by promotions and boolean conversions above, the rules are:
if the target type can represent the entire range of values of the source type, the value is unchanged
otherwise, if the target type is unsigned, the value 2b
, where b is the number of bits in the target type, is repeatedly subtracted or added to the source value until the result fits in the target type. In other words, unsigned integers implement modulo arithmetic.
otherwise, if the target type is signed, the behavior is implementation-defined (which may include raising a signal)*/
            
            //A value of any scalar type can be implicitly converted to _Bool. The values that compare equal to zero are converted to ​0​, all other values are converted to 1

            //A pointer to void can be implicitly converted to and from any pointer to object type
            
            throw new NotImplementedException();
        }
        
        public bool CanConvertToType(CType type)
        {
            /*If type-name is void, then expression is evaluated for its side-effects and its returned value is discarded, same as when expression is used on its own, as an expression statement.
Otherwise, if type-name is exactly the type of expression, nothing is done (except that if expression has floating type and is represented with greater range and precision than its type indicates -- see below)
Otherwise, the value of expression is converted to the type named by type-name, as follows:
Every implicit conversion as if by assignment is allowed.
In addition to the implicit conversions, the following conversions are allowed:
Any integer can be cast to any pointer type. Except for the null pointer constants such as NULL (which doesn't need a cast), the result is implementation-defined, may not be correctly aligned, may not point to an object of the referenced type, and may be a trap representation.
Any pointer type can be cast to any integer type. The result is implementation-defined, even for null pointer values (they do not necessarily result in the value zero). If the result cannot be represented in the target type, the behavior is undefined (unsigned integers do not implement modulo arithmetic on a cast from pointer)
Any pointer to object can be cast to any other pointer to object. If the value is not correctly aligned for the target type, the behavior is undefined. Otherwise, if the value is converted back to the original type, it compares equal to the original value. If a pointer to object is cast to pointer to any character type, the result points at the lowest byte of the object and may be incremented up to sizeof the target type (in other words, can be used to examine object representation or to make a copy via memcpy or memmove).
Any pointer to function can be cast to a pointer to any other function type. If the resulting pointer is converted back to the original type, it compares equal to the original value. If the converted pointer is used to make a function call, the behavior is undefined (unless the function types are compatible)
When casting between pointers (either object or function), if the original value is a null pointer value of its type, the result is the correct null pointer value for the target type.*/
            
            throw new NotImplementedException();
        }

        public CExpression ConvertToType(CType type)
        {
            throw new NotImplementedException();
        }
    }
}
