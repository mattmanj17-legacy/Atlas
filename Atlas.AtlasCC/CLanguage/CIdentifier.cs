using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atlas.AtlasCC.CLanguage;
using Atlas.Architecture;

namespace Atlas.AtlasCC
{
    public class CIdentifier : IEmitter
    {
        public static IEnumerable<CIdentifier> FileScopeIdentifiers
        {
            get
            {
                return fileScope.Values;
            }
        }

        private static Dictionary<string, CIdentifier> fileScope = new Dictionary<string, CIdentifier>();

        private static Dictionary<string, CIdentifier> currentFunctionScope = null;

        private static List<Dictionary<string, CIdentifier>> BlockScopes = new List<Dictionary<string, CIdentifier>>();
        
        public static CIdentifier IdentifierFromNameInCurrentScope(string name)
        {
            List<Dictionary<string, CIdentifier>> revScopes = BlockScopes.ToList();
            revScopes.Reverse();
            foreach(var scope in revScopes)
            {
                if(scope.ContainsKey(name))
                {
                    return scope[name];
                }
            }

            if(currentFunctionScope.ContainsKey(name))
            {
                return currentFunctionScope[name];
            }

            if(fileScope.ContainsKey(name))
            {
                return fileScope[name];
            }

            throw new SemanticException("identifier " + name + " not found in this scope");
        }

        private static List<CParamater> funcParams = new List<CParamater>();

        //todo handle scope
        public static CIdentifier CreateIdentifierInCurrentScope(string name, CType cType, CStorageClass StorageClass = CStorageClass.Ignore, CConstType ConstType = CConstType.Ignore)
        {
            if(CType.InStructDefinition)
            {
                //member variable
                CIdentifier ident = new CIdentifier(cType, name, true, false, 0, CType.MemberOffset(cType.Size));
                CType.GlobalStructMembers.Add(ident);
                return ident;
            }
            else if(BlockScopes.Count > 0)
            {
                if (BlockScopes[BlockScopes.Count - 1].ContainsKey(name))
                {
                    throw new SemanticException("idenitier " + name + " is already defined in this scope");
                }
                
                //local var
                CIdentifier ident = new CIdentifier(cType, name, false, true, CStatment.PushLocal(cType.Size), 0);
                BlockScopes[BlockScopes.Count - 1][name] = ident;
                return ident;
            }
            else
            {
                if(fileScope.ContainsKey(name))
                {
                    throw new SemanticException("idenitier " + name + " is already defined in this scope");
                }
                
                //global var
                CIdentifier ident = new CIdentifier(cType, name, false, false, 0, 0);
                fileScope[name] = ident;
                return ident;
            }
        }

        public CInitilizer Init
        {
            get
            {
                return m_init;
            }
            set
            {
                if(type.IsArray && value.IsList)
                {
                    m_init = value;
                }
                else if(value.Expression.CanConvertImplicitlyToType(type))
                {
                    m_init = value;
                }
                else
                {
                    throw new SemanticException("initilizer is of incorrect type");
                }
            }
        }

        private CInitilizer m_init;
        
        //TODO clean this up
        //genertic ident
        private CIdentifier(CType type, string name, bool isMember, bool isLocal, int stackOffset, int structOffset)
        {
            this.type = type;
            this.name = name;
            this.isMember = isMember;
            this.islocal = isLocal;
            this.stackPointerOffset = stackOffset;
            this.structOffset = structOffset;
            
            if(!type.IsStruct && !type.IsArray && type.TypeClass != CTypeClass.CFunction && type.TypeClass != CTypeClass.CVoid)
            {
                CExpression.PushConstant("0");
                m_init = new CInitilizer(CExpression.PopExpression());
            }
        }

        //function
        public CIdentifier(CType signature, string name, CStatment cStatment)
        {
            //TODO check return type of function
            //TODO (big job) check for "not all paths return a value"
            
            this.name = name;
            this.type = signature;
            this.functionBody = cStatment;
        }

        //label
        public CIdentifier(string label)
        {
            this.islabel = true;
            this.name = AutoGenerateLabel(label);
        }

