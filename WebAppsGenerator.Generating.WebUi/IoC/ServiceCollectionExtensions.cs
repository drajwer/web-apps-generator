using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.WebUi.Interfaces;
using WebAppsGenerator.Generating.WebUi.Services;

namespace WebAppsGenerator.Generating.WebUi.IoC
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add web UI generator with dependencies
        /// </summary>
        public static IServiceCollection AddWebUiCoreGenerator(this IServiceCollection services)
        {
            RegisterGeneratorSpecificServices(services);
            RegisterGenerators(services);

            return services;
        }

        /// <summary>
        /// Provide configuration for web UI generator
        /// </summary>
        public static IServiceCollection AddWebUiConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AnnotationOptions>(configuration.GetSection("AllowedAnnotationsUi"));

            return services;
        }

        private static void RegisterGeneratorSpecificServices(IServiceCollection services)
        {
            services.AddScoped<IWebUiFirstRunProvider, WebUiFirstRunProvider>();
            services.AddScoped<IWebUiFileService, WebUiFileService>();
            services.AddScoped<IWebUiProjectTemplatingConfigProvider, WebUiTemplateConfigProvider>();
            services.AddTransient<TemplateFileProvider>();
            services.AddScoped<SolutionPathService>();
            services.AddScoped<WebUiDropFactory>();
            services.AddScoped<TypeScriptEntityService>();
        }

        private static void RegisterGenerators(IServiceCollection services)
        {
            services.AddScoped<IWebUiChildGenerator, WebUiGenerator>();
            services.AddScoped<IWebUiChildGenerator, ScriptGenerator>();
            services.AddScoped<IGenerator, WebClientProjectGenerator>();
        }
    }
}
