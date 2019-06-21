using System.Collections.Generic;
using System.Linq;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models;

namespace WebAppsGenerator.Tests.Mocks
{
    public class TemplateConfigProviderMock : ITemplatingConfigProvider
    {
        public List<TemplatingConfig> TemplatingConfigs { get; set; }

        public TemplateConfigProviderMock(List<TemplatingConfig> templatingConfigs)
        {
            TemplatingConfigs = templatingConfigs;
        }

        public TemplatingConfig GetConfig(string sectionName)
        {
            return TemplatingConfigs.FirstOrDefault();
        }

        public IEnumerable<TemplatingConfig> GetTemplatingConfigs()
        {
            return TemplatingConfigs;
        }
    }
}
