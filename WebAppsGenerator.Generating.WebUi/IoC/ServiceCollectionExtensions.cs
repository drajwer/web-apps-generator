using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.WebUi.Interfaces;
using WebAppsGenerator.Generating.WebUi.Services;

namespace WebAppsGenerator.Generating.WebUi.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebUiCoreGenerator(this IServiceCollection services)
        {
            RegisterGeneratorSpecificServices(services);

            services.AddScoped<IGenerator>(provider =>
            {
                var webUiGenerator = provider.GetService<WebUiGenerator>();
                //var generatorConfiguration = provider.GetService<IGeneratorConfiguration>();
                var commandLineService = provider.GetService<ICommandLineService>();
                var pathService = provider.GetService<SolutionPathService>();

                return new WebClientProjectGenerator(pathService, commandLineService, webUiGenerator);
            });

            return services;
        }

        //public static IServiceCollection AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.Configure<AnnotationOptions>(configuration.GetSection("AllowedAnnotations"));
        //    services.Configure<GeneratorOptions>(configuration.GetSection("GeneratorOptions"));

        //    return services;
        //}

        private static void RegisterGeneratorSpecificServices(IServiceCollection services)
        {
            services.AddScoped<IWebUiFileService, WebUiFileService>();
            services.AddScoped<IWebUiProjectTemplatingConfigProvider, WebUiTemplateConfigProvider>();
            services.AddTransient<TemplateFileProvider>();
            services.AddScoped<SolutionPathService>();
            services.AddScoped<WebUiGenerator>();
            services.AddScoped<WebUiDropFactory>();
            services.AddScoped<TypeScriptEntityService>();
        }
    }
}
