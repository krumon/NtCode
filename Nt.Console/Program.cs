using Nt.Core.Data;

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
            Microsoft.Extensions.DependencyInjection.ServiceProvider serviceProvider;
            IHostService host = Host.CreateDefaultBuilder()
                .ConfigureHostOptions((options) =>
                {
                    options.IsInDesignMode = true;
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

            IServiceProvider dataSeries = (IServiceProvider)host.GetService(OptionalServiceType.DataSeries);
            IRequiredService chartData = (IRequiredService)host.GetService(RequiredServiceType.Data);
             

        }
    }
}
