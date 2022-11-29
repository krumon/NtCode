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
                    sc.Add<DataService>(new DataService()
                    {
                        UserTimeZoneInfo = TimeZoneInfo.Local,
                        InstrumentName = "MES",
                        TradingHoursName = "Central Standard Time"
                    });
                    sc.Add<ChartDataService_2>((sp) =>
                    {
                        var data = (DataService)sp.GetService(typeof(DataService));
                        return new ChartDataService_2()
                        {
                            InstrumentName = data.InstrumentName,
                            TradingHoursName = "TH_2",
                            UserTimeZoneInfo = data.UserTimeZoneInfo
                        };
                    });
                })
                .UseDataSeries((builder) =>
                {
                    builder.ConfigureServices((sc) =>
                    {
                        sc
                        .AddDataSerie(InstrumentKey.Default, PeriodType.Minute, 5)
                        .AddDataSerie(InstrumentKey.Default, PeriodType.Second, 5);
                    })
                    .ConfigureServiceOptions((options) =>
                    {

                    });
                })
                .Build();

            var chartDataService = host.Services.GetService<DataService>();
            var chartDataService_2 = host.Services.GetService<ChartDataService_2>();
             

        }
    }
}
