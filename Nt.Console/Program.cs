using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Nt.Connect;
using Nt.Core;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
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
            SessionsManager sessionManager = new SessionsManager();
            sessionManager.Configure(SessionOptions);

            //SessionsManager sessionManager = 
            //    new SessionsManager()
            //    .Configure((options) =>
            //    {
            //        options.MaxSessionsToStored = 100;
            //    })
            //    .ConfigureFilters((filters) =>
            //    {
            //        filters.AddDateFilters(
            //            initialYear: 2000,
            //            initialMonth: 6,
            //            initialDay: 15
            //            );
            //    });

            var s = sessionManager;
            //sessionManager.Load(null, null);
            Wait();

        }

        public static void SessionOptions(SessionManagerOptions options)
        {
            options.MaxSessionsToStored = 100;
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
