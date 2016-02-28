using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    public partial class AtlasCodeGen : IEmitter
    {
        public string Emit()
        {
            return Emmiters.Reverse().Select(Emiter => Emiter.Emit()).Aggregate((a,b) => a + b);
        }
        
        private void CodeGenError(string s)
        {
            throw new CompilerExcepion("code gen error : " + s);
        }

        private int num_gen_labels = 0;

        private string AutoGenerateLabel(string hint)
        {
            return "__AutoGenerateLabel" + num_gen_labels++ + "_" + hint;
        }

        private string AutoGenerateLabel()
        {
            return AutoGenerateLabel("");
        }

        private Stack<IEmitter> Emmiters = new Stack<IEmitter>(); 
    }
}
