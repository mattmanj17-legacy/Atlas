using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.IO;

//I used alot of documentation from this site
//http://en.cppreference.com/w/c/language
//there are links to parts of this site through out the code

namespace Atlas.AtlasCC
{
    public class SemanticException : Exception
    {
        public SemanticException(string msg) : base(msg) { }
    }

    public class AtlasCCompilerFrontEnd : CBaseListener, IAntlrErrorListener<IToken>
    {
        //TOP LEVEL INTERFACE

        //by default errors and warnings are logged to stdout
        public AtlasCCompilerFrontEnd() : this(Console.Out) { }

        //point the assembler output (errors and warnings) to a custom locations
        public AtlasCCompilerFrontEnd(TextWriter outStream)
        {
            m_outStream = outStream;
        }

        public string Compile(ICharStream cSource)
        {
            CLexer lexer = new CLexer(cSource);
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CParser parser = new CParser(tokens);
            parser.RemoveErrorListeners();

            parser.AddErrorListener(this);

            try
            {
                CParser.CompilationUnitContext compilationUnit = parser.compilationUnit();
                ParseTreeWalker walker = new ParseTreeWalker();

                walker.Walk(this, compilationUnit);

                string exprs = "";
                foreach (CExpression expr in CExpression.Expressions)
                {
                    exprs += expr.Emit();
                }

                foreach (CIdentifier ident in CIdentifier.StaticIdentifiers)
                {
                    exprs += ident.Emit();
                }

                return exprs;
            }
            catch (SemanticException e)
            {
                m_outStream.WriteLine("Compilation Failed: " + e.Message);
                return null;
            }
        }

        //ERROR HANDELING

        public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            m_outStream.Write("Error on line " + line + ": ");
            m_outStream.WriteLine("Syntax Error at " + line + ":" + charPositionInLine);
            m_outStream.WriteLine(msg);

            throw new SemanticException("Syntax Error");
        }

        private void SematicError(ParserRuleContext ctx, String msg)
        {
            m_outStream.WriteLine("Error on line " + ctx.start.Line + ": \n" + msg);
            m_outStream.WriteLine("Offending Code: ");
            m_outStream.WriteLine("\b\"" + ctx.GetText() + "\"");
            throw new SemanticException("Sematic Error");
        }

        private void SafeCall(ParserRuleContext rule, Action func)
        {
            try
            {
                func();
            }
            catch (SemanticException e)
            {
                SematicError(rule, e.Message);
            }
        }

        //destination for errors and warnings
        private readonly TextWriter m_outStream;

        //DECLARATIONS

        public override void EnterFunctionDefinition(CParser.FunctionDefinitionContext context)
        {
            CDeclaration.BeginFunctionDefinition();
        }

        public override void ExitFunctionDefinition(CParser.FunctionDefinitionContext context)
        {
            //declarationSpecifiers? declarator declarationList? compoundStatement
            CDeclaration.EndFunctionDefinition(GetSpecifierCount(context.declarationSpecifiers()));
        }

        private int GetSpecifierCount(CParser.DeclarationSpecifiersContext specifiers)
        {
            if (specifiers == null)
            {
                return 0;
            }
            else
            {
                return specifiers.declarationSpecifier().Count;
            }
        }

        private int GetDeclarationListCount(CParser.DeclarationListContext declList)
        {
            if (declList == null)
            {
                return 0;
            }
            else if (declList.declarationList() == null)
            {
                return 1;
            }
            else
            {
                return GetDeclarationListCount(declList.declarationList()) + 1;
            }
        }

        private int GetDeclarationListCount(CParser.StructDeclarationListContext declList)
        {
            if (declList == null)
            {
                return 0;
            }
            else if (declList.structDeclarationList() == null)
            {
                return 1;
            }
            else
            {
                return GetDeclarationListCount(declList.structDeclarationList()) + 1;
            }
        }

        public override void ExitDeclaration(CParser.DeclarationContext context)
        {
            //declarationSpecifiers initDeclaratorList? ';'
            if (context.declarationSpecifiers() != null)
            {
                CDeclaration.Declaration(GetSpecifierCount(context.declarationSpecifiers()), GetDeclaratorListCount(context.initDeclaratorList()));
            }
            //staticAssertDeclaration
            else
            {
                SematicError(context, "staticAssert not supported");
            }
        }

