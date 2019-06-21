using Antlr4.Runtime;
using WebAppsGenerator.Core.Files.Services;
using WebAppsGenerator.Core.Grammar.ErrorListeners;
using WebAppsGenerator.Core.Interfaces;

namespace WebAppsGenerator.Core.Services
{
    /// <summary>
    /// Builds lexer based on input files
    /// </summary>
    public class LexerProvider
    {
        private readonly ConcatFileService _concatFileService;
        private readonly IExceptionHandler _exceptionHandler;

        public LexerProvider(ConcatFileService concatFileService, IExceptionHandler exceptionHandler)
        {
            _concatFileService = concatFileService;
            _exceptionHandler = exceptionHandler;
        }

        public SneakLexer CreateLexer()
        {
            var inputStream = new AntlrInputStream(_concatFileService.ConcatFile);
            var lexer = new SneakLexer(inputStream);

            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(new SneakLexerErrorListener(_exceptionHandler));

            return lexer;
        }
    }
}
