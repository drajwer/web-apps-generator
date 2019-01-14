using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class AmbigiousAnnotationException : ParsingException
    {
        public AmbigiousAnnotationException(string annotationName, Annotation annotation)
            : base($"Ambiguity between annotations. More than one annotation definition matches {annotationName} annotation", annotation)
        {
        }
    }
}
