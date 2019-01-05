using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Core.Exceptions
{
    public class MultiplePropException : ParsingException
    {
        public MultiplePropException(string propName, string className) : base($"Property '{propName}' already exists in class '{className}'")
        {
        }
    }
}
