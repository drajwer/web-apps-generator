using WebAppsGenerator.Generating.Abstract.Models;

namespace WebAppsGenerator.Generating.Abstract.Interfaces
{
    public interface ITemplatingConfigProvider
    {
        TemplatingConfig GetConfig(string sectionName);
    }
}