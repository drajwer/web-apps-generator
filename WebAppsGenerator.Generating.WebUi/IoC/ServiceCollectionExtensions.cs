using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.WebUi.Services;

namespace WebAppsGenerator.Generating.WebUi.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWebUiCoreGenerator(this IServiceCollection services)
        {
            RegisterGeneratorSpecificServices(services);

            services.AddScoped<IGenerator, WebUiGenerator>();

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
            services.AddTransient(provider => new TemplateFileProvider(Assembly.GetCallingAssembly()));
            services.AddTransient<TemplateFileProvider>();//(provider => new TemplateFileProvider(/*Assembly.GetAssembly(typeof(SolutionGenerator))*/));
            services.AddScoped<SolutionPathService>();
        }
    }
}