        private int GetDeclaratorListCount(CParser.InitDeclaratorListContext decltorList)
        {
            if (decltorList == null)
            {
                return 0;
            }
            else if (decltorList.initDeclaratorList() == null)
            {
                return 1;
            }
            else
            {
                return GetDeclaratorListCount(decltorList.initDeclaratorList()) + 1;
            }
        }

        //declarationSpecifiers

        public override void ExitStorageClassSpecifier(CParser.StorageClassSpecifierContext context)
        {
            CDeclaration.PushStorageClassSpecifier(context.GetText());
        }

        public override void EnterTypeSpecifier(CParser.TypeSpecifierContext context)
        {
            //beginstructdef
            throw new NotImplementedException();
        }

        public override void ExitTypeSpecifier(CParser.TypeSpecifierContext context)
        {
            //atomicTypeSpecifier
            if (context.atomicTypeSpecifier() != null)
            {
                SematicError(context, "atomic types not supported");
            }
            //structOrUnionSpecifier
            else if (context.structOrUnionSpecifier() != null)
            {
                if (context.structOrUnionSpecifier().structOrUnion().GetText() == "union")
                {
                    SematicError(context, "unions not supported");
                }
                else if (context.structOrUnionSpecifier().structDeclarationList() != null)
                {
                    string idstring = context.structOrUnionSpecifier().Identifier() != null ? context.structOrUnionSpecifier().Identifier().GetText() : null;
                    CDeclaration.EndStructDefinition(idstring);
                }
                else
                {
                    CDeclaration.PushStructDeclaration(context.structOrUnionSpecifier().Identifier().GetText());
                }
            }
            //enumSpecifier
            else if (context.enumSpecifier() != null)
            {
                if (context.enumSpecifier().enumeratorList() != null)
                {
                    string idString = context.enumSpecifier().Identifier() != null ? context.enumSpecifier().Identifier().GetText() : null;
                    CDeclaration.EnumDefinition(idString, GetEnumeratorListCount(context.enumSpecifier().enumeratorList()));
                }
                else
                {
                    CDeclaration.PushEnumDeclaration(context.enumSpecifier().Identifier().GetText());
                }
            }
            //'__typeof__' '(' constantExpression ')' --- GCC extension not supported
            else if (context.constantExpression() != null)
            {
                SematicError(context, "gcc typeof not supported");
            }
            //else, a fundamental type
            else
            {
                CDeclaration.PushTypeSpecifier(context.GetText());
            }
        }

        public override void ExitEnumerator(CParser.EnumeratorContext context)
        {
            CDeclaration.PushEnumerator(context.enumerationConstant().Identifier().GetText(), context.constantExpression() != null);
        }

        public override void ExitTypeQualifier(CParser.TypeQualifierContext context)
        {
            CDeclaration.PushTypeQualifier(context.GetText());
        }

        public override void ExitFunctionSpecifier(CParser.FunctionSpecifierContext context)
        {
            SematicError(context, "function specifier not supported");
        }

        public override void ExitAlignmentSpecifier(CParser.AlignmentSpecifierContext context)
        {
            SematicError(context, "Alignas not supported");
        }

        private int GetEnumeratorListCount(CParser.EnumeratorListContext enumList)
        {
            if (enumList == null)
            {
                return 0;
            }
            else if (enumList.enumeratorList() == null)
            {
                return 1;
            }
            else
            {
                return GetEnumeratorListCount(enumList.enumeratorList()) + 1;
            }
        }

        //declarators

        public override void EnterDeclarator(CParser.DeclaratorContext context)
        {
            //beginFunctionDeclarator 
            throw new NotImplementedException();
        }

