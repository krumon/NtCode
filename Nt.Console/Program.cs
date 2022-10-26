using Nt.Core;
using Nt.Core.Tests;
using Nt.Core.Trading;

namespace ConsoleApp
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            TradingSessionTests tradingSessionTests = new TradingSessionTests();

            tradingSessionTests.Run();

            ITradingSession tradingSession = KrumonTrade.CreateTradingSessionBuilder()
                .Build();
            
        }
    }
}
