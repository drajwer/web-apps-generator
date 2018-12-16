using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.AspNetCore.Services;
using WebAppsGenerator.Tests.Mocks;

namespace WebAppsGenerator.Tests.Generating.AspNetCore
{
    [TestClass]
    public class SolutionGeneratorTests
    {
        private SolutionGenerator _generator;
        private GeneratorConfiguration _configuration;
        private CommandLineServiceMock _commandLineService;
        private bool _webApiGeneratorCalled;
        private bool _coreGeneratorCalled;

        [TestInitialize]
        public void Setup()
        {
            _configuration = new GeneratorConfiguration(new OptionsWrapper<GeneratorOptions>(new GeneratorOptions { CoreProjectPackages = new List<NuGetPackageDetails>() }));
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
            _configuration.ProjectName = "TestProject";
            _configuration.OutputPath = "test/some/path";

            // Act
            Assert.ThrowsException<ArgumentNullException>(() => _generator.Generate(null));
            // Assert

        }

        [TestMethod]
        public void NoEntitiesTest()
        {
            // Arrange
            _configuration.ProjectName = "TestProject";
            _configuration.OutputPath = "test/some/path";

            // Act & Assert
            _generator.Generate(new List<Entity>());
        }

        [TestMethod]
        public void SomeEntitiesTest()
        {
            // Arrange
            _configuration.ProjectName = "TestProject";
            _configuration.OutputPath = "test/some/path";
            var entity = new Entity();

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
