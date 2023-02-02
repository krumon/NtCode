using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Core.Logging.Configuration;
using Nt.Core.Services;
using Nt.Scripts;
using Nt.Scripts.Ninjascripts.Design;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        private static IHost _host;
        private static ILogger _logger;
        private static string format = "Krumon";

        public static void Main(string[] args)
        {
            Microsoft.Extensions.DependencyInjection.ServiceDescriptor sd;
            Microsoft.Extensions.DependencyInjection.ServiceProvider sp;
            Microsoft.Extensions.DependencyInjection.ServiceCollection src = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            //Microsoft.Extensions.Logging.Console.
            //Microsoft.Extensions.Hosting.Internal.Host
            //Microsoft.Extensions.Configuration.ConfigurationProvider
            //Microsoft.Extensions.Hosting.Internal.ConsoleLifetime
            //Microsoft.Extensions.Primitives.ChangeToken


            //UseNinjascriptHost();
            UseColorConsoleLogger();
            _logger = _host?.Services.GetService<ILogger<Program>>();
            _logger?.LogInformationSource(new Exception("Exception test"), "Creating hosting services.");
            _logger?.LogWarning("Logging Service");
            _logger?.LogError("Logging a message by {format}.",format);
            using (_logger.BeginScope("[scope is enabled]"))
            {
                _logger.LogInformation("Hello World!");
                _logger.LogInformation("Logs contain timestamp and log level.");
                _logger.LogInformation("Systemd console logs never provide color options.");
                _logger.LogInformation("Systemd console logs always appear in a single line.");
            }


            IConfigurationRoot config = (IConfigurationRoot)(_host?.Services.GetService<HostBuilderContext>().Configuration);
            IConfiguration configure = _host?.Services.GetService<IConfiguration>();
        }

        private static void UseNinjascriptHost()
        {
            _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context,builder) =>
            {
                builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddLogging((builder) =>
                {
                    builder.SetMinimumLevel(LogLevel.Debug);
                    builder.AddConsole();
                    builder.AddDebug();
                    builder.AddConfiguration(context.Configuration.GetSection("Logging"));
                });
                //services.AddTransient<IConfigureService, GlobalsDataDesignScript>();
                //services.AddSingleton<IConfigureService, ChartDataDesignScript>();
                //services.AddScoped<IConfigureService, ChartDataDesignScript>();
                //services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureService, GlobalsDataDesignScript>());
            })
            .Build();

            _host.Configure(null);
            _host.DataLoaded(null);
            _host.OnBarUpdate();
            _host.OnMarketData();
            _host.OnSessionUpdate();
            _host.Dispose();

        }
        private static void UseNinjascriptHost2()
        {
            _host = Host.CreateDefaultBuilder()
                //.ConfigureAppConfiguration((context, builder) =>
                //{
                //    builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                //})
                .ConfigureServices((context, services) =>
                {
                    //services.AddLogging(builder =>
                    //{
                    //    builder.SetMinimumLevel(LogLevel.Information);
                    //    builder.AddConsole();
                    //    builder.AddDebug();
                    //});
                    services.AddSingleton<IGlobalsDataService>(new GlobalsDataDesignScript())
                    .AddScoped<IChartDataService, ChartDataDesignScript>()
                    .AddSessions<ISessionsService>((builder) =>
                    {
                        builder.ConfigureFilters((options) =>
                        {
                            options.IncludePartialHolidays = false;
                            options.IncludeHolidays = false;
                            options.ExcludeHistoricalData = true;
                        });
                    })
                    ;
                })
                .Build();
        }
        private static void UseColorConsoleLogger()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging(builder =>
                    {
                        builder.ClearProviders();
                        builder.AddConsole();
                        builder.AddFile();
                    })
                    ;
                })
                .Build();
        }
    }
}
