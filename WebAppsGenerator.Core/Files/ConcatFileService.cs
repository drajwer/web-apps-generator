using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAppsGenerator.Core.Files.Providers;

namespace WebAppsGenerator.Core.Files
{
    public class ConcatFileService
    {
        private readonly List<FileInfo> _files;
        public ConcatFileService(IFilesProvider filesProvider)
        {
            ConcatFile = "";
            _files = new List<FileInfo>();
            var fileNames = filesProvider.GetFiles();

            foreach (var file in fileNames)
            {
                var lines = File.ReadLines(file).Count() + 1;
                var content = File.ReadAllText(file);

                ConcatFile += content + "\r\n";
                _files.Add(new FileInfo(lines, file));
            }
        }

        public string ConcatFile { get; }
        public LineInfo GetLineInfo(int concatFileLineNo)
        {
            int lineCounter = 0;
            foreach (var fileInfo in _files)
            {
                if (lineCounter + fileInfo.Lines > concatFileLineNo)
                {
                    int lineNo = concatFileLineNo - lineCounter;
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
