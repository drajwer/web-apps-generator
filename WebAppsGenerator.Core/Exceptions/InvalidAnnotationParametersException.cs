using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class InvalidAnnotationParametersException : ParsingException
    {
        public InvalidAnnotationParametersException(string annotationName, Annotation annotation) 
            : base($"None of definitions of {annotationName} annotation matches provided parameters." +
                   "Make sure all required parameters are present and every parameter's value has proper type", annotation)
        {
        }
    }
}
