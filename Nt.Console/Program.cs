using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NinjaTrader.NinjaScript;
using Nt.Core;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using static ConsoleApp.ConsoleHelpers;

namespace ConsoleApp
{
    internal class Program
    {

        //static SimulatorTests simulatorTests = new SimulatorTests();
        //static SessionTimeTests sessionTimeTests = new SessionTimeTests();
        //static SessionHoursTests sessionHoursTests = new SessionHoursTests();

        public static void Main(string[] args)
        {

            ISessionsManager sessionsManager = (ISessionsManager)(ISessionsManagerBuilder)((ISessionsManagerBuilder)SessionsManager.CreateDefaultBuilder())
                .Add<SessionFilters, SessionFiltersOptions>((op) =>
                {
                    op.Name = "Session Filters";
                    op.Calculate = Calculate.OnEachTick;
                    op.BarsRequiredToPlot = 50;
                    op.AddDateFilters(year: 2020, isInitial: true);
                    op.AddDateFilters(year: 2022, isInitial: false);
                    op.AddDateFilters(new DateTime(2020, 6, 12), new DateTime(2022, 9, 20));
                })
                .Add<SessionHours, SessionHoursOptions>((op) =>
                {
                    op.Name = "Session Hours";
                })
                .Build();

            //INinjascript sessionFilters = 
            //    SessionFilters.CreateDefaultBuilder()
            //    .Configure<SessionFilters, SessionFiltersOptions>((op) =>
            //    {
            //        op.Name = "Session Filters";
            //        op.Calculate = Calculate.OnEachTick;
            //        op.BarsRequiredToPlot = 50;
            //        op.AddDateFilters(year: 2020, isInitial: true);
            //        op.AddDateFilters(year: 2022, isInitial: false);
            //        op.AddDateFilters(new DateTime(2020, 6, 12), new DateTime(2022, 9, 20));
            //    })
            //    .Build();

            //INinjascript sessionHours = SessionHours.CreateDefaultBuilder()
            //    .Configure<SessionHours,SessionHoursOptions>((op) =>
            //    {
            //        op.Name = "Session Hours";
            //    })
            //    .Build();

            INinjascript sessionStats = SessionStats.CreateDefaultBuilder()
                .Build();

            INinjascript sessionHoursList = SessionHoursList.CreateDefaultBuilder()
                .Configure<SessionHoursList,SessionHoursListOptions>((op) =>
                {
                    op.MaxSessionsToStored = 13;
                })
                .Build();

            //sessionsManager.Add(sessionFilters);
            //sessionsManager.Add(sessionHours);
            //sessionsManager.Add(sessionHoursList);

            Wait();

            //builder.Configure((op) =>
            //{
            //    op.Calculate = Calculate.OnEachTick;
            //});

            //SessionFiltersBuilder sessionFiltersBuilder1 = SessionFilters.CreateBuilder<SessionFiltersBuilder>();

            //sessionFilters = sessionFiltersBuilder.Build();

            //SessionFilters.CreateBuilder<SessionFiltersBuilder>()
            //.Build(null);



            // OnStateChanged => State.Configure
            //SessionsManager sessionManager =
            //    SessionsManager.CreateSessionManagerBuilder()
            //    .ConfigureProperties((p) =>
            //    {
            //        p.Calculate = Calculate.OnBarClose;
            //    })
            //    .UseSessionHoursList((o) =>
            //    {
            //        o.MaxSessionsToStored = 100;
            //    })
            //    .UseSessionFilters((o) =>
            //    {
            //        o.AddDateFilters(
            //            year: 2000,
            //            month: 6,
            //            day: 15,
            //            isInitial: true
            //            );
            //        o.IncludePartialHolidays = false;
            //    })
            //    .UseSessionFilters((filters) =>
            //    {
            //        filters.AddDateFilters(
            //            finalYear: 2023,
            //            finalMonth: 9,
            //            finalDay: 5
            //            );
            //    })
            //    .Build(null);

            //var th = NinjaTrader.Data.TradingHours.Get("CME US Index Futures ETH");
            //var th2 = NinjaTrader.Data.TradingHours.String2TradingHours("CME US Index Futures ETH");

            //var s = sessionManager;
            //var f = sessionManager.SessionFilters;
            //bool check = f.Check();

            //sessionManager.Load (null,null);

            Wait();

        }

        //public static void SessionOptions(SessionsManagerOptions options)
        //{
        //    options.MaxSessionsToStored = 100;
        //}

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
