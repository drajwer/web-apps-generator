﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.AspNetCore.Services;

namespace WebAppsGenerator.Generating.AspNetCore.IoC
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAspNetCoreGenerator(this IServiceCollection services)
        {
            services.AddScoped<IGenerator>(provider =>
            {
                var generatorConfiguration = provider.GetService<IGeneratorConfiguration>();
                var commandLineService = provider.GetService<ICommandLineService>();
                var liquidTemplateService = provider.GetService<LiquidTemplateService>();
                var fileService =
                    new FileService(new TemplateFileProvider(Assembly.GetAssembly(typeof(SolutionGenerator))), liquidTemplateService);
                var webApiGenerator = new WebApiProjectGenerator(generatorConfiguration, fileService);
                var coreGenerator = new CoreProjectGenerator(generatorConfiguration, commandLineService);

                //return new SolutionGenerator(generatorConfiguration, commandLineService, webApiGenerator, coreGenerator);
                return new EntityGenerator(generatorConfiguration, liquidTemplateService);
            });

            return services;
        }
    }
}