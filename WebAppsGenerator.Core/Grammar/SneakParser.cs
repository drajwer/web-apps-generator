//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\drejerk\source\repos\WebAppsGenerator\WebAppsGenerator.Core\Grammar\SneakParser.g4 by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace WebAppsGenerator.Core.Grammar {
using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public partial class SneakParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		INDENT=1, DEDENT=2, WS=3, CLASS=4, ID=5, COLON=6, TYPE=7, NEWLINE=8;
	public const int
		RULE_compileUnit = 0, RULE_class = 1, RULE_property = 2;
	public static readonly string[] ruleNames = {
		"compileUnit", "class", "property"
	};

	private static readonly string[] _LiteralNames = {
		null, "'\t'", null, "' '", "'class'", null, "':'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "INDENT", "DEDENT", "WS", "CLASS", "ID", "COLON", "TYPE", "NEWLINE"
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

	public override string GrammarFileName { get { return "SneakParser.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static SneakParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public SneakParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public SneakParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}
	public partial class CompileUnitContext : ParserRuleContext {
		public ITerminalNode Eof() { return GetToken(SneakParser.Eof, 0); }
		public CompileUnitContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_compileUnit; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISneakParserListener typedListener = listener as ISneakParserListener;
			if (typedListener != null) typedListener.EnterCompileUnit(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISneakParserListener typedListener = listener as ISneakParserListener;
			if (typedListener != null) typedListener.ExitCompileUnit(this);
		}
	}

	[RuleVersion(0)]
	public CompileUnitContext compileUnit() {
		CompileUnitContext _localctx = new CompileUnitContext(Context, State);
		EnterRule(_localctx, 0, RULE_compileUnit);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 6; Match(Eof);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ClassContext : ParserRuleContext {
		public ITerminalNode CLASS() { return GetToken(SneakParser.CLASS, 0); }
		public ITerminalNode ID() { return GetToken(SneakParser.ID, 0); }
		public ITerminalNode NEWLINE() { return GetToken(SneakParser.NEWLINE, 0); }
		public PropertyContext[] property() {
			return GetRuleContexts<PropertyContext>();
		}
		public PropertyContext property(int i) {
			return GetRuleContext<PropertyContext>(i);
		}
		public ClassContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_class; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISneakParserListener typedListener = listener as ISneakParserListener;
			if (typedListener != null) typedListener.EnterClass(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISneakParserListener typedListener = listener as ISneakParserListener;
			if (typedListener != null) typedListener.ExitClass(this);
		}
	}

	[RuleVersion(0)]
	public ClassContext @class() {
		ClassContext _localctx = new ClassContext(Context, State);
		EnterRule(_localctx, 2, RULE_class);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 8; Match(CLASS);
			State = 9; Match(ID);
			State = 10; Match(NEWLINE);
			State = 14;
			ErrorHandler.Sync(this);
			_la = TokenStream.LA(1);
			while (_la==ID) {
				{
				{
				State = 11; property();
				}
				}
				State = 16;
				ErrorHandler.Sync(this);
				_la = TokenStream.LA(1);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class PropertyContext : ParserRuleContext {
		public ITerminalNode ID() { return GetToken(SneakParser.ID, 0); }
		public ITerminalNode COLON() { return GetToken(SneakParser.COLON, 0); }
		public ITerminalNode TYPE() { return GetToken(SneakParser.TYPE, 0); }
		public ITerminalNode NEWLINE() { return GetToken(SneakParser.NEWLINE, 0); }
		public PropertyContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_property; } }
		public override void EnterRule(IParseTreeListener listener) {
			ISneakParserListener typedListener = listener as ISneakParserListener;
			if (typedListener != null) typedListener.EnterProperty(this);
		}
		public override void ExitRule(IParseTreeListener listener) {
			ISneakParserListener typedListener = listener as ISneakParserListener;
			if (typedListener != null) typedListener.ExitProperty(this);
		}
	}

	[RuleVersion(0)]
	public PropertyContext property() {
		PropertyContext _localctx = new PropertyContext(Context, State);
		EnterRule(_localctx, 4, RULE_property);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 17; Match(ID);
			State = 18; Match(COLON);
			State = 19; Match(TYPE);
			State = 20; Match(NEWLINE);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x3', '\n', '\x19', '\x4', '\x2', '\t', '\x2', '\x4', '\x3', 
		'\t', '\x3', '\x4', '\x4', '\t', '\x4', '\x3', '\x2', '\x3', '\x2', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\a', '\x3', '\xF', '\n', 
		'\x3', '\f', '\x3', '\xE', '\x3', '\x12', '\v', '\x3', '\x3', '\x4', '\x3', 
		'\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x3', '\x4', '\x2', 
		'\x2', '\x5', '\x2', '\x4', '\x6', '\x2', '\x2', '\x2', '\x16', '\x2', 
		'\b', '\x3', '\x2', '\x2', '\x2', '\x4', '\n', '\x3', '\x2', '\x2', '\x2', 
		'\x6', '\x13', '\x3', '\x2', '\x2', '\x2', '\b', '\t', '\a', '\x2', '\x2', 
		'\x3', '\t', '\x3', '\x3', '\x2', '\x2', '\x2', '\n', '\v', '\a', '\x6', 
		'\x2', '\x2', '\v', '\f', '\a', '\a', '\x2', '\x2', '\f', '\x10', '\a', 
		'\n', '\x2', '\x2', '\r', '\xF', '\x5', '\x6', '\x4', '\x2', '\xE', '\r', 
		'\x3', '\x2', '\x2', '\x2', '\xF', '\x12', '\x3', '\x2', '\x2', '\x2', 
		'\x10', '\xE', '\x3', '\x2', '\x2', '\x2', '\x10', '\x11', '\x3', '\x2', 
		'\x2', '\x2', '\x11', '\x5', '\x3', '\x2', '\x2', '\x2', '\x12', '\x10', 
		'\x3', '\x2', '\x2', '\x2', '\x13', '\x14', '\a', '\a', '\x2', '\x2', 
		'\x14', '\x15', '\a', '\b', '\x2', '\x2', '\x15', '\x16', '\a', '\t', 
		'\x2', '\x2', '\x16', '\x17', '\a', '\n', '\x2', '\x2', '\x17', '\a', 
		'\x3', '\x2', '\x2', '\x2', '\x3', '\x10',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}
} // namespace WebAppsGenerator.Core.Grammar