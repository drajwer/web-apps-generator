using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generator.Generator;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class SolutionGenerator : BaseGenerator
    {
        private readonly CommandLineService _commandLineService;

        public SolutionGenerator(IGeneratorConfiguration generatorConfiguration) : base(generatorConfiguration)
        {
            _commandLineService = new CommandLineService();
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
            _commandLineService.RunCommand("mkdir Output");
            _commandLineService.RunCommand($"dotnet new webapi -n {_generatorConfiguration.ProjectName} -o {_generatorConfiguration.OutputPath}");
        }
    }
}
