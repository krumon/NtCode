using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
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
            //Microsoft.Extensions.Hosting.HostBuilder
            //Microsoft.Extensions.Logging.ConsoleLoggerExtensions
            //Microsoft.Extensions.Configuration.ConfigurationProvider
            //Microsoft.Extensions.Hosting.Internal.ConsoleLifetime


            UseNinjascriptHost();
            _logger = _host?.Services.GetService<ILogger<Program>>();
            _logger?.LogInformationSource("The Host is built");
            _logger?.Log(LogLevel.Information, "Logging Service");
            _logger?.LogCritical("Logging a message by {format}.",format);
            IConfigurationRoot config = (IConfigurationRoot)(_host?.Services.GetService<HostBuilderContext>().Configuration);
            var section = config.GetSection("Logging").GetSection("File").GetSection("LogLevel").GetSection("Default");
        }

        private static void UseNinjascriptHost()
        {
            _host = Hosting.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context,builder) =>
            {
                builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                services.AddLogging((builder) =>
                {
                    builder.AddConsole();
                });
                services.AddTransient<IConfigureService, GlobalsDataDesignScript>();
                services.AddSingleton<IConfigureService, ChartDataDesignScript>();
                services.AddScoped<IConfigureService, ChartDataDesignScript>();
                services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureService, GlobalsDataDesignScript>());
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
            _host = Hosting.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddLogging(configure =>
                    {
                        configure.SetMinimumLevel(LogLevel.Information);
                        configure.AddConsole();
                    })
                    .AddSingleton<IGlobalsDataService>(new GlobalsDataDesignScript())
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

    }
}
