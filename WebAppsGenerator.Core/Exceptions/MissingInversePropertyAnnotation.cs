using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Exceptions
{
    public class MissingInversePropertyAnnotation : ParsingException
    {
        public MissingInversePropertyAnnotation(string referencedEntityName, BaseModel model)
            : base($"Cannot find corresponding field in class {referencedEntityName} to configure relationship", model)
        {
        }
    }
}
