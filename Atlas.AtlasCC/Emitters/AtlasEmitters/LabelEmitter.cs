using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    public class LabelEmitter : IEmitter
    {
        public LabelEmitter(string label)
        {
            m_label = label;
        }

        private readonly string m_label;

        public string Emit()
        {
            return m_label + ":\n";
        }
    }
}
