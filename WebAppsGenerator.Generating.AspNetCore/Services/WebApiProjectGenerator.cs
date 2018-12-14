using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.Models.Templating;
using FileInfo = WebAppsGenerator.Generating.Abstract.Models.FileInfo;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class WebApiProjectGenerator : BaseGenerator
    {
        private readonly SolutionPathService _pathService;
        private readonly IFileService _fileService;

        public WebApiProjectGenerator(IGeneratorConfiguration generatorConfiguration, IFileService fileService,
            IWebApiProjectTemplatingConfigProvider templatingConfigProvider, CSharpDropFactory dropFactory)
            : base(generatorConfiguration, dropFactory, templatingConfigProvider, fileService)
        {
            _fileService = fileService;
            _pathService = new SolutionPathService(generatorConfiguration);
        }

        private void GenerateViewModels(IEnumerable<Entity> entities)
        {
            var baseVmFileInfo = new FileInfo
            {
                NameTemplate = "{{Params.Entity.Name}}BaseViewModel.cs",
                TemplatePath = "WebApi.BaseViewModel.liquid",
                OutputPath = Path.Combine(_pathService.WebApiDirPath, "ViewModels", "Generated")
            };
            var vmFileInfo = new FileInfo()
            {
                NameTemplate = "{{Params.Entity.Name}}ViewModel.cs",
                TemplatePath = "WebApi.ViewModel.liquid",
                OutputPath = Path.Combine(_pathService.WebApiDirPath, "ViewModels")
            };
            var drops = GetModelDrops(entities);

            foreach (var drop in drops.Where(d => !d.IsJoinModel))
            {
                _fileService.CreateFromTemplate(baseVmFileInfo, new SingleEntityDrop(GeneratorConfiguration, _pathService, drop));
                _fileService.CreateFromTemplate(vmFileInfo, new SingleEntityDrop(GeneratorConfiguration, _pathService, drop));
            }
        }

        private void GenerateCsProj()
        {
            var csprojFileInfo = new FileInfo
            {
                NameTemplate = "{{Params.WebApiProjectName}}.csproj",
                TemplatePath = "WebApi.ProjectFile.liquid",
                OutputPath = _pathService.WebApiDirPath
            };

            _fileService.CreateFromTemplate(csprojFileInfo, new WebApiBaseDrop(_pathService, GeneratorConfiguration));
        }
        private void GenerateStartup(IEnumerable<Entity> entities)
        {
            var startupFileInfo = new FileInfo
            {
                NameTemplate = "Startup.cs",
                TemplatePath = "WebApi.Startup.liquid",
                OutputPath = _pathService.WebApiDirPath
            };
            var extensionsFileInfo = new FileInfo
            {
                NameTemplate = "StartupExtensions.cs",
                TemplatePath = "WebApi.StartupExtensions.liquid",
                OutputPath = Path.Combine(_pathService.WebApiDirPath, "Extensions")
            };

            var drops = GetModelDrops(entities).Where(d => !d.IsJoinModel);

            _fileService.CreateFromTemplate(startupFileInfo, new WebApiBaseDrop(_pathService, GeneratorConfiguration));
            _fileService.CreateFromTemplate(extensionsFileInfo, new EntityListDrop(GeneratorConfiguration, _pathService, drops));
        }

        private void GenerateControllers(IEnumerable<Entity> entities)
        {
            var controllerFileInfo = new FileInfo
            {
                NameTemplate = "{{Params.Entity.Name}}Controller.cs",
                TemplatePath = "WebApi.Controller.liquid",
                OutputPath = Path.Combine(_pathService.WebApiDirPath, "Controllers")
            };

            var sampleControllerPath = Path.Combine(controllerFileInfo.OutputPath, "ValuesController.cs");
            File.Delete(sampleControllerPath);

            var drops = GetModelDrops(entities).Where(d => !d.IsJoinModel);
            foreach (var modelDrop in drops)
            {
                _fileService.CreateFromTemplate(controllerFileInfo, new SingleEntityDrop(GeneratorConfiguration, _pathService, modelDrop));
            }
        }

        private void GenerateAppsettings()
        {
            var appsettingsFileInfo = new FileInfo
            {
                NameTemplate = "appsettings.json",
                TemplatePath = "WebApi.appsettings.liquid",
                OutputPath = _pathService.WebApiDirPath
            };

            _fileService.CreateFromTemplate(appsettingsFileInfo, new WebApiBaseDrop(_pathService, GeneratorConfiguration));
        }

        private static List<ModelDrop> GetModelDrops(IEnumerable<Entity> entities)
        {
            var service = new ModelService();
            var drops = service.CreateModelDrops(entities);
            return drops;
        }
    }
}
