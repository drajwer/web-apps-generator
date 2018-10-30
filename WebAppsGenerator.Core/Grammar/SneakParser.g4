parser grammar SneakParser;

options { tokenVocab=SneakLexer; }

compileUnit
	:	EOF
	;

file			:	 (classDef)*
				;
classDef		:	CLASS CLASS_NAME ENDL_CLASS property*
				;
property		:	/*INDENT*/ ID COLON TYPE ENDL_PROP
				;
annotation		:	HASH ANN_NAME PARAMS
				; 
params			:	/* epsilon */
				|	'(' PARAMLIST ')'
				;
paramlist		:	PARAM ',' PARAMLIST
				|	PARAM
				;