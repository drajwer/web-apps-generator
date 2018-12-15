namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    public interface ITemplateFileProvider
    {
        string GetTemplate(Models.FileInfo templateFileInfo);
    }
}
