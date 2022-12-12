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

            IHost host = Hosting.CreateDefaultBuilder()
                .ConfigureHostOptions((options) =>
                {
                    options.IsInDesignMode = true;
                })
                .ConfigureServices((sc) =>
                {
                    sc.Add<IGlobalsDataService, GlobalsDataDesignScript>();
                    sc.Add<IChartDataService, ChartDataDesignScript>();
                    sc.Add<ISessionService, SessionDesignScript>((sp) => new SessionDesignScript((IGlobalsDataScript)sp.GetService<IGlobalsDataService>()));
                })
                .Build();

            var globalsData = host.Services.GetService<IGlobalsDataService>();
            var chartData = host.Services.GetService<IChartDataService>();
            var session = host.Services.GetService<ISessionService>();
            
            host.Configure(null);
            host.DataLoaded(null);
            host.OnBarUpdate();
            host.OnMarketData();
            host.OnSessionUpdate();
            host.Dispose();
            
        }
    }
}
