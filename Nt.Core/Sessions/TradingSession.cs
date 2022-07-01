
using System.Collections.Generic;

namespace NtCore
{
    /// <summary>
    /// Represents the session definitions.
    /// </summary>
    public class TradingSession
    {

        #region Private members

        /// <summary>
        /// Contents the day trading sessions.
        /// </summary>
        private List<TradingSession> sessions = new List<TradingSession>();

        #endregion

        #region Public properties

        /// <summary>
        /// Represents the session time zone.
        /// </summary>
        public TradingTimeZone TimeZone { get; set; }

        /// <summary>
        /// Contents the day trading sessions.
        /// </summary>
        public List<TradingSession> Sessions { get => sessions; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create default instance of <see cref="TradingSession"/> class.
        /// </summary>
        public TradingSession()
        {

        }

        #endregion

    }
}