        public override void ExitDeclarator(CParser.DeclaratorContext context)
        {
            if (context.gccDeclaratorExtension().Count > 0)
            {
                throw new SemanticException("gcc Declarator Extensions not supported");
            }

            bool isPointer = context.pointer() != null;

            CParser.DirectDeclaratorContext directDecltor = context.directDeclarator();

            if (directDecltor.Identifier() != null)
            {
                CDeclaration.PushIdentifierDeclarator(isPointer, directDecltor.GetText());
            }
            else if (directDecltor.declarator() != null)
            {
                CDeclaration.NestedDeclarator(isPointer);
            }
            else if (directDecltor.GetText().EndsWith("*]"))
            {
                SematicError(context, "VLA not supported");
            }
            else if (directDecltor.GetText().EndsWith(")"))
            {
                if (directDecltor.identifierList() != null)
                {
                    SematicError(context, "old style (K&R) style functions not supported");
                }
                else
                {
                    CDeclaration.EndFunctionDeclarator(isPointer, GetParameterTypeListCount(directDecltor.parameterTypeList()));
                }
            }
            else
            {
                bool hasAssgnExpr = directDecltor.assignmentExpression() != null;
                int numTypeQualifiers = GetTypeQualifierListCount(directDecltor.typeQualifierList());
                CDeclaration.ArrayDeclarator(isPointer, numTypeQualifiers, hasAssgnExpr);
            }

        }

        private int GetParameterTypeListCount(CParser.ParameterTypeListContext parameterTypeListContext)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<string> GetIdentifiersFromList(CParser.IdentifierListContext identifierListContext)
        {
            throw new NotImplementedException();
        }

        private int GetTypeQualifierListCount(CParser.TypeQualifierListContext typeQualifierListContext)
        {
            throw new NotImplementedException();
        }

        public override void ExitPointer(CParser.PointerContext context)
        {
            if (context.GetText().StartsWith("^"))
            {
                SematicError(context, "blocks extention not supported");
            }
            else
            {
                CDeclaration.PushPointerModifier(GetTypeQualifierListCount(context.typeQualifierList()), context.pointer() != null);
            }
        }

        public override void ExitParameterDeclaration(CParser.ParameterDeclarationContext context)
        {
            if (context.declarationSpecifiers2() != null)
            {
                SematicError(context, "abstract parameter declaration not supported (???????)");
            }
            else
            {
                int numSpecifiers = context.declarationSpecifiers().declarationSpecifier().Count;
                CDeclaration.ParamaterDeclaration(numSpecifiers);
            }
        }

        public override void ExitInitDeclarator(CParser.InitDeclaratorContext context)
        {
            if (context.initializer() != null)
            {
                CDeclaration.DeclaratorWithInitilizer();
            }
        }

        public override void ExitInitializer(CParser.InitializerContext context)
        {
            if (context.assignmentExpression() != null)
            {
                CDeclaration.Initilizer();
            }
            else
            {
                CDeclaration.InitilizerList(GetInitializerListCount(context.initializerList()));
            }
        }

        private int GetInitializerListCount(CParser.InitializerListContext initializerListContext)
        {
            throw new NotImplementedException();
        }

        //STATEMENTS
        //http://en.cppreference.com/w/c/language/statements

        public override void ExitLabeledStatement(CParser.LabeledStatementContext context)
        {
            //Identifier ':' statement
            if (context.Identifier() != null)
            {
                SafeCall(context, () => CStatment.LabeledStatement(context.Identifier().GetText()));
            }
            //'case' constantExpression ':' statement
            else if (context.constantExpression() != null)
            {
                SafeCall(context, CStatment.CaseStatement);
            }
            //'default' ':' statement
            else
            {
                SafeCall(context, CStatment.DefaultStatement);
            }
        }

        public override void EnterCompoundStatement(CParser.CompoundStatementContext context)
        {
            SafeCall(context, CStatment.BeginCompoundStatement);
        }

        public override void ExitCompoundStatement(CParser.CompoundStatementContext context)
        {
            //'{' blockItemList? '}'
            SafeCall(context, () => CStatment.EndCompoundStatement(GetBlockItemTypes(context.blockItemList())));
        }

        private List<CCompoundStatmentItemType> GetBlockItemTypes(CParser.BlockItemListContext itmeList)
        {
            throw new NotImplementedException();
        }

