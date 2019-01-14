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
        private readonly ParsingExceptionWriter _exceptionWriter;
        public Queue<ParsingException> ParsingExceptions { get; set; }

        public QueuedExceptionHandler(ParsingExceptionWriter exceptionWriter)
        {
            _exceptionWriter = exceptionWriter;
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

        /// <summary>
        /// Writes all queued exceptions and flushes the queue.
        /// </summary>
        public void WriteExceptions()
        {
            _exceptionWriter.WriteExceptions(ParsingExceptions);
            ParsingExceptions = new Queue<ParsingException>();
        }
    }
}
