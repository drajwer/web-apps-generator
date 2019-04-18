using System.Collections.Generic;

namespace WebAppsGenerator.Generating.Abstract.Options
{
    /// <summary>
    /// Helper class used for injecting configuration to generator
    /// </summary>
    public class GeneratorOptions
    {
        // Paths
        public string InputPath { get; set; }
        public string OverwriteFileInputPath { get; set; }
        public string OverwriteFileOutputPath { get; set; }
        public string OutputPath { get; set; }
        
        // Names
        public string ProjectName { get; set; }
        public string MigrationName { get; set; }

        // Flags
        public bool AddMigration { get; set; }
        public bool RunAspNetCoreGen { get; set; }
        public bool RunWebUiGen { get; set; }
        public bool RunReactAppCreation { get; set; }
        public bool OverwriteAll { get; set; }
    }
}