        //string
        public CIdentifier(string name, string stringValue)
        {
            this.name = name;
            this.stringValue = stringValue;

            this.type = CType.PointerTo(CType.FromTypeClass(CTypeClass.CChar));
            this.isMember = false;
            this.islocal = false;
            this.stackPointerOffset = 0;
            this.structOffset = 0;
        }
        
        public string Name
        {
            get
            {
                return name;
            }
        }

        public bool IsMember
        {
            get
            {
                return isMember;
            }
        }

        public bool IsLocal
        {
            get
            {
                return islocal;
            }
        }

        public int StackPointerOffset
        {
            get
            {
                return stackPointerOffset;
            }
        }

        public int StructOffset
        {
            get
            {
                return structOffset;
            }
        }

        public CType Type
        {
            get
            {
                return type;
            }
        }

        private static int num_gen_labels = 0;

        public static string AutoGenerateLabel(string hint)
        {
            return "__AutoGenerateLabel_" + num_gen_labels++ + "_" + hint;
        }

        public static string AutoGenerateLabel()
        {
            return AutoGenerateLabel("");
        }

        private readonly int stackPointerOffset;
        private readonly int structOffset;
        private readonly bool isMember;
        private readonly bool islocal;
        private readonly bool islabel;
        private readonly CType type;
        private readonly string name;
        private CStatment functionBody;
        private string stringValue;

        //private static Dictionary<string, CIdentifier> identifiers = new Dictionary<string, CIdentifier>();

        private string stringFromSize(int size)
        {
            switch(size)
            {
                case 1:
                    return "BYTE";
                case 2:
                    return "HALF";
                case 4:
                    return "WORD";
                default:
                    throw new InvalidOperationException("unrecgognized size");
            }
        }
        
        private string GetDefinitionName()
        {
            if(islocal)
            {
                throw new InvalidOperationException("local variables to not have names");
            }
            else if(isMember)
            {
                return AutoGenerateLabel("StructMember");
            }
            else
            {
                return name;
            }
        }

        public string Emit()
        {
            if(islocal)
            {
                throw new InvalidOperationException("cannot emit local Variable");
            }
            else if(stringValue != null)
            {
                return new StringEmitter(name, stringValue).Emit();
            }
            else if(islabel)
            {
                return new LabelEmitter(name).Emit();
            }
            else
            {
                EmitterList list = new EmitterList();
                
                if(type.TypeClass == CTypeClass.CVoid)
                {
                    //CVoid
                    throw new InvalidOperationException("cannot emit variable of void type");
                }
                else if(type.TypeClass == CTypeClass.CFunction)
                {
                    //CFunction
                    list.Add(new LabelEmitter(name));
                    list.Add(functionBody);
                }
                else if (type.TypeClass == CTypeClass.CFloat || type.TypeClass == CTypeClass.CDouble || type.TypeClass == CTypeClass.CLongDouble)
                {
                    throw new InvalidOperationException("cannot emit variable of floating type");
                }
                else if(type.IsStruct)
                {
                    //CStruct
                    list.Add(new LabelEmitter(GetDefinitionName()));
                    foreach(CIdentifier ident in type.StructMembers)
                    {
                        list.Add(ident);
                    }
                }
                else if(type.IsArray)
                {
                    //CArray
                    throw new NotSupportedException("array not implimented yet");
                }
                else
                {
                   /*
                    CChar,
                    CSChar,
                    CUChar,
                    CShortInt,
                    CUShortInt,
                    CInt,
                    CUInt,
                    CLongInt,
                    CULongInt,
                    CLongLongInt,
                    CULongLongInt, 
                    CEnum,
                    CPointer
                  */

                   list.Add(new VariableEmitter(GetDefinitionName(), type.Size));
                }

                return list.Emit();
            }
        }

        internal static void EnterFunctionScope()
        {
            currentFunctionScope = new Dictionary<string, CIdentifier>();
            BlockScopes.Add(new Dictionary<string, CIdentifier>());
        }

        internal static void ExitFunctionScope()
        {
            currentFunctionScope = null;

            CleanBlock(BlockScopes[BlockScopes.Count - 1]);

            BlockScopes.RemoveAt(BlockScopes.Count - 1);
        }

        private static void CleanBlock(Dictionary<string, CIdentifier> dictionary)
        {
            foreach(CIdentifier ident in dictionary.Values)
            {
                CStatment.PopLocal(ident.type.Size);
            }
        }

