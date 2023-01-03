//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging.Configuration;
//using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Primitives;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Core.Logging.Console;
using Nt.Core.Services;
using Nt.Scripts;
using Nt.Scripts.Ninjascripts.Design;
using System;
using LogLevel = Nt.Core.Logging.LogLevel;

namespace ConsoleApp
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            //System.IServiceProvider sc;
            //sc.GetServices
            //Microsoft.Extensions.Logging.ConsoleLoggerExtensions
            Microsoft.Extensions.DependencyInjection.ServiceDescriptor sd;
            Microsoft.Extensions.DependencyInjection.ServiceProvider sp;
            Microsoft.Extensions.DependencyInjection.ServiceCollection src = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            //Microsoft.Extensions.DependencyInjection.Extensions.ServiceCollectionDescriptorExtensions            

            //sc.AddSingleton<IOnBarUpdateService>();
            //Microsoft.Extensions.Options.OptionsBuilder options;

            //builder.SetMinimumLevel

            //Microsoft.Extensions.DependencyInjection.ServiceProvider
            //Microsoft.Extensions.Options.Options
            //IHost host = Hosting.CreateDefaultBuilder()
            //    .ConfigureHostOptions((options) =>
            //    {
            //        options.IsInDesignMode = true;
            //    })
            //    .ConfigureServices((serviceCollection) => 
            //    {
            //        serviceCollection
            //        .AddLogging(configure => 
            //        {
            //            configure.SetMinimumLevel(LogLevel.Information);
            //            configure.AddConsole();
            //        })
            //        .Add<IGlobalsDataService>(new GlobalsDataDesignScript())
            //        .Add<IChartDataService, ChartDataDesignScript>()
            //        .AddSessions<ISessionsService>((builder) =>
            //        {
            //            builder.ConfigureFilters((options) =>
            //            {
            //                options.IncludePartialHolidays = false;
            //                options.IncludeHolidays = false;
            //                options.ExcludeHistoricalData = true;
            //            });
            //        })
            //        ;
            //    })
            //    .Build();

            IHost host = Hosting.CreateDefaultBuilder()
                .ConfigureHostOptions((options) =>
                {
                    options.IsInDesignMode = true;
                })
                .ConfigureServices((sc) => 
                {
                    sc.AddLogging((o) =>
                    {
                        o.AddConsole();
                    });
                    sc.AddTransient<IConfigureService,GlobalsDataDesignScript>();
                    sc.AddSingleton<IConfigureService, ChartDataDesignScript>();
                    sc.AddScoped<IConfigureService, ChartDataDesignScript>();
                    sc.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureService, GlobalsDataDesignScript>());

                    //sc.AddLogging(configure => 
                    //{
                    //    configure.SetMinimumLevel(LogLevel.Information);
                    //    configure.AddConsole();
                    //})
                })
                .Build();

            Nt.Core.Logging.ILogger<Program> logger = host.Services.GetService<Nt.Core.Logging.ILogger<Program>>();
            logger.LogInformationSource("The Host is built");
            logger.Log(LogLevel.Trace,"Logging Service",Array.Empty<string>());
            //var globalsData = host.Services.GetService<IGlobalsDataService>();
            //var sessionsIterator = host.Services.GetService<ISessionsIteratorService>();
            //var sessionsFilters = host.Services.GetService<ISessionsFiltersService>();
            //var session = host.Services.GetService<ISessionsService>();
            //var session2 = host.Services.GetService<ISessionScript>();

            //sessionsIterator.ActualSessionBegin = DateTime.Now;

            host.Configure(null);
            host.DataLoaded(null);
            host.OnBarUpdate();
            host.OnMarketData();
            host.OnSessionUpdate();
            host.Dispose();
            
        }
    }
}
