using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Parsing.Annotations
{
    public interface IAnnotationParamParser
    {
        AnnotationParam ParseAnnotationParam(string name, string valueString);
    }
}