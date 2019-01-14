using System.Collections.Generic;
using WebAppsGenerator.Core.Models;

namespace WebAppsGenerator.Core.Interfaces
{
    /// <summary>
    /// Provides business model parsed from input files given by user.
    /// </summary>
    public interface IInputModelProvider
    {
        IEnumerable<Entity> CreateModel();
    }
}