using Nt.Core;
using System;

namespace ConsoleApp
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
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods

        private static void CreateAndPrintSession()
        {
            // Create
            NsSession session = new NsSession();

            // Print
            Console.WriteLine("NINJATRADER SESSION");
            Console.WriteLine("-------------------");
            Console.WriteLine(session.SessionHours.ToString());
        }

        private static void PrintTradingSessions()
        {
            foreach (var timeZone in TradingSession.Asian.ToArray())
                Console.WriteLine(timeZone.ToSessionHours().ToString());
        }

        #endregion


    }
}
