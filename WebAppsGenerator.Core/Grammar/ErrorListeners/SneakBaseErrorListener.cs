using System.IO;
using Antlr4.Runtime;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Files.Services;
using WebAppsGenerator.Core.Interfaces;

namespace WebAppsGenerator.Core.Grammar.ErrorListeners
{
    public class SneakBaseErrorListener<T>: IAntlrErrorListener<T>
    {
        private readonly IExceptionHandler _exceptionHandler;

        public SneakBaseErrorListener(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, T offendingSymbol, int line, int charPositionInLine,
            string msg, RecognitionException e)
        {
            _exceptionHandler.ThrowException(new ParsingException(msg, line, charPositionInLine));
        }
    }
}