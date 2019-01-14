using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class InvalidInversePropertyArgumentException : ParsingException
    {
        public InvalidInversePropertyArgumentException(Annotation model) 
            : base("Invalid Name parameter value in InverseProperty annotation.", model)
        {
        }
    }
}
