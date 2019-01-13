using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Interfaces;

namespace WebAppsGenerator.Core.Services
{
    /// <inheritdoc/>
    /// <summary>
    /// Service that queues <see cref="ParsingException"/> for further processing later.
    /// </summary>
    public class QueuedExceptionHandler : IExceptionHandler
    {
        public Queue<ParsingException> ParsingExceptions { get; set; }

        public QueuedExceptionHandler()
        {
            ParsingExceptions = new Queue<ParsingException>();
        }

        public void ThrowException(Exception e)
        {
            try
            {
                throw e;
            }
            catch (ParsingException exception)
            {
                ParsingExceptions.Enqueue(exception);
            }
        }

        public void WriteExceptions()
        {
            foreach (var exception in ParsingExceptions)
            {
                Console.WriteLine($"ERROR: ({exception.LineNumber}, {exception.CharPositionInLine}): {exception.Message}");
            }
        }
    }
}
