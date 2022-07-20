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
            //foreach(var timeZone in TradingSession.Asian.ToArray())
            //    Console.WriteLine(timeZone.ToSessionHours().ToString());

            client = new Client();
            int connect = client.Connected(1);
            client.SubscribeMarketData("MES");

            Console.WriteLine(String.Format("{0} | Connected to NT8: {1}",DateTime.Now,connect.ToString()));

            timer = new Timer()
            {
                Interval = 1000
            };

            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;

            Console.WriteLine("Antes del red key");

            Console.ReadKey();
            
            Console.WriteLine("Después del red key");
            
            client.UnsubscribeMarketData("MES");
            timer.Elapsed -= Timer_Elapsed;
            timer.Enabled = false;
            timer.Dispose();

            Console.WriteLine("Fin del programa.");
            if (timer != null)
            {
                Console.WriteLine("El timer sigue activo");
                Console.WriteLine(timer.Enabled.ToString());
                Console.WriteLine(timer.Container);

            }
            timer.Close();
            int disconnect = client.TearDown();
            Console.WriteLine(String.Format("{0} | Disconnected to NT8: {1}", DateTime.Now, disconnect.ToString()));
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //if (client == null )
            //    return;

            //double lastPrice = client.MarketData("MES", 0);

            //Console.WriteLine(string.Format("{0} | Last: {1}",DateTime.Now,lastPrice));
        }
    }
}
