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

            //PrintSessionTime();

            PrintTradingSessions();

            //TimeSpanTester();

            //ATIConnection();

            Console.ReadKey();

        }

        private static void PrintSessionTime()
        {
            while (true)
            {
                Console.WriteLine("TEST PARA IMPRIMIR POR CONSOLA UNA HORA ESPECÍFICA DE UNA SESIÓN.");
                Console.WriteLine("-----------------------------------------------------------------");

                Console.Write("- Introduzca el código de la session( AM | EU | AE | AS | EL | RG | OVN): ");
                string session = Console.ReadLine().ToUpper().Trim();

                if (string.IsNullOrEmpty(session) || string.IsNullOrWhiteSpace(session))
                    return;

                if (session == "AM" || session == "AS")
                {
                    Console.Write("- Es una sesión residual (Y/N): ");
                    string isResidual = Console.ReadLine().ToUpper().Trim();

                    if (isResidual == "Y")
                    {
                        session += "-RS";

                        if (session == "AM-RS")
                        {
                            Console.Write("- Introduce el código de la sesión residual: ( EXT | EOD | NWD ): ");
                            string specificSession = Console.ReadLine().ToUpper().Trim();

                            if (specificSession == "EXT" || specificSession == "EOD" || specificSession == "NWD")
                                session += "-" + specificSession;
                        }
                    }
                }

                Console.Write("- Introduzca el momento temporal de la sesión: ( O | C ): ");
                string price = Console.ReadLine().ToUpper();

                if (price == "O" || price == "C")
                    session += "-" + price;
                else
                    return;

                Console.WriteLine();
                Console.WriteLine(String.Format("Código: {0} | {1} | {2}", session, session.ToTradingTime().ToSessionTime().ToLocalTime.ToString(),session.ToTradingTime().ToDescription()));
                Console.ReadKey();
                Console.Clear();

            }


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
            Console.WriteLine(String.Format("Time 1: {0}",sessionTime1.ToUtcTime.ToString()));
            Console.WriteLine(String.Format("Time 2: {0}",sessionTime2.ToUtcTime.ToString()));
        }

    }
}
