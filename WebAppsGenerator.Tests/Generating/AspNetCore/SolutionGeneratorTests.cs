using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.AspNetCore.Options;
using WebAppsGenerator.Generating.AspNetCore.Services;
using WebAppsGenerator.Tests.Mocks;

namespace WebAppsGenerator.Tests.Generating.AspNetCore
{
    [TestClass]
    public class SolutionGeneratorTests
    {
        private SolutionGenerator _generator;
        private AspNetCoreGeneratorConfiguration _configuration;
        private GeneratorConfiguration _baseConfiguration;
        private CommandLineServiceMock _commandLineService;
        private bool _webApiGeneratorCalled;
        private bool _coreGeneratorCalled;

        [TestInitialize]
        public void Setup()
        {
            _baseConfiguration = new GeneratorConfiguration(
                new OptionsWrapper<GeneratorOptions>(new GeneratorOptions {ProjectName = "test", OutputPath = "test", RunAspNetCoreGen = true}),
                new ExceptionHandlerMock());

            _configuration = new AspNetCoreGeneratorConfiguration(_baseConfiguration,
                new OptionsWrapper<AspNetCoreGeneratorOptions>(new AspNetCoreGeneratorOptions()
                    {CoreProjectPackages = new List<NuGetPackageDetails>()}));

            _commandLineService = new CommandLineServiceMock();
            var webApiGenerator = new Mock<IGenerator>();
            webApiGenerator.Setup(g => g.Generate(It.IsAny<IEnumerable<Entity>>())).Callback(() => _webApiGeneratorCalled = true);
            var coreGenerator = new Mock<IGenerator>();
            coreGenerator.Setup(g => g.Generate(It.IsAny<IEnumerable<Entity>>())).Callback(() => _coreGeneratorCalled = true);

            _generator = new SolutionGenerator(_configuration, _commandLineService, webApiGenerator.Object, coreGenerator.Object);
        }

        [TestMethod]
        public void NullEntitiesTest()
        {
            // Arrange
            _baseConfiguration.ProjectName = "TestProject";
            _baseConfiguration.OutputPath = "test/some/path";

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => _generator.Generate(null));
        }

        [TestMethod]
        public void NoEntitiesTest()
        {
            // Arrange
            _baseConfiguration.ProjectName = "TestProject";
            _baseConfiguration.OutputPath = "test/some/path";

            // Act & Assert
            _generator.Generate(new List<Entity>());
        }

        // commented because IsEnabled causes failure

        [TestMethod]
        public void SomeEntitiesTest()
        {
            // Arrange
            _baseConfiguration.ProjectName = "TestProject";
            _baseConfiguration.OutputPath = "test/some/path";
            var entity = new Entity(-1, -1);

            // Act
            _generator.Generate(new List<Entity>() { entity });

            // Assert
            Assert.IsTrue(_webApiGeneratorCalled);
            Assert.IsTrue(_coreGeneratorCalled);
            Assert.AreEqual(6, _commandLineService.Commands.Count);
            Assert.IsTrue(_commandLineService.Commands.All(c => c.StartsWith("dotnet")));
            // TODO: Assert if commands executed in correct order and with valid args.
        }
    }
}
