using System.Collections.Generic;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    /// <summary>
    /// Root implementation of <see cref="IValidator"/>. Runs all received validators.
    /// </summary>
    public class RootValidator : IValidator
    {
        private readonly IEnumerable<IValidator> _validators;

        public RootValidator(IEnumerable<IValidator> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Validates types and annotations for given entities
        /// </summary>
        public void Validate(IEnumerable<Entity> entities)
        {
            foreach (var validator in _validators)
            {
                validator.Validate(entities);
            }
        }
    }
}
