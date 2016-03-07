using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    public class EmitterList : List<IEmitter>, IEmitter
    {
        public virtual string Emit()
        {
            if (Count > 0)
            {
                return this.Select(emitter => emitter.Emit()).Aggregate((a, b) => a + b);
            }
            else
            {
                return "";
            }
        }
    }
}
