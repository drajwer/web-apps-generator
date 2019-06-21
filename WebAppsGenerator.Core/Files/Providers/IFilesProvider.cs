namespace WebAppsGenerator.Core.Files.Providers
{
    public interface IFilesProvider
    {
        /// <summary>
        /// Returns files' paths
        /// </summary>
        string[] GetFiles();
    }
}