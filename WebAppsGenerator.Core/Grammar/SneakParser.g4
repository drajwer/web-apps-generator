parser grammar SneakParser;

options { tokenVocab=SneakLexer; }

compileUnit		:	EOF
				;

file			:	classDefs
				;

classDefs		:	classDef classDefs
				|	/* epsilon */
				;

classDef		:	CLASS ID NEWLINE body
				;

body			:	INDENT properties DEDENT
				|	/* epsilon */
				;

properties		:	(property NEWLINE)* property NEWLINE?
				; 	

property		:	annotations ID COLON TYPE
				;

annotations		:	(annotation NEWLINE)*
				;

annotation		:	HASH ID params
				; 

params			:	/* epsilon */
				|	'(' paramlist ')'
				;

paramlist		:	(param COMMA)* param
				;

param			:	ID
				|	ID '=' VALUE
				;