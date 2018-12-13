using System.Collections.Generic;
using System.IO;
using DotLiquid;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using FileInfo = WebAppsGenerator.Generating.Abstract.Models.FileInfo;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    public class BaseGenerator: IGenerator
    {
        protected IGeneratorConfiguration GeneratorConfiguration;
        protected IFileService FileService;
        protected IDropFactory DropFactory;
        protected ITemplatingConfigProvider ConfigProvider;

        public BaseGenerator(IGeneratorConfiguration generatorConfiguration, IDropFactory dropFactory, ITemplatingConfigProvider configProvider, IFileService fileService)
        {
            GeneratorConfiguration = generatorConfiguration;
            DropFactory = dropFactory;
            ConfigProvider = configProvider;
            FileService = fileService;
        }

        public virtual void Generate(IEnumerable<Entity> entities)
        {
            throw new System.NotImplementedException();
        }

        public virtual void GenerateSection(string sectionName, IEnumerable<Entity> entities)
        {
            var templatingConfig = ConfigProvider.GetConfig(sectionName);
            if (templatingConfig.Multiple)
            {
                var baseDrops = DropFactory.CreateDropList(templatingConfig.DropId, entities);
                foreach (var drop in baseDrops)
                {
                    FileService.CreateFromTemplate(templatingConfig.FileInfo, drop);
                }
            }
            else
            {
                var drop = DropFactory.CreateDrop(templatingConfig.DropId, entities);
                FileService.CreateFromTemplate(templatingConfig.FileInfo, drop);
            }
        }
        public virtual void GenerateSection(string sectionName, IEnumerable<Drop> drops)
        {
            var templatingConfig = ConfigProvider.GetConfig(sectionName);

            foreach (var drop in drops)
            {
                FileService.CreateFromTemplate(templatingConfig.FileInfo, drop);
            }
        }

        public virtual void GenerateSection(string sectionName, Drop drop)
        {
            var templatingConfig = ConfigProvider.GetConfig(sectionName);

            FileService.CreateFromTemplate(templatingConfig.FileInfo, drop);
        }
    }
}
