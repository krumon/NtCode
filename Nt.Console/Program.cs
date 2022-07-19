using NtCore;
using System;
using NinjaTrader.Client;
using System.Timers;

namespace NtConsole
{
    internal class Program
    {
        public static Client client = new Client();
        public static Timer timer;

        static void Main(string[] args)
        {
            //foreach(var timeZone in TradingSession.Asian.ToArray())
            //    Console.WriteLine(timeZone.ToSessionHours().ToString());

            int connect = client.Connected(1);
            Console.WriteLine(String.Format("{0} | Connected to NT8: {1}",DateTime.Now,connect.ToString()));

            client.SubscribeMarketData("MES");

            timer = new Timer()
            {
                Interval = 1000
            };

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

            Console.ReadKey();

            client.UnsubscribeMarketData("MES");
            timer.Enabled = false;
            timer.Elapsed -= Timer_Elapsed;
            timer.Dispose();

        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (client == null )
                return;

            double lastPrice = client.MarketData("MES", 0);

            Console.WriteLine(string.Format("{0} | Last: {1}",DateTime.Now,lastPrice));
        }
    }
}
