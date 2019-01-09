using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Console
{
    public class Application
    {
        private readonly IInputModelProvider _modelProvider;
        private readonly RootValidator _validator;
        private readonly RootGenerator _generator;

        public Application(IInputModelProvider modelProvider, RootValidator validator, RootGenerator generator)
        {
            _modelProvider = modelProvider;
            _validator = validator;
            _generator = generator;
        }

        public void Run()
        {
            var entities = _modelProvider.CreateModel();
            _validator.Validate(entities);
            _generator.Generate(entities);
        }
    }
}