        public override void ExitExpressionStatement(CParser.ExpressionStatementContext context)
        {
            //expression? ';'
            if (context.expression() != null)
            {
                SafeCall(context, CStatment.ExpressionStatement);
            }
        }

        public override void EnterSelectionStatement(CParser.SelectionStatementContext context)
        {
            //beginswitch
            throw new NotImplementedException();
        }

        public override void ExitSelectionStatement(CParser.SelectionStatementContext context)
        {
            //'if' '(' expression ')' statement ('else' statement)?
            if (context.GetText().StartsWith("if"))
            {
                SafeCall(context, () => CStatment.IfStatement(context.statement(1) != null));
            }
            //'switch' '(' expression ')' statement
            else
            {
                SafeCall(context, CStatment.EndSwitchStatement);
            }
        }

        public override void EnterIterationStatement(CParser.IterationStatementContext context)
        {
            //beginwhile
            //begindowhile
            //begin for
            throw new NotImplementedException();
        }

        public override void ExitIterationStatement(CParser.IterationStatementContext context)
        {
            //'while' '(' expression ')' statement
            if (context.GetText().StartsWith("while"))
            {
                SafeCall(context, CStatment.EndWhileLoopStatement);
            }
            //'do' statement 'while' '(' expression ')' ';'
            else if (context.GetText().StartsWith("do"))
            {
                SafeCall(context, CStatment.EndDoWhileLoopStatement);
            }
            else
            {
                //'for' '(' declaration expression? ';' expression? ')' statement
                if (context.declaration() != null)
                {
                    SafeCall(context, () => CStatment.EndForLoopStatement(CForInitType.Decl, context.expression(0) != null, context.expression(1) != null));
                }
                //'for' '(' expression? ';' expression? ';' expression? ')' statement
                else
                {
                    SafeCall(context, () => CStatment.EndForLoopStatement(context.expression(0) != null ? CForInitType.Expression : CForInitType.None, context.expression(1) != null, context.expression(2) != null));
                }
            }
        }

        public override void ExitJumpStatement(CParser.JumpStatementContext context)
        {
            //'goto' Identifier ';'
            if (context.Identifier() != null)
            {
                SafeCall(context, () => CStatment.GotoStatement(context.Identifier().GetText()));
            }
            //'continue' ';'
            if (context.GetText().StartsWith("continue"))
            {
                SafeCall(context, CStatment.ContinueStatement);
            }
            //'break' ';'
            if (context.GetText().StartsWith("break"))
            {
                SafeCall(context, CStatment.BreakStatement);
            }
            //'return' expression? ';'
            if (context.GetText().StartsWith("return"))
            {
                SafeCall(context, () => CStatment.ReturnStatement(context.expression() != null));
            }
            //'goto' unaryExpression ';' -- GCC extension not supported
            else
            {
                SematicError(context, "gcc goto extention not supported");
            }
        }


        //EXPRESSIONS
        public override void ExitExpression(CParser.ExpressionContext context)
        {
            if (context.expression() != null)
            {
                // expression ',' assignmentExpression
                SafeCall(context, CExpression.CommaOperator);
            }
        }

        public override void ExitConstantExpression(CParser.ConstantExpressionContext context)
        {
            //TODO constant expression optimization
            //constant expressions of floating-point types are always evaluated during run-time
            /*
             * address constant expression
                a null pointer
                lvalue designating an object of static storage duration or a function designator, converted to a pointer either
                    by using the unary address-of operator
                    by casting an integer constant to a pointer
                    by array-to-pointer or function-to-pointer implicet conversion
             */
            //Integer constant expressions are evaluated at compile time
            throw new NotSupportedException("constant expressions not implimented yet");
        }

