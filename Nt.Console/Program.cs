using Nt.Core.Data;
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
                    sc.Add<IChartDataService, ChartData>((sp) => new ChartData()
                    {
                        InstrumentName = "MES",
                        TradingHoursName = "Central Standard Time"
                    });
                    sc.Add<ChartStyleService>((sp) =>
                    {
                        var data = (ChartData)sp.GetService(typeof(IChartDataService));
                        return new ChartStyleService()
                        {
                        };
                    });
                })
                .UseDataSeries((builder) =>
                {
                    builder.ConfigureServices((sc) =>
                    {
                        sc
                        .AddDataSerie(InstrumentCode.Default, PeriodType.Minute, 5)
                        .AddDataSerie(InstrumentCode.Default, PeriodType.Second, 5);
                    })
                    .ConfigureServiceOptions((options) =>
                    {

                    });
                })
                .Build();

            var chartData = host.Services.GetService<IChartDataService>();
            //object configureServices = host.Services.GetServices<IChartDataService>();
            object configureServices = host.Services.GetServices<IConfigureService>();

        }
    }
}
