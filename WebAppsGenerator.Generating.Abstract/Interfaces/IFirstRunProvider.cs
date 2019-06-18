namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    /// <summary>
    /// Provides information if the project is created from scratched or just extended
    /// </summary>
    public interface IFirstRunProvider
    {
        bool IsFirstRun();
    }
}