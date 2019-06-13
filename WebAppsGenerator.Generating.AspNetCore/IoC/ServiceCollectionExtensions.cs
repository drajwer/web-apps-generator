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

            services.AddScoped<IGenerator>(provider =>
            {
                var generatorConfiguration = provider.GetService<AspNetCoreGeneratorConfiguration>();
                var commandLineService = provider.GetService<ICommandLineService>();
                var webApiGenerator = provider.GetService<WebApiProjectGenerator>();
                var coreGenerator = provider.GetService<CoreProjectGenerator>();
                var overwriteService = provider.GetService<IOverwriteService>();

                return new SolutionGenerator(generatorConfiguration, commandLineService, webApiGenerator, coreGenerator, overwriteService);
            });

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
            services.AddScoped<WebApiProjectGenerator>();
            services.AddScoped<CoreProjectGenerator>();
            services.AddScoped<MigrationService>();
            services.AddScoped<SolutionPathService>();

            services.AddScoped<ICoreProjectTemplatingConfigProvider, CoreProjectTemplatingConfigProvider>();
            services.AddScoped<IWebApiProjectTemplatingConfigProvider, WebApiProjectTemplatingConfigProvider>();
            services.AddScoped<CSharpDropFactory>();

            services.AddScoped<ModelService>();
        }
    }
}
