// Generated from SneakLexer.g4 by ANTLR 4.7.1
import org.antlr.v4.runtime.Lexer;
import org.antlr.v4.runtime.CharStream;
import org.antlr.v4.runtime.Token;
import org.antlr.v4.runtime.TokenStream;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.misc.*;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class SneakLexer extends Lexer {
	static { RuntimeMetaData.checkVersion("4.7.1", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		INDENT=1, DEDENT=2, NEWLINE=3, WS=4, CLASS=5, COLON=6, HASH=7, COMMA=8, 
		OPEN_BRACKET=9, CLOSE_BRACKET=10, EQUALS=11, COMMENT=12, MULTILINE_COMM=13, 
		VALUE=14, ID=15, TYPE=16, PROP_WS=17;
	public static final int
		PROPERTY=1;
	public static String[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static String[] modeNames = {
		"DEFAULT_MODE", "PROPERTY"
	};

	public static final String[] ruleNames = {
		"IDENTIFIER", "NUM", "WHITESPACES", "NEWLINE", "WS", "INDENT", "DEDENT", 
		"CLASS", "COLON", "HASH", "COMMA", "OPEN_BRACKET", "CLOSE_BRACKET", "EQUALS", 
		"COMMENT", "MULTILINE_COMM", "VALUE", "ID", "TYPE", "PROP_WS"
	};

	private static final String[] _LITERAL_NAMES = {
		null, "'INDENT'", "'DEDENT'", null, null, "'class'", "':'", "'#'", "','", 
		"'('", "')'", "'='"
	};
	private static final String[] _SYMBOLIC_NAMES = {
		null, "INDENT", "DEDENT", "NEWLINE", "WS", "CLASS", "COLON", "HASH", "COMMA", 
		"OPEN_BRACKET", "CLOSE_BRACKET", "EQUALS", "COMMENT", "MULTILINE_COMM", 
		"VALUE", "ID", "TYPE", "PROP_WS"
	};
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}


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



	public SneakLexer(CharStream input) {
		super(input);
		_interp = new LexerATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	@Override
	public String getGrammarFileName() { return "SneakLexer.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public String[] getChannelNames() { return channelNames; }

	@Override
	public String[] getModeNames() { return modeNames; }

	@Override
	public ATN getATN() { return _ATN; }

	@Override
	public void action(RuleContext _localctx, int ruleIndex, int actionIndex) {
		switch (ruleIndex) {
		case 3:
			NEWLINE_action((RuleContext)_localctx, actionIndex);
			break;
		case 4:
			WS_action((RuleContext)_localctx, actionIndex);
			break;
		}
	}
	private void NEWLINE_action(RuleContext _localctx, int actionIndex) {
		switch (actionIndex) {
		case 0:

				if (pendingDent) { Skip(); }
				pendingDent = true;
				indentCount = 0;
				initialIndentToken = null;

			break;
		}
	}
	private void WS_action(RuleContext _localctx, int actionIndex) {
		switch (actionIndex) {
		case 1:

				Skip();
				if (pendingDent) { indentCount += getIndentationCount(Text); }

			break;
		}
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\2\23\u00c4\b\1\b\1"+
		"\4\2\t\2\4\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t"+
		"\n\4\13\t\13\4\f\t\f\4\r\t\r\4\16\t\16\4\17\t\17\4\20\t\20\4\21\t\21\4"+
		"\22\t\22\4\23\t\23\4\24\t\24\4\25\t\25\3\2\6\2.\n\2\r\2\16\2/\3\2\7\2"+
		"\63\n\2\f\2\16\2\66\13\2\3\3\5\39\n\3\3\3\7\3<\n\3\f\3\16\3?\13\3\3\3"+
		"\3\3\6\3C\n\3\r\3\16\3D\3\3\6\3H\n\3\r\3\16\3I\5\3L\n\3\3\4\6\4O\n\4\r"+
		"\4\16\4P\3\5\5\5T\n\5\3\5\3\5\5\5X\n\5\3\5\3\5\3\6\3\6\3\6\3\7\3\7\3\7"+
		"\3\7\3\7\3\7\3\7\3\7\3\7\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\b\3\t\3\t\3"+
		"\t\3\t\3\t\3\t\3\n\3\n\3\n\3\n\3\13\3\13\3\f\3\f\3\r\3\r\3\16\3\16\3\17"+
		"\3\17\3\20\3\20\3\20\3\20\6\20\u0089\n\20\r\20\16\20\u008a\3\20\3\20\3"+
		"\21\3\21\3\21\3\21\7\21\u0093\n\21\f\21\16\21\u0096\13\21\3\21\3\21\3"+
		"\21\3\21\3\21\3\22\3\22\7\22\u009f\n\22\f\22\16\22\u00a2\13\22\3\22\3"+
		"\22\3\22\3\22\3\22\3\22\3\22\3\22\3\22\3\22\3\22\5\22\u00af\n\22\3\23"+
		"\3\23\3\24\6\24\u00b4\n\24\r\24\16\24\u00b5\3\24\5\24\u00b9\n\24\3\24"+
		"\3\24\5\24\u00bd\n\24\3\24\3\24\3\25\3\25\3\25\3\25\4\u0094\u00a0\2\26"+
		"\4\2\6\2\b\2\n\5\f\6\16\3\20\4\22\7\24\b\26\t\30\n\32\13\34\f\36\r \16"+
		"\"\17$\20&\21(\22*\23\4\2\3\t\5\2C\\aac|\6\2\62;C\\aac|\4\2--//\3\2\62"+
		";\4\2\13\13\"\"\4\2\f\f\17\17\4\2C\\c|\2\u00d2\2\n\3\2\2\2\2\f\3\2\2\2"+
		"\2\16\3\2\2\2\2\20\3\2\2\2\2\22\3\2\2\2\2\24\3\2\2\2\2\26\3\2\2\2\2\30"+
		"\3\2\2\2\2\32\3\2\2\2\2\34\3\2\2\2\2\36\3\2\2\2\2 \3\2\2\2\2\"\3\2\2\2"+
		"\2$\3\2\2\2\2&\3\2\2\2\3(\3\2\2\2\3*\3\2\2\2\4-\3\2\2\2\68\3\2\2\2\bN"+
		"\3\2\2\2\nW\3\2\2\2\f[\3\2\2\2\16^\3\2\2\2\20g\3\2\2\2\22p\3\2\2\2\24"+
		"v\3\2\2\2\26z\3\2\2\2\30|\3\2\2\2\32~\3\2\2\2\34\u0080\3\2\2\2\36\u0082"+
		"\3\2\2\2 \u0084\3\2\2\2\"\u008e\3\2\2\2$\u00ae\3\2\2\2&\u00b0\3\2\2\2"+
		"(\u00b3\3\2\2\2*\u00c0\3\2\2\2,.\t\2\2\2-,\3\2\2\2./\3\2\2\2/-\3\2\2\2"+
		"/\60\3\2\2\2\60\64\3\2\2\2\61\63\t\3\2\2\62\61\3\2\2\2\63\66\3\2\2\2\64"+
		"\62\3\2\2\2\64\65\3\2\2\2\65\5\3\2\2\2\66\64\3\2\2\2\679\t\4\2\28\67\3"+
		"\2\2\289\3\2\2\29K\3\2\2\2:<\t\5\2\2;:\3\2\2\2<?\3\2\2\2=;\3\2\2\2=>\3"+
		"\2\2\2>@\3\2\2\2?=\3\2\2\2@B\7\60\2\2AC\t\5\2\2BA\3\2\2\2CD\3\2\2\2DB"+
		"\3\2\2\2DE\3\2\2\2EL\3\2\2\2FH\t\5\2\2GF\3\2\2\2HI\3\2\2\2IG\3\2\2\2I"+
		"J\3\2\2\2JL\3\2\2\2K=\3\2\2\2KG\3\2\2\2L\7\3\2\2\2MO\t\6\2\2NM\3\2\2\2"+
		"OP\3\2\2\2PN\3\2\2\2PQ\3\2\2\2Q\t\3\2\2\2RT\7\17\2\2SR\3\2\2\2ST\3\2\2"+
		"\2TU\3\2\2\2UX\7\f\2\2VX\7\17\2\2WS\3\2\2\2WV\3\2\2\2XY\3\2\2\2YZ\b\5"+
		"\2\2Z\13\3\2\2\2[\\\5\b\4\2\\]\b\6\3\2]\r\3\2\2\2^_\7K\2\2_`\7P\2\2`a"+
		"\7F\2\2ab\7G\2\2bc\7P\2\2cd\7V\2\2de\3\2\2\2ef\b\7\4\2f\17\3\2\2\2gh\7"+
		"F\2\2hi\7G\2\2ij\7F\2\2jk\7G\2\2kl\7P\2\2lm\7V\2\2mn\3\2\2\2no\b\b\4\2"+
		"o\21\3\2\2\2pq\7e\2\2qr\7n\2\2rs\7c\2\2st\7u\2\2tu\7u\2\2u\23\3\2\2\2"+
		"vw\7<\2\2wx\3\2\2\2xy\b\n\5\2y\25\3\2\2\2z{\7%\2\2{\27\3\2\2\2|}\7.\2"+
		"\2}\31\3\2\2\2~\177\7*\2\2\177\33\3\2\2\2\u0080\u0081\7+\2\2\u0081\35"+
		"\3\2\2\2\u0082\u0083\7?\2\2\u0083\37\3\2\2\2\u0084\u0085\7\61\2\2\u0085"+
		"\u0086\7\61\2\2\u0086\u0088\3\2\2\2\u0087\u0089\n\7\2\2\u0088\u0087\3"+
		"\2\2\2\u0089\u008a\3\2\2\2\u008a\u0088\3\2\2\2\u008a\u008b\3\2\2\2\u008b"+
		"\u008c\3\2\2\2\u008c\u008d\b\20\4\2\u008d!\3\2\2\2\u008e\u008f\7\61\2"+
		"\2\u008f\u0090\7,\2\2\u0090\u0094\3\2\2\2\u0091\u0093\13\2\2\2\u0092\u0091"+
		"\3\2\2\2\u0093\u0096\3\2\2\2\u0094\u0095\3\2\2\2\u0094\u0092\3\2\2\2\u0095"+
		"\u0097\3\2\2\2\u0096\u0094\3\2\2\2\u0097\u0098\7,\2\2\u0098\u0099\7\61"+
		"\2\2\u0099\u009a\3\2\2\2\u009a\u009b\b\21\6\2\u009b#\3\2\2\2\u009c\u00a0"+
		"\7$\2\2\u009d\u009f\13\2\2\2\u009e\u009d\3\2\2\2\u009f\u00a2\3\2\2\2\u00a0"+
		"\u00a1\3\2\2\2\u00a0\u009e\3\2\2\2\u00a1\u00a3\3\2\2\2\u00a2\u00a0\3\2"+
		"\2\2\u00a3\u00af\7$\2\2\u00a4\u00af\5\6\3\2\u00a5\u00a6\7v\2\2\u00a6\u00a7"+
		"\7t\2\2\u00a7\u00a8\7w\2\2\u00a8\u00af\7g\2\2\u00a9\u00aa\7h\2\2\u00aa"+
		"\u00ab\7c\2\2\u00ab\u00ac\7n\2\2\u00ac\u00ad\7u\2\2\u00ad\u00af\7g\2\2"+
		"\u00ae\u009c\3\2\2\2\u00ae\u00a4\3\2\2\2\u00ae\u00a5\3\2\2\2\u00ae\u00a9"+
		"\3\2\2\2\u00af%\3\2\2\2\u00b0\u00b1\5\4\2\2\u00b1\'\3\2\2\2\u00b2\u00b4"+
		"\t\b\2\2\u00b3\u00b2\3\2\2\2\u00b4\u00b5\3\2\2\2\u00b5\u00b3\3\2\2\2\u00b5"+
		"\u00b6\3\2\2\2\u00b6\u00b8\3\2\2\2\u00b7\u00b9\7A\2\2\u00b8\u00b7\3\2"+
		"\2\2\u00b8\u00b9\3\2\2\2\u00b9\u00bc\3\2\2\2\u00ba\u00bb\7]\2\2\u00bb"+
		"\u00bd\7_\2\2\u00bc\u00ba\3\2\2\2\u00bc\u00bd\3\2\2\2\u00bd\u00be\3\2"+
		"\2\2\u00be\u00bf\b\24\7\2\u00bf)\3\2\2\2\u00c0\u00c1\5\b\4\2\u00c1\u00c2"+
		"\3\2\2\2\u00c2\u00c3\b\25\6\2\u00c3+\3\2\2\2\25\2\3/\648=DIKPSW\u008a"+
		"\u0094\u00a0\u00ae\u00b5\u00b8\u00bc\b\3\5\2\3\6\3\2\3\2\7\3\2\b\2\2\6"+
		"\2\2";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}