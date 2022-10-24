using ConsoleApp.Tests;

namespace ConsoleApp
{
    internal class Program
    {

        public static void Main(string[] args)
        {
            TradingSessionTests tradingSessionTests = new TradingSessionTests();

            tradingSessionTests.Run();
            
        }
    }
}
