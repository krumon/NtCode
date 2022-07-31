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
        public static NtSimulator simulator;

        static void Main(string[] args)
        {
            // Inicializo el simulador con un intervalos de un segundo y un factor de tiempo de un minuto.
            simulator = new NtSimulator
            {
                Interval = 1000,            // Reloj virtual que lanza un evento cada segundo.
                SpeddFactor = 60,           // Cada segundo hago que pase un minute en el reloj de simulación.
                ShowTimeInConsole = true,   // Muestro en consola el tiempo.
                ShowBarInConsole = true,    // Muestro en pantalla los valores de la barra.
            };

            // Me subscribo al evento BarUpdated para ejecutar las pruebas.
            simulator.BarUpdated += Simulator_BarUpdated; 

            // Comienzo la simulación.
            simulator.Start();

            // Paro el hilo principal de la aplicación para poder ver las pruebas en la consola.
            Console.ReadKey();

            // Libero la memoria.
            Dispose();

        }

        private static void Simulator_BarUpdated(Bar bar)
        {
            simulator.ShowText = string.Format("Index: {0}", bar.Idx.ToString());
        }

        private static void Dispose()
        {
            simulator.BarUpdated -= Simulator_BarUpdated;
            simulator.Dispose();
        }

        private static void NtSessionHoursIteratorTest()
        {
            KrSession session = new KrSession();
            session.SessionHours.Iterator(() =>
            {
                Console.WriteLine("Estoy dentro del iterador.");
            });
        }

        private static void CreateAndPrintNtSession()
        {
            // Create
            KrSession session = new KrSession();

            // Print
            Console.WriteLine("NINJATRADER SESSION");
            Console.WriteLine("-------------------");
            Console.WriteLine(session.SessionHours.ToString());
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

            timer.Elapsed += ATI_Timer_Elapsed;
            timer.Enabled = true;

            Console.ReadKey();

            client.UnsubscribeMarketData("MES");
            timer.Elapsed -= ATI_Timer_Elapsed;
            timer.Enabled = false;
            timer.Dispose();
            timer.Close(); // Creo que no es necesario
            
            int disconnect = client.TearDown();
            Console.WriteLine(String.Format("{0} | Disconnected to NT8: {1}", DateTime.Now, disconnect.ToString()));
            Console.ReadKey();

        }

        private static void TimeSpanTester()
        {
            SessionTime sessionTime1 = SessionTime.CreateCustomSessionTime(TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"),22,0,0);
            SessionTime sessionTime2 = SessionTime.CreateCustomSessionTime(TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"),2,0,0);

            Console.WriteLine(String.Format("Utc Offset: {0}",sessionTime1.TimeZoneInfo.BaseUtcOffset.ToString()));
            Console.WriteLine(String.Format("Time 1: {0}",sessionTime1.ToUtcTime.ToString()));
            Console.WriteLine(String.Format("Time 2: {0}",sessionTime2.ToUtcTime.ToString()));
        }

        private static void ATI_Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (client == null)
                return;

            double lastPrice = client.MarketData("MES", 0);

            Console.WriteLine(string.Format("{0} | Last: {1}", DateTime.Now, lastPrice));
        }

    }
}
