using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Core.Models
{
    public class BaseModel
    {
        public int LineNumber { get; set; }
        public int CharPositionInLine { get; set; }

        public BaseModel(BaseModel model)
        {
            LineNumber = model.LineNumber;
            CharPositionInLine = model.CharPositionInLine;
        }

        public BaseModel(int lineNumber, int charPositionInLine)
        {
            LineNumber = lineNumber;
            CharPositionInLine = charPositionInLine;
        }
    }
}
