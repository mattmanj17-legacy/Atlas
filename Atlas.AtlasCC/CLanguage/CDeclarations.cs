using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    public partial class AtlasCodeGen
    {
        private void InitDeclarations()
        {
            CreateVariable("x", CIdentifier.Global(CType.FromTypeClass(CTypeClass.CInt), "x"));
            CreateVariable("true", CIdentifier.Global(CType.FromTypeClass(CTypeClass.CBool), "true"));
            CreateVariable("false", CIdentifier.Global(CType.FromTypeClass(CTypeClass.CBool), "false"));
            
            throw new NotImplementedException();
        }

        CType CTypeFromName(string name)
        {
            return types[ResolveTypeDef(name)];
        }

        private CIdentifier MemberFromName(string name, CType type)
        {
            return type.StructMembers.First(member => member.ToString().Equals(name));
        }

        private CIdentifier IdentifierFromNameInScope(string name)
        {
            //todo handle scope
            if (variables.ContainsKey(name))
            {
                return variables[name];
            }
            else
            {
                return null;
            }
        }

        //todo handle scope
        private void CreateVariable(string name, CIdentifier label)
        {
            variables[name] = label;
        }

        private string ResolveTypeDef(string name)
        {
            if (types.ContainsKey(name)) return name;
            else if (!typeDefs.ContainsKey(name)) throw new CompilerExcepion("type " + name + " is undefined");
            else return ResolveTypeDef(typeDefs[name]);
        }

        public void TypeDef(string typeDefName, CType type)
        {
            if (typeDefs.ContainsKey(typeDefName))
            {
                throw new CompilerExcepion("Cannot typedef previosly typedefed name");
            }

            typeDefs[typeDefName] = type.TypeName;
        }

        private Dictionary<string, CIdentifier> variables = new Dictionary<string, CIdentifier>();
        private Dictionary<string, CType> types = new Dictionary<string, CType>();
        private Dictionary<string, string> typeDefs = new Dictionary<string, string>();
    }
}
