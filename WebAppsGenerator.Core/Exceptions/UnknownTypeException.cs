using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Core.Exceptions
{
    public class UnknownTypeException: Exception
    {
        public UnknownTypeException(string type): base($"Type {type} is not declared")
        {
        }
    }
}
