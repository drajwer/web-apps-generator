﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace WebAppsGenerator.Generating.Abstract.Services
{
    public class TemplateFileProvider
    {
        private readonly Assembly _assembly;

        public TemplateFileProvider(Assembly assembly = null)
        {
            _assembly = assembly ?? Assembly.GetCallingAssembly();
        }
        public string GetTemplate(Models.FileInfo templateFileInfo)
        {
            var assemblyNamespace = _assembly.FullName.Split(',')[0];
            var resourceName = $"{assemblyNamespace}.Templates.{templateFileInfo.TemplatePath}";

            using (var reader = new StreamReader(_assembly.GetManifestResourceStream(resourceName)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}