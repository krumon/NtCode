using System;

namespace Nt.Core
{
    /// <summary>
    /// Represents the trading hours session definition.
    /// </summary>
    public class SessionChangedEventArgs : EventArgs
    {

        #region Public properties

        /// <summary>
        /// Represents the actual session begin
        /// </summary>
        public DateTime NewSessionBeginTime { get; set; }

        /// <summary>
        /// Represents the actual session end.
        /// </summary>
        public DateTime NewSessionEndTime { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="NtNewSessionEventArgs"/> default instance.
        /// </summary>
        public SessionChangedEventArgs()
        {

        }

        /// <summary>
        /// Create <see cref="NtNewSessionEventArgs"/> new instance with specific date times.
        /// </summary>
        /// <param name="newSessionBegin">The initial time of the new session.</param>
        /// <param name="newSessionEnd">The final time of the new session.</param>
        public SessionChangedEventArgs(DateTime newSessionBegin, DateTime newSessionEnd)
        {
            this.NewSessionBeginTime = newSessionBegin;
            this.NewSessionEndTime = newSessionEnd;
        }

        #endregion

    }
}
