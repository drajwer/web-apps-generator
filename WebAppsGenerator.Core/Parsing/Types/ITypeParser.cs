using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Parsing.Types
{
    public interface ITypeParser
    {
        Type ParseTypeName(string typeName, int lineNo, int charNo);
    }
}