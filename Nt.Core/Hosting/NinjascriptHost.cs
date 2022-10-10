using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Nt.Core.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Nt.Core.Hosting
{
    public class NinjascriptHost : IDisposable
    {
        private IHost ninjascriptHost;

        public IServiceProvider Services => ninjascriptHost.Services;

        public IConfiguration Configuration => Services.GetRequiredService<IConfiguration>();

        public IHostEnvironment Environment => Services.GetRequiredService<IHostEnvironment>();

        public IHostBuilder CreateDefaultBuilder()
        {
            return Host
                .CreateDefaultBuilder()
                //.UseEnvironment("Development")
                .ConfigureServices(services =>
                {
                    services.AddHostedService<NinjascriptLifetimeHostedService>();
                })
                .ConfigureLogging((builder) =>
                {
                    builder.ClearProviders()
                    .AddNinjascriptConsoleLogger(configuration =>
                    {
                        // Replace warning value from appsettings.json of "Cyan"
                        configuration.LogLevelToColorMap[LogLevel.Warning] = ConsoleColor.Magenta;
                        // Replace warning value from appsettings.jsno of "Red"
                        configuration.LogLevelToColorMap[LogLevel.Error] = ConsoleColor.DarkRed;
                    });
                });
                //.ConfigureHostConfiguration(builder =>
                //{
                //    builder.AddEnvironmentVariables("DOTNET_");
                //});
        }

        public IHost Build()
        {
            return ninjascriptHost = CreateDefaultBuilder().Build();
        }

        public async Task<bool> RunAsync() 
        { 
            await ninjascriptHost?.RunAsync(); 

            return true;
        }
        public async Task StartAsync() => await ninjascriptHost?.StartAsync();
        public async Task StopAsync() => await ninjascriptHost?.StopAsync();

        public void Dispose()
        {
            ninjascriptHost?.Dispose();
        }
    }
}
