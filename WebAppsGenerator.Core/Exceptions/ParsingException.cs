using System;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class ParsingException : Exception
    {
        public int LineNumber { get; }
        public int CharPositionInLine { get; }

        public ParsingException(string message, int lineNumber, int charPositionInLine): base(message)
        {
            LineNumber = lineNumber;
            CharPositionInLine = charPositionInLine;
        }

        public ParsingException(string message, BaseModel model) : base(message)
        {
            LineNumber = model.LineNumber;
            CharPositionInLine = model.CharPositionInLine;
        }
    }
}
