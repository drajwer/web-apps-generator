namespace WebAppsGenerator.Core.Files.Services
{
    public interface IFileService
    {
        LineInfo GetLineInfo(int concatFileLineNo);
    }
}