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
            throw new NotImplementedException();
        }

        private LabelInfo LabelInfoFromName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
