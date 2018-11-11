using System;

namespace WebAppsGenerator.Core.Exceptions
{
    public class UnknownAnnotationParameterException: Exception
    {
        public UnknownAnnotationParameterException(string paramName, string annName)
            : base($"Unknown parameter in annotation {annName}: {paramName}")
        {
            
        }
    }
}