using Antlr4.Runtime;
using WebAppsGenerator.Core.Files.FileSrevices;

namespace WebAppsGenerator.Core.Grammar.ErrorListeners
{
    public class SneakParserErrorListener: SneakBaseErrorListener<IToken>
    {
        public SneakParserErrorListener(IFileService fileService): base(fileService)
        {
        }
    }
}