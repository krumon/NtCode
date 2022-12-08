using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using ConsoleApp;
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
                sc.Add<IChartDataScript, ChartDataScript>();// (sp) => new ChartDataDesignScript());
                    sc.Add<IChartStyleService, ChartStyleService>((sp) => new ChartStyleService(sp.GetService<IChartDataService>()));
                })
                .Build();

            var chartData = host.Services.GetService<IChartDataService>();
            var chartStyle = host.Services.GetService<IChartStyleService>();
            
            host.Configure(null);
            host.DataLoaded(null);
            host.OnBarUpdate();
            host.OnMarketData();
            host.Dispose();

        }
    }
}
