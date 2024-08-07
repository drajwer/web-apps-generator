﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using WebAppsGenerator.Core.Interfaces;
using WebAppsGenerator.Core.Services;
using WebAppsGenerator.Generating.Abstract.Interfaces;
using WebAppsGenerator.Generating.Abstract.Services;
using WebAppsGenerator.Generating.Abstract.Services.Validators;

namespace WebAppsGenerator.Generating.Abstract.IoC
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add base generator with dependencies
        /// </summary>
        public static IServiceCollection AddRootGenerator(this IServiceCollection services)
        {
            services.AddScoped<RootGenerator>();

            services.AddScoped<LiquidTemplateService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IEntitiesFixer, EntitiesFixer>();
            services.AddScoped<OverwriteServiceFactory>();
            services.AddScoped<IOverwriteService>(context => context.GetRequiredService<OverwriteServiceFactory>().Create());
            services.AddScoped<OverwriteFileGenerator>();
            return services;
        }

        /// <summary>
        /// Add all validators from <see cref="Abstract"/> assembly
        /// </summary>
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            var assembly = Assembly.GetAssembly(typeof(IValidator));
            foreach (var validatorType in GetAssemblyValidators(assembly))
            {
                services.AddScoped(typeof(IValidator), validatorType);
            }
            services.AddScoped<RootValidator>();

            return services;
        }

        private static IEnumerable<Type> GetAssemblyValidators(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(t => typeof(IValidator).IsAssignableFrom(t) && !t.IsEquivalentTo(typeof(RootValidator)) && !t.IsInterface && !t.IsAbstract);
        }
    }
}
