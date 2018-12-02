using System.Collections.Generic;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generator.Generator;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class CoreProjectGenerator : BaseGenerator
    {
        private readonly ICommandLineService _commandLineService;

        public CoreProjectGenerator(IGeneratorConfiguration generatorConfiguration, ICommandLineService commandLineService) : base(generatorConfiguration)
        {
            _commandLineService = commandLineService;
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
        }
    }
}
