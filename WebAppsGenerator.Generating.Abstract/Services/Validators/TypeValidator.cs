using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Services.Validators
{
    /// <summary>
    /// Validates all <see cref="TypeKind.Entity"/> types are pointing to existing entities
    /// </summary>
    public class TypeValidator : IValidator
    {
        private readonly IExceptionHandler _exceptionHandler;

        public TypeValidator(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }
        public void Validate(IEnumerable<Entity> entities)
        {
            entities = entities.ToList();
            var declaredClasses = entities.Select(e => e.Name).ToList();

            foreach (var entity in entities)
            {
                foreach (var prop in entity.Fields)
                {
                    if (prop.Type.BaseTypeKind == TypeKind.Entity && !declaredClasses.Contains(prop.Type.EntityName))
                        _exceptionHandler.ThrowException(new UnknownTypeException(prop.Type));
                }
            }
        }
    }
}
