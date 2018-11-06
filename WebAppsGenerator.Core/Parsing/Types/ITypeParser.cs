using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Parsing.Types
{
    public interface ITypeParser
    {
        bool IsSimpleType(string type);
        Type ParseTypeName(string typeName);
    }
}