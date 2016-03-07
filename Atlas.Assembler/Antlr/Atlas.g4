grammar Atlas;
/*
 * Parser Rules
 */
root: (labelDecl | variableDecl | arrayDecl | stringDecl | instruction | end | SPACE)*;

//Top level nodes
labelDecl: ID COLON (end|EOF);

variableDecl : ID SPACE? COLON SPACE? type SPACE literal SPACE? (end|EOF); 

arrayDecl : ID SPACE? COLON SPACE? type SPACE arrayInitilizer SPACE? (end|EOF);

stringDecl : ID SPACE? COLON SPACE? STRING SPACE? (end|EOF);

instruction
	: instructionCode (end|EOF)
	| instructionCodeNeedsArg SPACE (ID | literal) (end|EOF);

end: NL | COMMENT | BLOCK_COMMENT;

// Used in Variable/array Declaration
type : BYTE | HALF | WORD ;

// used in variable Declartions and as arguments to Instructions which require an argument
literal 
	: INT 
	| HEX 
	| CHAR 
	| boolean 
	| NULL;

boolean : (TRUE | FALSE);

//used to declare an array
arrayInitilizer
	//declare an array of a certain size, filled with zero
	: OSQUAREBRACE (INT | HEX) CSQUAREBRACE 
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
	| LB
	| LUB
	| LH
	| LUH
	| LW
	| SB
	| SH
	| SW
	| JMP
	| JIF
	| PUSHBP
	| COPY
	| POPB
	| POPH
	| POPW
	| BEGINARGS
	| CALL
	| SYSCALL
	| RETV
	| RET;

//instructions that require an argument
instructionCodeNeedsArg  
	: PUSHB
	| PUSHH
	| PUSHW;

/*
 * Lexer Rules
 */

//Keywords
//Types
BYTE : 'BYTE';
HALF : 'HALF';
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
LB:'LB' ;
LUB:'LUB' ;
LH:'LH' ;
LUH:'LUH' ;
LW:'LW' ;
SB:'SB' ;
SH:'SH' ;
SW:'SW' ;
JMP:'JMP';
JIF:'JIF' ;
PUSHBP:'PUSHBP'; 
COPY:'COPY';
POPW:'POPW' ;
POPH:'POPH' ;
POPB:'POPB' ;
BEGINARGS:'BEGINARGS'; 
CALL:'CALL' ;
SYSCALL:'SYSCALL';
RETV:'RETV' ;
RET:'RET' ;

//instructions (one arg)
PUSHW:'PUSHW';
PUSHH:'PUSHH';
PUSHB:'PUSHB';

//Literals
//numeric
INT :'-'?[0-9]+;
HEX :'0''x'HEX_DIGIT+;
//boolean
TRUE : 'TRUE';
FALSE : 'FALSE';
//NULL
NULL : 'NULL';
//Character
CHAR:  '\'' ( ESC_SEQ | ~('\''|'\\') ) '\'';
//string
STRING:  '"' ( ESC_SEQ | ~('\\'|'"') )* '"';
//framgens (make up other literals)
fragment
ESC_SEQ
    :   '\\' ('b'|'t'|'n'|'f'|'r'|'\"'|'\''|'\\')
    |   UNICODE_ESC
    |   OCTAL_ESC
    ;

fragment
OCTAL_ESC
    :   '\\' ('0'..'3') ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7')
    ;

fragment
UNICODE_ESC
    :   '\\' 'u' HEX_DIGIT HEX_DIGIT HEX_DIGIT HEX_DIGIT
    ;

fragment
HEX_DIGIT : ('0'..'9'|'A'..'F') ;

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
