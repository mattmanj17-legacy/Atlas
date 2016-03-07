using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC.CLanguage
{
    public class VariableEmitter : IEmitter
    {
        public VariableEmitter(string name, int size)
        {
            string sizeString = "";

            switch(size)
            {
                case 1:
                    sizeString = "BYTE";
                    break;
                case 2:
                    sizeString = "HALF";
                    break;
                case 4:
                    sizeString = "WORD";
                    break;
                default:
                    throw new InvalidOperationException("un rocognized size");
            }

            m_label = name + " : " + sizeString + " 0 \n";
        }

        private readonly string m_label;

        public string Emit()
        {
            return m_label;
        }
    }
}
