using Atlas.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC.Emitters
{
    public class OpCodeEmitter : IEmitter
    {
        //fix it so i don thave to use this hack of passing a string
        public OpCodeEmitter(OpCode code, string arg = "")
        {
            m_code = code;
            m_arg = arg == "" ? "" : " " + arg;
        }

        private readonly OpCode m_code;
        private readonly string m_arg;

        public string Emit()
        {
            return m_code.ToString() + m_arg + "\n";
        }
    }
}
