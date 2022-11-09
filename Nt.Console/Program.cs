using Nt.Core;
using Nt.Core.Data;
using Nt.Core.Providers;
using Nt.Core.Tests;

namespace ConsoleApp
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            TradingSessionTests tradingSessionTests = new TradingSessionTests();

            tradingSessionTests.Run();

            ISessionProvider sessionProvider = KrumonTrade.CreateDefaultSessionBuilder()
                .AddSessionCollectionByTypes
                (
                    SessionType.Asian, 
                    SessionType.European,
                    SessionType.American_RS_EOD,
                    SessionType.Asian_RS,
                    SessionType.AmericanAndEuropean,
                    SessionType.Regular,
                    SessionType.Electronic
                )
                
                .Build();
            
        }
    }
}
