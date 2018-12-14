using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Models;
using WebAppsGenerator.Generating.Abstract.Options;
using FileInfo = WebAppsGenerator.Generating.Abstract.Models.FileInfo;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    public class TemplatingConfigProvider : ITemplatingConfigProvider
    {
        private const string RootFolder = "Templates";
        private Assembly _assembly;
        private readonly string _parentSectionName;
        private Dictionary<string, TemplatingConfig> _configs;

        public TemplatingConfigProvider(Assembly assembly, string parentSectionName)
        {
            _assembly = assembly;
            _parentSectionName = parentSectionName;
            _configs = new Dictionary<string, TemplatingConfig>();
        }

        public virtual IEnumerable<TemplatingConfig> GetTemplatingConfigs()
        {
            var resources = _assembly.GetManifestResourceNames().ToList();
            var filtered = resources.Where(r => r.Contains($"{RootFolder}.{_parentSectionName}"));
            foreach (var resource in filtered)
            {
                var parts = resource.Split('.');
                var config = ReadConfig(parts[parts.Length - 2]);

                if (config == null)
                {
                    var fullName = TruncateToRelativePath(parts).Aggregate("", (sum, next) => sum + "." + next).Trim('.');
                    if (!string.IsNullOrWhiteSpace(fullName))
                        fullName += ".";
                    fullName += parts[parts.Length - 2];
                    config = ReadConfig(fullName);
                    if (config == null)
                        continue;
                }

                if (config.FileInfo == null)
                {
                    config.FileInfo = new FileInfo
                    {
                        NameTemplate = config.NameTemplate,
                        TemplatePath = resource.Substring(resource.IndexOf(RootFolder, StringComparison.Ordinal) + RootFolder.Length + 1),
                        OutputPath = TruncateToRelativePath(parts).Aggregate("", Path.Combine)
                    };
                }

                yield return config;
            }
        }

        private IEnumerable<string> TruncateToRelativePath(string[] parts)
        {
            return parts.SkipWhile(e => _parentSectionName == null ? e != RootFolder : e != _parentSectionName).Skip(1).TakeWhile(e => e != parts[parts.Length - 2]);
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
                var jToken = _parentSectionName == null ? jObject : jObject[_parentSectionName];
                foreach (var section in sectionName.Split('.'))
                {
                    jToken = jToken[section];
                    if (jToken == null)
                        return null;
                }
                config = jToken.ToObject<TemplatingConfig>();
            }

            return config;
        }
    }
}
