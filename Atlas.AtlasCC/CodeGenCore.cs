using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    public partial class AtlasCodeGen
    {
        public Action<string> CodeGenErrorHandler;

        private void CodeGenError(string s)
        {
            if (CodeGenErrorHandler != null)
            {
                CodeGenErrorHandler("codegen error : " + s);
            }
            else
            {
                throw new CompilerExcepion("codegen error handler not set! (this is an internal error, that means its not your fault)");
            }
        }
        
        //todo handle exceptions being thrown from code gen 
        public string GetText()
        {
            return text.ToString();
        }

        private void Emit(string s)
        {
            if (writeToDefered)
            {
                getfunctionName.Append(s);
            }
            else
            {
                text.Append(s);
            }
        }

        private void EmitLine(string s)
        {
            if (writeToDefered)
            {
                getfunctionName.AppendLine(s);
            }
            else
            {
                text.AppendLine(s);
            }
        }

        private StringBuilder text = new StringBuilder();

        bool writeToDefered = false;
        StringBuilder getfunctionName = new StringBuilder();
    }
}
