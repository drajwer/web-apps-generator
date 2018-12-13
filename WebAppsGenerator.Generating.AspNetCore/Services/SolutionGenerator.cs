using System;
using System.Collections.Generic;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.Abstract.Services;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    /// <summary>
    /// Creates solution with subprojects and runs projects' generators.
    /// </summary>
    public class SolutionGenerator : BaseGenerator
    {
        private readonly ICommandLineService _commandLineService;
        private readonly IGenerator _webApiProjectGenerator;
        private readonly IGenerator _coreProjectGenerator;
        private readonly IGenerator _webGenerator;
        private readonly IGeneratorConfiguration _generatorConfiguration;
        private readonly SolutionPathService _pathService;

        public SolutionGenerator(IGeneratorConfiguration generatorConfiguration, ICommandLineService commandLineService,
            IGenerator webApiProjectGenerator, IGenerator coreProjectGenerator, IGenerator webGenerator) : base(generatorConfiguration)
        {
            _commandLineService = commandLineService;
            _webApiProjectGenerator = webApiProjectGenerator;
            _coreProjectGenerator = coreProjectGenerator;
            _webGenerator = webGenerator;
            _generatorConfiguration = generatorConfiguration;
            _pathService = new SolutionPathService(generatorConfiguration);
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException();

            CreateSolutionWithProjects();

            AddNuGetPackages($"{_pathService.CoreDirPath}\\{_pathService.CoreProjectName}.csproj", _generatorConfiguration.CoreProjectPackages);

            _webApiProjectGenerator.Generate(entities);
            _coreProjectGenerator.Generate(entities);
            _webGenerator.Generate(entities);
        }

        private void CreateSolutionWithProjects()
        {
            _commandLineService.RunCommand($"dotnet new sln -n {GeneratorConfiguration.ProjectName} -o {_pathService.SolutionDirPath}");
            _commandLineService.RunCommand($"dotnet new classlib -o {_pathService.CoreDirPath}");
            _commandLineService.RunCommand($"dotnet new webapi -o {_pathService.WebApiDirPath}");
            _commandLineService.RunCommand($"dotnet sln {_pathService.SolutionFilePath} add {_pathService.CoreDirPath}");
            _commandLineService.RunCommand($"dotnet sln {_pathService.SolutionFilePath} add {_pathService.WebApiDirPath}");
            _commandLineService.RunCommand($"dotnet add {_pathService.WebApiDirPath}\\{_pathService.WebApiProjectName}.csproj reference {_pathService.CoreDirPath}\\{_pathService.CoreProjectName}.csproj");
        }

        private void AddNuGetPackages(string csprojPath, IEnumerable<NuGetPackageDetails> packages)
        {
            foreach (var package in packages)
            {
                _commandLineService.RunCommand($"dotnet add {csprojPath} package --version {package.Version} {package.Name}");
            }
        }
    }
}
