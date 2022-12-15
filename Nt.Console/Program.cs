using Microsoft.Extensions.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Services;
using Nt.Scripts.Ninjascripts.Design;

namespace ConsoleApp
{
    internal class Program
    {

        public static void Main(string[] args)
        {
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
                    //.Add<IGlobalsDataScript, GlobalsDataDesignScript>()
                    .Add<IChartDataService, ChartDataDesignScript>()
                    //.Add<ISessionScript, SessionDesignScript>((sp) => new SessionDesignScript((IGlobalsDataScript)sp.GetService<IGlobalsDataService>()))
                    .Add<ISessionsIteratorService, SessionsIteratorDesignScript>()
                    //.AddSessionService<SessionDesignScript>((builder) =>
                    //{
                    //    builder
                    //    .AddFilters()
                    //    .AddStats()
                    //    .AddGenericSessions()
                    //    .AddCustomSessions();
                    //})

                    ;
                })
                .ConfigureSessions((builder) =>
                {
                    builder.ConfigureFilters((options) =>
                    {
                        options.IncludePartialHolidays = false;
                        options.IncludeHolidays = false;
                        options.ExcludeHistoricalData = true;
                    });
                })
                .Build();

            var globalsData = host.Services.GetService<IGlobalsDataService>();
            var chartData = host.Services.GetService<IChartDataService>();
            var session = host.Services.GetService<ISessionsIteratorService>();
            //var session2 = host.Services.GetService<ISessionScript>();

            host.Configure(null);
            host.DataLoaded(null);
            host.OnBarUpdate();
            host.OnMarketData();
            host.OnSessionUpdate();
            host.Dispose();
            
        }
    }
}
