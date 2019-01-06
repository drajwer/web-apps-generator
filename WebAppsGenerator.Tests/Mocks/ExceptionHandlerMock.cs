using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppsGenerator.Core.Interfaces;

namespace WebAppsGenerator.Tests.Mocks
{
    class ExceptionHandlerMock : IExceptionHandler
    {
        public List<Exception> Exceptions { get; }

        public ExceptionHandlerMock()
        {
            Exceptions = new List<Exception>();
        }

        public void ThrowException(Exception e)
        {
            Exceptions.Add(e);
        }

        public bool IsExceptionThrown<T>() => IsExceptionThrown(typeof(T));
        public bool IsExceptionThrown(Type exceptionType)
        {
            return Exceptions.Any(e => e.GetType().IsEquivalentTo(exceptionType));
        }
    }
}
