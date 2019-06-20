using System;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    /// <summary>
    /// Base exception for errors in Sneak source code.
    /// Provides information about line and position of error
    /// </summary>
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
