﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.AtlasCC
{
    public enum FundamentalType
    {
        uint32
    }
    
    public class CTypeInfo
    {
        public static CTypeInfo FromFundamentalType(FundamentalType type)
        {
            return new CTypeInfo(type, false);
        }

        public CTypeInfo(FundamentalType type, bool pointer)
        {
            ftype = type;
            isPointer = pointer;
        }

        private FundamentalType ftype;
        bool isPointer;
        
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public int SizeOf
        {
            get
            {
                return 4;
            }
        }

        public CTypeInfo GetPointerType()
        {
            return new CTypeInfo(ftype, true);
        }

        public bool IsPointer
        {
            get
            {
                return isPointer;
            }
        }

        public CTypeInfo TypePointedTo
        {
            get
            {
                return new CTypeInfo(ftype, false);
            }
        }

        //todo handel arithmetic promotion in operators
        public CTypeInfo ToSigned()
        {
            //donothing
            return this;
        }

        public bool CompatableWith(CTypeInfo cTypeInfo)
        {
            return true;
        }

        public bool CanImplicentlyConvertTo(CTypeInfo cTypeInfo)
        {
            return true;
        }

        public bool IsNaturalNumber
        {
            get
            {
                return true;
            }
        }

        public bool IsNumber
        {
            get
            {
                return true;
            }
        }

        public bool HasMembers
        {
            get
            {
                return false;
            }
        }

        internal bool HasMember(LabelInfo label)
        {
            return false;
        }

        internal int GetMemberOffset(LabelInfo label)
        {
            throw new NotImplementedException();
        }

        internal CTypeInfo GetMemberType(LabelInfo label)
        {
            throw new NotImplementedException();
        }

        public bool IsFunction
        {
            get
            {
                return false;
            }
        }

        internal bool CheckFuncArguments(List<ExpressionInfo> args)
        {
            throw new NotImplementedException();
        }

        public CTypeInfo ReturnType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        internal LabelInfo GetMemberByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
