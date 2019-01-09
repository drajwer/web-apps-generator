using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using WebAppsGenerator.Core.Files.FileSrevices;
using WebAppsGenerator.Core.Grammar.ErrorListeners;

namespace WebAppsGenerator.Core.Services
{
    public class LexerProvider
    {
        private readonly ConcatFileService _concatFileService;

        public LexerProvider(ConcatFileService concatFileService)
        {
            _concatFileService = concatFileService;
        }

        public SneakLexer CreateLexer()
        {
            var inputStream = new AntlrInputStream(_concatFileService.ConcatFile);
            var lexer = new SneakLexer(inputStream);

            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new SneakLexerErrorListener(_concatFileService));

            return lexer;
        }
    }
}
