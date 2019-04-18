using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using WebAppsGenerator.Core.Helpers;
using WebAppsGenerator.Generating.Abstract.Options;

namespace WebAppsGenerator.Console
{
    public static class ArgsParser
    {
        /// <summary>
        /// Returns options or null if args cannot be parsed.
        /// </summary>
        /// <param name="args">Arguments given by user</param>
        public static GeneratorOptions ParseArguments(string[] args)
        {
            if (args.Length != 1)
            {
                ConsoleHelper.WriteInfo("USAGE: WebAppsGenerator config_file.json");
                return null;
            }

            var configPath = args[0];
            if (!File.Exists(configPath))
            {
                ConsoleHelper.WriteError($"Specified file: {configPath} does not exists.");
                return null;
            }
            GeneratorOptions options;
            try
            {
                options = JsonConvert.DeserializeObject<GeneratorOptions>(File.ReadAllText(configPath));
                if (options.InputPath == null || !Directory.Exists(options.InputPath))
                {
                    ConsoleHelper.WriteError($"Specified file: {configPath} has unspecified or invalid input path. Please provide valid config file.");
                    return null;
                }
            }
            catch (Exception)
            {
                ConsoleHelper.WriteError($"Specified file: {configPath} is invalid. Please provide valid config file.");
                return null;
            }

            if (options.OverwriteAll && !args.Contains("--force") && !ConsoleHelper.Prompt("Do you really want to overwrite all files?"))
                return null;
            return options;
        }
    }
}
