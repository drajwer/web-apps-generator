using System.Linq;
using WebAppsGenerator.Core.Helpers;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Console
{
    public class Application
    {
        private readonly IInputModelProvider _modelProvider;
        private readonly RootValidator _validator;
        private readonly RootGenerator _generator;
        private readonly QueuedExceptionHandler _exceptionHandler;

        public Application(IInputModelProvider modelProvider, RootValidator validator, RootGenerator generator, QueuedExceptionHandler exceptionHandler)
        {
            _modelProvider = modelProvider;
            _validator = validator;
            _generator = generator;
            _exceptionHandler = exceptionHandler;
        }

        public void Run()
        {
            ConsoleHelper.WriteInfo("Start building...");
            var entities = _modelProvider.CreateModel();
            _validator.Validate(entities);

            if (_exceptionHandler.ParsingExceptions.Any())
            {
                ConsoleHelper.WriteInfo("Build failed. See errors below.");
                _exceptionHandler.WriteExceptions();

                return;
            }

            ConsoleHelper.WriteInfo("Build succeeded.");
            ConsoleHelper.WriteInfo("Start generating...");
   
            _generator.Generate(entities);
            ConsoleHelper.WriteInfo("Generating finished.");
        }
    }
}
