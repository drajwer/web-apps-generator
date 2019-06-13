using System;
using System.Collections.Generic;
using System.Text;
using WebAppsGenerator.Generating.Abstract.Interfaces;

namespace WebAppsGenerator.Tests.Mocks
{
    public class GeneratorConfigurationMock : IGeneratorConfiguration
    {
        public string ProjectName { get; }
        public string OutputPath { get; }
        public string MigrationName { get; }
        public bool AddMigration { get; }
        public bool RunAspNetCoreGen { get; }
        public bool RunWebUiGen { get; }
        public bool RunReactAppCreation { get; }
    }
}