        public override void ExitAssignmentExpression(CParser.AssignmentExpressionContext context)
        {
            if (context.assignmentExpression() != null)
            {
                // unaryExpression assignmentOperator assignmentExpression

                /*
                    assignmentOperator
                        :   '=' | '*=' | '/=' | '%=' | '+=' | '-=' | '<<=' | '>>=' | '&=' | '^=' | '|='
                        ;
                 */

                string operation = context.assignmentOperator().GetText();

                if (operation == "=")
                {
                    SafeCall(context, CExpression.BasicAssignmentOperator);
                }
                else if (operation == "*=")
                {
                    SafeCall(context, CExpression.MultiplicationAssignmentOperator);
                }
                else if (operation == "/=")
                {
                    SafeCall(context, CExpression.DivisionAssignmentOperator);
                }
                else if (operation == "%=")
                {
                    SafeCall(context, CExpression.ModuloAssignmentOperator);
                }
                else if (operation == "+=")
                {
                    SafeCall(context, CExpression.AdditionAssignmentOperator);
                }
                else if (operation == "-=")
                {
                    SafeCall(context, CExpression.SubtractionAssignmentOperator);
                }
                else if (operation == "<<=")
                {
                    SafeCall(context, CExpression.BitwiseLeftShiftAssignmentOperator);
                }
                else if (operation == ">>=")
                {
                    SafeCall(context, CExpression.BitwiseRightShiftAssignmentOperator);
                }
                else if (operation == "&=")
                {
                    SafeCall(context, CExpression.BitwiseAndAssignmentOperator);
                }
                else if (operation == "^=")
                {
                    SafeCall(context, CExpression.BitwiseXorAssignmentOperator);
                }
                else if (operation == "|=")
                {
                    SafeCall(context, CExpression.BitwiseOrAssignmentOperator);
                }
            }
        }

        public override void ExitConditionalExpression(CParser.ConditionalExpressionContext context)
        {
            if (context.conditionalExpression() != null)
            {
                // logicalOrExpression ('?' expression ':' conditionalExpression)?

                SafeCall(context, CExpression.CoditionalOperator);
            }
        }

        public override void ExitLogicalOrExpression(CParser.LogicalOrExpressionContext context)
        {
            if (context.logicalOrExpression() != null)
            {
                // logicalOrExpression '||' logicalAndExpression
                SafeCall(context, CExpression.LogicalOrOperator);
            }
        }

        public override void ExitLogicalAndExpression(CParser.LogicalAndExpressionContext context)
        {
            if (context.logicalAndExpression() != null)
            {
                // logicalAndExpression '&&' inclusiveOrExpression
                SafeCall(context, CExpression.LogicalAndOperator);
            }
        }

        public override void ExitInclusiveOrExpression(CParser.InclusiveOrExpressionContext context)
        {
            if (context.inclusiveOrExpression() != null)
            {
                // inclusiveOrExpression '|' exclusiveOrExpression
                SafeCall(context, CExpression.BitwiseOrOperator);
            }
        }

        public override void ExitExclusiveOrExpression(CParser.ExclusiveOrExpressionContext context)
        {
            if (context.exclusiveOrExpression() != null)
            {
                // exclusiveOrExpression '^' andExpression
                SafeCall(context, CExpression.BitwiseXorOperator);
            }
        }

        public override void ExitAndExpression(CParser.AndExpressionContext context)
        {
            if (context.andExpression() != null)
            {
                // andExpression '&' equalityExpression
                SafeCall(context, CExpression.BitwiseAndOperator);
            }
        }

        public override void ExitEqualityExpression(CParser.EqualityExpressionContext context)
        {
            if (context.equalityExpression() != null)
            {
                string operation = context.GetText().Substring(context.equalityExpression().GetText().Length, 2);

                if (operation == "==")
                {
                    //equalityExpression '==' relationalExpression
                    SafeCall(context, CExpression.EqualToOperator);
                }
                else
                {
                    //equalityExpression '!=' relationalExpression
                    SafeCall(context, CExpression.NotEqualToOperator);
                }
            }
        }

