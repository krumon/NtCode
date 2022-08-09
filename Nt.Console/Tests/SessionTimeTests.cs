using NtCore;
using System;

namespace ConsoleApp
{
    internal class SessionTimeTests : BaseTests
    {

        #region Private members

        SessionTime st;

        #endregion

        #region Public Properties

        public string SessionTimeText => st.ToString();
        public string SessionTimeLongText => st.ToLongString();

        #endregion

        #region Constructor

        /// <summary>
        /// Create a <see cref="SessionTimeTests"/> default instance.
        /// </summary>
        public SessionTimeTests()
        {
        }

        #endregion

        #region Public methods

        public override void Run()
        {
            // Comprobación de la instancia por defecto de SessionTime y
            // compruebo los dos métodos ToString.
            st = new SessionTime();
            //Console.WriteLine();
            //Console.WriteLine("Test de los métodos ToString...");
            //Console.WriteLine();
            WriteTitle("Test de los métodos ToString...");
            Console.WriteLine($"ToString Method => {st}");
            Console.WriteLine($"ToLongString Method => {st.ToLongString()}");

            // Asigno a st un SessinTime creado a través de un tipo.
            st = SessionTime.CreateSessionTimeByType(TradingTime.American_Open);
            WriteTitle("Creación de un objeto SessionTime por el tipo...");
            Console.WriteLine(st.ToLongString());
            Wait();
        }

        #endregion

        #region Private methods

        private void ToStringTest()
        {
            Console.WriteLine($"ToString Method => {SessionTimeText}");
            Console.WriteLine($"ToLongString Method => {SessionTimeLongText}");
        }

        #endregion

        static void PrintSession()
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
                Console.WriteLine(String.Format("Código: {0} | {1} | {2}", session, session.ToTradingTime().ToSessionTime().LocalTime.ToString(), session.ToTradingTime().ToDescription()));
                Console.ReadKey();
                Console.Clear();

            }


        }

        //private static void TimeSpanTester()
        //{
        //    SessionTime sessionTime1 = SessionTime.CreateCustomSessionTime(22,0,0, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));
        //    SessionTime sessionTime2 = SessionTime.CreateCustomSessionTime(2,0,0, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));

        //    Console.WriteLine(String.Format("Utc Offset: {0}",sessionTime1.TimeZoneInfo.BaseUtcOffset.ToString()));
        //    Console.WriteLine(String.Format("Time 1: {0}",sessionTime1.ToUtcTime.ToString()));
        //    Console.WriteLine(String.Format("Time 2: {0}",sessionTime2.ToUtcTime.ToString()));
        //}

    }
}
