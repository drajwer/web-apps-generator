using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class UnknownTypeException: ParsingException
    {
        public UnknownTypeException(Type type): base($"Type {type.EntityName} is not declared", type)
        {
        }
    }
}
