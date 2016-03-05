using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC
{
    public class CIdentifier : IEmitter
    {
        public static CIdentifier IdentifierFromNameInCurrentScope(string name)
        {
            //todo handle scope
            if (identifiers.ContainsKey(name))
            {
                return identifiers[name];
            }
            else
            {
                return null;
            }
        }

        //todo handle scope
        public static CIdentifier CreateIdentifierInCurrentScope(string name, CType cType, CStorageClass StorageClass = CStorageClass.Ignore, CConstType ConstType = CConstType.Ignore)
        {
            throw new NotImplementedException();
        }

        public void Define(CInitilizer cInitilizer)
        {
            throw new NotImplementedException();
        }
        
        static CIdentifier()
        {
            //hack so we have some variables to play with
            CreateIdentifierInCurrentScope("x32", CType.FromTypeClass(CTypeClass.CInt));
            CreateIdentifierInCurrentScope("x16", CType.FromTypeClass(CTypeClass.CShortInt));
            CreateIdentifierInCurrentScope("xu16", CType.FromTypeClass(CTypeClass.CUShortInt));
            CreateIdentifierInCurrentScope("x8", CType.FromTypeClass(CTypeClass.CChar));
            CreateIdentifierInCurrentScope("xu8", CType.FromTypeClass(CTypeClass.CUChar));
        }
        
        //clean this up
        private CIdentifier(CType type, string name, bool isMember, bool isLocal, int stackOffset, int structOffset)
        {
            this.type = type;
            this.name = name;
            this.ismember = isMember;
            this.islocal = isLocal;
            this.stackPointerOffset = stackOffset;
            this.structOffset = structOffset;
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
                return ismember;
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
            return "__AutoGenerateLabel" + num_gen_labels++ + "_" + hint;
        }

        public static string AutoGenerateLabel()
        {
            return AutoGenerateLabel("");
        }

        private readonly int stackPointerOffset;
        private readonly int structOffset;
        private readonly bool ismember;
        private readonly bool islocal;
        private readonly CType type;
        private readonly string name;

        private static Dictionary<string, CIdentifier> identifiers = new Dictionary<string, CIdentifier>();

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
        
        public string Emit()
        {
            return name + " : " + stringFromSize(type.Size) + " 0\n";
        }

        public static IEnumerable<CIdentifier> StaticIdentifiers
        {
            get
            {
                return identifiers.Values;
            }
        }

        internal static void EnterFunctionScope()
        {
            throw new NotImplementedException();
        }

        internal static void ExitFunctionScope()
        {
            throw new NotImplementedException();
        }

        internal static void DefineFunction(CType cType, string p, CStorageClass cStorageClass)
        {
            throw new NotImplementedException();
        }

        internal static void EnterFunctionPrototypeScope()
        {
            throw new NotImplementedException();
        }

        internal static void ExitFunctionPrototypeScope()
        {
            throw new NotImplementedException();
        }

        internal static CParamater CreateFunctionParameter(string p, CType cType, CStorageClass cStorageClass, CConstType cConstType)
        {
            throw new NotImplementedException();
        }

        internal static CIdentifier CreatLabelInFunctionScope(string label)
        {
            throw new NotImplementedException();
        }

        internal static void EnterBlockScope()
        {
            throw new NotImplementedException();
        }

        internal static void ExitBlockScope()
        {
            throw new NotImplementedException();
        }
    }
}
