using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Interfaces;

namespace WebAppsGenerator.Core.Services
{
    /// <summary>
    /// Service that throws exception immediately after receiving.
    /// </summary>
    public class ImmediateExceptionHandler : IExceptionHandler
    {
        public void ThrowException(Exception e)
        {
            throw e;
        }
    }
}
