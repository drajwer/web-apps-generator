using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Core.Interfaces
{
    /// <summary>
    /// Provides gathering exception for processing.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Add exception for processing.
        /// </summary>
        /// <param name="e">Exception to be processed</param>
        void ThrowException(Exception e);
    }
}
