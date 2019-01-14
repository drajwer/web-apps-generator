using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class MultiplePropException : ParsingException
    {
        public MultiplePropException(Field field, string className) : base($"Property '{field.Name}' already exists in class '{className}'", field)
        {
        }
    }
}
