using NtCore;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    internal class SessionTimeTests : BaseTests
    {

        #region Private members

        #endregion

        #region Properties

        #endregion

        #region Constructor

        /// <summary>
        /// Create a <see cref="SessionTimeTests"/> default instance.
        /// </summary>
        public SessionTimeTests()
        {
        }

        #endregion

        #region Run method

        public override void Run()
        {
            DefaultInstance();
            Console.Clear();
            SessionTimeCreateByType(TradingTime.American_RS_EOD_Close);
            Console.Clear();
            SpecificSessionTimes();
            Console.Clear();
            SessionTimesCompareMethods(TradingTime.American_Close,TradingTime.American_RS_Open);
            WaitAndClear();
        }

        #endregion

        #region Private methods

        private void DefaultInstance()
        {
            // Create a default instance.
            SessionTime st = new SessionTime();
            WriteTitle("Print a session time default instance with all methods.");
            Console.WriteLine($"Method ToString() => {st}");
            Console.WriteLine($"Method ToShortString() => {st.ToShortString()}");
            Console.WriteLine($"Method ToLongString() => {st.ToLongString()}");

        }

        private void SessionTimeCreateByType(TradingTime type)
        {
            // Create a session time by type.
            SessionTime st = SessionTime.CreateSessionTimeByType(type);

            WriteTitle("Method to create a session time by type.");
            Console.WriteLine(st.ToString());
            Console.WriteLine(st.ToString("LOCAL"));

        }

        private void SpecificSessionTimes()
        {
            WriteTitle("Print the specific session times.");
            WriteEnum<TradingTime>((t) =>
            {
                if (t != TradingTime.Custom)
                    Console.WriteLine(t.ToSessionTime().ToString("Local"));
            });

        }

        private void SessionTimesCompareMethods(TradingTime t1, TradingTime t2)
        {
            SessionTime st1 = SessionTime.CreateSessionTimeByType(t1);
            SessionTime st2 = SessionTime.CreateSessionTimeByType(t2);
            int i;
            bool b = false;
            string s = string.Empty;

            WriteTitle("methods to compare and operators.");
            Console.WriteLine($"  {nameof(st1)} = {st1.ToString("local")}");
            Console.WriteLine($"  {nameof(st2)} = {st2.ToString("local")}");
            Console.WriteLine();

            #region Method CompareTo(st1,st2)
            
            i = SessionTime.Compare(st1,st2);

            if (i < 0)
                s = $"=> {nameof(st1)} is minor than {nameof(st2)}.";
            if (i > 0)
                s = $"=> {nameof(st1)} is major than {nameof(st2)}.";
            if (i == 0)
                s = $"=> {nameof(st1)} and {nameof(st2)} are equals.";

            Console.WriteLine($"- Method CompareTo(st1,st2) {s}");

            #endregion

            #region Method st1.CompareTo(st2)

            i = st1.CompareTo(st2);

            if (i < 0)
                s = $"=> {nameof(st1)} is minor than {nameof(st2)}.";
            if (i > 0)
                s = $"=> {nameof(st1)} is major than {nameof(st2)}.";
            if (i == 0)
                s = $"=> {nameof(st1)} and {nameof(st2)} are equals.";

            Console.WriteLine($"- Method st1.CompareTo(st2) {s}");

            #endregion

            #region Method Equals(st1,st2)
            
            b = SessionTime.Equals(st1,st2);

            if (b)
                s = $"=> {nameof(st1)} and {nameof(st2)} are equals.";
            else
                s = $"=> {nameof(st1)} and {nameof(st2)} are not equals.";

            Console.WriteLine($"- Method Equals(st1,st2) {s}");

            #endregion

            #region Method st1.Equals(st2)

            if (st1.Equals(st2))
                s = $"=> {nameof(st1)} and {nameof(st2)} are equals.";
            else
                s = $"=> {nameof(st1)} and {nameof(st2)} are not equals.";

            Console.WriteLine($"- Method st1.Equals(st2) {s}");

            #endregion

            #region Method operators

            if (st1 <= st2)
                s = $"{nameof(st1)} is equal to or less than {nameof(st2)}.";
            else if (st1 >= st2)
                s = $"{nameof(st1)} is equal to or greater than {nameof(st2)}.";
            else if (st1 < st2)
                s = $"{nameof(st1)} is less than {nameof(st2)}.";
            else if (st1 > st2)
                s = $"{nameof(st1)} is greater than {nameof(st2)}.";

            Console.WriteLine(s);
            Console.WriteLine($"{nameof(st1)} + {nameof(st2)} = {(st1+st2).ToString()}");
            Console.WriteLine($"{nameof(st1)} - {nameof(st2)} = {(st1-st2).ToString()}");

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

        #endregion

    }
}
