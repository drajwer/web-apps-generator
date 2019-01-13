
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class InvalidTypeParsingException : ParsingException
    {
        public InvalidTypeParsingException(Type type) : base($"Invalid type: {type.FullTypeName}", type)
        {
        }
    }
}
