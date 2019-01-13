using Antlr4.Runtime;
using WebAppsGenerator.Core.Files.Services;
using WebAppsGenerator.Core.Interfaces;

namespace WebAppsGenerator.Core.Grammar.ErrorListeners
{
    public class SneakParserErrorListener: SneakBaseErrorListener<IToken>
    {
        public SneakParserErrorListener(IExceptionHandler exceptionHandler) : base(exceptionHandler)
        {
        }
    }
}