using System.Collections.Generic;
using System.Linq;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Services.Validators
{
    /// <inheritdoc />
    /// <summary>
    /// Validates if all properties are unique within entity 
    /// and no entity contains Id nor id property.
    /// </summary>
    public class PropertyValidator : IValidator
    {
        private readonly IExceptionHandler _exceptionHandler;

        public PropertyValidator(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public void Validate(IEnumerable<Entity> entities)
        {
            ValidateUniqueness(entities);
            ValidateIdExistence(entities);
        }

        /// <summary>
        /// Validates if all properties are unique.
        /// </summary>
        /// <param name="entities"></param>
        public void ValidateUniqueness(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var dict = new HashSet<string>();
                foreach (var entityField in entity.Fields)
                {
                    if (dict.Contains(entityField.Name))
                        _exceptionHandler.ThrowException(new MultiplePropException(entityField, entity.Name));
                    dict.Add(entityField.Name);
                }
            }
        }

        /// <summary>
        /// Validates if no entity contains property Id nor id.
        /// </summary>
        /// <param name="entities"></param>
        private void ValidateIdExistence(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                foreach (var entityField in entity.Fields)
                {
                    if (entityField.Name.ToLowerInvariant() == "id")
                        _exceptionHandler.ThrowException(new IdPropExistsException(entityField));
                }
            }
        }
    }
}
