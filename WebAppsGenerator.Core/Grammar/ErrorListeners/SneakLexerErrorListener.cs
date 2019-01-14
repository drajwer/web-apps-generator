using WebAppsGenerator.Core.Interfaces;

namespace WebAppsGenerator.Core.Grammar.ErrorListeners
{
    public class SneakLexerErrorListener: SneakBaseErrorListener<int>
    {
        public SneakLexerErrorListener(IExceptionHandler exceptionHandler) : base(exceptionHandler)
        {
        }
    }
}