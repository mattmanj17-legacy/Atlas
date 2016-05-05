grammar Atlas;
/*
 * Parser Rules
 */
root: (labelDecl | variableDecl | arrayDecl | instruction | end | SPACE)*;

//Top level nodes
labelDecl: ID COLON (end|EOF);

variableDecl : ID SPACE? COLON SPACE? type SPACE literal SPACE? (end|EOF); 

arrayDecl : ID SPACE? COLON SPACE? type SPACE arrayInitilizer SPACE? (end|EOF);

instruction
	: instructionCode (end|EOF)
	| instructionCodeNeedsArg SPACE (ID | literal) (end|EOF);

end: NL | COMMENT | BLOCK_COMMENT;

// Used in Variable/array Declaration
type : WORD ;

// used in variable Declartions and as arguments to Instructions which require an argument
literal 
	: INT ;

//used to declare an array
arrayInitilizer
	//declare an array of a certain size, filled with zero
	: OSQUAREBRACE (INT) CSQUAREBRACE 
	//declare an array filled with literal values
	| (OCURLYBRACE SPACE? literal SPACE? (COMMA SPACE? literal SPACE?)* CCURLYBRACE); 

//instructions that require no argument
instructionCode 
	: NOP
	| ADD
	| SUB
	| NEG
	| LESS
	| EQU
	| MUL
	| SLL
	| SRL
	| NOT
	| AND
	| OR
	| XOR
	| LW
	| SW
	| JMP
	| JIF
	| PUSHBP
	| COPY
	| POPW
	| BEGINARGS
	| CALL
	| RETV
	| RET;

//instructions that require an argument
instructionCodeNeedsArg  
	: PUSHW;

/*
 * Lexer Rules
 */

//Keywords
//Types
WORD : 'WORD';

//Instructions (no arg)
NOP:'NOP';
ADD:'ADD';
SUB:'SUB';
NEG:'NEG'; 
LESS :'LESS' ;
EQU:'EQU';
MUL :'MUL' ;
SLL:'SLL' ;
SRL:'SRL' ;
NOT:'NOT' ;
AND:'AND' ;
OR:'OR' ;
XOR:'XOR' ;
LW:'LW' ;
SW:'SW' ;
JMP:'JMP';
JIF:'JIF' ;
PUSHBP:'PUSHBP'; 
COPY:'COPY';
POPW:'POPW' ;
BEGINARGS:'BEGINARGS'; 
CALL:'CALL' ;
RETV:'RETV' ;
RET:'RET' ;

//instructions (one arg)
PUSHW:'PUSHW';

//Literals
//numeric
INT :'-'?[0-9]+;

//ID (variable/Array/string/label name)
ID: [a-zA-Z_][a-zA-Z0-9_]*;

//control tokens
OSQUAREBRACE : '[';
CSQUAREBRACE : ']';
OCURLYBRACE : '{';
CCURLYBRACE : '}';
COMMA: ',';
COLON: ':' ;

//comments
BLOCK_COMMENT: ('/*' .*? '*/');
COMMENT: ('//' ~[\r\n]* (NL | EOF));

//White space
NL: ('\r'? '\n');
SPACE : (' ')+;
WS: ('\t')+ -> skip;
