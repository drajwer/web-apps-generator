namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    /// <summary>
    /// Provides retrieving templates for specified <see cref="WebAppsGenerator.Generating.Abstract.Models.FileInfo"/>
    /// </summary>
    public interface ITemplateFileProvider
    {
        string GetTemplate(Models.FileInfo templateFileInfo);
    }
}
