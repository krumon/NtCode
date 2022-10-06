using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nt.Core.Services;
using System;
using System.IO;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Nt.Core.Hosting
{
    public static class NinjascriptHost
    {
        private static IHost ninjascriptHost;

        public static IServiceProvider Services => ninjascriptHost.Services;

        public static IConfiguration Configuration => Services.GetRequiredService<IConfiguration>();

        public static 

        public static IHost CreateHost()
        {
            return ninjascriptHost = Host.CreateDefaultBuilder()
                    .ConfigureLogging((builder) =>
                    {
                        builder.ClearProviders()
                        .AddNinjascriptConsoleLogger(configuration =>
                        {
                            // Replace warning value from appsettings.json of "Cyan"
                            configuration.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.Cyan;
                            // Replace warning value from appsettings.jsno of "Red"
                            configuration.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.Red;
                        });
                        
                    })
                    .ConfigureAppConfiguration((context,builder) =>
                    {
                        builder.SetBasePath(Path.Combine(Assembly.GetExecutingAssembly().Location))
                            .AddJsonFile()
                    })
                    .Build();
        }

        public static Task RunAsync() => ninjascriptHost?.RunAsync();

    }
}
