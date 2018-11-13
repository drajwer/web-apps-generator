using System.IO;
using Antlr4.Runtime;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Files.FileSrevices;

namespace WebAppsGenerator.Core.Grammar.ErrorListeners
{
    public class SneakBaseErrorListener<T>: IAntlrErrorListener<T>
    {
        protected readonly IFileService FileService;

        public SneakBaseErrorListener(IFileService fileService)
        {
            FileService = fileService;
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, T offendingSymbol, int line, int charPositionInLine,
            string msg, RecognitionException e)
        {
            var lineInfo = FileService.GetLineInfo(line);

            throw new ParsingException($"File {lineInfo.FileName}, line {lineInfo.LineNumber}: {charPositionInLine} + {msg}");
        }
    }
}