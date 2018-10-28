parser grammar SneakParser;

options { tokenVocab=SneakLexer; }

compileUnit
	:	EOF
	;

class		:	CLASS ID NEWLINE property*
			;
property	:	ID COLON TYPE NEWLINE
			;