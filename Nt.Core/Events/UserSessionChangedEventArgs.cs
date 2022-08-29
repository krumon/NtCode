using System;

namespace Nt.Core
{
    /// <summary>
    /// Represents the trading hours session definition.
    /// </summary>
    public class UserSessionChangedEventArgs : EventArgs
    {

        #region Public properties

        /// <summary>
        /// The element id or index
        /// </summary>
        public int Idx { get; set; }

        /// <summary>
        /// Represents the actual session begin
        /// </summary>
        public DateTime NewSessionBeginTime { get; set; }

        /// <summary>
        /// Represents the actual session end.
        /// </summary>
        public DateTime NewSessionEndTime { get; set; }

        /// <summary>
        /// Indicates if the trading hours is a partial holiday.
        /// </summary>
        public bool IsPartialHoliday { get; set; }

        /// <summary>
        /// Indicates if the partial holiday has a late begin time.
        /// </summary>
        public bool IsLateBegin { get; set; }

        /// <summary>
        /// Indicates if the partial holiday has a early end.
        /// </summary>
        public bool IsEarlyEnd { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="NtNewSessionEventArgs"/> default instance.
        /// </summary>
        public UserSessionChangedEventArgs()
        {

        }

        /// <summary>
        /// Create <see cref="NtNewSessionEventArgs"/> new instance with specific date times.
        /// </summary>
        /// <param name="newSessionBegin">The initial time of the new session.</param>
        /// <param name="newSessionEnd">The final time of the new session.</param>
        public UserSessionChangedEventArgs(DateTime newSessionBegin, DateTime newSessionEnd)
        {
            this.NewSessionBeginTime = newSessionBegin;
            this.NewSessionEndTime = newSessionEnd;
        }

        #endregion

    }
}
