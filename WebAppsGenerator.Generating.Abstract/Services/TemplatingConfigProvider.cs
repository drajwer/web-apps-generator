using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    public class TemplatingConfigProvider : ITemplatingConfigProvider
    {
        private Assembly _assembly;
        private readonly string _parentSectionName;
        private Dictionary<string, TemplatingConfig> _configs;

        public TemplatingConfigProvider(Assembly assembly, string parentSectionName)
        {
            _assembly = assembly;
            _parentSectionName = parentSectionName;
            _configs = new Dictionary<string, TemplatingConfig>();
        }

        public TemplatingConfig GetConfig(string sectionName)
        {
            if (!_configs.ContainsKey(sectionName))
            {
                _configs.Add(sectionName, ReadConfig(sectionName));
            }
            return _configs[sectionName];
        }

        protected virtual TemplatingConfig ReadConfig(string sectionName)
        {
            var assemblyNamespace = _assembly.FullName.Split(',')[0];
            var resourceName = $"{assemblyNamespace}.template-config.json";
            TemplatingConfig config;
            using (var reader = new StreamReader(_assembly.GetManifestResourceStream(resourceName)))
            {
                var json = reader.ReadToEnd();
                var jObject = JObject.Parse(json);
                config = jObject[_parentSectionName][sectionName].ToObject<TemplatingConfig>();
                if (config == null)
                    throw new ArgumentException($"Cannot find config for section {sectionName}");
            }

            return config;
        }
    }
}
