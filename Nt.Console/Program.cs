using NinjaTrader.Data;
using Nt.Core.Data;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Services;
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
                    sc.Add<ChartDataService>(new ChartDataService()
                    {
                        InstrumentName = "MES",
                        TradingHoursName = "Central Standard Time"
                    });
                    sc.Add<ChartStyleService>((sp) =>
                    {
                        var data = (ChartDataService)sp.GetService(typeof(ChartDataService));
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

            var chartDataService = host.Services.GetService<ChartDataService>();
            var chartStyleService = host.Services.GetService<ChartStyleService>();
                
        }
    }
}
