namespace WebAppsGenerator.Core.Files.FileSrevices
{
    public interface IFileService
    {
        LineInfo GetLineInfo(int concatFileLineNo);
    }
}