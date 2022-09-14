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
        /// The first bar of the session index
        /// </summary>
        public int Idx { get; set; }

        /// <summary>
        /// The number of the session.
        /// </summary>
        public int N { get; set; }

        /// <summary>
        /// Represents the actual session begin time.
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// Represents the actual session end time.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Represents the actual session begin <see cref="DayOfWeek"/>.
        /// </summary>
        public DayOfWeek BeginDay => BeginTime.DayOfWeek;

        /// <summary>
        /// Represents the actual session end <see cref="DayOfWeek"/>.
        /// </summary>
        public DayOfWeek EndDay => EndTime.DayOfWeek;

        /// <summary>
        /// Represents the actual session <see cref="DayOfWeek"/>.
        /// The day represents the <see cref="DayOfWeek"/> of the EndTime./>
        /// </summary>
        public DayOfWeek TradingDay => EndTime.DayOfWeek;

        /// <summary>
        /// Represents the actual session time zone info.
        /// </summary>
        public TimeZoneInfo NewSessionTimeZoneInfo { get; set; }

        /// <summary>
        /// Indicates if the trading hours is a partial partialHoliday.
        /// </summary>
        public bool IsPartialHoliday { get; set; }

        /// <summary>
        /// Indicates if the partial partialHoliday has a late begin time.
        /// </summary>
        public bool IsLateBegin { get; set; }

        /// <summary>
        /// Indicates if the partial partialHoliday has a early end.
        /// </summary>
        public bool IsEarlyEnd { get; set; }


        #endregion

        #region Constructors

        /// <summary>
        /// Create <see cref="SessionChangedEventArgs"/> default instance.
        /// </summary>
        public SessionChangedEventArgs()
        {
        }

        #endregion

    }
}
