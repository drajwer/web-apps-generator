using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using WebAppsGenerator.Core.Files.Services;
using WebAppsGenerator.Core.Grammar.ErrorListeners;
using WebAppsGenerator.Core.Interfaces;

namespace WebAppsGenerator.Core.Services
{
    public class ParserProvider
    {
        private readonly LexerProvider _lexerProvider;
        private readonly IExceptionHandler _exceptionHandler;

        public ParserProvider(LexerProvider lexerProvider, IExceptionHandler exceptionHandler)
        {
            _lexerProvider = lexerProvider;
            _exceptionHandler = exceptionHandler;
        }

        public SneakParser CreateParser()
        {
            var lexer = _lexerProvider.CreateLexer();
            var commonTokenStream = new CommonTokenStream(lexer);

            var parser = new SneakParser(commonTokenStream);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new SneakParserErrorListener(_exceptionHandler));

            return parser;
        }
    }
}
