using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC.CLanguage
{
    public class StringEmitter : IEmitter
    {
        private string name;
        private string stringValue;

        public StringEmitter(string name, string stringValue)
        {
            // TODO: Complete member initialization
            this.name = name;
            this.stringValue = stringValue;
        }
        public string Emit()
        {
            return name + " : " + stringValue + "\n";
        }
    }
}
