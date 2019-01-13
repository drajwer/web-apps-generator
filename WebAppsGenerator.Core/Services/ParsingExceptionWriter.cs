using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Files.Services;
using WebAppsGenerator.Core.Helpers;

namespace WebAppsGenerator.Core.Services
{
    /// <summary>
    /// Writes exception details to specific stream.
    /// </summary>
    public class ParsingExceptionWriter
    {
        private readonly TextWriter _writer;
        private readonly bool _usingConsoleOut;
        private readonly ConcatFileService _fileService;

        public ParsingExceptionWriter(ConcatFileService fileService, TextWriter outStream = null)
        {
            _fileService = fileService;

            _usingConsoleOut = outStream == null;
            _writer = outStream;
        }

        public void WriteExceptions(IEnumerable<ParsingException> exceptions)
        {
            foreach (var parsingException in exceptions)
            {
                WriteException(parsingException);
            }
        }

        private void WriteException(ParsingException exception)
        {
            var lineInfo = _fileService.GetLineInfo(exception.LineNumber);
            var errorMsg = $"ERROR in {lineInfo.FileName}, line: {lineInfo.LineNumber}, char: {exception.CharPositionInLine}: {exception.Message}";

            if(_writer != null)
                _writer.WriteLine(errorMsg);
            else
                ConsoleHelper.WriteError(errorMsg);
        }
    }
}
