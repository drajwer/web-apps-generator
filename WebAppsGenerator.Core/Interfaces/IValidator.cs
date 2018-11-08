using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Interfaces
{
    public interface IValidator
    {
        void ValidateTypes(IEnumerable<Entity> entities);
        void ValidateAnnotations(IEnumerable<Entity> entities);
    }
}
