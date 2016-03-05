using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC
{
    /* Objects, functions, and expressions have a property called type, 
     * which determines the interpretation of the binary value stored in an object or 
     * evaluated by the expression.
     */
    //http://en.cppreference.com/w/c/language/type
    
    public enum CTypeClass
    {
        //void
        CVoid,
        
        /*Arithmetic types*/
        //http://en.cppreference.com/w/c/language/arithmetic_types
        
        /* bool (byte)
         * bool, capable of holding one of the two values: 1(true) and 0(false).
         * Note that conversion to _Bool does not work the same as conversion to other integer types: (bool)0.5 evaluates to 1, whereas (int)0.5 evaluates to ​0​.
         */
        CBool,
        
        /* char (byte)
         * type for character representation. 
         * Equivalent to either signed char or unsigned char (which one is implementation-defined), 
         * but char is a distinct type, different from both signed char both unsigned char
         */
        CChar,
        
        /* signed (two's complement) char (byte) 
         * type for signed character representation.
         */
        CSChar,
        
        /* unsigned char - type for unsigned character representation. 
         * Also used to inspect object representations (raw byte).
         */
        CUChar,

        // sizes of Integer types based on ILP32

        /*Note: integer arithmetic is defined differently for the signed and unsigned integer types. 
         * See arithmetic operators, in particular integer overflows.*/
        //http://en.cppreference.com/w/c/language/operator_arithmetic

        // signed (two's complement), 2 byte int
        CShortInt,
        // unsigned, 2 byte int
        CUShortInt,
        // signed (two's complement), 4 byte int
        CInt,
        // unsigned, 4 byte int
        CUInt,
        // signed (two's complement), 4 byte int
        CLongInt,
        // unsigned, 4 byte int
        CULongInt,
        // signed (two's complement), 8 byte int (not implimented)
        CLongLongInt,
        // unsigned, 8 byte int (not implimented)
        CULongLongInt,


        //FLOATING POINT UNSUPPORTED
        //ADD MORE DOCUMENTATION IF FLOAT SUPPORT IS ADDED

        // IEEE-754 https://en.wikipedia.org/wiki/IEEE_floating_point
        
        //single precision floating point type. Matches IEEE-754 32 bit floating point type if supported.
        CFloat,
        //double precision floating point type. Matches IEEE-754 64 bit floating point type if supported
        CDouble,
        /* extended precision floating point type. 
         * Matches IEEE-754 extended floating-point type if supported, 
         * otherwise matches some non-standard extended floating-point type 
         * as long as its precision is better than double and range is at least as good as double, 
         * otherwise matches the type double.*/
        CLongDouble,

        //COMPLEX FLOATING TYPES UNSUPPORTED
        //IMAGINARY FLOATING TYPES UNSUPPORTED

        /* An enumerated type is a distinct type whose value 
         * is a value of its underlying type (int), 
         * which includes the values of explicitly named constants (enumeration constants).
         * FOR SIMPLICITY, ALL ENUMS ARE WORD SIZE
         */
        //http://en.cppreference.com/w/c/language/enum
        CEnum,

        /* Array is a type consisting of a contiguously allocated 
         * nonempty sequence of objects with a particular element type. 
         * The number of those objects (the array size) 
         * never changes during the array lifetime.
         */
        CArray,

        /* A struct is a type consisting of a sequence of members 
         * whose storage is allocated in an ordered sequence.
         */
        CStruct,

        //UNION NOT SUPPORTED

        /* A function is a C language construct that associates a compound statement 
         * (the function body) 
         * with an identifier (the function name). 
         * Every C program begins execution from the main function, 
         * which either terminates, or invokes other functions 
         * Functions may accept zero or more parameters, 
         * which are initialized from the arguments of a function call operator, 
         * and may return a value to its caller by means of the return statement.
         */
        // http://en.cppreference.com/w/c/language/functions
        CFunction,

        /* Pointer is a type of an object that refers to a function or an object of another type, 
         * possibly adding qualifiers. 
         * Pointer may also refer to nothing, which is indicated by the special null pointer value. 
         * Pointers are used for indirection, which is a ubiquitous programming technique; 
         * they can be used to implement pass-by-reference semantics, 
         * to access objects with dynamic storage duration, 
         * to implement "optional" types (using the null pointer value), 
         * aggregation relationship between structs, 
         * callbacks (using pointers to functions), 
         * generic interfaces (using pointers to void), 
         * and much more.
         * Pointer to object of any type can be implicitly converted to pointer to void 
         * and vice versa:
         * Pointers to void are used to pass objects of unknown type
         * Pointers of every type have a special value known as null pointer value of that type. 
         * A pointer whose value is null does not point to an object or a function (dereferencing a null pointer is undefined behavior), 
         * and compares equal to all pointers of the same type whose value is also null.
         * To initialize a pointer to null or to assign the null value to an existing pointer, 
         * an integer constant with the value zero may be used
         */
        //http://en.cppreference.com/w/c/language/pointer
        CPointer

        //ATOMIC NOT SUPORTED
    }

    /* For every type listed above several qualified versions of its type may exist, 
     * corresponding to the combinations of one, two, or all three of the 
     * const, volatile, and restrict 
     * qualifiers 
     * (where allowed by the qualifier's semantics).
     * for two types to be compatible, their qualifications must be identical.
     */

    //unused for now, because all we car about is const
    public enum CTypeQualification
    {
        /* Objects declared with const-qualified types may be placed in 
         * read-only memory by the compiler,
         * and if the address of a const object is never taken in a program, 
         * it may not be stored at all.
         * const semantics apply to lvalue expressions only; 
         * whenever a const lvalue expression is used in context that does not require an lvalue, 
         * its const qualifier is lost (note that volatile qualifier, if present, isn't lost).
         * lvalue expressions that designate objects of const-qualified type
         * or lvalue objects of struct or union type with at least one member of const-qualified type
         * are NOT modifiable lvalues. In particular, they are not assignable
         * A pointer to an non-const type can be implicitly converted 
         * to a pointer to const-qualified version of the same or compatible type. 
         * The reverse conversion can be performed with a cast expression (THIS CAST IS NOT SUPPORTED BY ATLAS).
         */
        //http://en.cppreference.com/w/c/language/const
        CConst,

        // volatile is related to compiler optimizations, 
        // which do not exist in atlas right now. so for now, this is ignored
        //http://en.cppreference.com/w/c/language/volatile
        CVolatile,

        // restrict is related to compiler optimizations, 
        // which do not exist in atlas right now. so for now, this is ignored
        //http://en.cppreference.com/w/c/language/restrict
        CRestrict
    }

    [Flags]
    public enum CTypeGroups
    {
        CObject = 1 << 0, //all types that aren't function types
        CCharacter = 1 << 1, // char, signed char, unsigned char
        CInt = 1 << 2, //  char, signed integer types, unsigned integer types, enumerated types
        CArithmetic = 1 << 3, // integer types and floating types
        CScalar = 1 << 4, // arithmetic types and pointer types
        CAggregate = 1 << 5, // array types and structure types
        CDerivedType = 1 << 6 // array types, function types, and pointer types
    }
    
    public class CType
    {
        static CType()
        {
            InitBasicType(CTypeClass.CVoid);
            InitBasicType(CTypeClass.CChar);
            InitBasicType(CTypeClass.CSChar);
            InitBasicType(CTypeClass.CUChar);
            InitBasicType(CTypeClass.CShortInt);
            InitBasicType(CTypeClass.CUShortInt);
            InitBasicType(CTypeClass.CInt);
            InitBasicType(CTypeClass.CUInt);
            InitBasicType(CTypeClass.CLongInt);
            InitBasicType(CTypeClass.CULongInt);
            InitBasicType(CTypeClass.CLongLongInt);
            InitBasicType(CTypeClass.CULongLongInt);
            InitBasicType(CTypeClass.CFloat);
            InitBasicType(CTypeClass.CDouble);
            InitBasicType(CTypeClass.CLongDouble);
        }

        private static void InitBasicType(CTypeClass typeClass)
        {
            string name = NameFromTypeClass(typeClass);
            types[NameFromTypeClass(typeClass)] = new CType(typeClass,name);
        }
        
        public static CType PointerTo(CType cType)
        {
            string name = NameFromPointerToType(cType);
            if (!types.ContainsKey(name)) types[name] = new CType(CTypeClass.CPointer, name, cType);

            return types[name];
        }

        private static string NameFromPointerToType(CType cType)
        {
            return "pointer to " + cType.TypeName;
        }

        /*private readonly CTypeClass m_typeClass;
        private readonly bool m_isConst;
        private readonly string m_typeName;
        private readonly int m_arraySize;
        private readonly CType m_containedType;
        private readonly IReadOnlyList<CIdentifier> m_enumConstants;
        private IReadOnlyList<CIdentifier> m_structMembers;
        private bool m_structComplete = false;
        private readonly CType m_functionReturnType;
        private readonly IReadOnlyList<CType> m_functionArgumentTypes;*/

        private CType(CTypeClass typeClass, string name)
        {
            //todo expand this for more complex types
            m_typeClass = typeClass;
            m_isConst = false;
            m_typeName = name;
            m_arraySize = 0;
            m_containedType = null;
            m_enumConstants = null;
            m_structMembers = null;
            m_functionReturnType = null;
            m_functionArgumentTypes = null;
        }

        private CType(CTypeClass typeClass, string name, CType containedType)
        {
            //todo expand this for more complex types
            m_typeClass = typeClass;
            m_isConst = false;
            m_typeName = name;
            m_arraySize = 0;
            m_containedType = containedType;
            m_enumConstants = null;
            m_structMembers = null;
            m_functionReturnType = null;
            m_functionArgumentTypes = null;
        }
        
        public static CTypeGroups GetTypeGroup(CTypeClass classification)
        {
            switch(classification)
            {
                case CTypeClass.CVoid:
                    return CTypeGroups.CObject;
                case CTypeClass.CChar:
                case CTypeClass.CSChar:
                case CTypeClass.CUChar:
                    return CTypeGroups.CObject | CTypeGroups.CCharacter | CTypeGroups.CInt | CTypeGroups.CArithmetic | CTypeGroups.CScalar;
                case CTypeClass.CShortInt:
                case CTypeClass.CUShortInt:
                case CTypeClass.CInt:
                case CTypeClass.CUInt:
                case CTypeClass.CLongInt:
                case CTypeClass.CULongInt:
                case CTypeClass.CLongLongInt:
                case CTypeClass.CULongLongInt:
                case CTypeClass.CEnum:
                    return CTypeGroups.CObject | CTypeGroups.CInt | CTypeGroups.CArithmetic | CTypeGroups.CScalar;
                case CTypeClass.CFloat:
                case CTypeClass.CDouble:
                case CTypeClass.CLongDouble:
                    return CTypeGroups.CObject | CTypeGroups.CArithmetic | CTypeGroups.CScalar;
                case CTypeClass.CArray:
                    return CTypeGroups.CObject | CTypeGroups.CAggregate | CTypeGroups.CDerivedType;
                case CTypeClass.CStruct:
                    return CTypeGroups.CObject | CTypeGroups.CAggregate;
                case CTypeClass.CFunction:
                    return CTypeGroups.CDerivedType;
                case CTypeClass.CPointer:
                    return CTypeGroups.CObject | CTypeGroups.CScalar | CTypeGroups.CDerivedType;
                default:
                    throw new InvalidOperationException("unrecognized type clasification");
            }
        }

        public static CType CTypeFromName(string name)
        {
            if(!types.ContainsKey(name))
            {
                throw new SemanticException("unrecognized type " + name);
            }
            
            return types[ResolveTypeDef(name)];
        }

        public static CType FromTypeClass(CTypeClass typeClass)
        {
            return CTypeFromName(NameFromTypeClass(typeClass));
        }

        private static string NameFromTypeClass(CTypeClass typeClass)
        {
            switch (typeClass)
            {
                case CTypeClass.CVoid:
                    return "void";
                case CTypeClass.CChar:
                    return "char";
                case CTypeClass.CSChar:
                    return "signed char";
                case CTypeClass.CUChar:
                    return "unsigned char";
                case CTypeClass.CShortInt:
                    return "short int";
                case CTypeClass.CUShortInt:
                    return "unsigned short int";
                case CTypeClass.CInt:
                    return "int";
                case CTypeClass.CUInt:
                    return "unsigned int";
                case CTypeClass.CLongInt:
                    return "long int";
                case CTypeClass.CULongInt:
                    return "unsigned long int";
                case CTypeClass.CLongLongInt:
                    return "long long int";
                case CTypeClass.CULongLongInt:
                    return "unsigned long long int";
                case CTypeClass.CFloat:
                    return "float";
                case CTypeClass.CDouble:
                    return "double";
                case CTypeClass.CLongDouble:
                    return "long double";
                case CTypeClass.CEnum:
                case CTypeClass.CArray:
                case CTypeClass.CStruct:
                case CTypeClass.CFunction:
                case CTypeClass.CPointer:
                default:
                    throw new InvalidOperationException("cannopt get name from this type");
            }
        }

        private static string ResolveTypeDef(string name)
        {
            if (types.ContainsKey(name)) return name;
            else if (!typeDefs.ContainsKey(name)) throw new SemanticException("type " + name + " is undefined");
            else return ResolveTypeDef(typeDefs[name]);
        }

        public static void TypeDef(string typeDefName, CType type)
        {
            if (typeDefs.ContainsKey(typeDefName))
            {
                throw new SemanticException("Cannot typedef previosly typedefed name");
            }

            typeDefs[typeDefName] = type.TypeName;
        }

        public static int TypeClassSizeInBytes(CTypeClass typeClass)
        {
            switch (typeClass)
            {
                //1 (8 bits)
                case CTypeClass.CChar:
                case CTypeClass.CSChar:
                case CTypeClass.CUChar:
                    return 1;
                //2 (16 bits)
                case CTypeClass.CShortInt:
                case CTypeClass.CUShortInt:
                    return 2;
                //4 (32 bits)
                case CTypeClass.CInt:
                case CTypeClass.CUInt:
                case CTypeClass.CLongInt:
                case CTypeClass.CULongInt:
                case CTypeClass.CEnum:
                case CTypeClass.CFloat:
                case CTypeClass.CPointer:
                    return 4;
                //8 (64 bits)
                case CTypeClass.CLongLongInt:
                case CTypeClass.CULongLongInt:
                case CTypeClass.CDouble:
                case CTypeClass.CLongDouble:
                    return 8;
                //incomplete types
                case CTypeClass.CVoid:
                case CTypeClass.CArray:
                case CTypeClass.CStruct:
                case CTypeClass.CFunction:
                    return -1;
                default:
                    throw new InvalidOperationException("unrecognized type clasification");
            }
        }

        public bool CompatableWith(CType type)
        {
            /* In a C program, 
             * the declarations referring to the same object or function in different translation units 
             * do not have to use the same type. 
             * They only have to use sufficiently similar types, 
             * formally known as compatible types. 
             * Same applies to function calls and lvalue accesses; 
             * argument types must be compatible with parameter types and lvalue expression type 
             * must be compatible with the object type that is accessed.
             * The types T and U are compatible, if...
             */
            bool equ = false;
            // they are the same type
            equ = equ || this.Equals(type);
            // they are pointer types and are pointing to compatible types
            equ = equ || (TypeClass == CTypeClass.CPointer && type.TypeClass == CTypeClass.CPointer && ContainedType.CompatableWith(type.ContainedType));
            /*they are array types, their element types are compatible, and both have the same size.*/
            equ = equ || (TypeClass == CTypeClass.CArray && type.TypeClass == CTypeClass.CArray && ArraySize == type.ArraySize && ContainedType.CompatableWith(type.ContainedType));
            //one is an enumerated type and the other is that enumeration's underlying type
            equ = equ || ((TypeClass == CTypeClass.CEnum && type.TypeClass == CTypeClass.CInt) || (TypeClass == CTypeClass.CInt && type.TypeClass == CTypeClass.CEnum));
            /* they are function types, and 
             * their return types are compatible 
             * they both use parameter lists, the number of parameters is the same, 
             * and the corresponding parameters have compatible types
             */
            equ = equ || ((TypeClass == CTypeClass.CFunction && type.TypeClass == CTypeClass.CFunction) && FunctionTypesCompatable(type));
            return equ;
        }

        private bool FunctionTypesCompatable(CType type)
        {
            bool equ = true;
            equ = equ && FunctionReturnType.CompatableWith(type);
            equ = equ && FunctionArgumentTypes.Count == type.FunctionArgumentTypes.Count;

            for(int i = 0; i < FunctionArgumentTypes.Count; i++)
            {
                if (!equ) break;

                equ = FunctionArgumentTypes[i].CompatableWith(type.FunctionArgumentTypes[i]);
            }

            return equ;
        }

        public int Size
        {
            get
            {
                if(!Complete)
                {
                    throw new SemanticException("cannot get size of incompleate type");
                }
                else
                {
                    int classSize = TypeClassSizeInBytes(TypeClass);
                    if(classSize != -1)
                    {
                        return classSize;
                    }
                    else if(TypeClass == CTypeClass.CArray)
                    {
                        return ArraySize * ContainedType.Size;
                    }
                    else if(TypeClass == CTypeClass.CStruct)
                    {
                        int structSize = 0;
                        foreach (var member in StructMembers)
                        {
                            structSize += member.Type.Size;
                        }

                        return structSize;
                    }
                    else
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }

        /* An incomplete type is an object type that lacks sufficient information 
         * to determine the size of the objects of that type. 
         * An incomplete type may be completed at some point in the translation unit.
         * The following types are incomplete:
         * the type void. This type cannot be completed.
         * structure type of unknown content. 
         * It can be completed by a declaration of the same structure that defines its content later in the same scope.
         */
        public bool Complete
        {
            get
            {
                if(TypeClass == CTypeClass.CVoid || TypeClass == CTypeClass.CFunction)
                {
                    return false;
                }
                else if(TypeClass == CTypeClass.CStruct)
                {
                    bool membersComplete = true;
                    foreach(var member in StructMembers)
                    {
                        if(!member.Type.Complete)
                        {
                            membersComplete = false;
                            break;
                        }
                    }
                    return m_structComplete && membersComplete;
                }
                else if (TypeClass == CTypeClass.CArray)
                {
                    return m_containedType.Complete;
                }
                else
                {
                    return true;
                }
            }
        }

        public void SpecifyStructMembers(IReadOnlyList<CIdentifier> members)
        {
            m_structMembers = members;
            m_structComplete = true;
        }

        public bool InTypeGroup(CTypeGroups group)
        {
            return GetTypeGroup(m_typeClass).HasFlag(group);
        }

        public bool IsPointer
        {
            get
            {
                return TypeClass == CTypeClass.CPointer;
            }
        }

        public bool IsArray
        {
            get
            {
                return TypeClass == CTypeClass.CArray;
            }
        }

        public bool IsFunctionPointer
        {
            get
            {
                return TypeClass == CTypeClass.CPointer && m_containedType.TypeClass == CTypeClass.CFunction;
            }
        }

        public bool IsStruct
        {
            get
            {
                return TypeClass == CTypeClass.CStruct;
            }
        }

        public bool IsInteger
        {
            get
            {
                return InTypeGroup(CTypeGroups.CInt);
            }
        }

        public CTypeClass TypeClass
        {
            get
            {
                return m_typeClass;
            }
        }

        public bool IsConst
        {
            get
            {
                return m_isConst;
            }
        }

        public string TypeName
        {
            get
            {
                return m_typeName;
            }
        }

        public int ArraySize
        {
            get
            {
                return m_arraySize;
            }
        }

        public CType ContainedType
        {
            get
            {
                return m_containedType;
            }
        }

        public IReadOnlyList<CIdentifier> EnumConstants
        {
            get
            {
                return m_enumConstants;
            }
        }

        public IReadOnlyList<CIdentifier> StructMembers
        {
            get
            {
                return m_structMembers;
            }
        }

        public CType FunctionReturnType
        {
            get
            {
                return m_functionReturnType;
            }
        }
        
        public IReadOnlyList<CType> FunctionArgumentTypes
        {
            get
            {
                return m_functionArgumentTypes;
            }
        }

        public override bool Equals(object obj)
        {
            CType type = (obj as CType);

            if(type == null)
            {
                return false;
            }

            bool equ = true;
            equ = equ && TypeClass == type.TypeClass;
            equ = equ && IsConst == type.IsConst;
            equ = equ && TypeName.Equals(type.TypeName);
            equ = equ && ArraySize == type.ArraySize;
            equ = equ && ContainedType ==  type.ContainedType;
            equ = equ && (EnumConstants == type.EnumConstants || (EnumConstants != null && type.EnumConstants != null && Enumerable.SequenceEqual(EnumConstants, type.EnumConstants)));
            equ = equ && (StructMembers == type.StructMembers || (StructMembers != null && type.StructMembers != null && Enumerable.SequenceEqual(StructMembers, type.StructMembers)));
            equ = equ && FunctionReturnType == type.FunctionReturnType;
            equ = equ && (FunctionArgumentTypes == type.FunctionArgumentTypes || (FunctionArgumentTypes != null && type.FunctionArgumentTypes != null && Enumerable.SequenceEqual(FunctionArgumentTypes, type.FunctionArgumentTypes)));
            return equ;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private readonly CTypeClass m_typeClass;
        private readonly bool m_isConst;
        private readonly string m_typeName;
        private readonly int m_arraySize;
        private readonly CType m_containedType;
        private readonly IReadOnlyList<CIdentifier> m_enumConstants;
        private IReadOnlyList<CIdentifier> m_structMembers;
        private bool m_structComplete = false;
        private readonly CType m_functionReturnType;
        private readonly IReadOnlyList<CType> m_functionArgumentTypes;

        private static Dictionary<string, CType> types = new Dictionary<string, CType>();
        private static Dictionary<string, string> typeDefs = new Dictionary<string, string>();

        public bool HasConstMember
        {
            get
            {
                if(TypeClass != CTypeClass.CStruct)
                {
                    return false;
                }
                else
                {
                    return m_structMembers.Any(member => member.Type.IsConst || member.Type.HasConstMember);
                }
            }
        }

        public bool TypeInArithmeticRange(CType type)
        {
            if(IsUnsigned == type.IsUnsigned)
            {
                return Size >= type.Size;
            }
            else
            {
                if(IsUnsigned)
                {
                    return false;
                }
                else
                {
                    return Size > type.Size;
                }
            }
        }

        public bool IsVoidPointer
        {
            get
            {
                return IsPointer && m_containedType.TypeClass == CTypeClass.CVoid;
            }
        }

        public bool IsUnsigned
        {
            get
            {
                switch (TypeClass)
                {
                    case CTypeClass.CUChar:
                    case CTypeClass.CUShortInt:
                    case CTypeClass.CUInt:
                    case CTypeClass.CULongInt:
                    case CTypeClass.CULongLongInt:
                        return true;
                    default:
                        return false;
                }
            }
        }

        internal static CType DeclareStruct(string idString)
        {
            throw new NotImplementedException();
        }

        internal static CType DeclareEnum(string idString)
        {
            throw new NotImplementedException();
        }

        internal static void BeginDefineStruct()
        {
            throw new NotImplementedException();
        }

        internal static void EndDefineStruct(string idString)
        {
            throw new NotImplementedException();
        }

        internal static void DefineEnum(List<CEnumerator> enums)
        {
            throw new NotImplementedException();
        }
    }
}
