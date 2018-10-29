lexer grammar SneakLexer;

tokens { INDENT, DEDENT }

@lexer::members 
{
	private int current_line_indent = 0;   		/* indentation of the current line */
	private int prev_line_indent = 0;          /* indentation level of previous line */ 
	
	static int getIndentationCount(String spaces) {
    int current_line_indent = 0;
    foreach (char ch in spaces) {
      switch (ch) {
        case '\t':
          current_line_indent += 8 - (count % 8);
          break;
        default:
          // A normal space char.
          count++;
      }
    }
}

	
fragment IDENTIFIER		:	[A-Za-z_]+[A-Za-z_0-9]*;

fragment NUM 			:	[-+]?([0-9]* '.' [0-9]+|[0-9]+);

fragment WHITESPACES	:	('\t' | '\r' | '\n' | ' ')+;

fragment NEWLINE		:	('\r'? '\n' | '\r');

/* default mode is active before first non-whitespace character of every line */
CLASS			:	'class' -> pushMode(CLASS_DEF)
				;

ENDL			:	NEWLINE -> skip
				;

ID				:	IDENTIFIER -> pushMode(PROP_DEF)
				;
TRAILING_WS		:	(' ' | '\t')+
					{
						int indent = getIndentationCount(getText());
						if(indent > prev_line_indent) emit(
ALL_WS			:	WHITESPACES -> skip
				;
				
mode CLASS_DEF;

CLASS_NAME		:	IDENTIFIER
				;

ENDL_CLASS		:	NEWLINE -> popMode
				;
				
WS_TAB			:	 (' ' | '\t')+ -> skip
				;
				
				
mode PROP_DEF;


WS				:	(' ' | '\t')+ -> skip
				;
COLON			:	':' 
				;
COMMMA			:	','
				;
ENDL_PROP			:	NEWLINE -> popMode
				;
INDENT			:	'\t'
				;
HASH			:	'#'
				;
OPEN_BRACKET	:	'('
				;
CLOSE_BRACKET	:	')'
				;
TYPE			:	[A-Za-z]+
				;
PARAM			:	IDENTIFIER
				|	IDENTIFIER '=' VALUE
				;
VALUE			:	'"' IDENTIFIER '"' 
				|	NUM
				;
	