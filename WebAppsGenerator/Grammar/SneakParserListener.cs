//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.7.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from .\SneakParser.g4 by ANTLR 4.7.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="SneakParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.7.1")]
[System.CLSCompliant(false)]
public interface ISneakParserListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="SneakParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompileUnit([NotNull] SneakParser.CompileUnitContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SneakParser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompileUnit([NotNull] SneakParser.CompileUnitContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SneakParser.class"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClass([NotNull] SneakParser.ClassContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SneakParser.class"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClass([NotNull] SneakParser.ClassContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SneakParser.property"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProperty([NotNull] SneakParser.PropertyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SneakParser.property"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProperty([NotNull] SneakParser.PropertyContext context);
}
