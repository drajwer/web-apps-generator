using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class InvalidAnnotationForPropTypeException : ParsingException
    {
        public InvalidAnnotationForPropTypeException(string annName, TypeKind propTypeKind, Annotation annotation)
            : base($"Invalid annotation for given property type. Annotation {annName} cannot be " +
                   $"used for properties of type {propTypeKind:G}", annotation)
        {

        }
    }
}