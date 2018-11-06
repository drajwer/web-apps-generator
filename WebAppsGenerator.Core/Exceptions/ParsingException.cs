using System;

namespace WebAppsGenerator.Core.Exceptions
{
    public class ParsingException : Exception
    {
        public ParsingException(string message) : base(message)
        {
        }
    }
}
