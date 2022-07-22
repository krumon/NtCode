using NtCore;
using System;
using NinjaTrader.Client;
using System.Timers;

namespace NtConsole
{
    internal class Program
    {
        public static Client client;
        public static Timer timer;

        static void Main(string[] args)
        {

            PrintTradingSessions();

            //TimeSpanTester();

            //ATIConnection();

            Console.ReadKey();

        }

        private static void PrintTradingSessions()
        {
            foreach (var timeZone in TradingSession.Asian.ToArray())
                Console.WriteLine(timeZone.ToSessionHours().ToString());
        }

        private static void ATIConnection()
        {
            client = new Client();
            int connect = client.Connected(1);
            client.SubscribeMarketData("MES");

            Console.WriteLine(String.Format("{0} | Connected to NT8: {1}", DateTime.Now, connect.ToString()));
            Console.WriteLine(String.Format("Cash Value: {0}", client.CashValue("")));
            Console.WriteLine(String.Format("Buying Power: {0}", client.BuyingPower("")));

            timer = new Timer()
            {
                Interval = 1000
            };

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

            Console.ReadKey();

            client.UnsubscribeMarketData("MES");
            timer.Elapsed -= Timer_Elapsed;
            timer.Enabled = false;
            timer.Dispose();
            timer.Close(); // Creo que no es necesario
            
            int disconnect = client.TearDown();
            Console.WriteLine(String.Format("{0} | Disconnected to NT8: {1}", DateTime.Now, disconnect.ToString()));
            Console.ReadKey();

        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (client == null)
                return;

            double lastPrice = client.MarketData("MES", 0);

            Console.WriteLine(string.Format("{0} | Last: {1}", DateTime.Now, lastPrice));
        }

        private static void TimeSpanTester()
        {
            SessionTime sessionTime1 = SessionTime.CreateCustomSessionTime(TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"),22,0,0);
            SessionTime sessionTime2 = SessionTime.CreateCustomSessionTime(TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"),2,0,0);

            Console.WriteLine(String.Format("Utc Offset: {0}",sessionTime1.TimeZoneInfo.BaseUtcOffset.ToString()));
            Console.WriteLine(String.Format("Time 1: {0}",sessionTime1.UtcTime.ToString()));
            Console.WriteLine(String.Format("Time 2: {0}",sessionTime2.UtcTime.ToString()));
        }

    }
}
