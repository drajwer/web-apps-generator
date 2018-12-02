using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DotLiquid;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generator.Extensions;
using WebAppsGenerator.Generator.Generator;
using Type = System.Type;

namespace WebAppsGenerator.Generating.AspNetCore.Services
{
    public class EntityGenerator : BaseGenerator
    {
        private readonly List<Type> _registerTypes;

        public EntityGenerator(IGeneratorConfiguration generatorConfiguration) : base(generatorConfiguration)
        {
            _registerTypes = new List<Type>();
        }

        public string GenerateEntities(IEnumerable<Entity> entities)
        {
            var entityList = entities.ToList();
            var entity = entityList[0];
            var templateTxt = GetTemplate();
            //RegisterTypesForObject(typeof(Entity));

            var template = Template.Parse(templateTxt); // Parses and compiles the template

            var generatedEntity = template.Render(Hash.FromAnonymousObject(new { entity }));
            Console.Write(generatedEntity);
            return null;
        }

        private static string GetTemplate()
        {
            var assembly = Assembly.GetCallingAssembly();//.GetAssembly(typeof(Generator));//GetExecutingAssembly();
            var resourceName = $"WebAppsGenerator.Generating.AspNetCore.Templates.Entity.liquid";
            var r = assembly.GetManifestResourceNames();

            using (var reader = new StreamReader(assembly.GetManifestResourceStream(resourceName)))
            {
                return reader.ReadToEnd();
            }
        }

        
    }
}