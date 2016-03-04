using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
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
            throw new NotImplementedException();
        }

        // Case label in a switch statement.
        // only allowed in case statment
        // see http://en.cppreference.com/w/c/language/switch
        public static void CaseStatement()
        {
            //case constant_expression : statement
            throw new NotImplementedException();
        }

        // Default label in a switch statement.
        // only allowed in case statment
        // see http://en.cppreference.com/w/c/language/switch
        public static void DefaultStatement()
        {
            //default : statement
            throw new NotImplementedException();
        }

        //A compound statement, or block, is a brace-enclosed sequence of statements and declarations.
        //The compound statement allows a set of declarations and statements to be grouped into one unit that can be used 
        // anywhere a single statement is expected (for example, in an if statement or an iteration statement)
        //Each compound statement introduces its own block scope.
        //http://en.cppreference.com/w/c/language/scope
        public static void BeginCompoundStatement()
        {
            throw new NotImplementedException();
        }

        public static void EndCompoundStatement(int numStatements)
        {
            throw new NotImplementedException();
        }

        //An expression followed by a semicolon is a statement.
        public static void ExpressionStatement()
        {
            throw new NotImplementedException();
        }

        //http://en.cppreference.com/w/c/language/if
        public static void IfStatement(bool hasElse)
        {
            //expression must be an expression of any scalar type.
            //If expression compares not equal to the integer zero, statement_true is executed.
            //In the else form, if expression compares equal to the integer zero, statement_false is executed.
            
            //if ( expression ) statement_true
            if(!hasElse)
            {

            }
            //if ( expression ) statement_true else statement_false	
            else
            {

            }

            throw new NotImplementedException();
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
        public static void BeginSwitchStatement()
        {
            throw new NotImplementedException();
        }

        public static void EndSwitchStatement()
        {
            throw new NotImplementedException();
        }

        //http://en.cppreference.com/w/c/language/while
        //Executes a statement repeatedly, until the value of expression becomes equal to zero. The test takes place before each iteration.
        // while ( expression ) statement	
        // expression must be of scalar type
        //As with all other selection and iteration statements, the while statement establishes block scope: 
        // any identifier introduced in the expression goes out of scope after the statement.
        public static void BeginWhileLoopStatement()
        {
            throw new NotImplementedException();
        }

        public static void EndWhileLoopStatement()
        {
            throw new NotImplementedException();
        }

        //http://en.cppreference.com/w/c/language/do
        //Executes a statement repeatedly until the value of condition becomes false. The test takes place after each iteration.
        //do statement while ( expression ) ;
        // expression must be of scalar type
        //As with all other selection and iteration statements, the dowhile statement establishes block scope: 
        // any identifier introduced in the expression goes out of scope after the statement.
        public static void BeginDoWhileLoopStatement()
        {
            throw new NotImplementedException();
        }

        public static void EndDoWhileLoopStatement()
        {
            throw new NotImplementedException();
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
         *  init_claus if decl
         *  while(cond_expression)
         *  {
         *      loop_statement
         *      iteration_expression;
         *  }
         * }
         */
        public static void BeginForLoopWithDeclStatement()
        {
            throw new NotImplementedException();
        }

        public static void EndForLoopWithDeclStatement(bool hasConditionExpression, bool hasUpdateExpression)
        {
            throw new NotImplementedException();
        }

        public static void BeginForLoopStatement()
        {
            throw new NotImplementedException();
        }

        public static void EndForLoopStatement(bool hasInitExpression, bool hasConditionExpression, bool hasUpdateExpression)
        {
            throw new NotImplementedException();
        }

        //http://en.cppreference.com/w/c/language/goto
        // Transfers control unconditionally to the desired location defined by a label.
        // goto label ;
        public static void GotoStatement(string label)
        {
            throw new NotImplementedException();
        }

        //http://en.cppreference.com/w/c/language/continue
        //Causes the remaining portion of the enclosing for, while or do-while loop body to be skipped.
        //can only be used in a loop
        //The continue statement causes a jump, as if by goto
        public static void ContinueStatement()
        {
            throw new NotImplementedException();
        }

        //http://en.cppreference.com/w/c/language/break
        //Causes the enclosing for, while or do-while loop or switch statement to terminate.
        //Appears only within the statement of a loop body (while, do, for) or within the statement of a switch.
        //After this statement the control is transferred to the statement or declaration immediately following the enclosing loop or switch, as if by goto.
        //A break statement cannot be used to break out of multiple nested loops. The goto statement may be used for this purpose.
        public static void BreakStatement()
        {
            throw new NotImplementedException();
        }

        //http://en.cppreference.com/w/c/language/return
        //Terminates current function and returns specified value to the caller function.
        //If the type of the expression is different from the return type of the function, its value is converted as if by assignment to an object whose type is the return type of the function (must be possible to convert)
        //Reaching the end of a function other than main is equivalent to return;
        //Reaching the end of the main function is equivalent to return 0;
        // see http://en.cppreference.com/w/c/language/main_function
        public static void ReturnStatement()
        {
            throw new NotImplementedException();
        }

        public static CStatment PopStatement()
        {
            throw new NotImplementedException();
        }

        private static void PushStatement()
        {
            throw new NotImplementedException();
        }
    }
}
