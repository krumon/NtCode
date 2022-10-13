using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NinjaTrader.NinjaScript;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Nt.Core.Ninjascript;
using Nt.Core;
using static ConsoleApp.ConsoleHelpers;
using Nt.Core.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsoleApp
{
    internal class Program
    {

        //static SimulatorTests simulatorTests = new SimulatorTests();
        //static SessionTimeTests sessionTimeTests = new SessionTimeTests();
        //static SessionHoursTests sessionHoursTests = new SessionHoursTests();

        public static async Task Main(string[] args)
        {
            Task waittingTheHost;

            NinjascriptHosting host = new NinjascriptHosting();
            host.Build();
            waittingTheHost = host.RunAsync();

            IManager sessionsManager = (IManager)SessionsManager.CreateDefaultBuilder()
            .AddSessionFilters((op) =>
            {
                op.Name = "Session Filters";
                op.Calculate = Calculate.OnEachTick;
                op.BarsRequiredToPlot = 50;
                op.AddDateFilters(year: 2020, isInitial: true);
                op.AddDateFilters(year: 2022, isInitial: false);
                op.AddDateFilters(new DateTime(2020, 6, 12), new DateTime(2022, 9, 20));
                op.AddOrder(EventType.Configure, 5);
                op.AddOrder(EventType.BarUpdate, 1);
            })
            .AddSessionHours((op) =>
            {
                op.Name = "Session Hours 1";
                op.AddOrder(EventType.Configure, 4);
                op.AddOrder(EventType.BarUpdate, 2);
            })
            .AddSessionHours((op) =>
            {
                op.Name = "Session Hours 2";
                op.AddOrder(EventType.Configure, 3);
                op.AllowManagerMultiUse = true;
                op.AddOrder(EventType.BarUpdate, 3);
            })
            .AddSessionStats((op) =>
            {
                op.Name = "Session Stats 1";
                op.AddOrder(EventType.Configure, 2);
                op.AddOrder(EventType.BarUpdate, 4);
            })
            .AddSessionStats((op) =>
            {
                op.Name = "Session Stats 2";
                op.AddOrder(EventType.Configure, 1);
                op.AddOrder(EventType.BarUpdate, 5);
                op.AllowManagerMultiUse = true;
            })
            .Configure((op) =>
            {
                op.Name = "My Sessions Manager";
            })
            .Build();

            var list = sessionsManager.GetSortedList(EventType.BarUpdate);
            //sessionsManager.OnBarUpdate();

            //var logger = host.Services.GetRequiredService<ILogger<Program>>();
            //var configuration = host.Services.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>();
            //var s = configuration.GetSection("Environment").Value;

            //logger.LogDebug(1, "Does this line get hit?");    // Not logged
            //logger.LogInformation(3, "Nothing to see here."); // Logs in ConsoleColor.DarkGreen
            //logger.LogWarning(5, "Warning... that was odd."); // Logs in ConsoleColor.DarkCyan
            //logger.LogError(7, "Oops, there was an error.");  // Logs in ConsoleColor.DarkRed
            //logger.LogTrace(5, "== 120.");                   // Not logged

            //var th = NinjaTrader.Data.TradingHours.Get("CME US Index Futures ETH");
            //var th2 = NinjaTrader.Data.TradingHours.String2TradingHours("CME US Index Futures ETH");
            await waittingTheHost;
            
        }

        public static async Task HostingTest()
        {
            using (IHost host = Host.CreateDefaultBuilder().ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
            {
                configurationBuilder.Sources.Clear();

                IHostEnvironment env = hostingContext.HostingEnvironment;
                var path = "appsettings.json"; // Path.Combine(env.ContentRootPath, "appsettings.json");

                configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                configurationBuilder
                    .AddJsonFile(path, optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettingsNt.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables((configure) =>
                    {
                        configure.Prefix = "PROCESSOR_";
                    });

                IConfigurationRoot configurationRoot = configurationBuilder.Build();

                //TransientFaultHandlingOptions options = new TransientFaultHandlingOptions();
                //configurationRoot.GetSection(nameof(TransientFaultHandlingOptions))
                //                 .Bind(options);

                //Console.WriteLine($"TransientFaultHandlingOptions.Enabled={options.Enabled}");
                //Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={options.AutoRetryDelay}");

            }).Build())
            { 
                //Enter code

                await host.RunAsync();
            }

        }

    }
}
