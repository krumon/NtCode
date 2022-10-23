using Kr.Core.Helpers;
using Nt.Core;
using System;

namespace ConsoleApp.Tests
{
    internal class SessionHoursTests : BaseTests
    {

        #region Private members


        #endregion

        #region Public Properties


        #endregion

        #region Constructor

        /// <summary>
        /// Create a <see cref="SessionHoursTests"/> default instance.
        /// </summary>
        public SessionHoursTests()
        {

        }

        #endregion

        #region Public methods

        public override void Run()
        {
            InstanceTests();
            WaitAndClear();
            ToStringTests(TradingSession.American_RS);
            WaitAndClear();
            //SessionHoursEnumTest();
            //WaitAndClear();
            //OperatorTests(TradingSession.Asian, TradingSession.European);
            //WaitAndClear();
        }

        #endregion

        #region Private methods

        private void InstanceTests()
        {
            // Create a custom instance.
            Title("Test of Instance methods.");

            TradingSessionInfo sh = TradingSessionInfo.CreateCustomSessionHours(
                TradingTime.American_Open,
                TradingTime.Asian_Close,
                "My Custom TradingSessionInfo Hours");
            Console.WriteLine($"Method ToString() => {sh}");
            Console.WriteLine($"Method ToShortString() => {sh.ToShortString("u")}");
            Console.WriteLine($"Method ToLongString() => {sh.ToLongString("l")}");

            sh = TradingSessionInfo.CreateCustomSessionHours(new TimeSpan(12,15,0),TimeZoneInfo.Local,TradingTime.European_Close);
            NewLine();
            Console.WriteLine($"Method ToString() => {sh}");
            Console.WriteLine($"Method ToShortString() => {sh.ToShortString("u")}");
            Console.WriteLine($"Method ToLongString() => {sh.ToLongString("l")}");

            // Create instance by type
            sh = TradingSessionInfo.CreateSessionHoursByType(TradingSession.American);
            NewLine();
            Console.WriteLine($"Method ToString() => {sh}");
            Console.WriteLine($"Method ToShortString() => {sh.ToShortString("l")}");
            Console.WriteLine($"Method ToLongString() => {sh.ToLongString("u")}");

        }

        private void ToStringTests(TradingSession type)
        {
            // Create a session time by type.
            TradingSessionInfo sh = TradingSessionInfo.CreateSessionHoursByType(type);

            Title("Test of To String methods.");
            Console.WriteLine(sh.ToString());
            Console.WriteLine(sh.ToShortString());
            Console.WriteLine(sh.ToLongString());
            //Console.WriteLine(sh.ToString("LOCAL"));
            //Console.WriteLine(sh.ToString("UTC"));
        }

        private void SessionHoursEnumTest()
        {
            Title("Test of iteration and check methods.");
            EnumHelpers.Writer<TradingSession>();

            NewLine();
            EnumHelpers.ForEach<TradingSession>((t) =>
            {
                if (t != TradingSession.Custom)
                    Console.WriteLine(t.ToSessionHours().ToString());
            });

            NewLine();
            TradingSessionInfo sh = TradingSessionInfo.CreateSessionHoursByType(TradingSession.American);
            //bool exist = sh.Exist();

            //string s = exist ? "exist" : "don't exist";
            //Write(sh.ToString());
            //Write($"{sh.Description} {s} in {nameof(TradingTime)} enum.");

        }

