using System.Collections.Generic;
using Microsoft.Extensions.Options;
using WebAppsGenerator.Core.Exceptions;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Generating.Abstract.Options
{
    /// <summary>
    /// Default implementation of <see cref="IGeneratorConfiguration"/>
    /// </summary>
    public class GeneratorConfiguration : IGeneratorConfiguration
    {
        public string ProjectName { get; set; }
        public string OutputPath { get; set; }
        public string MigrationName { get; set; }
        public bool AddMigration { get; set; }
        public bool RunAspNetCoreGen { get; set; }
        public bool RunWebUiGen { get; set; }

        public GeneratorConfiguration(IOptions<GeneratorOptions> generatorOptions, IExceptionHandler exceptionHandler)
        {
            var options = generatorOptions.Value;
            ProjectName = options.ProjectName;
            OutputPath = options.OutputPath;
            MigrationName = options.MigrationName;
            AddMigration = options.AddMigration;
            RunAspNetCoreGen = options.RunAspNetCoreGen;
            RunWebUiGen = options.RunWebUiGen;

            if (ProjectName == null)
                exceptionHandler.ThrowException(new ParsingException("ProjectName must be specified in the config.", -1, -1));
            if (OutputPath == null)
                exceptionHandler.ThrowException(new ParsingException("OutputPath must be specified in the config.", -1, -1));
        }
    }
}
