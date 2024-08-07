//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from SneakLexer.g4 by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using System;
using System.IO;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public partial class SneakLexer : Lexer {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		INDENT=1, DEDENT=2, NEWLINE=3, WS=4, CLASS=5, COLON=6, HASH=7, COMMA=8, 
		OPEN_BRACKET=9, CLOSE_BRACKET=10, EQUALS=11, COMMENT=12, MULTILINE_COMM=13, 
		VALUE=14, ID=15, TYPE=16, PROP_WS=17;
	public const int
		PROPERTY=1;
	public static string[] channelNames = {
		"DEFAULT_TOKEN_CHANNEL", "HIDDEN"
	};

	public static string[] modeNames = {
		"DEFAULT_MODE", "PROPERTY"
	};

	public static readonly string[] ruleNames = {
		"IDENTIFIER", "NUM", "WHITESPACES", "NEWLINE", "WS", "INDENT", "DEDENT", 
		"CLASS", "COLON", "HASH", "COMMA", "OPEN_BRACKET", "CLOSE_BRACKET", "EQUALS", 
		"COMMENT", "MULTILINE_COMM", "VALUE", "ID", "TYPE", "PROP_WS"
	};


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



	public SneakLexer(ICharStream input)
	: this(input, Console.Out, Console.Error) { }

	public SneakLexer(ICharStream input, TextWriter output, TextWriter errorOutput)
	: base(input, output, errorOutput)
	{
		Interpreter = new LexerATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	private static readonly string[] _LiteralNames = {
		null, "'INDENT'", "'DEDENT'", null, null, "'class'", "':'", "'#'", "','", 
		"'('", "')'", "'='"
	};
	private static readonly string[] _SymbolicNames = {
		null, "INDENT", "DEDENT", "NEWLINE", "WS", "CLASS", "COLON", "HASH", "COMMA", 
		"OPEN_BRACKET", "CLOSE_BRACKET", "EQUALS", "COMMENT", "MULTILINE_COMM", 
		"VALUE", "ID", "TYPE", "PROP_WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "SneakLexer.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ChannelNames { get { return channelNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static SneakLexer() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}
	public override void Action(RuleContext _localctx, int ruleIndex, int actionIndex) {
		switch (ruleIndex) {
		case 3 : NEWLINE_action(_localctx, actionIndex); break;
		case 4 : WS_action(_localctx, actionIndex); break;
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

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x2', '\x13', '\xC4', '\b', '\x1', '\b', '\x1', '\x4', '\x2', 
		'\t', '\x2', '\x4', '\x3', '\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x4', 
		'\x5', '\t', '\x5', '\x4', '\x6', '\t', '\x6', '\x4', '\a', '\t', '\a', 
		'\x4', '\b', '\t', '\b', '\x4', '\t', '\t', '\t', '\x4', '\n', '\t', '\n', 
		'\x4', '\v', '\t', '\v', '\x4', '\f', '\t', '\f', '\x4', '\r', '\t', '\r', 
		'\x4', '\xE', '\t', '\xE', '\x4', '\xF', '\t', '\xF', '\x4', '\x10', '\t', 
		'\x10', '\x4', '\x11', '\t', '\x11', '\x4', '\x12', '\t', '\x12', '\x4', 
		'\x13', '\t', '\x13', '\x4', '\x14', '\t', '\x14', '\x4', '\x15', '\t', 
		'\x15', '\x3', '\x2', '\x6', '\x2', '.', '\n', '\x2', '\r', '\x2', '\xE', 
		'\x2', '/', '\x3', '\x2', '\a', '\x2', '\x33', '\n', '\x2', '\f', '\x2', 
		'\xE', '\x2', '\x36', '\v', '\x2', '\x3', '\x3', '\x5', '\x3', '\x39', 
		'\n', '\x3', '\x3', '\x3', '\a', '\x3', '<', '\n', '\x3', '\f', '\x3', 
		'\xE', '\x3', '?', '\v', '\x3', '\x3', '\x3', '\x3', '\x3', '\x6', '\x3', 
		'\x43', '\n', '\x3', '\r', '\x3', '\xE', '\x3', '\x44', '\x3', '\x3', 
		'\x6', '\x3', 'H', '\n', '\x3', '\r', '\x3', '\xE', '\x3', 'I', '\x5', 
		'\x3', 'L', '\n', '\x3', '\x3', '\x4', '\x6', '\x4', 'O', '\n', '\x4', 
		'\r', '\x4', '\xE', '\x4', 'P', '\x3', '\x5', '\x5', '\x5', 'T', '\n', 
		'\x5', '\x3', '\x5', '\x3', '\x5', '\x5', '\x5', 'X', '\n', '\x5', '\x3', 
		'\x5', '\x3', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\x6', '\x3', 
		'\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\a', 
		'\x3', '\a', '\x3', '\a', '\x3', '\a', '\x3', '\b', '\x3', '\b', '\x3', 
		'\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x3', '\b', 
		'\x3', '\b', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', '\t', '\x3', 
		'\t', '\x3', '\t', '\x3', '\n', '\x3', '\n', '\x3', '\n', '\x3', '\n', 
		'\x3', '\v', '\x3', '\v', '\x3', '\f', '\x3', '\f', '\x3', '\r', '\x3', 
		'\r', '\x3', '\xE', '\x3', '\xE', '\x3', '\xF', '\x3', '\xF', '\x3', '\x10', 
		'\x3', '\x10', '\x3', '\x10', '\x3', '\x10', '\x6', '\x10', '\x89', '\n', 
		'\x10', '\r', '\x10', '\xE', '\x10', '\x8A', '\x3', '\x10', '\x3', '\x10', 
		'\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\a', '\x11', 
		'\x93', '\n', '\x11', '\f', '\x11', '\xE', '\x11', '\x96', '\v', '\x11', 
		'\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', '\x3', '\x11', 
		'\x3', '\x12', '\x3', '\x12', '\a', '\x12', '\x9F', '\n', '\x12', '\f', 
		'\x12', '\xE', '\x12', '\xA2', '\v', '\x12', '\x3', '\x12', '\x3', '\x12', 
		'\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', 
		'\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x3', '\x12', '\x5', '\x12', 
		'\xAF', '\n', '\x12', '\x3', '\x13', '\x3', '\x13', '\x3', '\x14', '\x6', 
		'\x14', '\xB4', '\n', '\x14', '\r', '\x14', '\xE', '\x14', '\xB5', '\x3', 
		'\x14', '\x5', '\x14', '\xB9', '\n', '\x14', '\x3', '\x14', '\x3', '\x14', 
		'\x5', '\x14', '\xBD', '\n', '\x14', '\x3', '\x14', '\x3', '\x14', '\x3', 
		'\x15', '\x3', '\x15', '\x3', '\x15', '\x3', '\x15', '\x4', '\x94', '\xA0', 
		'\x2', '\x16', '\x4', '\x2', '\x6', '\x2', '\b', '\x2', '\n', '\x5', '\f', 
		'\x6', '\xE', '\x3', '\x10', '\x4', '\x12', '\a', '\x14', '\b', '\x16', 
		'\t', '\x18', '\n', '\x1A', '\v', '\x1C', '\f', '\x1E', '\r', ' ', '\xE', 
		'\"', '\xF', '$', '\x10', '&', '\x11', '(', '\x12', '*', '\x13', '\x4', 
		'\x2', '\x3', '\t', '\x5', '\x2', '\x43', '\\', '\x61', '\x61', '\x63', 
		'|', '\x6', '\x2', '\x32', ';', '\x43', '\\', '\x61', '\x61', '\x63', 
		'|', '\x4', '\x2', '-', '-', '/', '/', '\x3', '\x2', '\x32', ';', '\x4', 
		'\x2', '\v', '\v', '\"', '\"', '\x4', '\x2', '\f', '\f', '\xF', '\xF', 
		'\x4', '\x2', '\x43', '\\', '\x63', '|', '\x2', '\xD2', '\x2', '\n', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '\f', '\x3', '\x2', '\x2', '\x2', '\x2', '\xE', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x10', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x12', '\x3', '\x2', '\x2', '\x2', '\x2', '\x14', '\x3', '\x2', 
		'\x2', '\x2', '\x2', '\x16', '\x3', '\x2', '\x2', '\x2', '\x2', '\x18', 
		'\x3', '\x2', '\x2', '\x2', '\x2', '\x1A', '\x3', '\x2', '\x2', '\x2', 
		'\x2', '\x1C', '\x3', '\x2', '\x2', '\x2', '\x2', '\x1E', '\x3', '\x2', 
		'\x2', '\x2', '\x2', ' ', '\x3', '\x2', '\x2', '\x2', '\x2', '\"', '\x3', 
		'\x2', '\x2', '\x2', '\x2', '$', '\x3', '\x2', '\x2', '\x2', '\x2', '&', 
		'\x3', '\x2', '\x2', '\x2', '\x3', '(', '\x3', '\x2', '\x2', '\x2', '\x3', 
		'*', '\x3', '\x2', '\x2', '\x2', '\x4', '-', '\x3', '\x2', '\x2', '\x2', 
		'\x6', '\x38', '\x3', '\x2', '\x2', '\x2', '\b', 'N', '\x3', '\x2', '\x2', 
		'\x2', '\n', 'W', '\x3', '\x2', '\x2', '\x2', '\f', '[', '\x3', '\x2', 
		'\x2', '\x2', '\xE', '^', '\x3', '\x2', '\x2', '\x2', '\x10', 'g', '\x3', 
		'\x2', '\x2', '\x2', '\x12', 'p', '\x3', '\x2', '\x2', '\x2', '\x14', 
		'v', '\x3', '\x2', '\x2', '\x2', '\x16', 'z', '\x3', '\x2', '\x2', '\x2', 
		'\x18', '|', '\x3', '\x2', '\x2', '\x2', '\x1A', '~', '\x3', '\x2', '\x2', 
		'\x2', '\x1C', '\x80', '\x3', '\x2', '\x2', '\x2', '\x1E', '\x82', '\x3', 
		'\x2', '\x2', '\x2', ' ', '\x84', '\x3', '\x2', '\x2', '\x2', '\"', '\x8E', 
		'\x3', '\x2', '\x2', '\x2', '$', '\xAE', '\x3', '\x2', '\x2', '\x2', '&', 
		'\xB0', '\x3', '\x2', '\x2', '\x2', '(', '\xB3', '\x3', '\x2', '\x2', 
		'\x2', '*', '\xC0', '\x3', '\x2', '\x2', '\x2', ',', '.', '\t', '\x2', 
		'\x2', '\x2', '-', ',', '\x3', '\x2', '\x2', '\x2', '.', '/', '\x3', '\x2', 
		'\x2', '\x2', '/', '-', '\x3', '\x2', '\x2', '\x2', '/', '\x30', '\x3', 
		'\x2', '\x2', '\x2', '\x30', '\x34', '\x3', '\x2', '\x2', '\x2', '\x31', 
		'\x33', '\t', '\x3', '\x2', '\x2', '\x32', '\x31', '\x3', '\x2', '\x2', 
		'\x2', '\x33', '\x36', '\x3', '\x2', '\x2', '\x2', '\x34', '\x32', '\x3', 
		'\x2', '\x2', '\x2', '\x34', '\x35', '\x3', '\x2', '\x2', '\x2', '\x35', 
		'\x5', '\x3', '\x2', '\x2', '\x2', '\x36', '\x34', '\x3', '\x2', '\x2', 
		'\x2', '\x37', '\x39', '\t', '\x4', '\x2', '\x2', '\x38', '\x37', '\x3', 
		'\x2', '\x2', '\x2', '\x38', '\x39', '\x3', '\x2', '\x2', '\x2', '\x39', 
		'K', '\x3', '\x2', '\x2', '\x2', ':', '<', '\t', '\x5', '\x2', '\x2', 
		';', ':', '\x3', '\x2', '\x2', '\x2', '<', '?', '\x3', '\x2', '\x2', '\x2', 
		'=', ';', '\x3', '\x2', '\x2', '\x2', '=', '>', '\x3', '\x2', '\x2', '\x2', 
		'>', '@', '\x3', '\x2', '\x2', '\x2', '?', '=', '\x3', '\x2', '\x2', '\x2', 
		'@', '\x42', '\a', '\x30', '\x2', '\x2', '\x41', '\x43', '\t', '\x5', 
		'\x2', '\x2', '\x42', '\x41', '\x3', '\x2', '\x2', '\x2', '\x43', '\x44', 
		'\x3', '\x2', '\x2', '\x2', '\x44', '\x42', '\x3', '\x2', '\x2', '\x2', 
		'\x44', '\x45', '\x3', '\x2', '\x2', '\x2', '\x45', 'L', '\x3', '\x2', 
		'\x2', '\x2', '\x46', 'H', '\t', '\x5', '\x2', '\x2', 'G', '\x46', '\x3', 
		'\x2', '\x2', '\x2', 'H', 'I', '\x3', '\x2', '\x2', '\x2', 'I', 'G', '\x3', 
		'\x2', '\x2', '\x2', 'I', 'J', '\x3', '\x2', '\x2', '\x2', 'J', 'L', '\x3', 
		'\x2', '\x2', '\x2', 'K', '=', '\x3', '\x2', '\x2', '\x2', 'K', 'G', '\x3', 
		'\x2', '\x2', '\x2', 'L', '\a', '\x3', '\x2', '\x2', '\x2', 'M', 'O', 
		'\t', '\x6', '\x2', '\x2', 'N', 'M', '\x3', '\x2', '\x2', '\x2', 'O', 
		'P', '\x3', '\x2', '\x2', '\x2', 'P', 'N', '\x3', '\x2', '\x2', '\x2', 
		'P', 'Q', '\x3', '\x2', '\x2', '\x2', 'Q', '\t', '\x3', '\x2', '\x2', 
		'\x2', 'R', 'T', '\a', '\xF', '\x2', '\x2', 'S', 'R', '\x3', '\x2', '\x2', 
		'\x2', 'S', 'T', '\x3', '\x2', '\x2', '\x2', 'T', 'U', '\x3', '\x2', '\x2', 
		'\x2', 'U', 'X', '\a', '\f', '\x2', '\x2', 'V', 'X', '\a', '\xF', '\x2', 
		'\x2', 'W', 'S', '\x3', '\x2', '\x2', '\x2', 'W', 'V', '\x3', '\x2', '\x2', 
		'\x2', 'X', 'Y', '\x3', '\x2', '\x2', '\x2', 'Y', 'Z', '\b', '\x5', '\x2', 
		'\x2', 'Z', '\v', '\x3', '\x2', '\x2', '\x2', '[', '\\', '\x5', '\b', 
		'\x4', '\x2', '\\', ']', '\b', '\x6', '\x3', '\x2', ']', '\r', '\x3', 
		'\x2', '\x2', '\x2', '^', '_', '\a', 'K', '\x2', '\x2', '_', '`', '\a', 
		'P', '\x2', '\x2', '`', '\x61', '\a', '\x46', '\x2', '\x2', '\x61', '\x62', 
		'\a', 'G', '\x2', '\x2', '\x62', '\x63', '\a', 'P', '\x2', '\x2', '\x63', 
		'\x64', '\a', 'V', '\x2', '\x2', '\x64', '\x65', '\x3', '\x2', '\x2', 
		'\x2', '\x65', '\x66', '\b', '\a', '\x4', '\x2', '\x66', '\xF', '\x3', 
		'\x2', '\x2', '\x2', 'g', 'h', '\a', '\x46', '\x2', '\x2', 'h', 'i', '\a', 
		'G', '\x2', '\x2', 'i', 'j', '\a', '\x46', '\x2', '\x2', 'j', 'k', '\a', 
		'G', '\x2', '\x2', 'k', 'l', '\a', 'P', '\x2', '\x2', 'l', 'm', '\a', 
		'V', '\x2', '\x2', 'm', 'n', '\x3', '\x2', '\x2', '\x2', 'n', 'o', '\b', 
		'\b', '\x4', '\x2', 'o', '\x11', '\x3', '\x2', '\x2', '\x2', 'p', 'q', 
		'\a', '\x65', '\x2', '\x2', 'q', 'r', '\a', 'n', '\x2', '\x2', 'r', 's', 
		'\a', '\x63', '\x2', '\x2', 's', 't', '\a', 'u', '\x2', '\x2', 't', 'u', 
		'\a', 'u', '\x2', '\x2', 'u', '\x13', '\x3', '\x2', '\x2', '\x2', 'v', 
		'w', '\a', '<', '\x2', '\x2', 'w', 'x', '\x3', '\x2', '\x2', '\x2', 'x', 
		'y', '\b', '\n', '\x5', '\x2', 'y', '\x15', '\x3', '\x2', '\x2', '\x2', 
		'z', '{', '\a', '%', '\x2', '\x2', '{', '\x17', '\x3', '\x2', '\x2', '\x2', 
		'|', '}', '\a', '.', '\x2', '\x2', '}', '\x19', '\x3', '\x2', '\x2', '\x2', 
		'~', '\x7F', '\a', '*', '\x2', '\x2', '\x7F', '\x1B', '\x3', '\x2', '\x2', 
		'\x2', '\x80', '\x81', '\a', '+', '\x2', '\x2', '\x81', '\x1D', '\x3', 
		'\x2', '\x2', '\x2', '\x82', '\x83', '\a', '?', '\x2', '\x2', '\x83', 
		'\x1F', '\x3', '\x2', '\x2', '\x2', '\x84', '\x85', '\a', '\x31', '\x2', 
		'\x2', '\x85', '\x86', '\a', '\x31', '\x2', '\x2', '\x86', '\x88', '\x3', 
		'\x2', '\x2', '\x2', '\x87', '\x89', '\n', '\a', '\x2', '\x2', '\x88', 
		'\x87', '\x3', '\x2', '\x2', '\x2', '\x89', '\x8A', '\x3', '\x2', '\x2', 
		'\x2', '\x8A', '\x88', '\x3', '\x2', '\x2', '\x2', '\x8A', '\x8B', '\x3', 
		'\x2', '\x2', '\x2', '\x8B', '\x8C', '\x3', '\x2', '\x2', '\x2', '\x8C', 
		'\x8D', '\b', '\x10', '\x4', '\x2', '\x8D', '!', '\x3', '\x2', '\x2', 
		'\x2', '\x8E', '\x8F', '\a', '\x31', '\x2', '\x2', '\x8F', '\x90', '\a', 
		',', '\x2', '\x2', '\x90', '\x94', '\x3', '\x2', '\x2', '\x2', '\x91', 
		'\x93', '\v', '\x2', '\x2', '\x2', '\x92', '\x91', '\x3', '\x2', '\x2', 
		'\x2', '\x93', '\x96', '\x3', '\x2', '\x2', '\x2', '\x94', '\x95', '\x3', 
		'\x2', '\x2', '\x2', '\x94', '\x92', '\x3', '\x2', '\x2', '\x2', '\x95', 
		'\x97', '\x3', '\x2', '\x2', '\x2', '\x96', '\x94', '\x3', '\x2', '\x2', 
		'\x2', '\x97', '\x98', '\a', ',', '\x2', '\x2', '\x98', '\x99', '\a', 
		'\x31', '\x2', '\x2', '\x99', '\x9A', '\x3', '\x2', '\x2', '\x2', '\x9A', 
		'\x9B', '\b', '\x11', '\x6', '\x2', '\x9B', '#', '\x3', '\x2', '\x2', 
		'\x2', '\x9C', '\xA0', '\a', '$', '\x2', '\x2', '\x9D', '\x9F', '\v', 
		'\x2', '\x2', '\x2', '\x9E', '\x9D', '\x3', '\x2', '\x2', '\x2', '\x9F', 
		'\xA2', '\x3', '\x2', '\x2', '\x2', '\xA0', '\xA1', '\x3', '\x2', '\x2', 
		'\x2', '\xA0', '\x9E', '\x3', '\x2', '\x2', '\x2', '\xA1', '\xA3', '\x3', 
		'\x2', '\x2', '\x2', '\xA2', '\xA0', '\x3', '\x2', '\x2', '\x2', '\xA3', 
		'\xAF', '\a', '$', '\x2', '\x2', '\xA4', '\xAF', '\x5', '\x6', '\x3', 
		'\x2', '\xA5', '\xA6', '\a', 'v', '\x2', '\x2', '\xA6', '\xA7', '\a', 
		't', '\x2', '\x2', '\xA7', '\xA8', '\a', 'w', '\x2', '\x2', '\xA8', '\xAF', 
		'\a', 'g', '\x2', '\x2', '\xA9', '\xAA', '\a', 'h', '\x2', '\x2', '\xAA', 
		'\xAB', '\a', '\x63', '\x2', '\x2', '\xAB', '\xAC', '\a', 'n', '\x2', 
		'\x2', '\xAC', '\xAD', '\a', 'u', '\x2', '\x2', '\xAD', '\xAF', '\a', 
		'g', '\x2', '\x2', '\xAE', '\x9C', '\x3', '\x2', '\x2', '\x2', '\xAE', 
		'\xA4', '\x3', '\x2', '\x2', '\x2', '\xAE', '\xA5', '\x3', '\x2', '\x2', 
		'\x2', '\xAE', '\xA9', '\x3', '\x2', '\x2', '\x2', '\xAF', '%', '\x3', 
		'\x2', '\x2', '\x2', '\xB0', '\xB1', '\x5', '\x4', '\x2', '\x2', '\xB1', 
		'\'', '\x3', '\x2', '\x2', '\x2', '\xB2', '\xB4', '\t', '\b', '\x2', '\x2', 
		'\xB3', '\xB2', '\x3', '\x2', '\x2', '\x2', '\xB4', '\xB5', '\x3', '\x2', 
		'\x2', '\x2', '\xB5', '\xB3', '\x3', '\x2', '\x2', '\x2', '\xB5', '\xB6', 
		'\x3', '\x2', '\x2', '\x2', '\xB6', '\xB8', '\x3', '\x2', '\x2', '\x2', 
		'\xB7', '\xB9', '\a', '\x41', '\x2', '\x2', '\xB8', '\xB7', '\x3', '\x2', 
		'\x2', '\x2', '\xB8', '\xB9', '\x3', '\x2', '\x2', '\x2', '\xB9', '\xBC', 
		'\x3', '\x2', '\x2', '\x2', '\xBA', '\xBB', '\a', ']', '\x2', '\x2', '\xBB', 
		'\xBD', '\a', '_', '\x2', '\x2', '\xBC', '\xBA', '\x3', '\x2', '\x2', 
		'\x2', '\xBC', '\xBD', '\x3', '\x2', '\x2', '\x2', '\xBD', '\xBE', '\x3', 
		'\x2', '\x2', '\x2', '\xBE', '\xBF', '\b', '\x14', '\a', '\x2', '\xBF', 
		')', '\x3', '\x2', '\x2', '\x2', '\xC0', '\xC1', '\x5', '\b', '\x4', '\x2', 
		'\xC1', '\xC2', '\x3', '\x2', '\x2', '\x2', '\xC2', '\xC3', '\b', '\x15', 
		'\x6', '\x2', '\xC3', '+', '\x3', '\x2', '\x2', '\x2', '\x15', '\x2', 
		'\x3', '/', '\x34', '\x38', '=', '\x44', 'I', 'K', 'P', 'S', 'W', '\x8A', 
		'\x94', '\xA0', '\xAE', '\xB5', '\xB8', '\xBC', '\b', '\x3', '\x5', '\x2', 
		'\x3', '\x6', '\x3', '\x2', '\x3', '\x2', '\a', '\x3', '\x2', '\b', '\x2', 
		'\x2', '\x6', '\x2', '\x2',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
