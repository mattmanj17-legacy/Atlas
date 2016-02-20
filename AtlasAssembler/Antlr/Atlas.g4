grammar Atlas;

/*
 * Parser Rules
 */

source: (label | variable | arrayDec | string | instruction | arginstruction | end)*;

variable : ID SPACE? COLON SPACE? type SPACE literal (end|EOF); 
arrayDec : ID SPACE? COLON SPACE? type SPACE array (end|EOF);
string : ID SPACE? COLON SPACE? STRING (end|EOF);
label: ID COLON (end|EOF);
arginstruction: arginstructioncode SPACE (ID | literal) (end|EOF);
instruction: instructioncode (end|EOF); 

end: NL | COMMENT | BLOCK_COMMENT;

/*
 * Lexer Rules
 */

literal : (INT | HEX | CHAR | BOOL | NULL);

INT :'-'?[0-9]+;
HEX :'0''x'HEX_DIGIT+;

BOOL : (TRUE | FALSE);
TRUE : 'true';
FALSE : 'false';
NULL : 'nullptr';

CHAR:  '\'' ( ESC_SEQ | ~('\''|'\\') ) '\'';

STRING:  '"' ( ESC_SEQ | ~('\\'|'"') )* '"';

array: (OBRACE(INT|HEX)CBRACE) | (OBRACK SPACE? literal (COMMA SPACE? literal )* SPACE? CBRACK); 

OBRACE : '[';
CBRACE : ']';
OBRACK : '{';
CBRACK : '}';
SPACE : ' ';
COMMA: ',';

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

type :
(
	BYTE |
	HALF |
	WORD
);

BYTE : 'BYTE';
HALF : 'HALF';
WORD : 'WORD';

instructioncode : 
(
	ADD |
	SUB |
	NEG |
	LESS |
	MUL |
	SLL |
	SRL |
	SRA |
	ROR |
	NOT |
	AND |
	OR |
	XOR |
	LB |
	LUB |
	LH |
	LUH |
	LW |
	SB |
	SH |
	SW |
	JMP |
	JIF |
	PUSHBP |
	POPB |
	POPH |
	POPW |
	BEGINARGS |
	CALL |
	RETV |
	RET
);

ADD:'ADD';
SUB:'SUB';
NEG:'NEG'; 
LESS :'LESS' ;
MUL :'MUL' ;
SLL:'SLL' ;
SRL:'SRL' ;
SRA:'SRA' ;
ROR :'ROR' ;
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
POPB:'POPB' ;
POPH:'POPH' ;
POPW:'POPW';
BEGINARGS:'BEGINARGS'; 
CALL:'CALL' ;
RETV:'RETV' ;
RET:'RET' ;


arginstructioncode : 
( 
	PUSHB |
	PUSHH |
	PUSHW
);

PUSHB:'PUSHB';
PUSHH:'PUSHH';
PUSHW:'PUSHW';

ID: [a-zA-Z_][a-zA-Z0-9_]*;

BLOCK_COMMENT: ('/*' .*? '*/');
COMMENT: ('//' ~[\r\n]* ('\r'? '\n' | EOF));

COLON: ':' ;

NL: SPACE* ('\r'? '\n');
WS: ('\t')+ -> skip;
