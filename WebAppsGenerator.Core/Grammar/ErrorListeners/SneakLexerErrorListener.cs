using WebAppsGenerator.Core.Files.FileSrevices;

namespace WebAppsGenerator.Core.Grammar.ErrorListeners
{
    public class SneakLexerErrorListener: SneakBaseErrorListener<int>
    {
        public SneakLexerErrorListener(IFileService fileService) : base(fileService)
        {
        }
    }
}