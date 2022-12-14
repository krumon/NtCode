using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Services;
using Nt.Scripts.Ninjascripts;
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
                    .Add<IGlobalsDataScript>(new GlobalsDataDesignScript())
                    //.Add<IGlobalsDataScript, GlobalsDataDesignScript>()
                    .Add<IChartDataService, ChartDataDesignScript>()
                    //.Add<ISessionScript, SessionDesignScript>((sp) => new SessionDesignScript((IGlobalsDataScript)sp.GetService<IGlobalsDataService>()))
                    .Add<ISessionScript, SessionDesignScript>()
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
                    builder.ConfigureSessions((options) =>
                    {

                    });
                })
                .Build();

            var globalsData = host.Services.GetService<IGlobalsDataScript>();
            var chartData = host.Services.GetService<IChartDataService>();
            var session = host.Services.GetService<ISessionScript>();
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
