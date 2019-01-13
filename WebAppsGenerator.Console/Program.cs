using System;
using Microsoft.Extensions.DependencyInjection;

namespace WebAppsGenerator.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = ArgsParser.ParseArguments(args);
            if (options == null)
                return;

            var startup = new Startup(options);
            var serviceProvider = ConfigureServices(startup);

            var application = serviceProvider.GetService<Application>();
            application.Run();
        }

        private static IServiceProvider ConfigureServices(Startup startup)
        {
            var services = new ServiceCollection();

            startup.ConfigureServices(services);
            return services.BuildServiceProvider();
        }
    }
}
