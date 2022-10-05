using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NinjaTrader.NinjaScript;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using static ConsoleApp.ConsoleHelpers;
using Nt.Core.Ninjascript;

namespace ConsoleApp
{
    internal class Program
    {

        //static SimulatorTests simulatorTests = new SimulatorTests();
        //static SessionTimeTests sessionTimeTests = new SessionTimeTests();
        //static SessionHoursTests sessionHoursTests = new SessionHoursTests();

        public static void Main(string[] args)
        {
            INinjascript sessionsManager = SessionsManager.CreateDefaultBuilder()
                .AddSessionFilters((op) =>
                {
                    op.Name = "Session Filters";
                    op.Calculate = Calculate.OnEachTick;
                    op.BarsRequiredToPlot = 50;
                    op.AddDateFilters(year: 2020, isInitial: true);
                    op.AddDateFilters(year: 2022, isInitial: false);
                    op.AddDateFilters(new DateTime(2020, 6, 12), new DateTime(2022, 9, 20));
                })
                .AddSessionHours((op) =>
                {
                    op.Name = "Session Hours";
                })
                .Configure((op) =>
                {
                    op.Name = "My Sessions Manager";
                })
                .Build();

            INinjascript sessionStats = SessionStats.CreateDefaultBuilder()
                .Build();

            INinjascript sessionHoursList = SessionHoursList.CreateDefaultBuilder()
                .Configure<SessionHoursList, SessionHoursListOptions>((op) =>
                {
                    op.MaxSessionsToStored = 13;
                })
                .Build();

            var builder = SessionsManager.CreateDefaultBuilder();
            
            
            // Code to throw errors.
            //INinjascript CreateBuilderException = new SessionFilters().CreateBuilder<SessionFilters, SessionStatsBuilder>().Build();
            //INinjascript CreateSetOptionsException = new SessionFilters();
            //CreateSetOptionsException.SetOptions(new SessionFiltersOptions());


            Wait();

            //var th = NinjaTrader.Data.TradingHours.Get("CME US Index Futures ETH");
            //var th2 = NinjaTrader.Data.TradingHours.String2TradingHours("CME US Index Futures ETH");

        }

        public static async Task HostingTest()
        {
            using (IHost host = Host.CreateDefaultBuilder().ConfigureAppConfiguration((hostingContext, configurationBuilder) =>
            {
                //configurationBuilder.Sources.Clear();

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

                TransientFaultHandlingOptions options = new TransientFaultHandlingOptions();
                configurationRoot.GetSection(nameof(TransientFaultHandlingOptions))
                                 .Bind(options);

                Console.WriteLine($"TransientFaultHandlingOptions.Enabled={options.Enabled}");
                Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={options.AutoRetryDelay}");

            }).Build())
            { 
                //Enter code

                await host.RunAsync();
            }

        }

    }
}
