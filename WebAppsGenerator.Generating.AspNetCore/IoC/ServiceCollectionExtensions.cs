using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Options;
using WebAppsGenerator.Generating.AspNetCore.Interfaces;
using WebAppsGenerator.Generating.AspNetCore.Options;
using WebAppsGenerator.Generating.AspNetCore.Services;

namespace WebAppsGenerator.Generating.AspNetCore.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAspNetCoreGenerator(this IServiceCollection services)
        {
            RegisterGeneratorSpecificServices(services);

            services.AddScoped<IGenerator, SolutionGenerator>();

            return services;
        }

        public static IServiceCollection AddAspNetCoreConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AnnotationOptions>(configuration.GetSection("AllowedAnnotationsCore"));
            services.Configure<AspNetCoreGeneratorOptions>(configuration.GetSection("GeneratorOptions"));
            services.AddScoped<AspNetCoreGeneratorConfiguration>();

            return services;
        }

        private static void RegisterGeneratorSpecificServices(IServiceCollection services)
        {
            services.AddTransient<IAspNetCoreFileService, AspNetCoreFileService>();
            services.AddScoped<IAspNetCoreFirstRunProvider, AspNetCoreFirstRunProvider>();
            services.AddScoped<IAspNetCoreChildGenerator, WebApiProjectGenerator>();
            services.AddScoped<IAspNetCoreChildGenerator, CoreProjectGenerator>();
            services.AddScoped<MigrationService>();
            services.AddScoped<SolutionPathService>();

            services.AddScoped<ICoreProjectTemplatingConfigProvider, CoreProjectTemplatingConfigProvider>();
            services.AddScoped<IWebApiProjectTemplatingConfigProvider, WebApiProjectTemplatingConfigProvider>();
            services.AddScoped<CSharpDropFactory>();

            services.AddScoped<ModelService>();
        }
    }
}
