using System;
using System.Collections.Generic;
using System.Text;

namespace WebAppsGenerator.Core.Interfaces
{
    /// <summary>
    /// Provides gathering exception for later processing.
    /// </summary>
    public interface IExceptionHandler
    {
        /// <summary>
        /// Add exception for later processing.
        /// </summary>
        /// <param name="e"></param>
        void ThrowException(Exception e);
    }
}
