
using System.Collections.Generic;

namespace NtCore
{
    /// <summary>
    /// Represents the day trading session.
    /// </summary>
    public class DayTradingSession
    {

        #region Public properties

        /// <summary>
        /// Contents the day trading sessions.
        /// </summary>
        public TradingSession Session { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create default instance of <see cref="DayTradingSession"/> class.
        /// </summary>
        private DayTradingSession()
        {
        }

        #endregion

        #region Class static instances

        public static DayTradingSession CreateDayTradingSession()
        {
            return new DayTradingSession();
        }

        #endregion

        #region Public methods



        #endregion

    }
}
