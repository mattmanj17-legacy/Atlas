using Antlr4.Runtime;
using Atlas.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    public static class COperator
    {
        //Assignment operators
        //http://en.cppreference.com/w/c/language/operator_assignment
        
        public static void BasicAssignmentOperator()
        {
            /*
                The simple assignment operator expressions have the form
                    lhs = rhs		
                where
                    lhs	-	modifiable lvalue expression of any complete object type
                    rhs	-	expression of any type implicitly convertible to lhs or compatible with lhs
                Assignment performs implicit conversion from the value of rhs to the type of rhs and then replaces the value in the object designated by lhs with the converted value of rhs.
                Assignment also returns the same value as what was stored in lhs (so that expressions such as a = b = c are possible). The value category of the assignment operator is non-lvalue (so that expressions such as (a=b)=c are invalid).
                rhs and lhs must satisfy one of the following:
                    both lhs and rhs have compatible struct or union type, or..
                    rhs must be implicitly convertible to lhs, which implies
                    both lhs and rhs have arithmetic types, in which case lhs may be volatile-qualified or atomic
                    both lhs and rhs have pointer to compatible (ignoring qualifiers) types, or one of the pointers is a pointer to void, and the conversion would not add qualifiers to the pointed-to type. lhs may be volatile or restrict-qualified or atomic.
                    lhs is a pointer (possibly qualified or atomic) and rhs is a null pointer constant such as NULL
                    lhs has type _Bool (possibly qualified or atomic) and rhs is a pointer
                    Notes
                    If rhs and lhs overlap in memory (e.g. they are members of the same union), the behavior is undefined unless the overlap is exact and the types are compatible.
                Although arrays are not assignable, an array wrapped in a struct is assignable to another object of the same (or compatible) struct type.
                assignment operators are rvalue expressions
             */

            //In the assignment operator, the value of the right-hand operand is converted to the unqualified type of the left-hand operand.
            //the left hand operand of assignment and compund assignment is not converted to an rvalue

            //A modifiable lvalue is any lvalue expression of complete, non-array type which is not const-qualified, and, if it's a struct/union, has no members that are const-qualified, recursively.
            //Only modifiable lvalue expressions may be used as arguments to increment/decrement, and as left-hand arguments of assignment and compound assignment operators.

            CExpression rhs = PopExpression().ToRValue();
            CExpression lhs = PopExpression();

            if(!lhs.IsModifiableLValue)
            {
                throw new SemanticException("Cannot assign to a non modifiable lvalue");
            }

            if (!lhs.CanConvertImplicitlyToType(rhs.Type))
            {
                throw new SemanticException("cannot assign value of type \n'" + rhs.Type.TypeName + "'\n to value of type\n '" + lhs.Type.TypeName + "'");
            }

            CExpression result = new CExpression(rhs.Type,ValueCatagory.RValue);

            result.Add(lhs);
            result.Add(new OpCodeEmitter(OpCode.COPY));
            result.Add(rhs.ConvertToType(lhs.Type));
            result.Add(new OpCodeEmitter(lhs.GetStoreOperation()));
            result.Add(new OpCodeEmitter(lhs.GetLoadOperation()));

            PushExpression(result);
        }

        private static void CompoundAssignment(Action arithmeticOperator)
        {
            CExpression rhs = PopExpression().ToRValue();
            CExpression lhs = PopExpression();
            CExpression lhsValue = lhs.ToRValue();

            PushExpression(lhsValue);
            PushExpression(rhs);

            arithmeticOperator();

            rhs = PopExpression();

            PushExpression(lhs);
            PushExpression(rhs);

            BasicAssignmentOperator();
        }

        public static void AdditionAssignmentOperator()
        {
            CompoundAssignment(AdditionOperator);
        }

        public static void SubtractionAssignmentOperator()
        {
            CompoundAssignment(SubscriptOperator);
        }

        public static void MultiplicationAssignmentOperator()
        {
            CompoundAssignment(MultiplicationOperator);
        }

        public static void DivisionAssignmentOperator()
        {
            CompoundAssignment(DivisionOperator);
        }

        public static void ModuloAssignmentOperator()
        {
            CompoundAssignment(ModuloOperator);
        }

        public static void BitwiseAndAssignmentOperator()
        {
            CompoundAssignment(BitwiseAndOperator);
        }

        public static void BitwiseOrAssignmentOperator()
        {
            CompoundAssignment(BitwiseOrOperator);
        }

        public static void BitwiseXorAssignmentOperator()
        {
            CompoundAssignment(BitwiseXorOperator);
        }

        public static void BitwiseLeftShiftAssignmentOperator()
        {
            CompoundAssignment(BitwiseLeftShiftOperator);
        }

        public static void BitwiseRightShiftAssignmentOperator()
        {
            CompoundAssignment(BitwiseRightShiftOperator);
        }

        //Increment/decrement operators
        //Increment/decrement operators are unary operators that increment/decrement the value of a variable by 1.
        //http://en.cppreference.com/w/c/language/operator_incdec

        //the operand of the pre/post increment and decrement operators is not converted to an rvalue

        //A modifiable lvalue is any lvalue expression of complete, non-array type which is not const-qualified, and, if it's a struct/union, has no members that are const-qualified, recursively.
        //Only modifiable lvalue expressions may be used as arguments to increment/decrement, and as left-hand arguments of assignment and compound assignment operators.

        public static void PostfixIncrementOperator()
        {
            IncrementDecrement(PrefixType.Postfix, IncrementType.Increment);
        }
        
        public static void PostfixDecrementOperator()
        {
            IncrementDecrement(PrefixType.Postfix, IncrementType.Decrement);
        }

        public static void PrefixIncrementOperator()
        {
            IncrementDecrement(PrefixType.Prefix, IncrementType.Increment);
        }
        
        public static void PrefixDecrementOperator()
        {
            IncrementDecrement(PrefixType.Prefix, IncrementType.Decrement);
        }

        private enum PrefixType
        {
            Prefix,
            Postfix
        }

        private enum IncrementType
        {
            Increment,
            Decrement
        }

        private static void IncrementDecrement(PrefixType prefixType, IncrementType incrementType)
        {
            // The operand expr of both prefix and postfix increment or decrement must be a modifiable lvalue of integer type or a pointer type.
            // The result of the postfix increment and decrement operators is the value of expr.
            // The result of the prefix increment operator is the result of adding the value 1 to the value of expr: the expression ++e is equivalent to e+=1. 
            // The result of the prefix decrement operator is the result of subtracting the value 1 from the value of expr: the expression --e is equivalent to e-=1

            CExpression expr = PopExpression();

            if (!expr.IsModifiableLValue)
            {
                throw new SemanticException("The operand of both prefix and postfix increment or decrement must be a modifiable lvalue");
            }

            if (!expr.Type.IsInteger && !expr.Type.IsPointer)
            {
                throw new SemanticException("The operand of both prefix and postfix increment or decrement\n must be integer type, or a pointer type");
            }

            CExpression result = new CExpression(expr.Type, ValueCatagory.RValue);

            result.Add(expr);
            if (prefixType == PrefixType.Postfix)
            {
                result.Add(new OpCodeEmitter(expr.GetLoadOperation()));
                //TODO fix it so we dont have to do this iefficent thing
                //do some twiddling with the stack pointer
                result.Add(expr);
            }
            else
            {
                result.Add(new OpCodeEmitter(OpCode.COPY));
            }
            result.Add(new OpCodeEmitter(OpCode.COPY));
            result.Add(new OpCodeEmitter(expr.GetLoadOperation()));
            result.Add(new OpCodeEmitter(OpCode.PUSH, (expr.Type.IsPointer ? expr.Type.ContainedType.Size : 1).ToString()));

            if (incrementType == IncrementType.Increment)
            {
                result.Add(new OpCodeEmitter(OpCode.ADD));
            }
            else if (incrementType == IncrementType.Decrement)
            {
                result.Add(new OpCodeEmitter(OpCode.SUB));
            }

            result.Add(new OpCodeEmitter(expr.GetStoreOperation())); // make size dependend
            if (prefixType == PrefixType.Prefix)
            {
                result.Add(new OpCodeEmitter(expr.GetLoadOperation()));
            }

            PushExpression(result);
        }

        //Arithmetic operators
        //Arithmetic operators apply standard mathematical operations to their operands.
        //http://en.cppreference.com/w/c/language/operator_arithmetic

        /* Overflows
         * Unsigned integer arithmetic is always performed modulo 2n
         * where n is the number of bits in that particular integer.
         * When signed integer arithmetic operation overflows (the result does not fit in the result type) 
         * it wraps around according to the rules of the representation (2's complement), 
         */

        //Both unary plus and unary minus first apply integral promotions to their operand
        /*Integer promotions
         * Integer promotion is the implicit conversion of a value of any integer type 
         * with rank less or equal to rank of int 
         * to the value of type int or unsigned int 
         * If int can represent the entire range of values of the original type, 
         * the value is converted to type int. Otherwise the value is converted to unsigned int.
         * Integer promotions preserve the value, including the sign:
         */

        private static CExpression PromoteInteger(CExpression expr)
        {
            if(!expr.Type.IsInteger)
            {
                throw new SemanticException("cannot promote non integer type");
            }

            if(CType.FromTypeClass(CTypeClass.CInt).TypeInArithmeticRange(expr.Type))
            {
                expr.ConvertToType(CType.FromTypeClass(CTypeClass.CInt));
            }
            else
            {
                expr.ConvertToType(CType.FromTypeClass(CTypeClass.CUInt));
            }

            return expr;
        }

        public static void UnaryPlusOperator()
        {
            CExpression expr = PopExpression().ToRValue();

            if (!expr.Type.IsInteger)
            {
                throw new SemanticException("sign manipulation operations can only be applied to integer types");
            }

            expr = PromoteInteger(expr);

            PushExpression(expr);
        }

        public static void UnaryMinusOperator()
        {
            UnaryPlusOperator();

            CExpression expr = PopExpression();

            expr.Add(new OpCodeEmitter(OpCode.NEG));

            PushExpression(expr);
        }

        //all binary arithmetic operator perform unsual arithmetic convertion
        //http://en.cppreference.com/w/c/language/conversion#Usual_arithmetic_conversions

        // ONLY INT TYPE SUPPORTED
        /* First of all, both operands undergo integer promotions (converts to 32 bit int/uint). Then
         * If the types after promotion are the same, that type is the common type (32 bit int/uint)
         * 
         * Otherwise, the signedness is different: 
         *  
         *  If the operand with the unsigned type has conversion rank greater or equal than the rank of the type of the signed operand, 
         *  then the operand with the signed type is implicitly converted to the unsigned type
         *  
         *  Otherwise, the signedness is different and the signed operand's rank is greater than unsigned operand's rank. 
         *  In this case, 
         *      if the signed type can represent all values of the unsigned type, 
         *      then the operand with the unsigned type is implicitly converted to the type of the signed operand.
         * 
         *      Otherwise, both operands undergo implicit conversion to the unsigned type counterpart of the signed operand's type.
         */

        private static CExpression UsualArithmaticConversion()
        {
            CExpression rhs = PromoteInteger(PopExpression().ToRValue());
            CExpression lhs = PromoteInteger(PopExpression().ToRValue());

            if(lhs.Type != rhs.Type)
            {
                CExpression uexpr;
                CExpression sexpr;
                
                if(lhs.Type.IsUnsigned && !rhs.Type.IsUnsigned)
                {
                    uexpr = lhs;
                    sexpr = rhs;
                }
                else
                {
                    uexpr = rhs;
                    sexpr = lhs;
                }

                if(uexpr.Type.Size >= sexpr.Type.Size)
                {
                    sexpr.ConvertToType(uexpr.Type);
                }
                else
                {
                    if(sexpr.Type.TypeInArithmeticRange(uexpr.Type))
                    {
                        uexpr.ConvertToType(sexpr.Type);
                    }
                    else
                    {
                        sexpr.ToUnsigned();
                        uexpr.ConvertToType(sexpr.Type);
                    }
                }
            }

            CExpression result = new CExpression(lhs.Type, ValueCatagory.RValue);
            result.Add(lhs);
            result.Add(rhs);

            return result;
        }

        private static CExpression PointerArithmatic()
        {
            CExpression rhs = PopExpression().ToRValue();
            CExpression lhs = PopExpression().ToRValue();

            if(!lhs.Type.IsPointer && !rhs.Type.IsPointer)
            {
                PushExpression(lhs);
                PushExpression(rhs);

                return UsualArithmaticConversion();
            }

            if(lhs.Type.IsPointer && rhs.Type.IsPointer)
            {
                throw new SemanticException("cannot add two pointer types");
            }
            else
            {
                CExpression ptr;
                CExpression arith;

                if(lhs.Type.IsPointer && rhs.Type.IsInteger)
                {
                    ptr = lhs;
                    arith = rhs;
                }
                else if (lhs.Type.IsInteger && rhs.Type.IsPointer)
                {
                    ptr = rhs;
                    arith = lhs;
                }
                else
                {
                    throw new SemanticException("cannot add/substract expresion of these types");
                }

                arith.Add(new OpCodeEmitter(OpCode.PUSH, ptr.Type.ContainedType.Size.ToString()));
                arith.Add(new OpCodeEmitter(OpCode.MUL));

                CExpression result = new CExpression(ptr.Type, ValueCatagory.RValue);
                result.Add(ptr);
                result.Add(arith);

                return result;
            }
        }
        
        public static void AdditionOperator()
        {
            CExpression result = PointerArithmatic();
            result.Add(new OpCodeEmitter(OpCode.ADD));
            PushExpression(result);
        }

        public static void SubtractionOperator()
        {
            CExpression result = PointerArithmatic();
            result.Add(new OpCodeEmitter(OpCode.SUB));
            PushExpression(result);
        }

        public static void MultiplicationOperator()
        {
            CExpression result = UsualArithmaticConversion();
            result.Add(new OpCodeEmitter(OpCode.MUL));
            PushExpression(result);
        }

        public static void DivisionOperator()
        {
            throw new NotSupportedException("Atlas dosnt have hardware division");
        }

        public static void ModuloOperator()
        {
            throw new NotSupportedException("Atlas dosnt have hardware division");
        }

        public static void BitwiseNotOperator()
        {
            //~ rhs
            //the operator ~ performs integer promotions on its only operand
            // for unsigned types (after promotion), the expression ~E is equivalent to the maximum value representable by the result type minus the original value of E

            CExpression rhs = PopExpression().ToRValue();

            if (!rhs.Type.IsInteger)
            {
                throw new SemanticException("cannot perform bitwise not on non integer type");
            }

            rhs = PromoteInteger(rhs);

            //todo handle constant
            CExpression result = new CExpression(CType.FromTypeClass(CTypeClass.CInt), ValueCatagory.RValue);
            result.Add(rhs);
            result.Add(new OpCodeEmitter(OpCode.NOT));

            PushExpression(result);
        }

        public static void BitwiseAndOperator()
        {
            CExpression result = UsualArithmaticConversion();
            result.Add(new OpCodeEmitter(OpCode.AND));
            PushExpression(result);
        }

        public static void BitwiseOrOperator()
        {
            CExpression result = UsualArithmaticConversion();
            result.Add(new OpCodeEmitter(OpCode.OR));
            PushExpression(result);
        }

        public static void BitwiseXorOperator()
        {
            CExpression result = UsualArithmaticConversion();
            result.Add(new OpCodeEmitter(OpCode.XOR));
            PushExpression(result);
        }

        public static void BitwiseLeftShiftOperator()
        {
            CExpression result = UsualArithmaticConversion();
            result.Add(new OpCodeEmitter(OpCode.SLL));
            PushExpression(result);
        }

        public static void BitwiseRightShiftOperator()
        {
            CExpression result = UsualArithmaticConversion();
            result.Add(new OpCodeEmitter(OpCode.SRL));
            PushExpression(result);
        }

        //Logical operators
        //Logical operators apply standard boolean algebra operations to their operands.
        //http://en.cppreference.com/w/c/language/operator_logical

        public static void LogicalNotOperator()
        {
            /*
                The logical NOT expression has the form
                    ! expression		
                where
                    expression	-	an expression of any scalar type
                The logical NOT operator has type int. Its value is ​0​ if expression evaluates to a value that compares unequal to zero. 
                Its value is 1 if expression evaluates to a value that compares equal to zero. (so !E is the same as (E==0))
                see note on ==
             */

            PushConstant("0");
            EqualToOperator();
        }

        public static void LogicalAndOperator()
        {
            /*
                The logical AND expression has the form
                    lhs && rhs		
                where
                    lhs	-	an expression of any scalar type
                    rhs	-	an expression of any scalar type, which is only evaluated if lhs does not compare equal to ​0​
                The logical-AND operator has type int and the value 1 if both lhs and rhs compare unequal to zero. It has the value ​0​ otherwise (if either lhs or rhs or both compare equal to zero).
                If the result of lhs compares equal to zero, then rhs is not evaluated at all (so-called short-cirquit evaluation)
             */

            CExpression rhs = PopExpression().ToRValue();
            CExpression lhs = PopExpression().ToRValue();

            if (!lhs.Type.InTypeGroup(CTypeGroups.CScalar) || !rhs.Type.InTypeGroup(CTypeGroups.CScalar))
            {
                throw new SemanticException("operands to logical operation must be scalar");
            }

            //(lhs)
            //duptop
            //lnot
            //push end
            //jif
            //pop
            //(rhs)
            //end:
            CExpression result = new CExpression(CType.FromTypeClass(CTypeClass.CInt), ValueCatagory.RValue);

            //(lhs)
            result.Add(lhs);
            //duptop
            result.Add(new OpCodeEmitter(OpCode.COPY));

            //lnot
            PushExpression(result);
            LogicalNotOperator();
            result = PopExpression();

            //push end
            string label = CIdentifier.AutoGenerateLabel("LogicalAndEnd");
            result.Add(new OpCodeEmitter(OpCode.PUSH, label));
            //jif
            result.Add(new OpCodeEmitter(OpCode.JIF));
            //pop
            result.Add(new OpCodeEmitter(OpCode.POP));
            //(rhs)
            result.Add(rhs);
            //end:
            result.Add(new LabelEmitter(label));

            PushExpression(result);
        }

        public static void LogicalOrOperator()
        {
            /*
                The logical AND expression has the form
                    lhs || rhs		
                where
                    lhs	-	an expression of any scalar type
                    rhs	-	an expression of any scalar type, which is only evaluated if lhs compares equal to ​0​
                The logical-AND operator has type int and the value 1 if either lhs and rhs compare unequal to zero. It has the value ​0​ otherwise (if both compare equal to zero).
                If the result of lhs does not compare equal to zero, then rhs is not evaluated at all (so-called short-cirquit evaluation)
             */

            CExpression rhs = PopExpression().ToRValue();
            CExpression lhs = PopExpression().ToRValue();

            if (!lhs.Type.InTypeGroup(CTypeGroups.CScalar) || !rhs.Type.InTypeGroup(CTypeGroups.CScalar))
            {
                throw new SemanticException("operands to logical operation must be scalar");
            }

            //(lhs)
            //duptop
            //push end
            //jif
            //pop
            //(rhs)
            //end:
            CExpression result = new CExpression(CType.FromTypeClass(CTypeClass.CInt), ValueCatagory.RValue);

            //(lhs)
            result.Add(lhs);
            //duptop
            result.Add(new OpCodeEmitter(OpCode.COPY));
            //push end
            string label = CIdentifier.AutoGenerateLabel("LogicalOrEnd");
            result.Add(new OpCodeEmitter(OpCode.PUSH, label));
            //jif
            result.Add(new OpCodeEmitter(OpCode.JIF));
            //pop
            result.Add(new OpCodeEmitter(OpCode.POP));
            //(rhs)
            result.Add(rhs);
            //end:
            result.Add(new LabelEmitter(label));

            PushExpression(result);
        }

        //Comparison operators
        //Comparison operators are binary operators that test a condition and return 1 if that condition is logically true and 0 if that condition is false
        //http://en.cppreference.com/w/c/language/operator_comparison

        /* The type of any relational operator expression is int.
         * If lhs and rhs are expressions of any int type, then
         *  usual arithmetic conversions are performed
         *  the values of the operands after conversion are compared in the usual mathematical sense
         *  
         * If lhs and rhs are expressions of pointer type, they must be both pointers to objects of compatible types.
         */

        public static void EqualToOperator()
        {
            CExpression result = UsualArithmaticConversion();
            result.Add(new OpCodeEmitter(OpCode.EQU));
            PushExpression(result);
        }

        public static void NotEqualToOperator()
        {
            EqualToOperator();
            LogicalNotOperator();
        }

        public static void LessThanOperator()
        {
            CExpression result = UsualArithmaticConversion();
            result.Add(new OpCodeEmitter(OpCode.LESS));
            PushExpression(result);
        }

        public static void GreaterThanOperator()
        {
            SwapArgs();
            LessThanOperator();
        }

        public static void LessThanOrEqualOperator()
        {
            CExpression rhs = PopExpression().ToRValue();
            rhs.Add(new OpCodeEmitter(OpCode.PUSH, "1")); //todo remember this will break if we ever go to floats
            rhs.Add(new OpCodeEmitter(OpCode.ADD));
            PushExpression(rhs);
            LessThanOperator();
        }

        public static void GreaterThanOrEqualOperator()
        {
            SwapArgs();
            LessThanOrEqualOperator();
        }

        private static void SwapArgs()
        {
            CExpression a = PopExpression();
            CExpression b = PopExpression();

            PushExpression(a);
            PushExpression(b);
        }

        //Member access operators
        //Member access operators allow access to the members of their operands
        //http://en.cppreference.com/w/c/language/operator_member_access

        public static void SubscriptOperator()
        {
            // The array subscrpt expression has the form
            // pointer-expression [ integer-expression ]	
            /*
             * 
                pointer-expression	-	an expression of type pointer to complete object
                integer-expression	-	an expression of integer type
                The subscript operator expression is is lvalue expression whose type is the type of the object pointed to by pointer-expression.
                
                By definition, the subscript operator E1[E2] is exactly identical to *((E1)+(E2)). 
             */

            CExpression index = PopExpression().ToRValue();
            CExpression array = PopExpression().ToRValue();

            if (!array.Type.IsPointer && !array.Type.IsArray)
            {
                throw new SemanticException("cannot use array subscript operator on non pointer type");
            }

            if (!index.Type.IsInteger)
            {
                throw new SemanticException("array index expression must be of integer type");
            }

            int elementSize = array.Type.ContainedType.Size;

            //todo support const arrays/ const array elements
            CExpression result = new CExpression(array.Type.ContainedType, ValueCatagory.LValue);

            result.Add(array);
            result.Add(index);
            result.Add(new OpCodeEmitter(OpCode.PUSH, elementSize.ToString()));
            result.Add(new OpCodeEmitter(OpCode.MUL));
            result.Add(new OpCodeEmitter(OpCode.ADD));

            PushExpression(result);
        }

        public static void DereferenceOperator()
        {
            //unaryOperator castExpression
            /*
             The dereference or indirection expression has the form
                *pointer-expression		
             where
                    pointer-expression	-	an expression of any pointer type
            If pointer-expression is a pointer to object, the result is an lvalue expression that designates the pointed-to object.
            
             Dereferencing a null pointer, a pointer to an object outside of its lifetime (a dangling pointer), a misaligned pointer, or a pointer with indeterminate value is undefined behavior (handled in architecture)
             */

            CExpression rhs = PopExpression().ToRValue();

            if (!rhs.Type.IsPointer)
            {
                throw new SemanticException("cannot dereference non pointer type");
            }

            CExpression result;

            if (rhs.Type.IsFunctionPointer)
            {
                //a function designator with type “function returning type” is converted to an expression that has type “pointer to function returning type”
                //ie, do nothing

                result = rhs;
            }
            else
            {
                //todo handle pointer to const
                result = new CExpression(rhs.Type.ContainedType, ValueCatagory.LValue);
                result.Add(rhs);
            }

            PushExpression(result);
        }

        public static void AddressOfOperator()
        {
            //  the operand of the address-of operator is not converted to an rvalue 
            
            CExpression rhs = PopExpression();

            if (rhs.Type.IsFunctionPointer)
            {
                //ignore taking the address of a function (it is already a pointer)
                PushExpression(rhs);
            }
            else
            {
                if (rhs.valueCatagory != ValueCatagory.LValue)
                {
                    throw new SemanticException("cannot take the address of a non lvalue");
                }

                CExpression expr = new CExpression(CType.PointerTo(rhs.Type), ValueCatagory.RValue);
                expr.Add(rhs);

                PushExpression(expr);
            }
        }

        public static void MemberAccess(string idString)
        {
            //the left-hand operand of the member access operator is not converted to an rvalue
            
            CExpression obj = PopExpression();

            if (obj.valueCatagory != ValueCatagory.LValue)
            {
                throw new SemanticException("cannot get member from rvalue expression");
            }

            if (obj.Type.IsStruct && obj.Type.StructMembers.Any(id => id.Name.Equals(idString)))
            {
                CIdentifier ident = obj.Type.StructMembers.First(id => id.Name.Equals(idString));

                CExpression result = new CExpression(ident.Type, ValueCatagory.LValue);
                result.Add(obj);
                result.Add(new OpCodeEmitter(OpCode.PUSH, ident.StructOffset.ToString()));
                result.Add(new OpCodeEmitter(OpCode.ADD));

                PushExpression(result);
            }
            else
            {
                throw new SemanticException("type " + obj.Type.TypeName + " has no member named \"" + idString + "\"");
            }
        }

        public static void MemberAccessThroughPointer(string idString)
        {
            DereferenceOperator();
            MemberAccess(idString);
        }

        //Other oprators
        //A collection of operators that do not fit into any of the other major categories.
        //http://en.cppreference.com/w/c/language/operator_other

        public static void FunctionCall(int numPassedArguments)
        {
            // postfixExpression '(' argumentExpressionList? ')'
            /*
              The function call expression has the form
                expression ( argument-list(optional) )		
                    where
                        expression	-	any expression of pointer-to-function type (after lvalue conversions)
                        argument-list	-	comma-separated list of expressions (which cannot be comma operators) of any complete object type. May be omitted when calling functions that take no arguments.
             */
            //The type of each parameter must he a type such that implicit conversion as if by assignment exists that converts the unqualified type of the corresponding argument to the type of the parameter.
            //Function is executed, and the value it returns becomes the value of the function call expression (if the function returns void, the function call expression is a void expression)

            //In a function-call expression, to a function that has a prototype, the value of each argument expression is converted to the type of the unqualified declared types of the corresponding parameter
            //In a return statement, the value of the operand of return is converted to an object having the return type of the function

            List<CExpression> arguments = new List<CExpression>();

            for (int i = 0; i < numPassedArguments; i++)
            {
                //C function calls are only call-by-value
                arguments.Add(PopExpression().ToRValue());
            }

            CExpression functionName = PopExpression();

            //functionName	-	any expression of pointer-to-function type
            if (!functionName.Type.IsFunctionPointer)
            {
                throw new SemanticException("canot call expresion of type " + functionName.Type.TypeName + ": expected pointer to function");
            }

            IReadOnlyList<CType> argTyps = functionName.Type.ContainedType.FunctionArgumentTypes;

            //The number of parameters must equal the number of arguments
            if (numPassedArguments < argTyps.Count)
            {
                throw new SemanticException("to few arguments passed to function");
            }
            else if (numPassedArguments > argTyps.Count)
            {
                throw new SemanticException("to many arguments passed to function");
            }

            //The type of each parameter must he a type such that implicit conversion as if by assignment exists 
            //that converts the unqualified type of the corresponding argument to the type of the parameter
            for (int i = 0; i < numPassedArguments; i++)
            {
                if (!arguments[i].CanConvertImplicitlyToType(argTyps[1]))
                {
                    throw new SemanticException("error in " + i + "th argument: cannot convert argument of type " + arguments[i].Type + " to " + argTyps[i]);
                }
                else
                {
                    arguments[i].ConvertToType(argTyps[i]);
                }
            }

            CExpression result = new CExpression(functionName.Type.ContainedType.FunctionReturnType, ValueCatagory.RValue);

            result.Add(new OpCodeEmitter(OpCode.BEGINARGS));
            foreach (CExpression e in arguments)
            {
                result.Add(e);
            }
            result.Add(functionName);
            result.Add(new OpCodeEmitter(OpCode.CALL));

            PushExpression(result);
        }

        public static void CommaOperator()
        {
            //expression ',' assignmentExpression

            /*
             Comma operator
                The comma operator expression has the form
                    lhs , rhs		
                where
                    lhs	-	any expression
                    rhs	-	any expression other than another comma operator (in other words, comma operator's associativity is left-to-right)
                
                First, the left operand, lhs, is evaluated and its result value is discarded.
                
                Then, the right operand, rhs, is evaluated and its result is returned by the comma operator as a non-lvalue.
                
                Notes
                The type of the lhs may be void (that is, it may be a call to a function that returns void, or it can be an expression cast to void)
                The comma operator returns an rvalue
             */

            CExpression rhs = PopExpression().ToRValue();
            CExpression lhs = PopExpression();

            CExpression result = new CExpression(rhs.Type, ValueCatagory.RValue);
            result.Add(lhs);
            result.Add(new OpCodeEmitter(OpCode.POP));
            result.Add(rhs);

            PushExpression(result);
        }

        public static void TypeCast(string typeName)
        {
            CExpression rhs = PopExpression().ToRValue();

            CType type = CType.CTypeFromName(typeName);

            if (!rhs.CanConvertToType(type))
            {
                throw new SemanticException("cannot cast expresion of type " + rhs.Type + " to type " + type);
            }
            else
            {
                PushExpression(rhs.ConvertToType(type));
            }
        }

        public static void CoditionalOperator()
        {
            /*
                The conditional operator expression has the form
                    condition ? expression-true : expression-false		
                where
                    condition	-	an expression of scalar type
                    expression-true	-	the expression that will be evaluated if condition compares unequal to zero
                    expression-false	-	the expression that will be evaluated if condition compares equal to zero
                Only the following expressions are allowed as expression-true and expression-false
                    two expressions of any arithmetic type
                    two expressions of the same struct or union type
                    two expressions of void type
                    two expressions of pointer type, pointing to types that are compatible, ignoring cvr-qualifiers
                    one expression is a pointer and the other is the null pointer constant (such as NULL)
                    one expression is a pointer to object and the other is a pointer to void (possibly qualified)
                    IE, they must be compatible
                
                1) First, evaluates condition. There is a sequence point after this evaluation.
                2) If the result of condition compares unequal to zero, executes expression-true, otherwise executes exception-false
                3) Performs a conversion from the result of the evaluation to the common type, defined as follows:
                    1) if the expressions have arithmetic type, the common type is the type after usual arithmetic conversions
                    2) if the expressions have struct/union type, the common type is that struct/union type
                    3) if the expressions are both void, the entire conditional operator expression is a void expression
                    4) if one is a pointer and the other is a null pointer constant, the type is the type of that pointer
                    5) if both are pointers, the result is the pointer to the type that combines cvr-qualifiers of both pointed-to types (that is, if one is const int* and the other is volatile int*, the result is const volatile int*), and if the types were different, the pointed-to type is the composite type.
                    6) if one is a pointer to void, the result is a pointer to void with combined cvr-qualifiers
             */


            CExpression exprf = PopExpression().ToRValue();
            CExpression exprt = PopExpression().ToRValue();
            CExpression condition = PopExpression().ToRValue();

            //condition	-	an expression of scalar type
            if (!condition.Type.InTypeGroup(CTypeGroups.CScalar))
            {
                throw new SemanticException("codition in a conditional expresion must be a scalar type");
            }

            if (!exprt.Type.CompatableWith(exprf.Type))
            {
                throw new SemanticException("values in conditional expressions must be of compatable types");
            }

            //(condition)
            //LNOT
            //PUSH FALSECASE
            //JIF
            //(exprt)
            //pushw ENDCONDITION
            //JMP
            //FALSECASE:
            //(exprf)
            //ENDCONDITION:

            CExpression result = new CExpression(exprt.Type, ValueCatagory.RValue);

            //(lhs)
            result.Add(condition);

            //lnot
            PushExpression(result);
            LogicalNotOperator();
            result = PopExpression();

            //PUSH FALSECASE
            string falseCase = CIdentifier.AutoGenerateLabel("conditionalFalseCase");
            result.Add(new OpCodeEmitter(OpCode.PUSH, falseCase));

            //JIF
            result.Add(new OpCodeEmitter(OpCode.JIF));

            //(exprt)
            result.Add(exprt);
            //pushw ENDCONDITION
            string end = CIdentifier.AutoGenerateLabel("endConditional");
            result.Add(new OpCodeEmitter(OpCode.PUSH, end));
            //JMP
            result.Add(new OpCodeEmitter(OpCode.JMP));
            //FALSECASE:
            result.Add(new LabelEmitter(falseCase));
            //(exprf)
            result.Add(exprf);
            //ENDCONDITION:
            result.Add(new LabelEmitter(end));

            PushExpression(result);
        }

        //http://en.cppreference.com/w/c/language/sizeof
        
        public static void SizeOf(string typeName)
        {
            CType type = CType.CTypeFromName(typeName);
            
            CExpression expr = new CExpression(CType.FromTypeClass(CTypeClass.CUInt), ValueCatagory.RValue);
            expr.Add(new OpCodeEmitter(OpCode.PUSH, type.Size.ToString()));
            PushExpression(expr);
        }

        public static void SizeOf()
        {
            /*
             * The operands of the sizeof operator are expressions that are not evaluated  
             * Thus, "size_t n = sizeof(printf("%d", 4));" does not perform console output.
             */

            //the operand of sizeof is not converted to an rvalue

            CExpression rhs = PopExpression();
            SizeOf(rhs.Type.TypeName);
        }

        //ALIGNOF NOT SUPPORTED
        //http://en.cppreference.com/w/c/language/_Alignof

        // push rvalues for operations to consume
        public static void PushString(IEnumerable<string> literals)
        {
            // string literals are sequences of characters of type char[], char16_t[], char32_t[], or wchar_t[] that represent null-terminated strings

            // first, adjacent string literals (that is, string literals separated by whitespace only) are concatenated

            // Only two narrow or two wide string literals may be concatenated.
            // If one literal is unprefixed, the resulting string literal has the width/encoding specified by the prefixed literal. 
            // If the two string literals have different encoding prefixes, concatenation is implementation-defined.

            // String literals are not modifiable (and in fact may be placed in read-only memory such as .rodata). 
            // If a program attempts to modify the static array formed by a string literal, the behavior is undefined.

            // It is neither required nor forbidden for identical string literals to refer to the same location in memory. 
            // Moreover, overlapping string literals or string literals that are substrings of other string literals may be combined.
            // example : "def" == 3+"abcdef"; may be 1 or 0, implementation-defined

            //http://en.cppreference.com/w/c/language/string_literal

            throw new NotSupportedException("string literals not implimented yet");
        }

        public static void PushIdentifier(string idString)
        {
            CIdentifier id = CIdentifier.IdentifierFromNameInCurrentScope(idString);
            
            //TODO constant labels
            CExpression result = new CExpression(id.Type, id.Type.IsFunctionPointer ? ValueCatagory.RValue : ValueCatagory.LValue);

            //TODO move scope scheck into this function
            if (id.IsMember)
            {
                throw new InvalidOperationException("member label passed in reguler scope");
            }

            if (id.IsLocal)
            {
                //get address of local variable relative to stack pointer
                result.Add(new OpCodeEmitter(OpCode.PUSHBP));
                result.Add(new OpCodeEmitter(OpCode.PUSH, id.StackPointerOffset.ToString()));
                result.Add(new OpCodeEmitter(OpCode.ADD));
            }
            else
            {
                //the assembler will take care of figuring out the addresses of global variables
                result.Add(new OpCodeEmitter(OpCode.PUSH, id.Name));
            }

            PushExpression(result);
        }

        public static void PushConstant(string literal)
        {
            /*
             * Constant values of certain types may be embedded in the source code of a C program using specialized expressions known as literals
             * integer constants are decimal, octal, or hexadecimal numbers of integer type.
             * character constants are individual characters of type int suitable for conversion to a character type
             * floating constants are values of type float, double, or long double
             * NO SUPPORT FOR COMPUND LITERALS
             */

            CType type = TypeFromLiteral(literal);

            CExpression constant = new CExpression(type, ValueCatagory.RValue);
            constant.Add(new OpCodeEmitter(OpCode.PUSH, literal));

            PushExpression(constant);
        }
        
        //TODO should this be moved?
        private static CType TypeFromLiteral(string literal)
        {
            /*character constant 
             * ' c-char '	(1)	 
             * NON BYTE CHAR NOT SUPPORTED  THIS MEANS NO UNICODE FOR NOW
             * CHAR SEQUENCE NOT SUPPORTED	
             * 
             * c-char is either
             * a character from the basic source character set minus single-quote ('), backslash (\), or the newline character.
             * escape sequence: one of special character escapes \' \" \? \\ \a \b \f \n \r \t \v, hex escapes \x... or octal escapes \..., or universal \u... \U... as defined in
             * http://en.cppreference.com/w/c/language/escape
             */

            if (literal[0] == '\'')
            {
                return CType.FromTypeClass(CTypeClass.CChar);
            }


            /* integer constant
             * An integer constant is a RValue expression of the form 
             *  decimal-constant integer-suffix(optional)	(1)	
             *  OCTAL NOT SUPPORTED	
             *  hex-constant integer-suffix(optional)	(3)	
             * where
             *  decimal-constant is a non-zero decimal digit (1, 2, 3, 4, 5, 6, 7, 8, 9), followed by zero or more decimal digits (0, 1, 2, 3, 4, 5, 6, 7, 8, 9)
             *  OCTAL NOT SUPPORTED
             *  hex-constant is the character sequence 0x or the character sequence 0X followed by one or more hexadecimal digits (0, 1, 2, 3, 4, 5, 6, 7, 8, 9, a, A, b, B, c, C, d, D, e, E, f, F)
             *  integer-suffix, if provided, may contain one or both of the following (if both are provided, they may appear in any order:
             *      unsigned-suffix (the character u or the character U)
             *      long-suffix (the character l or the character L) or the long-long-suffix (the character sequence ll or the character sequence LL) (since C99)
             * Explanation
             *  1) Decimal integer constant (base 10, the first digit is the most significant).
             *  2) OCTAL NOT SUPPORTED
             *  3) Hexadecimal integer constant (base 16, the first digit is the most significant, the letters 'a' through 'f' represent the decimal values 10 through 15).
             */
            // INT SUFFIX NOT SUPPORTED
            // http://en.cppreference.com/w/c/language/integer_constant
            else
            {
                //OCTAL NOT SUPPORETD
                //FLOATING CONSTANTS NOT SUPPORTED

                //int
                return CType.FromTypeClass(CTypeClass.CInt);
            }
        }

        public static IEnumerable<CExpression> Expressions
        {
            get
            {
                return Emmiters.Reverse();
            }
        }
        
        public static CExpression PopExpression()
        {
            if (Emmiters.Count == 0)
            {
                throw new InvalidOperationException("emitter stack is empty");
            }

            IEmitter emitter = Emmiters.Pop();

            if ((emitter as CExpression) == null)
            {
                throw new InvalidOperationException("expected expression but got " + emitter.GetType().Name);
            }

            return (emitter as CExpression);
        }

        private static void PushExpression(CExpression exp)
        {
            Emmiters.Push(exp);
        }

        private static Stack<CExpression> Emmiters = new Stack<CExpression>();
    }
}
