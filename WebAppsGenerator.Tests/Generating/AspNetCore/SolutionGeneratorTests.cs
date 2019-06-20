using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.AspNetCore.Interfaces;
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
        private OverwriteServiceMock _overwriteService;
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
            _overwriteService = new OverwriteServiceMock();
            var solutionPathService = new SolutionPathService(_configuration);

            var webApiGenerator = new Mock<IAspNetCoreChildGenerator>();
            webApiGenerator.Setup(g => g.Generate(It.IsAny<IEnumerable<Entity>>())).Callback(() => _webApiGeneratorCalled = true);
            var coreGenerator = new Mock<IAspNetCoreChildGenerator>();
            coreGenerator.Setup(g => g.Generate(It.IsAny<IEnumerable<Entity>>())).Callback(() => _coreGeneratorCalled = true);
            var generators = new List<IAspNetCoreChildGenerator>() {webApiGenerator.Object, coreGenerator.Object};

            var firstRunProvider = new Mock<IAspNetCoreFirstRunProvider>();
            firstRunProvider.Setup(g => g.IsFirstRun()).Returns(true);

            _generator = new SolutionGenerator(_configuration, _commandLineService, _overwriteService,
                firstRunProvider.Object, solutionPathService, generators);
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
        }
    }
}
