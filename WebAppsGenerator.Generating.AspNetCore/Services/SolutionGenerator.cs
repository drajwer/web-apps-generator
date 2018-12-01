using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generator.Generator;
using WebAppsGenerator.Generator.Options;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class SolutionGenerator : BaseGenerator
    {
        private readonly CommandLineService _commandLineService;
        public IConfiguration Configuration { get; set; }
        public AnnotationOptions AnnotationOptions { get; set; }
        public EntityGenerator EntityGenerator { get; set; }

        public SolutionGenerator(IGeneratorConfiguration generatorConfiguration) : base(generatorConfiguration)
        {
            _commandLineService = new CommandLineService();
            AddConfiguration();
            AnnotationOptions = new AnnotationOptions();
            Configuration.Bind("AllowedAnnotations", AnnotationOptions);
            // pass annotation options to validator
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
            _commandLineService.RunCommand("mkdir Output");
            _commandLineService.RunCommand($"dotnet new webapi -n {_generatorConfiguration.ProjectName} -o {_generatorConfiguration.OutputPath}");
        }

        private void AddConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");

            Configuration = builder.Build();
        }
    }
}
