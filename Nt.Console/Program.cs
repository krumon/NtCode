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
            //TradingSessionTests tradingSessionTests = new TradingSessionTests();

            //tradingSessionTests.Run();

            //ISessionProvider sessionProvider = KrumonTrade.CreateDefaultSessionBuilder()
            //    .AddSessionCollectionByTypes
            //    (
            //        SessionType.Asian, 
            //        SessionType.European,
            //        SessionType.American_RS_EOD,
            //        SessionType.Asian_RS,
            //        SessionType.AmericanAndEuropean,
            //        SessionType.Regular,
            //        SessionType.Electronic
            //    )

            //    .Build();
            //Microsoft.Extensions.DependencyInjection.ServiceProvider serviceProvider;

            IHost host = Hosting.CreateDefaultBuilder()
                .ConfigureHostOptions((options) =>
                {
                    options.IsInDesignMode = true;
                })
                .ConfigureServices((sc) =>
                {
                    sc.Add<ChartDataService>(new ChartDataService()
                    {
                        UserTimeZoneInfo = TimeZoneInfo.Local,
                        InstrumentName = "MES",
                        TradingHoursName = "Central Standard Time"
                    });
                    sc.Add<ChartDataService_2>((sp) =>
                    {
                        var data = (ChartDataService)sp.GetService(typeof(ChartDataService));
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

            var chartDataService = host.Services.GetService<ChartDataService>();
            var chartDataService_2 = host.Services.GetService<ChartDataService_2>();
            //IServiceProvider dataSeries = (IServiceProvider)host.GetService(OptionalServiceType.DataSeries);
            //IRequiredService chartData = (IRequiredService)host.GetService(RequiredServiceType.Data);
             

        }
    }
}
