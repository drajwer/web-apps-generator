using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generator.Generator;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class EntityGenerator : BaseGenerator
    {
        private readonly LiquidTemplateService _liquidTemplateService;

        public EntityGenerator(IGeneratorConfiguration generatorConfiguration, LiquidTemplateService liquidTemplateService) : base(generatorConfiguration)
        {
            _liquidTemplateService = liquidTemplateService;
        }

        public override void Generate(IEnumerable<Entity> entities)
        {
            var entityList = entities.ToList();
            foreach (var entity in entityList)
            {
                var templateTxt = GetTemplate();
                var generatedEntity = _liquidTemplateService.RenderTemplate(templateTxt, entity);

                Console.WriteLine(generatedEntity);
            }
        }

        private static string GetTemplate()
        {
            var assembly = Assembly.GetCallingAssembly();
            var resourceName = $"WebAppsGenerator.Generating.AspNetCore.Templates.Entity.liquid";
            //var r = assembly.GetManifestResourceNames();

            using (var reader = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
            {
                return reader.ReadToEnd();
            }
        }

        
    }
}