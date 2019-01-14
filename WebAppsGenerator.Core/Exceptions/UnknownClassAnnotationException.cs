using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class UnknownClassAnnotationException : ParsingException
    {
        public UnknownClassAnnotationException(string annName, Annotation annotation)
            : base($"Unrecognized class annotation: {annName}", annotation)
        {
        }
    }
}