        internal static void DefineFunction(CType signature, string name, CStorageClass cStorageClass)
        {
            //cStorageClass (static, extern, or none)
            //ignore for now

            if(currentFunctionScope != null || BlockScopes.Count > 0)
            {
                throw new SemanticException("can only define function at file scope");
            }
            else
            {
                if (fileScope.ContainsKey(name))
                {
                    if (fileScope[name].functionBody == null && fileScope[name].type.Equals(signature))
                     {
                         fileScope[name].functionBody = CStatment.PopStatement();
                     }
                     else
                     {
                         throw new SemanticException("canot redifein function");
                     }
                }
                else
                {
                    fileScope[name] = new CIdentifier(signature, name, CStatment.PopStatement());
                }
            }
        }

        internal static void EnterFunctionPrototypeScope()
        {
             //do nothing
        }

        internal static void ExitFunctionPrototypeScope()
        {
            if (currentFunctionScope != null)
            {
                foreach(var param in funcParams)
                {
                    CreateIdentifierInCurrentScope(param.name, param.pType);
                }
            }

            funcParams.Clear();
        }

        internal static CParamater CreateFunctionParameter(string name, CType type)
        {
            CParamater param = new CParamater(name, type);
            funcParams.Add(param);
            return param;
        }

        internal static CIdentifier CreatLabel(string label)
        {
            CIdentifier lb = new CIdentifier(label);
            if(currentFunctionScope != null)
            {
                if (currentFunctionScope.ContainsKey(label))
                {
                    throw new SemanticException("label " + label + " already exists in this scope");
                }

                currentFunctionScope[label] = lb;
            }
            else
            {
                if(fileScope.ContainsKey(label))
                {
                    throw new SemanticException("label " + label + " already exists in this scope");
                }
                
                fileScope[label] = lb;
            }
            return lb;
        }

        internal static void EnterBlockScope()
        {
            BlockScopes.Add(new Dictionary<string, CIdentifier>());
        }

        internal static void ExitBlockScope()
        {
            CleanBlock(BlockScopes[BlockScopes.Count - 1]);
            BlockScopes.RemoveAt(BlockScopes.Count - 1);
        }

        internal CStatment GetDefinitionStatment()
        {
            if(Init == null)
            {
                return new CStatment();
            }

            if(type.IsStruct)
            {
                throw new SemanticException("local variables of type struct are not implimentd");
            }
            else if (type.IsArray || Init.IsList)
            {
                throw new SemanticException("local variables of type array are not implimentd");
            }
            else if(type.TypeClass == CTypeClass.CFunction)
            {
                throw new InvalidOperationException("wtf how did you declare a local function varable");
            }
            else if(type.TypeClass == CTypeClass.CVoid)
            {
                throw new InvalidOperationException("wtf how did you declare a local void varable");
            }
            else
            {
                OpCode PushOp;
 
                if(type.Size == 1)
                {
                    PushOp = OpCode.PUSHB;
                }
                else if (type.Size == 2)
                {
                    PushOp = OpCode.PUSHH;
                }
                else if (type.Size == 4)
                {
                    PushOp = OpCode.PUSHW;
                }
                else
                {
                    throw new InvalidOperationException("local variable of unknown size");
                }

                CStatment initilize = new CStatment();
                initilize.Add(new OpCodeEmitter(PushOp, "0"));

                CExpression localAddr = new CExpression(type,ValueCatagory.LValue);

                localAddr.Add(new OpCodeEmitter(OpCode.PUSHBP));
                localAddr.Add(new OpCodeEmitter(OpCode.PUSHW, stackPointerOffset.ToString()));
                localAddr.Add(new OpCodeEmitter(OpCode.ADD));

                CExpression.PushExpression(localAddr);
                CExpression.PushExpression(Init.Expression);

                CExpression.BasicAssignmentOperator();

                initilize.Add(new CStatment(CExpression.PopExpression()));

                return initilize;
            }
        }

        internal static CIdentifier InternString(string p)
        {
            string name = AutoGenerateLabel("internedString");

            var id = new CIdentifier(name, p);
            fileScope[name] = id;
            return id;
        }
    }
}
