using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Services;
using Nt.Scripts.Ninjascripts.Charts;
using System;

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
                    sc.Add<IChartDataService, ChartDataScript>((sp) => new ChartDataScript()
                    {
                        InstrumentName = "MES",
                        TradingHoursName = "Central Standard Time"
                    });
                    sc.Add<ChartStyleService>((sp) =>
                    {
                        var data = (ChartDataScript)sp.GetService(typeof(IChartDataService));
                        return new ChartStyleService()
                        {
                        };
                    });
                })
                .Build();

            host.Start(null);

            var chartData = host.Services.GetService<IChartDataService>();
            //object configureServices = host.Services.GetServices<IChartDataService>();
            //object configureServices = host.Services.GetServices<IDataLoadedService>();
            host.ExecuteServices<IDataLoadedService>();

        }
    }
}