        public override void ExitRelationalExpression(CParser.RelationalExpressionContext context)
        {
            if (context.relationalExpression() != null)
            {
                int contextLength = context.GetText().Length;
                int lhsLength = context.relationalExpression().GetText().Length;
                int rhsLength = context.shiftExpression().GetText().Length;

                string operation = context.GetText().Substring(lhsLength, contextLength - (lhsLength + rhsLength));

                if (operation == "<")
                {
                    //relationalExpression '<' shiftExpression
                    SafeCall(context, CExpression.LessThanOperator);
                }
                else if (operation == ">")
                {
                    //relationalExpression '>' shiftExpression
                    SafeCall(context, CExpression.GreaterThanOperator);
                }
                else if (operation == "<=")
                {
                    //relationalExpression '<=' shiftExpression
                    SafeCall(context, CExpression.LessThanOrEqualOperator);
                }
                else if (operation == ">=")
                {
                    //relationalExpression '>=' shiftExpression
                    SafeCall(context, CExpression.GreaterThanOrEqualOperator);
                }
            }
        }

        public override void ExitShiftExpression(CParser.ShiftExpressionContext context)
        {
            if (context.shiftExpression() != null)
            {
                string operation = context.GetText().Substring(context.shiftExpression().GetText().Length, 2);

                if (operation == "<<")
                {
                    //shiftExpression '<<' additiveExpression
                    SafeCall(context, CExpression.BitwiseLeftShiftOperator);
                }
                else if (operation == ">>")
                {
                    //shiftExpression '>>' additiveExpression
                    SafeCall(context, CExpression.BitwiseRightShiftOperator);
                }
            }
        }

        public override void ExitAdditiveExpression(CParser.AdditiveExpressionContext context)
        {
            if (context.additiveExpression() != null)
            {
                string operation = context.GetText().Substring(context.additiveExpression().GetText().Length, 1);

                if (operation == "+")
                {
                    //additiveExpression '+' multiplicativeExpression
                    SafeCall(context, CExpression.AdditionOperator);
                }
                else if (operation == "-")
                {
                    //additiveExpression '-' multiplicativeExpression
                    SafeCall(context, CExpression.SubtractionOperator);
                }
            }
        }

        public override void ExitMultiplicativeExpression(CParser.MultiplicativeExpressionContext context)
        {
            if (context.multiplicativeExpression() != null)
            {
                string operation = context.GetText().Substring(context.multiplicativeExpression().GetText().Length, 1);

                if (operation == "*")
                {
                    //multiplicativeExpression '*' castExpression
                    SafeCall(context, CExpression.MultiplicationOperator);
                }
                else if (operation == "/")
                {
                    //multiplicativeExpression '/' castExpression
                    SafeCall(context, CExpression.DivisionOperator);
                }
                else if (operation == "%")
                {
                    //multiplicativeExpression '%' castExpression
                    SafeCall(context, CExpression.ModuloOperator);
                }
            }
        }

        public override void ExitCastExpression(CParser.CastExpressionContext context)
        {
            if (context.typeName() != null)
            {
                //'(' typeName ')' castExpression
                SafeCall(context, () => CExpression.TypeCast(context.typeName().GetText()));
            }
        }

        public override void ExitUnaryExpression(CParser.UnaryExpressionContext context)
        {
            if (context.unaryOperator() != null)
            {
                // unaryOperator castExpression

                /*
                   unaryOperator
                    :   '&' | '*' | '+' | '-' | '~' | '!'
                    ;
                 */

                string operation = context.unaryOperator().GetText();

                if (operation == "&")
                {
                    SafeCall(context, CExpression.AddressOfOperator);
                }
                else if (operation == "*")
                {
                    SafeCall(context, CExpression.DereferenceOperator);
                }
                else if (operation == "+")
                {
                    SafeCall(context, CExpression.UnaryPlusOperator);
                }
                else if (operation == "-")
                {
                    SafeCall(context, CExpression.UnaryMinusOperator);
                }
                else if (operation == "~")
                {
                    SafeCall(context, CExpression.BitwiseNotOperator);
                }
                else if (operation == "!")
                {
                    SafeCall(context, CExpression.LogicalNotOperator);
                }
            }
            else if (context.GetText().StartsWith("++"))
            {
                //'++' unaryExpression
                SafeCall(context, CExpression.PrefixIncrementOperator);
            }
            else if (context.GetText().StartsWith("--"))
            {
                //'--' unaryExpression
                SafeCall(context, CExpression.PrefixDecrementOperator);
            }
            else if (context.GetText().StartsWith("sizeof"))
            {
                if (context.unaryExpression() != null)
                {
                    //'sizeof' unaryExpression
                    SafeCall(context, CExpression.SizeOf);
                }
                else
                {
                    //'sizeof' '(' typeName ')'
                    SafeCall(context, () => CExpression.SizeOf(context.typeName().GetText()));
                }
            }
            else if (context.Identifier() != null)
            {
                //'&&' Identifier
                SematicError(context, "GCC extension && not supported");
            }
            else if (context.GetText().StartsWith("_Alignof"))
            {
                //'_Alignof' '(' typeName ')'
                SematicError(context, "alignof not supported");
            }
        }

