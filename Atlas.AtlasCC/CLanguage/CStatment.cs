using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    public class CStatment : EmitterList
    {
        public static void LabeledStatement(string label)
        {
            throw new NotImplementedException();
        }

        public static void CaseStatement()
        {
            throw new NotImplementedException();
        }

        public static void DefaultStatement()
        {
            throw new NotImplementedException();
        }

        public static void CompoundStatement(int numStatements)
        {
            throw new NotImplementedException();
        }

        public static void ExpressionStatement()
        {
            throw new NotImplementedException();
        }

        public static void IfStatement(bool hasElse)
        {
            throw new NotImplementedException();
        }

        public static void SwitchStatement()
        {
            throw new NotImplementedException();
        }

        public static void WhileLoopStatement()
        {
            throw new NotImplementedException();
        }

        public static void DoWhileLoopStatement()
        {
            throw new NotImplementedException();
        }

        public static void ForLoopWithDeclStatement(bool hasConditionExpression, bool hasUpdateExpression)
        {
            throw new NotImplementedException();
        }

        public static void ForLoopStatement(bool hasInitExpression, bool hasConditionExpression, bool hasUpdateExpression)
        {
            throw new NotImplementedException();
        }

        public static void GotoStatement(string label)
        {
            throw new NotImplementedException();
        }

        public static void ContinueStatement()
        {
            throw new NotImplementedException();
        }

        public static void BreakStatement()
        {
            throw new NotImplementedException();
        }

        public static void ReturnStatement()
        {
            throw new NotImplementedException();
        }

        private static CStatment PopStatement()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CStatment> Statements
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private static void PushStatement()
        {
            throw new NotImplementedException();
        }
    }
}
