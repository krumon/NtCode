using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Services;
using Nt.Scripts;
using Nt.Scripts.Ninjascripts.Design;
using System;

namespace ConsoleApp
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            //LoggerFactory
            //Microsoft.Extensions.Options.OptionsBuilder
            IHost host = Hosting.CreateDefaultBuilder()
                .ConfigureHostOptions((options) =>
                {
                    options.IsInDesignMode = true;
                })
                .ConfigureServices((serviceCollection) => 
                {
                    serviceCollection
                    .Add<IGlobalsDataService>(new GlobalsDataDesignScript())
                    .Add<IChartDataService, ChartDataDesignScript>()
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

            var globalsData = host.Services.GetService<IGlobalsDataService>();
            var sessionsIterator = host.Services.GetService<ISessionsIteratorService>();
            var sessionsFilters = host.Services.GetService<ISessionsFiltersService>();
            var session = host.Services.GetService<ISessionsService>();
            //var session2 = host.Services.GetService<ISessionScript>();

            sessionsIterator.ActualSessionBegin = DateTime.Now;

            host.Configure(null);
            host.DataLoaded(null);
            host.OnBarUpdate();
            host.OnMarketData();
            host.OnSessionUpdate();
            host.Dispose();
            
        }
    }
}
