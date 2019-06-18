namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    /// <summary>
    /// Provides basic configuration for generator
    /// </summary>
    public interface IGeneratorConfiguration
    {
        string ProjectName { get; }
        string OutputPath { get; }
        string MigrationName { get; }
        bool AddMigration { get; }
        bool RunAspNetCoreGen { get; }
        bool RunWebUiGen { get; }
    }
}
