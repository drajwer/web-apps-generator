using System;

namespace WebAppsGenerator.Core.Exceptions
{
    public class IdPropExistsException : Exception
    {
        public IdPropExistsException()
            : base("Using \"Id\" and \"id\" as property name is forbidden. Identifier property will be added automagically in generated entity")
        {
        }
    }
}
