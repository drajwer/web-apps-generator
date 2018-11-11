using System;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class InvalidAnnotationParameterValueTypeException : Exception
    {
        public InvalidAnnotationParameterValueTypeException(string annName, string paramName, TypeKind actualType, TypeKind expectedType)
            : base($"Invalid value type of parameter {paramName} in annotation {annName}. Actual: {actualType:G}, expected: {expectedType:G}")
        {

        }
    }
}