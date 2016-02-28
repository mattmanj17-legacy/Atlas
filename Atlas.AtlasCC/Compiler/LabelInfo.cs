using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC
{
    public class CVariable
    {
        public CVariable(CType type, int offset, CType parentType)
        {
            this.type = type;
            this.offset = offset;
            ismember = true;
            islocal = false;
            name = "";
        }

        public CVariable(CType type, int offset)
        {
            this.type = type;
            this.offset = offset;
            ismember = false;
            islocal = true;
            name = "";
        }

        public CVariable(CType type, string name)
        {
            this.type = type;
            this.name = name;
            ismember = false;
            islocal = false;
            this.name = name;
        }
        
        public override string ToString()
        {
            return name;
        }

        public bool IsMember
        {
            get
            {
                return ismember;
            }
        }

        public bool IsLocal
        {
            get
            {
                return islocal;
            }
        }

        public int Offset
        {
            get
            {
                return offset;
            }
        }

        public CType Type
        {
            get
            {
                return type;
            }
        }

        private readonly int offset;
        private readonly bool ismember;
        private readonly bool islocal;
        private readonly CType type;
        private readonly string name;
    }
}