        public override void ExitPostfixExpression(CParser.PostfixExpressionContext context)
        {
            if (context.expression() != null)
            {
                //postfixExpression '[' expression ']'
                SafeCall(context, CExpression.SubscriptOperator);
            }
            else if (context.postfixExpression() != null && context.GetText().EndsWith(")"))
            {
                //postfixExpression '(' argumentExpressionList? ')'
                int numArgs = GetArgumentListLength(context.argumentExpressionList());
                SafeCall(context, () => CExpression.FunctionCall(numArgs));
            }
            else if (context.Identifier() != null)
            {
                int objStringLength = context.postfixExpression().GetText().Length;
                int idStringLength = context.Identifier().GetText().Length;
                int contextStringSize = context.GetText().Length;

                int operationStringSize = contextStringSize - (objStringLength + idStringLength);

                string idString = context.Identifier().GetText();

                //'.'
                if (operationStringSize == 1)
                {
                    //postfixExpression '.' Identifier
                    SafeCall(context, () => CExpression.MemberAccess(idString));
                }
                //'->'
                else
                {
                    //postfixExpression '->' Identifier
                    SafeCall(context, () => CExpression.MemberAccessThroughPointer(idString));
                }
            }
            else if (context.GetText().EndsWith("++"))
            {
                //postfixExpression '++'
                SafeCall(context, CExpression.PostfixIncrementOperator);
            }
            else if (context.GetText().EndsWith("--"))
            {
                //postfixExpression '--'
                SafeCall(context, CExpression.PostfixDecrementOperator);
            }
            else if (context.initializerList() != null)
            {
                /*
                        '(' typeName ')' '{' initializerList '}'
                    |   '(' typeName ')' '{' initializerList ',' '}'
                    |   '__extension__' '(' typeName ')' '{' initializerList '}'
                    |   '__extension__' '(' typeName ')' '{' initializerList ',' '}'
                 */
                SematicError(context, "compound literals not supported");
            }
        }

        public override void ExitPrimaryExpression(CParser.PrimaryExpressionContext context)
        {
            if (context.Identifier() != null)
            {
                //Identifier

                SafeCall(context, () => CExpression.PushIdentifier(context.Identifier().GetText()));
            }
            else if (context.Constant() != null)
            {
                //Constant
                SafeCall(context, () => CExpression.PushConstant(context.Constant().GetText()));
            }
            else if (context.StringLiteral().Count > 0)
            {
                //StringLiteral+
                var literals = context.StringLiteral().Select(lit => lit.GetText());
                SafeCall(context, () => CExpression.PushString(literals));
            }
            else if (context.compoundStatement() != null)
            {
                //'__extension__'? '(' compoundStatement ')'
                SematicError(context, "GCC blocks extention not supported");
            }
            else if (context.GetText().Contains("__builtin_va_arg"))
            {
                //'__builtin_va_arg' '(' unaryExpression ',' typeName ')'
                SematicError(context, "var args not supported");
            }
            else if (context.genericSelection() != null)
            {
                //genericSelection
                SematicError(context, "generic selection not supported");
            }
            else if (context.GetText().StartsWith("__builtin_offsetof"))
            {
                //'__builtin_offsetof' '(' typeName ',' unaryExpression ')'
                SematicError(context, "offsetof not supported");
            }
        }

        private int GetArgumentListLength(CParser.ArgumentExpressionListContext list)
        {
            if (list == null)
            {
                return 0;
            }

            if (list.argumentExpressionList() != null)
            {
                return GetArgumentListLength(list.argumentExpressionList()) + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}
