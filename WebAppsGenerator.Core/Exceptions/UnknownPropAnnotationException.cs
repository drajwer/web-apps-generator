using System;

namespace WebAppsGenerator.Core.Exceptions
{
    public class UnknownPropAnnotationException : Exception
    {
        public UnknownPropAnnotationException(string annName) : base($"Unrecognized property annotation: {annName}")
        {
        }
    }
}