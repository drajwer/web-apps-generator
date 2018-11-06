using System;

namespace WebAppsGenerator.Core.Exceptions
{
    public class InvalidTypeParsingException : Exception
    {
        public InvalidTypeParsingException(string typeName) : base($"Invalid type: {typeName}")
        {
        }
    }
}
