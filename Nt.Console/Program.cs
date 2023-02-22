using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Core.Logging.Configuration;
using Nt.Core.Services;
using Nt.Scripts;
using Nt.Scripts.DI;
using Nt.Scripts.Services;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace ConsoleApp
{
    internal class Program
    {
        //private static IHost _host;
        private static ILogger _logger;

        private static void SearchingInMS()
        {
            //Microsoft.Extensions.DependencyInjection.ServiceDescriptor sd;
            //Microsoft.Extensions.DependencyInjection.ServiceProvider sp;
            //Microsoft.Extensions.DependencyInjection.ServiceCollection src = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            //Microsoft.Extensions.Logging.Console.
            //Microsoft.Extensions.Hosting.Internal.Host
            //Microsoft.Extensions.Hosting.HostBuilder
            //Microsoft.Extensions.Configuration.ConfigurationProvider
            //Microsoft.Extensions.Hosting.Internal.ConsoleLifetime
            //Microsoft.Extensions.Primitives.ChangeToken
        }

        public static void Main(string[] args)
        {

            UseNinjascriptHostingServices();

            if (NinjaHost.Host == null)
                throw new ArgumentNullException(nameof(NinjaHost.Host));

            _logger = NinjaHost.Logger<Program>();
            _logger.LogInformation("Hello World!");

            IConfigurationRoot config = (IConfigurationRoot)(NinjaHost.Host?.Services.GetService<HostBuilderContext>().Configuration);
            IConfiguration configure = NinjaHost.Host?.Services.GetService<IConfiguration>();
        }

        private static void UseNinjascriptHostingServices()
        {
            NinjaHost.Create(Nt.Scripts.Hosting.Host.CreateNinjascriptDefaultBuilder(null).Build());
        }
        private static void UseNinjascriptHost()
        {
            if (NinjaHost.Host == null)
                return;

            NinjaHost.Create(Host.CreateDefaultBuilder()
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
            })
            .Build());

            NinjaHost.Host.Configure(null);
            NinjaHost.Host.DataLoaded(null);
            NinjaHost.Host.OnBarUpdate();
            NinjaHost.Host.OnMarketData();
            NinjaHost.Host.OnSessionUpdate();
            NinjaHost.Host.Dispose();

        }
        private static void UseNinjascriptHost2()
        {
            NinjaHost.Create(Host.CreateDefaultBuilder()
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
                    services.AddSingleton<IChartBarsData, ChartBarsData>()
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
                .Build());
        }
        private static void UseColorConsoleLogger()
        {
            NinjaHost.Create(Host.CreateDefaultBuilder()
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
                .Build());
        }
    }
}
