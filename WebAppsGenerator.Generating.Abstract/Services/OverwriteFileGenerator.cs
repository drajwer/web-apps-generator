using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;
namespace WebAppsGenerator.Generating.Abstract.Services
{
    /// <summary>
    /// Generates JSON file containing project file structure with overwrite flag for each file
    /// </summary>
    public class OverwriteFileGenerator
    {
        private readonly GeneratorOptions _config;
        private readonly Dictionary<string, bool> _overwritesDictionary;
        public OverwriteFileGenerator(IOptions<GeneratorOptions> config, IOverwriteService overwriteService, IGeneratorConfiguration generatorConfiguration)
        {
            _config = config.Value;
            _overwritesDictionary = overwriteService.GetOverwritesDictionary();
        }

        public void Generate()
        {
            var paths = _overwritesDictionary.Select(kp => (kp.Key, kp.Key)).ToList();
            var jObject = GetObject(paths);
            if(_config.OverwriteFileOutputPath != null)
                File.WriteAllText(_config.OverwriteFileOutputPath, jObject.ToString());
        }

        private JObject GetObject(List<(string current, string original)> paths)
        {
            var dictionary = _overwritesDictionary;
            var jObject = new JObject();
            var files = paths.Where(elem => elem.current.Split(Path.DirectorySeparatorChar).Count() == 1);
            foreach (var file in files)
            {
                jObject.Add(file.current, JToken.FromObject(_overwritesDictionary[file.original]));
            }
            var groups = paths.Where(elem => elem.current.Split(Path.DirectorySeparatorChar).Count() > 1).GroupBy(s => s.current.Split(Path.DirectorySeparatorChar).First());
            foreach (var group in groups)
            {
                jObject.Add(group.Key, GetObject(group.Select(g => (Path.Combine(g.current.Split(Path.DirectorySeparatorChar).Skip(1).ToArray()), g.original)).ToList()));
            }

            return jObject;
        }
    }
}
