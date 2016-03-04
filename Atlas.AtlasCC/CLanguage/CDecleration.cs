using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.AtlasCC
{
    public class CDeclaration
    {
        private class CDeclarationSpecifier { }

        private class CDeclarator { }

        private class CParamater { }

        private class CPointerModifier { }

        private class CEnumerator { }
        
        //storage and linkage
        //http://en.cppreference.com/w/c/language/storage_duration
        /*
         * Every object has a property called storage duration, which limits the object lifetime. There are four kinds of storage duration in C:
         * 
         * automatic storage duration. 
         * The storage is allocated when the block in which the object was declared is entered and 
         * deallocated when it is exited by any means (goto, return, reaching the end). If the block is entered recursively, a new allocation is performed for every recursion level. All function parameters and non-static block-scope objects have this storage duration.
         * 
         * static storage duration. 
         * The storage duration is the entire execution of the program, 
         * and the value stored in the object is initialized only once, prior to main function. 
         * All objects declared static and all objects with either internal or external linkage have this storage duration.
         * 
         * thread storage duration. NOT SUPPORTED
         * 
         * allocated storage duration. 
         * The storage is allocated and deallocated on request, using dynamic memory allocation functions.
         * 
         * Linkage
         * Linkage refers to the ability of an identifier (variable or function) to be referred to in other scopes. 
         * If a variable or function with the same identifier is declared in several scopes, 
         * but cannot be referred to from all of them, then several instances of the variable are generated. 
         * 
         * The following linkages are recognized:
         *  
         * no linkage. 
         * The identifier can be referred to only from the scope it is in. 
         * All function parameters and all non-extern block-scope variables (including the ones declared static) have this linkage.
         *  
         * internal linkage. 
         * The identifier can be referred to from all scopes in the current translation unit. 
         * All static identifiers (both functions and variables) have this linkage.
         * 
         * external linkage. 
         * The identifier can be referred to from any other translation units in the entire program. 
         * All non-static functions, all extern variables (unless earlier declared static), and all file-scope non-static variables have this linkage.
         * 
         * Names at file scope that are const and not extern have external linkage in C
         * 
         * Storage-class specifiers specify one of the following combinations of storage duration and linkage
         * auto - automatic duration and no linkage (on stack)
         * register - NOT SUPPORTED
         * static - static duration and internal linkage (unless at block scope) (in data or rodata segment)
         * extern - static duration and external linkage (unless already declared internal) (on heap)
         * 
         * If no storage-class specifier is provided, the defaults are:
         * extern for all functions
         * extern for objects at file scope
         * auto for objects at block scope
         * 
         * For any struct or union declared with a storage-class specifier, 
         * the storage duration (but not linkage) applies to their members, recursively.
         * 
         * Function declarations at block scope can use extern or none at all. 
         * Function declarations at file scope can use extern or static.
         */

        //scope
        //http://en.cppreference.com/w/c/language/scope
        /*Each identifier that appears in a C program is visible (that is, may be used) only in some possibly discontiguous portion of the source code called its scope.
         * Within a scope, an identifier may designate more than one entity only if the entities are in different name spaces.
         * C has four kinds of scopes:
         *  block scope
         *  file scope
         *  function scope
         *  function prototype scope
         *  
         * Nested scopes
         * If two different entities named by the same identifier are in scope at the same time, 
         * and the scopes are nested,
         * the declaration that appears in the inner scope hides the declaration that appears in the outer scope
         * 
         * Block Scope
         * The scope of any identifier declared inside a compound statement, 
         * including function bodies, 
         * or in any expression, declaration, or statement appearing in 
         * if, switch, for, while, or do-while statement, 
         * or within the parameter list of a function definition 
         * begins at the point of declaration and ends at the end of the block or statement in which it was declared.
         * 
         * File scope
         * The scope of any identifier declared outside of any block or parameter list begins at the point of declaration and ends at the end of the translation unit (file).
         * File-scope identifiers have external linkage and static storage duration by default.
         * 
         * Function scope
         * A label (and only a label) declared inside a function is in scope everywhere in that function, in all nested blocks, before and after its own declaration.
         * 
         * Function prototype scope
         * The scope of a name introduced in the parameter list of a function declaration ends at the end of the function declarator.
         */

        //declarations
        //http://en.cppreference.com/w/c/language/declarations
        //A declaration is a C language construct that introduces one or more identifiers into the program and specifies their meaning and properties.
        //Declarations may appear in any scope. Each declaration ends with a semicolon (just like a statement) and consists of two distinct parts:
        /*
         * specifiers-and-qualifiers	-	whitespace-separated list of, in any order,
         *  exactly one type specifier:
         *      void
         *      the name of an arithmetic type
         *      the name of an atomic type
         *      a name earlier introduced by a typedef declaration
         *      struct, union, or enum specifier
         *  zero or one storage-class specifiers:
         *      typedef, 
         *      auto, 
         *      static, 
         *      extern
         *  optional const
         * 
         *  declarators-and-initializers	-	comma-separated list of declarators 
         *  (each declarator provides additional type information and/or the identifier to declare). 
         *  Declarators may be accompanied by initializers. 
         */
        public static void Declaration(int specifiers, int declarators)
        {
            throw new NotImplementedException();
        }

        //declaration specifiers

        //storage specifers
        //http://en.cppreference.com/w/c/language/storage_duration
        public static void PushStorageClassSpecifier(string specifier)
        {
            throw new NotImplementedException();
        }

        //void, arithmetic type, typedefed name
        public static void PushFundamentalTypeSpecifier(string typeName)
        {
            throw new NotImplementedException();
        }

        //struct decl is a specifier
        public static void PushStructDeclaration(string idString)
        {
            throw new NotImplementedException();
        }

        //enum decl is a specifier
        //the only case where this is not also a definition, is in function prototypes (enum name x), and other weird places. this is one of the cruftyer parts of c
        public static void EnumDeclaration(string idString)
        {
            throw new NotImplementedException();
        }

        //specifies that this is a typedef, not a real decl
        public static void PushTypeDefName(string name)
        {
            throw new NotImplementedException();
        }

        //is this const or not
        public static void PushTypeQualifier(string qualifier)
        {
            throw new NotImplementedException();
        }

        private static CDeclarationSpecifier PopDeclSpec()
        {
            throw new NotImplementedException();
        }

        private static void PushDeclSpec(CDeclarationSpecifier declSpec)
        {
            declSpecs.Push(declSpec);
        }

        private static Stack<CDeclarationSpecifier> declSpecs = new Stack<CDeclarationSpecifier>();

        //definitions

        //A definition is a declaration that provides all information about the identifiers it declares.
        //Every declaration of an enum or a typedef is a definition.
        //For functions, a declaration that includes the function body is a function definition:
        //For objects, a declaration that allocates storage (automatic or static, but not extern) is a definition, 
        //while a declaration that does not allocate storage (external declaration) is not.
        //For structs, declarations that specify the list of members are definitions:
        //declarations which are not definitions can be repeted. declarations which are definitions cannot be repeted

        //http://en.cppreference.com/w/c/language/function_definition
        //Unlike function declaration, function definitions are allowed at file scope only (there are no nested functions).
        // functions are of the form
        // specifiers-and-qualifiers parameter-list-declarator function-body
        /*
         *  specifiers-and-qualifiers	-	a combination of
         *      type specifiers that, possibly modified by the declarator, form the return type
         *      storage class specifiers, which determine the linkage of the identifier (static, extern, or none)
         *  
         *  parameter-list-declarator	-	a declarator for a function type which uses a parameter list to designate function parameters
         *  
         *  function-body	-	a compound statement, that is a brace-enclosed sequence of declarations and statements, that is executed whenever this function is called
         *  
         * return type of function must be a complete non-array object type or the type void.
         */
        //enter function scope
        public static void BeginFunctionDefinition()
        {
            throw new NotImplementedException();
        }
        
        //exit function scope
        public static void EndFunctionDefinition(int numSpecifiers)
        {
            throw new NotImplementedException();
        }

        //see http://en.cppreference.com/w/c/language/struct (i know this is lazy, but a lot of this documentation is in CType.cs)
        //enter members definition
        public static void BeginStructDefinition()
        {
            throw new NotImplementedException();
        }

        //exit members definition
        public static void EndStructDefinition(string idString)
        {
            throw new NotImplementedException();
        }

        //see http://en.cppreference.com/w/c/language/enum (i know this is lazy, but a lot of this documentation is in CType.cs)
        public static void EnumDefinition(string idString, int enumeratorCount)
        {
            throw new NotImplementedException();
        }

        public static void PushEnumerator(string idString, bool hasConstant)
        {
            throw new NotImplementedException();
        }

        private static void PushEnumerator(CEnumerator enumerator)
        {
            throw new NotImplementedException();
        }

        private static CEnumerator PopEnumerator()
        {
            throw new NotImplementedException();
        }

        private static Stack<CEnumerator> enumerators = new Stack<CEnumerator>();

        // declarators

        /*Declarators
         * Each declarator is one of the following:
         *  
         * identifier	(1)	
         *  
         * ( declarator )	(2)	
         * 
         * * qualifiers(optional) declarator	(3)	
         * 
         * noptr-declarator [ static(optional) qualifiers(optional) expression ]
         * noptr-declarator [ qualifiers(optional) * ] (4)	
         * 
         * noptr-declarator ( parameters-or-identifiers )	(5)	
         * 
         * 1) the identifier that this declarator introduces.
         * 
         * 2) any declarator may be enclosed in parentheses; 
         * this is required to introduce pointers to arrays and pointers to functions.
         * 
         * 3) pointer declarator: 
         * the declaration S * cvr D; declares D as a pointer to the type determined by S. (THIS IS SUCH A CRUFYFT PART OF C. WHY THE HELL IS POINTER A PART OF THE DECLARATOR, AND NOT THE TYPE)
         * 
         * 4) array declarator: the declaration S D[N] declares D as an array of N objects of the type determined by S. 
         * noptr-declarator is any other declarator except unparenthesized pointer declarator. (THIS IS SUCH A CRUFYFT PART OF C. WHY THE HELL IS ARRAY A PART OF THE DECLARATOR, AND NOT THE TYPE)
         * 
         * 5) function declarator: the declaration S D(params) declared D as a function taking the parameters params and returning S. 
         * noptr-declarator is any other declarator except unparenthesized pointer declarator.
         * 
         * initilization
         * http://en.cppreference.com/w/c/language/initialization
         * 
         * A declaraton of an object may provide its initial value through the process known as initialization.
         * For each declarator, the initializer, if not omitted, may be one of the following:
         * 
         * = expression	(1)	(scalar initilization)
         * = { initializer-list }	(2) (array initilization)	
         * 
         * where initializer-list is a non-empty comma-separated list of initializers (with an optional trailing comma), 
         * where each initializer has one of two possible forms:
         *  expression	(1)	(value)
         *  { initializer-list }	(2) (nested array)	
         *  
         * Scalar initialization
         * When initializing an object of scalar type, the initializer must be a single expression
         * The initializer for a scalar must be a single expression, optionally enclosed in braces	
         * The expression is evaluated, and its value, after conversion as if by assignment to the type of the object, becomes the initial value of the object being initialized.
         * As with all other initializations, expression must be a constant expression when initializing objects of static or thread-local storage duration.
         * 
         * Array initialization
         * see http://en.cppreference.com/w/c/language/array_initialization
         * summary, i dont support init from a string. must init from list
         * init from list works how you expect
         * no supprot for desegnators
         */
        public static void DeclaratorWithInitilizer()
        {
            throw new NotImplementedException();
        }

        public static void PushIdentifierDeclarator(bool isPointer, string idString)
        {
            throw new NotImplementedException();
        }

        // has a (declarator) in it
        public static void NestedDeclarator(bool isPointer)
        {
            throw new NotImplementedException();
        }

        public static void ArrayDeclarator(bool isPointer, int numTypeQualifiers, bool hasAssgnExpr)
        {
            throw new NotImplementedException();
        }

        //parameter list
        //enter function prototype scope
        public static void BeginFunctionDeclarator()
        {
            throw new NotImplementedException();
        }

        //exit function prototype scope
        public static void EndFunctionDeclarator(bool isPointer)
        {
            throw new NotImplementedException();
        }

        private static CDeclarator PopDeclarator()
        {
            throw new NotImplementedException();
        }

        private static void PushDeclarator(CDeclarator declarator)
        {
            declarators.Push(declarator);
        }

        private static Stack<CDeclarator> declarators = new Stack<CDeclarator>();

        //paramaters

        //define param in function scope or function prototype scope
        public static void ParamaterDeclaration(int numSpecifiers)
        {
            throw new NotImplementedException();
        }

        private static CParamater PopParam()
        {
            throw new NotImplementedException();
        }

        private static void PushParam(CParamater param)
        {
            Params.Push(param);
        }

        private static Stack<CParamater> Params = new Stack<CParamater>();

        //pointer modifier
        //cahnges a type to be a pointer
        public static void PointerModifier(int numQualifiers, bool isNested)
        {
            throw new NotImplementedException();
        }

        private static CPointerModifier PopPointerModifier()
        {
            throw new NotImplementedException();
        }

        private static void PushPointerModifier(CPointerModifier param)
        {
            pointerMods.Push(param);
        }

        private static Stack<CPointerModifier> pointerMods = new Stack<CPointerModifier>();

        //initilizers

        //scalar init
        public static void Initilizer()
        {
            throw new NotImplementedException();
        }

        //array init
        public static void InitilizerList(int numInitilizers)
        {
            throw new NotImplementedException();
        }

        
    }
}
