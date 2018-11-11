using System;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class InvalidAnnotationForPropTypeException : Exception
    {
        public InvalidAnnotationForPropTypeException(string annName, TypeKind propTypeKind)
            : base($"Invalid annotation for given property type. Annotation {annName} cannot be used for properties of type {propTypeKind:G}")
        {

        }
    }
}