using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppsGenerator.Core.Files.Providers;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.IoC;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.IoC;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.AspNetCore.IoC;
using WebAppsGenerator.Generating.WebUi.IoC;

namespace WebAppsGenerator.Console
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.main.json", optional: false, reloadOnChange: true)
                .AddJsonFile("config.WebUi.json", optional: false, reloadOnChange: true)
                .AddJsonFile("config.AspNetCore.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<Application>();

            // Lexer, Parser, Visitor
            services.AddAntlrModelProvider();

            // Validators
            services.AddValidators();

            // Generators
            services.AddRootGenerator();
            services.AddAspNetCoreGenerator();
            services.AddWebUiCoreGenerator();

            // Specific implementations
            services.AddScoped<IGeneratorConfiguration, GeneratorConfiguration>();
            services.AddScoped<ICommandLineService, CommandLineService>();
            services.AddScoped<IExceptionHandler, QueuedExceptionHandler>(b => b.GetService<QueuedExceptionHandler>());
            services.AddScoped<QueuedExceptionHandler>();
            services.AddScoped<ParsingExceptionWriter>();
            services.AddScoped<IFilesProvider>(builder => new DeepDirectoryFilesProvider("./../../../TestDir", "sn"));

            // Options
            services.AddOptions();
            services.Configure<AnnotationOptions>(Configuration.GetSection("AllowedAnnotationsCommon"));
            services.AddAspNetCoreConfigurationOptions(Configuration);
            services.AddWebUiConfigurationOptions(Configuration);
        }

    }
}
