using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    partial class AtlasCCompiler : CBaseListener, IAntlrErrorListener<IToken>
    {   
        CTypeInfo CTypeFromName(string name)
        {
            throw new NotImplementedException();
        }

        private LabelInfo LabelInfoFromName(string name, CTypeInfo type)
        {
            return type.GetMemberByName(name);
        }

        private LabelInfo LabelInfoFromName(string name)
        {
            if(variables.ContainsKey(name))
            {
                return variables[name];
            }
            else
            {
                return null;
            }
        }

        private void CreatVariable(string name, LabelInfo label)
        {
            variables[name] = label;
        }

        private Dictionary<string, LabelInfo> variables = new Dictionary<string,LabelInfo>();
    }
}
