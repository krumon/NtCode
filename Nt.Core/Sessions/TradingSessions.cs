using System.Collections.Generic;

namespace NtCore
{
    /// <summary>
    /// Represents the day trading sessions.
    /// </summary>
    public class TradingSessions
    {
        #region Private members

        /// <summary>
        /// Contents the day trading sessions.
        /// </summary>
        private List<TradingSession> sessions = new List<TradingSession>();

        #endregion

        #region Public properties

        /// <summary>
        /// Contents the day trading sessions.
        /// </summary>
        public List<TradingSession> Sessions { get => sessions; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create default instance of <see cref="TradingSession"/> class.
        /// </summary>
        public TradingSessions()
        {

        }

        #endregion

        #region Public methods



        #endregion

    }
}
