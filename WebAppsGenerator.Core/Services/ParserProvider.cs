using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using WebAppsGenerator.Core.Files.FileSrevices;
using WebAppsGenerator.Core.Grammar.ErrorListeners;

namespace WebAppsGenerator.Core.Services
{
    public class ParserProvider
    {
        private readonly LexerProvider _lexerProvider;
        private readonly ConcatFileService _concatFileService;

        public ParserProvider(LexerProvider lexerProvider, ConcatFileService concatFileService)
        {
            _lexerProvider = lexerProvider;
            _concatFileService = concatFileService;
        }

        public SneakParser CreateParser()
        {
            var lexer = _lexerProvider.CreateLexer();
            var commonTokenStream = new CommonTokenStream(lexer);

            var parser = new SneakParser(commonTokenStream);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(new SneakParserErrorListener(_concatFileService));

            return parser;
        }
    }
}
