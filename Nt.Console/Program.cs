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

            IHost host = Hosting.CreateDefaultBuilder()
                .ConfigureHostOptions((options) =>
                {
                    options.IsInDesignMode = true;
                })
                .ConfigureServices((sc) =>
                {
                    sc.Add<IChartDataService, ChartDataDesignScript>((sp) => new ChartDataDesignScript());
                })
                .Build();

            var chartData = host.Services.GetService<IChartDataService>();
            
            host.Configure(null);
            host.DataLoaded(null);
            host.OnBarUpdate();
            host.MarketData();
            host.Dispose();

        }
    }
}
