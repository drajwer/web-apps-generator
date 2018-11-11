using System;

namespace WebAppsGenerator.Core.Exceptions
{
    public class UnknownClassAnnotationException : Exception
    {
        public UnknownClassAnnotationException(string annName) : base($"Unrecognized class annotation: {annName}")
        {
        }
    }
}