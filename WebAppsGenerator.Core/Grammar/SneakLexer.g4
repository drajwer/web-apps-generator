lexer grammar SneakLexer;

tokens { INDENT, DEDENT }

@lexer::members 
{
	// Source: https://github.com/wevrem/wry/blob/master/grammars/DentLexer.g4
	// Initializing `pendingDent` to true means any whitespace at the beginning
	// of the file will trigger an INDENT, which is a syntax error

	private bool pendingDent = true;
	private int indentCount = 0;
	private System.Collections.Generic.Queue<IToken> tokenQueue = new System.Collections.Generic.Queue<IToken>();
	private System.Collections.Generic.Stack<int> indentStack = new System.Collections.Generic.Stack<int>();
	private IToken initialIndentToken = null;
	private int getSavedIndent() { return indentStack.TryPeek(out var top) ? top : 0; }

	private CommonToken createToken(int type, String text, IToken next) {
		CommonToken token = new CommonToken(type, text);
		if (null != initialIndentToken) {
			token.StartIndex = initialIndentToken.StartIndex;
			token.Line = initialIndentToken.Line;
			token.Column = initialIndentToken.Column; // originally this prop is named CharPositionInLine
			token.StopIndex = next.StartIndex-1;
		}
		return token;
	}
	private int getIndentationCount(String spaces) {
		int spaces_count = 0;
		foreach (char ch in spaces) {
		  switch (ch) {
			case '\t':
			  spaces_count += 8;
			  break;
			default:
			  // A normal space char.
			  spaces_count++;
			  break;
		  }
		}
		return spaces_count;
	}

	
	public override IToken NextToken() {
		// Return tokens from the queue if it is not empty.
		if (tokenQueue.Count != 0) { return tokenQueue.Dequeue(); }

		// Grab the next token and if nothing special is needed, simply return it.
		// Initialize `initialIndentToken` if needed.
		IToken next = base.NextToken();

		if (pendingDent && null == initialIndentToken && NEWLINE != next.Type) { initialIndentToken = next; }
		if (null == next || Hidden == next.Channel || NEWLINE == next.Type) { return next; }

		// Handle EOF. In particular, handle an abrupt EOF that comes without an
		// immediately preceding NEWLINE.
		if (next.Type == Eof) {
			indentCount = 0;
			// EOF outside of `pendingDent` state means input did not have a final
			// NEWLINE before end of file.
			if (!pendingDent) {
				initialIndentToken = next;
				tokenQueue.Enqueue(createToken(NEWLINE, "NEWLINE", next));
			}
		}

		// Before exiting `pendingDent` state queue up proper INDENTS and DEDENTS.
		while (indentCount != getSavedIndent()) {
			if (indentCount > getSavedIndent()) {
				indentStack.Push(indentCount);
				tokenQueue.Enqueue(createToken(INDENT, "INDENT", next));
			} else {
				indentStack.Pop();
				tokenQueue.Enqueue(createToken(DEDENT, "DEDENT", next));
			}
		}
		pendingDent = false;
		tokenQueue.Enqueue(next);
		return tokenQueue.Dequeue();
	}

}


	
fragment IDENTIFIER		:	[A-Za-z_]+[A-Za-z_0-9]*;

fragment NUM 			:	[-+]?([0-9]* '.' [0-9]+|[0-9]+);

fragment WHITESPACES	:	(' ' | '\t')+ ;


NEWLINE : ( '\r'? '\n' | '\r' ) 
{
	if (pendingDent) { Skip(); }
	pendingDent = true;
	indentCount = 0;
	initialIndentToken = null;
};

WS : WHITESPACES
{
	Skip();
	if (pendingDent) { indentCount += getIndentationCount(Text); }
};

INDENT : 'INDENT' -> channel(HIDDEN);
DEDENT : 'DEDENT' -> channel(HIDDEN);

CLASS			:	'class'
				;
				
COLON			:	':' -> pushMode(PROPERTY)
				;
				
HASH			:	'#'
				;
		
COMMA			:	','
				;

OPEN_BRACKET	:	'('
				;
				
CLOSE_BRACKET	:	')'
				;
				
EQUALS			:	'='
				;
				
COMMENT 		: '//' ~[\r\n]+ -> channel(HIDDEN)
				;
				
MULTILINE_COMM	: '/*' .*? '*/' -> skip
				;
				
VALUE			:	'"' .*? '"' 
				|	NUM
				|	'true'
				|	'false'
				;
											
ID				:	IDENTIFIER
				;

mode PROPERTY;
			
TYPE			:	[A-Za-z]+ ('?')?('[]')? -> popMode
				;
PROP_WS			:	WHITESPACES -> skip
				;