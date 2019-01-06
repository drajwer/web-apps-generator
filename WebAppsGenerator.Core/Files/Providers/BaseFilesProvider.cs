namespace WebAppsGenerator.Core.Files.Providers
{
    public abstract class BaseFilesProvider : IFilesProvider
    {
        protected readonly string DirName;
        protected readonly string Extension;

        protected BaseFilesProvider(string dirName, string extension = null)
        {
            DirName = dirName;
            Extension = extension;
        }

        public abstract string[] GetFiles();
    }
}
