using System.Collections.Generic;

namespace WebAppsGenerator.Generating.Abstract.Options
{
    /// <summary>
    /// Helper class used for injecting configuration to generator
    /// </summary>
    public class GeneratorOptions
    {
        public string InputPath { get; set; }
        public string ProjectName { get; set; }
        public string OutputPath { get; set; }
        public string MigrationName { get; set; }
        public bool AddMigration { get; set; }
        public bool RunAspNetCoreGen { get; set; }
        public bool RunWebUiGen { get; set; }
        public bool RunReactAppCreation { get; set; }
    }
}