        private void OperatorTests(TradingSession t1, TradingSession t2)
        {
            TradingSessionInfo sh1 = TradingSessionInfo.CreateSessionHoursByType(t1);
            TradingSessionInfo sh2 = TradingSessionInfo.CreateSessionHoursByType(t2);
            int i;
            bool b;
            string s = string.Empty;
            string method = string.Empty;

            NewLine();
            Console.WriteLine($"{nameof(sh1)} = {sh1.ToString()}");
            Console.WriteLine($"{nameof(sh2)} = {sh2.ToString()}");

            #region Compare tests

            Title("Test of Compare methods.");
            i = TradingSessionInfo.Compare(sh1, sh2);
            method = "CompareTo(st1,st2) =>";

            if (i < 0)
                s = $"{method} {sh1.Code} is minor than {sh2.Code}.";
            if (i > 0)
                s = $"{method} {sh1.Code} is major than {sh2.Code}.";
            if (i == 0)
                s = $"{method} {sh1.Code} and {sh2.Code} have the same time.";
            Write(s);

            i = sh1.CompareTo(sh2);
            s = string.Empty;
            method = "CompareTo(st2) =>";

            if (i < 0)
                s = $"{method} {sh1.Code} is minor than {sh2.Code}.";
            if (i > 0)
                s = $"{method} {sh1.Code} is major than {sh2.Code}.";
            if (i == 0)
                s = $"{method} {sh1.Code} and {sh2.Code} have the same time.";
            Write(s);

            #endregion

            #region Equal tests

            Title("Test of Equal methods.");
            b = TradingTimeInfo.Equals(sh1, sh2);
            s = string.Empty;
            method = "Equals(st1,st2) =>";

            if (b)
                s = $"{method} {sh1.Code} and {sh2.Code} are equals.";
            else
                s = $"{method} {sh1.Code} and {sh2.Code} are not equals.";
            Write(s);

            b = sh1.Equals(sh2);
            s = string.Empty;
            method = "st1.Equals(st2) =>";

            if (sh1.Equals(sh2))
                s = $"{method} {nameof(sh1)} and {nameof(sh2)} are equals.";
            else
                s = $"{method} {nameof(sh1)} and {nameof(sh2)} are not equals.";

            Write(s);

            #endregion

            #region Method operators

            Title("Test of Operator methods.");
            s = string.Empty;

            if (sh1 == sh2)
                s += $"{sh1.Code} is equal to {sh2.Code}.{Environment.NewLine}";
            if (sh1 != sh2)
                s += $"{sh1.Code} is not equal to {sh2.Code}.{Environment.NewLine}";
            if (sh1 <= sh2)
                s += $"{sh1.Code} is equal to or less than {sh2.Code}.{Environment.NewLine}";
            if (sh1 >= sh2)
                s += $"{sh1.Code} is equal to or greater than {sh2.Code}.{Environment.NewLine}";
            if (sh1 < sh2)
                s += $"{sh1.Code} is less than {sh2.Code}.{Environment.NewLine}";
            if (sh1 > sh2)
                s += $"{sh1.Code} is greater than {sh2.Code}.{Environment.NewLine}";

            Console.WriteLine(s);
            Console.WriteLine($"{nameof(sh1)} + {nameof(sh2)} = {(sh1 + sh2).ToString()}");
            Console.WriteLine($"{nameof(sh1)} - {nameof(sh2)} = {(sh1 - sh2).ToString()}");

            #endregion

        }

        private void PrintSession()
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
                    Console.Write("- Es una sesión residual (Y/TNinjaScript): ");
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
        //    TradingTimeInfo sessionTime1 = TradingTimeInfo.CreateCustomSessionTime(22,0,0, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));
        //    TradingTimeInfo sessionTime2 = TradingTimeInfo.CreateCustomSessionTime(2,0,0, TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time"));

        //    Console.WriteLine(String.Format("Utc Offset: {0}",sessionTime1.TimeZoneInfo.BaseUtcOffset.ToString()));
        //    Console.WriteLine(String.Format("Time 1: {0}",sessionTime1.ToUtcTime.ToString()));
        //    Console.WriteLine(String.Format("Time 2: {0}",sessionTime2.ToUtcTime.ToString()));
        //}

        //private static void CreateAndPrintSession()
        //{
        //    // Create
        //    NsSession session = new NsSession();

        //    // Print
        //    Console.WriteLine("NINJATRADER SESSION");
        //    Console.WriteLine("-------------------");
        //    Console.WriteLine(session.TradingSessionInfo.ToString());
        //}

        //private static void PrintTradingSessions()
        //{
        //    foreach (var timeZone in TradingSession.Asian.ToArray())
        //        Console.WriteLine(timeZone.ToSessionHours().ToString());
        //}

        #endregion


    }
}
