lexer grammar SneakLexer;

tokens { INDENT, DEDENT }

WS
	:	' ' -> channel(HIDDEN)
	;
CLASS	:	'class' 
		;
INDENT	:	'\t' 
		;
ID		:	[A-Za-z]+ 
		;
COLON	:	':' 
		;
TYPE	:	('string' | 'int' | 'bool')
		;
NEWLINE	:	('\r'? '\n' | '\r')
		;