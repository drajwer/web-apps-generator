using System.Collections.Generic;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Services.Validators
{
    /// <summary>
    /// Validates if all properties are unique.
    /// </summary>
    public class PropertyValidator : IValidator
    {
        private IExceptionHandler _exceptionHandler;

        public PropertyValidator(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public void Validate(IEnumerable<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var dict = new HashSet<string>();
                foreach (var entityField in entity.Fields)
                {
                    if (dict.Contains(entityField.Name))
                        _exceptionHandler.ThrowException(new MultiplePropException(entityField.Name, entity.Name));
                    dict.Add(entityField.Name);
                }
            }
        }
    }
}
