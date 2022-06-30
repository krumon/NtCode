using System;

namespace NtCore
{
    /// <summary>
    /// Represents the session definition.
    /// </summary>
    public class TradingSessions
    {

        #region Public properties

        /// <summary>
        /// Represents the session time zone.
        /// </summary>
        public TradingTimeZone TimeZone { get; set; }

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
