using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class UnknownPropAnnotationException : ParsingException
    {
        public UnknownPropAnnotationException(string annName, Annotation annotation) 
            : base($"Unrecognized property annotation: {annName}", annotation)
        {
        }
    }
}