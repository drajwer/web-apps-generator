using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Exceptions;

namespace WebAppsGenerator.Generator.Validation
{
    public class Validator : IValidator
    {
        public void ValidateAnnotations(IEnumerable<Entity> entities)
        {
            throw new NotImplementedException();
        }

        public void ValidateTypes(IEnumerable<Entity> entities)
        {
            var declaredClasses = entities.Select(e => e.Name);

            foreach(var entity in entities)
            {
                foreach(var prop in entity.Fields)
                {
                    if (prop.Type.BaseTypeKind == TypeKind.Entity && !declaredClasses.Contains(prop.Type.EntityName))
                        throw new UnknownTypeException(prop.Type.EntityName);
                }
            }
        }
    }
}
