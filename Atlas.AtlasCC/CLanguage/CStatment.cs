using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    public enum CCompoundStatmentItemType
    {
        Statment,
        Declaration
    }

    public enum CForInitType
    {
        Expression,
        Decl,
        None
    }

    //http://en.cppreference.com/w/c/language/statements
    //Statements are fragments of the C program that are executed in sequence. The body of any function is a compound statement, which, in turn is a sequence of statements and declarations:
    public class CStatment : EmitterList
    {
        //Any statement can be labeled, by providing a name followed by a colon before the statement itself.
        // labeled statment is target for goto
        // Labels are the only identifiers that have function scope: 
        // they can be used (in a goto statement) anywhere in the same function in which they appear. There may be multiple labels before any statement.
        // see http://en.cppreference.com/w/c/language/goto
        public static void LabeledStatement(string label)
        {
            //identifier : statement
            PushStatement(PopStatement().AddLabel(label));
        }

        private CStatment AddLabel(string label)
        {
            CIdentifier.CreatLabelInFunctionScope(label);
            labels.Add(label);
            return this;
        }

        private List<string> labels = new List<string>();
        private List<CStatment> blockBody;
        private CExpression cExpression;
        private CStatment cStatment;
        private CStatment cStatment1;
        private CStatment cStatment2;
        private CIdentifier id;
        private bool p;

        public CStatment(List<CStatment> blockBody)
        {
            // TODO: Complete member initialization
            this.blockBody = blockBody;
            throw new NotImplementedException();
        }

        public CStatment(CExpression cExpression)
        {
            // TODO: Complete member initialization
            this.cExpression = cExpression;
        }

        public CStatment(CExpression cExpression, CStatment cStatment)
        {
            // TODO: Complete member initialization
            this.cExpression = cExpression;
            this.cStatment = cStatment;
        }

        public CStatment(CExpression cExpression, CStatment cStatment1, CStatment cStatment2)
        {
            // TODO: Complete member initialization
            this.cExpression = cExpression;
            this.cStatment1 = cStatment1;
            this.cStatment2 = cStatment2;
        }

        public CStatment(CIdentifier id)
        {
            // TODO: Complete member initialization
            this.id = id;
        }

        public CStatment(CExpression cExpression, bool p)
        {
            // TODO: Complete member initialization
            this.cExpression = cExpression;
            this.p = p;
        }

        public CStatment()
        {
            // TODO: Complete member initialization
        }

        // Case label in a switch statement.
        // only allowed in case statment
        // see http://en.cppreference.com/w/c/language/switch
        public static void CaseStatement()
        {
            //case constant_expression : statement
            //PushStatement(PopStatement().MakeCaseStatment(CExpression.PopExpression()));
            throw new NotSupportedException("switch not implimented");
        }

        // Default label in a switch statement.
        // only allowed in case statment
        // see http://en.cppreference.com/w/c/language/switch
        public static void DefaultStatement()
        {
            //default : statement
            //PushStatement(PopStatement().MakeDefaultStatment());
            throw new NotSupportedException("switch not implimented");
        }

        //A compound statement, or block, is a brace-enclosed sequence of statements and declarations.
        //The compound statement allows a set of declarations and statements to be grouped into one unit that can be used 
        // anywhere a single statement is expected (for example, in an if statement or an iteration statement)
        //Each compound statement introduces its own block scope.
        //http://en.cppreference.com/w/c/language/scope
        public static void BeginCompoundStatement()
        {
            CIdentifier.EnterBlockScope();
        }

        public static void EndCompoundStatement(List<CCompoundStatmentItemType> itemTypes)
        {
            CIdentifier.ExitBlockScope();

            List<CStatment> blockBody = new List<CStatment>();

            //the item types come in sequential order (top to bottom), but they are sitting on the stack in reverse order (botom to top)
            //revers the list to deal with this
            itemTypes.Reverse();
            foreach(var itemType in itemTypes)
            {
                if(itemType == CCompoundStatmentItemType.Statment)
                {
                    CStatment stat = PopStatement();
                    blockBody.Add(stat);
                }
                else
                {
                    CDeclaration decl = CDeclaration.PopDecl();

                    if(decl.IsDefinition)
                    {
                        //get a statments that initilizes the local variable
                        blockBody.Add(decl.GetDefinitionStatment());
                    }
                }
            }

            //put the statments back in top to bottom order
            blockBody.Reverse();

            PushStatement(new CStatment(blockBody));
        }

        //An expression followed by a semicolon is a statement.
        public static void ExpressionStatement()
        {
            PushStatement(new CStatment(CExpression.PopExpression()));
        }

        //http://en.cppreference.com/w/c/language/if
        public static void IfStatement(bool hasElse)
        {
            //expression must be an expression of any scalar type.
            //If expression compares not equal to the integer zero, statement_true is executed.
            //In the else form, if expression compares equal to the integer zero, statement_false is executed.

            //if ( expression ) statement_true
            if (!hasElse)
            {
                PushStatement(new CStatment(CExpression.PopExpression(), PopStatement()));
            }
            //if ( expression ) statement_true else statement_false	
            else
            {
                PushStatement(new CStatment(CExpression.PopExpression(), PopStatement(), PopStatement()));
            }
        }

        //Executes code according to the value of an integral argument.
        //http://en.cppreference.com/w/c/language/switch
        /* The body of a switch statement may have an arbitrary number of case: labels, as long as the values of all constant_expressions are unique (after conversion to the promoted type of expression). At most one default: label may be present (although nested switch statements may use their own default: labels or have case: labels whose constants are identical to the ones used in the enclosing switch).
         * If expression evaluates to the value that is equal to the value of one of constant_expressions after conversion to the promoted type of expression, then control is transferred to the statement that is labeled with that constant_expression.
         * If expression evaluates to a value that doesn't match any of the case: labels, and the default: label is present, control is transferred to the statement labeled with the default: label.
         * If expression evaluates to a value that doesn't match any of the case: labels, and the default: label is not present, none of the switch body is executed.
         * The break statement, when encountered anywhere in statement, exits the switch statement:
         */
        //switch ( expression ) statement
        // expression must be of integral type
        //break can be used in this statement
        //IMPLIMENTATION NOTE: right now, switch statments just tun into a bunch of if/else if's. i know there are optimized way to do this,
        //but optimization is not a priority of atlas right now
        public static void BeginSwitchStatement()
        {
            throw new NotSupportedException("switch not implimented yet");
        }

        public static void EndSwitchStatement()
        {
            throw new NotSupportedException("switch not implimented yet");
        }

        //http://en.cppreference.com/w/c/language/while
        //Executes a statement repeatedly, until the value of expression becomes equal to zero. The test takes place before each iteration.
        // while ( expression ) statement	
        // expression must be of scalar type
        //As with all other selection and iteration statements, the while statement establishes block scope: 
        // any identifier introduced in the expression goes out of scope after the statement.

        /*
         * {
         *      loopCheck:  if(expression) goto loopBody;
         *                  goto endloop:
         *      loopBody:   statment;
         *                  goto loopCheck;
         *      endLoop:    {};
         * }
         */

        private static Stack<CIdentifier> loopChechLabels = new Stack<CIdentifier>();
        private static Stack<CIdentifier> loopBodyLabels = new Stack<CIdentifier>();
        private static Stack<CIdentifier> endLoopLabels = new Stack<CIdentifier>();

        private static void EnterLoop()
        {
            loopChechLabels.Push(CIdentifier.CreatLabelInFunctionScope(CIdentifier.AutoGenerateLabel("LoopCheck")));
            loopBodyLabels.Push(CIdentifier.CreatLabelInFunctionScope(CIdentifier.AutoGenerateLabel("LoopBody")));
            endLoopLabels.Push(CIdentifier.CreatLabelInFunctionScope(CIdentifier.AutoGenerateLabel("EndLoop")));
        }

        private static void ExitLoop()
        {
            loopChechLabels.Pop();
            loopBodyLabels.Pop();
            endLoopLabels.Pop();
        }
        
        public static void BeginWhileLoopStatement()
        {
            BeginCompoundStatement();
            EnterLoop();
        }

        public static void EndWhileLoopStatement()
        {
            //loopCheck:  if(expression) goto loopBody;
            if(!CExpression.PeekExpression().IsScalar)
            {
                throw new SemanticException("condition expression in loop must be scalar");
            }

            CStatment gotoBody = new CStatment(loopBodyLabels.Peek());

            PushStatement(gotoBody);
            IfStatement(false);

            CStatment ifCheckGotoBody = PopStatement();
            ifCheckGotoBody.AddLabel(loopChechLabels.Peek().Name);

            //goto endloop:
            CStatment gotoEnd = new CStatment(endLoopLabels.Peek());

            //loopBody:   statment;
            CStatment loopBody = PopStatement();
            loopBody.AddLabel(loopBodyLabels.Peek().Name);

            //goto loopCheck;
            CStatment gotoCheck = new CStatment(loopChechLabels.Peek());

            //endLoop:    {};
            CStatment end = new CStatment();
            end.AddLabel(endLoopLabels.Peek().Name);

            PushStatement(ifCheckGotoBody);
            PushStatement(gotoEnd);
            PushStatement(loopBody);
            PushStatement(gotoCheck);
            PushStatement(end);

            ExitLoop();
            EndCompoundStatement(new List<CCompoundStatmentItemType> 
                { 
                    CCompoundStatmentItemType.Statment,
                    CCompoundStatmentItemType.Statment,
                    CCompoundStatmentItemType.Statment,
                    CCompoundStatmentItemType.Statment,
                    CCompoundStatmentItemType.Statment
                }
            );
        }

        //http://en.cppreference.com/w/c/language/do
        //Executes a statement repeatedly until the value of condition becomes false. The test takes place after each iteration.
        //do statement while ( expression ) ;
        // expression must be of scalar type
        //As with all other selection and iteration statements, the dowhile statement establishes block scope: 
        // any identifier introduced in the expression goes out of scope after the statement.

        /*
        * {
        *      loopBody:    statment;
        *      loopCheck:   if(expression) goto loopBody;
        *      endLoop:     {};
        * }
        */
        public static void BeginDoWhileLoopStatement()
        {
            BeginCompoundStatement();
            EnterLoop();
        }

        public static void EndDoWhileLoopStatement()
        {
            //loopBody:   statment;
            CStatment loopBody = PopStatement();
            loopBody.AddLabel(loopBodyLabels.Peek().Name);
            
            //loopCheck:  if(expression) goto loopBody;
            if (!CExpression.PeekExpression().IsScalar)
            {
                throw new SemanticException("condition expression in loop must be scalar");
            }

            CStatment gotoBody = new CStatment(loopBodyLabels.Peek());

            PushStatement(gotoBody);
            IfStatement(false);

            CStatment ifCheckGotoBody = PopStatement();
            ifCheckGotoBody.AddLabel(loopChechLabels.Peek().Name);

            //endLoop:    {};
            CStatment end = new CStatment();
            end.AddLabel(endLoopLabels.Peek().Name);

            PushStatement(loopBody);
            PushStatement(ifCheckGotoBody);
            PushStatement(end);

            ExitLoop();
            EndCompoundStatement(new List<CCompoundStatmentItemType> 
                { 
                    CCompoundStatmentItemType.Statment,
                    CCompoundStatmentItemType.Statment,
                    CCompoundStatmentItemType.Statment
                }
            );
        }

        //http://en.cppreference.com/w/c/language/for
        //Executes a loop. Used as a shorter equivalent of while loop.
        // for ( init_clause ; cond_expression ; iteration_expression ) loop_statement
        /*Behaves as follows:
         * init_clause may be an expression or a declaration
         * If it is an expression, it is evaluated once, before the first evaluation of cond_expression and its result is discarded.
         * If it is a declaration, it is in scope in the entire loop body, 
         * including the remainder of init_clause, the entire cond_expression, the entire iteration_expression and the entire loop_statement. 
         * cond_expression is evaluated before the loop body. If the result of the expression is zero, the loop statement is exited immediately.
         * iteration_expression is evaluated after the loop body and its result is discarded. After evaluating iteration_expression, control is transferred to cond_expression.
         * init_clause, cond_expression, and iteration_expression are all optional:
         * for(;;) {
         *  printf("endless loop!");
         * }
         */
        //equivalent to
        /*
         * {
         *      init_claus
         *      while(cond_expression)
         *      {
         *          loop_statement
         *          iteration_expression;
         *      }
         * }
         */
        public static void BeginForLoopStatement()
        {
            BeginCompoundStatement();
        }

        public static void EndForLoopStatement(CForInitType initType, bool hasConditionExpression, bool hasIterationExpression)
        {
            List<CCompoundStatmentItemType> compondItems = new List<CCompoundStatmentItemType> { CCompoundStatmentItemType.Statment, CCompoundStatmentItemType.Statment };

            CStatment loopBody = PopStatement();

            CStatment iterationStatement = hasIterationExpression ? new CStatment(CExpression.PopExpression()) : new CStatment();
            
            CExpression condition; 
            if(hasConditionExpression)
            {
                condition = CExpression.PopExpression();
            }
            else
            {
                CExpression.PushConstant("1");
                condition = CExpression.PopExpression();
            }

            //init_claus
            
            CStatment initClaus;
            if (initType == CForInitType.Decl)
            {
                initClaus = CDeclaration.PopDecl().GetDefinitionStatment();
            }
            else if (initType == CForInitType.Expression)
            {
                initClaus = new CStatment(CExpression.PopExpression());
            }
            else
            {
                initClaus = new CStatment();
            }

            // contruct loop body
            BeginCompoundStatement();
            PushStatement(loopBody);
            PushStatement(iterationStatement);
            EndCompoundStatement(compondItems);

            CStatment loopbody = PopStatement();
            
            // while(cond_expression) loopbody
            CExpression.PushExpression(condition);
            PushStatement(loopBody);
            BeginWhileLoopStatement();
            EndWhileLoopStatement();

            CStatment whilestat = PopStatement();

            PushStatement(initClaus);
            PushStatement(whilestat);
            
            EndCompoundStatement(compondItems);
        }

        //http://en.cppreference.com/w/c/language/goto
        // Transfers control unconditionally to the desired location defined by a label.
        // goto label ;
        public static void GotoStatement(string label)
        {
            CIdentifier id = CIdentifier.IdentifierFromNameInCurrentScope(label);
            PushStatement(new CStatment(id));
        }

        //http://en.cppreference.com/w/c/language/continue
        //Causes the remaining portion of the enclosing for, while or do-while loop body to be skipped.
        //can only be used in a loop
        //The continue statement causes a jump, as if by goto
        
        //goto loopcheck
        public static void ContinueStatement()
        {
            PushStatement(new CStatment(loopChechLabels.Peek()));
        }

        //http://en.cppreference.com/w/c/language/break
        //Causes the enclosing for, while or do-while loop or switch statement to terminate.
        //Appears only within the statement of a loop body (while, do, for) or within the statement of a switch.
        //After this statement the control is transferred to the statement or declaration immediately following the enclosing loop or switch, as if by goto.
        //A break statement cannot be used to break out of multiple nested loops. The goto statement may be used for this purpose.
        //goto endloop
        public static void BreakStatement()
        {
            PushStatement(new CStatment(endLoopLabels.Peek()));
        }

        //http://en.cppreference.com/w/c/language/return
        //Terminates current function and returns specified value to the caller function.
        //If the type of the expression is different from the return type of the function, its value is converted as if by assignment to an object whose type is the return type of the function (must be possible to convert)
        //Reaching the end of a function other than main is equivalent to return;
        //Reaching the end of the main function is equivalent to return 0;
        // see http://en.cppreference.com/w/c/language/main_function
        public static void ReturnStatement(bool retVal)
        {
            PushStatement(new CStatment(retVal ? CExpression.PopExpression() : null, true));
        }

        public static CStatment PopStatement()
        {
            return statments.Pop();
        }

        private static void PushStatement(CStatment statment)
        {
            statments.Push(statment);
        }

        private static Stack<CStatment> statments = new Stack<CStatment>();
    }
}
