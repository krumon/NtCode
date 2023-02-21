using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Core.Logging.Configuration;
using Nt.Core.Services;
using Nt.Scripts;
using Nt.Scripts.Ninjascripts.Design;
using Nt.Scripts.Services;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace ConsoleApp
{
    internal class Program
    {
        private static IHost _host;
        private static ILogger _logger;

        public static void Main(string[] args)
        {
            //Stopwatch timeMeasure = new Stopwatch();
            //Console.WriteLine($"Tiempo: {timeMeasure.Elapsed.TotalMilliseconds} ms");

            Microsoft.Extensions.DependencyInjection.ServiceDescriptor sd;
            Microsoft.Extensions.DependencyInjection.ServiceProvider sp;
            Microsoft.Extensions.DependencyInjection.ServiceCollection src = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            //Microsoft.Extensions.Logging.Console.
            //Microsoft.Extensions.Hosting.Internal.Host
            //Microsoft.Extensions.Hosting.HostBuilder
            //Microsoft.Extensions.Configuration.ConfigurationProvider
            //Microsoft.Extensions.Hosting.Internal.ConsoleLifetime
            //Microsoft.Extensions.Primitives.ChangeToken


            //UseNinjascriptHost();
            UseNinjascriptHostingServices();
            _logger = _host?.Services.GetService<ILogger<Program>>();
            //_logger?.LogInformation(new Exception("Exception test"), "Simple message.");
            _logger?.LogTraceSource("Logging message test from {0}.", typeof(Program));
            _logger?.LogDebugSource("Logging message test from {0}.", typeof(Program));
            _logger?.LogInformationSource("Logging message test from {0}.", typeof(Program));
            _logger?.LogWarning("Logging message test from {0}.", typeof(Program));
            _logger?.LogError("Logging message test from {0}.", typeof(Program));
            _logger?.LogCritical("Logging message test from {0}.", typeof(Program));
            //_logger?.LogWarning("Logging Service");
            //_logger?.LogError("Logging a message by {format}.",format);
            //_logger.LogInformation("Hello World!");

            IConfigurationRoot config = (IConfigurationRoot)(_host?.Services.GetService<HostBuilderContext>().Configuration);
            IConfiguration configure = _host?.Services.GetService<IConfiguration>();
        }

        private static void UseNinjascriptHostingServices()
        {
            _host = Nt.Scripts.Hosting.Host.CreateNinjascriptDefaultBuilder(null).Build();
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
                    services.AddScoped<IChartDataService, ChartDataDesignScript>()
                    //.AddSessions<ISessionsService>((builder) =>
                    //{
                    //    builder.ConfigureFilters((options) =>
                    //    {
                    //        options.IncludePartialHolidays = false;
                    //        options.IncludeHolidays = false;
                    //        options.ExcludeHistoricalData = true;
                    //    });
                    //})
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
