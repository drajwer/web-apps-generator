using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Models;
using WebAppsGenerator.Generator.Options;
using WebAppsGenerator.Generator.Validation;

namespace WebAppsGenerator.Generator.Generator
{
    public class Generator: IGenerator
    {
        public IConfiguration Configuration { get; set; }
        public AnnotationOptions AnnotationOptions { get; set; }

        public Generator()
        {
            AddConfiguration();
            AnnotationOptions = new AnnotationOptions();
            Configuration.Bind("AllowedAnnotations", AnnotationOptions);
        }
        public void ProcessVisitorResults(IEnumerable<Entity> entities)
        {
            var validator = new Validator();
            validator.ValidateAnnotations(entities, AnnotationOptions.Annotations);

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
