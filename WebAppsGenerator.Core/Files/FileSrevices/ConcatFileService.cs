using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAppsGenerator.Core.Files.Providers;

namespace WebAppsGenerator.Core.Files.FileSrevices
{
    public class ConcatFileService : IFileService
    {
        private readonly List<FileInfo> _files;
        public ConcatFileService(IFilesProvider filesProvider)
        {
            ConcatFile = "";
            _files = new List<FileInfo>();
            var fileNames = filesProvider.GetFiles();

            foreach (var file in fileNames)
            {
                var lines = File.ReadLines(file);
                var content = File.ReadAllText(file);
                // if the file ends with \r\n, there is an empty line at the end that ReadLines() skips but we need to count it
                var linesCount = content.EndsWith("\r\n") ? lines.Count() + 1 : lines.Count();

                ConcatFile += content + "\r\n";
                _files.Add(new FileInfo(linesCount, file));
            }
        }

        public string ConcatFile { get; }
        public LineInfo GetLineInfo(int concatFileLineNo)
        {
            var lineCounter = 0;
            foreach (var fileInfo in _files)
            {
                if (lineCounter + fileInfo.Lines > concatFileLineNo)
                {
                    var lineNo = concatFileLineNo - lineCounter;
                    return new LineInfo()
                    {
                        FileName = fileInfo.FilePath,
                        LineNumber = lineNo
                    };
                }

                lineCounter += fileInfo.Lines;
            }
            throw new ArgumentException($"Concatenated file has less lines than given line number {concatFileLineNo}");
        }
    }


    struct FileInfo
    {
        public int Lines { get; }
        public string FilePath { get; }

        public FileInfo(int lines, string filePath)
        {
            Lines = lines;
            FilePath = filePath;
        }
    }
}
