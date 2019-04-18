using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WebAppsGenerator.Generating.Abstract.Options;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    public class OverwriteServiceFactory
    {
        private GeneratorOptions options;
        private Dictionary<string, bool> _dictionary = new Dictionary<string, bool>();
        public OverwriteServiceFactory(IOptions<GeneratorOptions> optionsAccessor)
        {
            options = optionsAccessor.Value;
        }
        public OverwriteService Create()
        {
            if (!File.Exists(options.OverwriteFileInputPath))
                return new OverwriteService(null, options.OverwriteAll);

            var json = File.ReadAllText(options.OverwriteFileInputPath);
            var jObject = JObject.Parse(json);
            AddSection(jObject, "");
           
            return new OverwriteService(_dictionary, options.OverwriteAll);
        }

        private void AddSection(JObject jObject, string path)
        {
            foreach (var prop in jObject.Properties())
            {
                var key = Path.Combine(path, prop.Name);
                if (prop.Value<bool?>() != null)
                    _dictionary.Add(key, prop.Value<bool>());
                else if (prop.Value is JObject childObject)
                    AddSection(childObject, key);
            }
        }
    }
}
