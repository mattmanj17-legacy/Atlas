using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC
{
    //TODO this is ugly and messy. fix it
    public class CIdentifier
    {
        public static CIdentifier Global(CType cType, string p)
        {
            throw new NotImplementedException();
        }
        
        public CIdentifier()
        {
            throw new NotImplementedException();
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

        public int StackPointerOffset
        {
            get
            {
                return offset;
            }
        }

        public int StructOffset
        {
            get
            {
                throw new NotImplementedException();
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
