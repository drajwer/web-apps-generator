using System;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class IdPropExistsException : ParsingException
    {
        public IdPropExistsException(Field field)
            : base("Using \"Id\" and \"id\" as property name is forbidden. Identifier property will be added automagically in generated entity", field)
        {
        }
    }
}
