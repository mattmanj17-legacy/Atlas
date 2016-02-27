using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    public class EmitterList : List<IEmitter>, IEmitter
    {
        public string Emit()
        {
            return this.Select(emitter => emitter.Emit()).Aggregate((a, b) => a + b);
        }
    }
}
