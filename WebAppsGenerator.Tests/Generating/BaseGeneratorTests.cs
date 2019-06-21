using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Models;
using WebAppsGenerator.Tests.Mocks;

namespace WebAppsGenerator.Tests.Generating
{
    /// <summary>
    /// Tests if generator properly handles discovered template files
    /// </summary>
    [TestClass]
    public class BaseGeneratorTests
    {
        [TestMethod]
        public void SingleTemplate()
        {
            // Arrange
            var templatingConfigs = new List<TemplatingConfig>
            {
                new TemplatingConfig
                {
                    FileInfo = new FileInfo
                    {
                        OutputPath = "dir/file",
                        NameTemplate = "template",
                        TemplatePath = "path"
                    },
                    DropId = "id",
                    NameTemplate = "template",
                    Multiple = false,
                }
            };

            var baseGenerator = Setup(templatingConfigs);

            // Act
            baseGenerator.Generate(new List<Entity> { new Entity(-1, -1), new Entity(-1, -1) });

            // Assert
            Assert.IsTrue(baseGenerator.FileService.CreatedFiles.Count(f => f == "dir/file") == 1);
        }

        [TestMethod]
        public void TwoTemplates()
        {
            // Arrange
            var templatingConfigs = new List<TemplatingConfig>
            {
                new TemplatingConfig
                {
                    FileInfo = new FileInfo
                    {
                        OutputPath = "dir/file",
                        NameTemplate = "template",
                        TemplatePath = "path"
                    },
                    DropId = "id",
                    NameTemplate = "template",
                    Multiple = false,
                },
                new TemplatingConfig
                {
                    FileInfo = new FileInfo
                    {
                        OutputPath = "dir/file2",
                        NameTemplate = "template",
                        TemplatePath = "path"
                    },
                    DropId = "id",
                    NameTemplate = "template",
                    Multiple = false,
                },
            };

            var baseGenerator = Setup(templatingConfigs);

            // Act
            baseGenerator.Generate(new List<Entity> { new Entity(-1, -1), new Entity(-1, -1) });

            // Assert
            Assert.IsTrue(baseGenerator.FileService.CreatedFiles.Count(f => f == "dir/file") == 1);
            Assert.IsTrue(baseGenerator.FileService.CreatedFiles.Count(f => f == "dir/file2") == 1);
        }

        [TestMethod]
        public void SingleTemplateForEachEntity()
        {
            // Arrange
            var templatingConfigs = new List<TemplatingConfig>
            {
                new TemplatingConfig
                {
                    FileInfo = new FileInfo
                    {
                        OutputPath = "dir/file",
                        NameTemplate = "template",
                        TemplatePath = "path"
                    },
                    DropId = "id",
                    NameTemplate = "template",
                    Multiple = true,
                }
            };

            var baseGenerator = Setup(templatingConfigs);

            // Act
            baseGenerator.Generate(new List<Entity> { new Entity(-1, -1), new Entity(-1, -1) });

            // Assert
            Assert.IsTrue(baseGenerator.FileService.CreatedFiles.Count(f => f == "dir/file") == 2);
        }

        [TestMethod]
        public void TwoTemplatesForEachEntity()
        {
            // Arrange
            var templatingConfigs = new List<TemplatingConfig>
            {
                new TemplatingConfig
                {
                    FileInfo = new FileInfo
                    {
                        OutputPath = "dir/file",
                        NameTemplate = "template",
                        TemplatePath = "path"
                    },
                    DropId = "id",
                    NameTemplate = "template",
                    Multiple = true,
                },
                new TemplatingConfig
                {
                    FileInfo = new FileInfo
                    {
                        OutputPath = "dir/file2",
                        NameTemplate = "template",
                        TemplatePath = "path"
                    },
                    DropId = "id",
                    NameTemplate = "template",
                    Multiple = true,
                },
            };

            var baseGenerator = Setup(templatingConfigs);

            // Act
            baseGenerator.Generate(new List<Entity> { new Entity(-1, -1), new Entity(-1, -1) });

            // Assert
            Assert.IsTrue(baseGenerator.FileService.CreatedFiles.Count(f => f == "dir/file") == 2);
            Assert.IsTrue(baseGenerator.FileService.CreatedFiles.Count(f => f == "dir/file2") == 2);
        }

        private static BaseGeneratorMock Setup(List<TemplatingConfig> templatingConfigs)
        {
            var dropFactory = new DropFactoryMock();
            var fileService = new FileServiceMock();
            var generatorConfiguration = new GeneratorConfigurationMock();
            var templateConfigProvider = new TemplateConfigProviderMock(templatingConfigs);

            return new BaseGeneratorMock(generatorConfiguration, dropFactory, templateConfigProvider, fileService);
        }
    }
}
