using System.Collections.Generic;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Generator.Validation
{
    public interface IValidator
    {
        void ValidateTypes(IEnumerable<Entity> entities);
        void ValidateAnnotations(IEnumerable<Entity> entities, List<AnnotationDefinition> allowedAnnotations);
    }
}
