using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DotLiquid;
using Microsoft.Extensions.Configuration;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generator.Models;
using WebAppsGenerator.Generator.Options;
using WebAppsGenerator.Generator.Validation;

namespace WebAppsGenerator.Generator.Generators
{
    public class Generator: IGenerator
    {
        public IConfiguration Configuration { get; set; }
        public AnnotationOptions AnnotationOptions { get; set; }
        public EntityGenerator EntityGenerator { get; set; }
        public Generator()
        {
            AddConfiguration();
            AnnotationOptions = new AnnotationOptions();
            Configuration.Bind("AllowedAnnotations", AnnotationOptions);
            EntityGenerator = new EntityGenerator();
        }

        public void ProcessVisitorResults(IEnumerable<Entity> entities)
        {
            var entityList = entities.ToList();
            var validator = new Validator();
            validator.ValidateAnnotations(entityList, AnnotationOptions.Annotations);

            EntityGenerator.GenerateEntities(entityList);
            // here all generating methods will be called
        }

        
        private void AddConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json");

            Configuration = builder.Build();
        }
    }
}
