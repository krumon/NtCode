using System;
using System.Collections.Generic;

namespace NtCore
{
    /// <summary>
    /// Contents properties and methods of specific trading time zone.
    /// </summary>
    public class SessionHours
    {

        #region Private members

        private readonly SpecificSessionHours specificSessionHours;

        //private InstrumentCode instrumentCode;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the unique code of the <see cref="SessionHours"/>.
        /// </summary>
        public string Code => specificSessionHours.ToCode();

        /// <summary>
        /// Gets the description of the <see cref="SessionHours"/>.
        /// </summary>
        public string Description => specificSessionHours.ToDescription();

        /// <summary>
        /// The initial <see cref="SessionTime"/>.
        /// </summary>
        public SessionTime BeginSessionTime { get; set; }

        /// <summary>
        /// The final <see cref="SessionTime"/>.
        /// </summary>
        public SessionTime EndSessionTime { get; set; }

        /// <summary>
        /// Collection of minor sessions in the <see cref="SessionHours"/>.
        /// </summary>
        public List<SessionHours> Sessions { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with specific session times.
        /// </summary>
        /// <param name="beginSessionTime">The initial <see cref="SessionTime"/> of the <see cref="SessionHours"/> class.</param>
        /// <param name="endSessionTime">The final <see cref="SessionTime"/> of the <see cref="SessionHours"/> class.</param>
        private SessionHours(SpecificSessionTime beginSessionTime, SpecificSessionTime endSessionTime)
        {
            this.BeginSessionTime = SessionTime.CreateSessionTimeByType(beginSessionTime);
            this.EndSessionTime = SessionTime.CreateSessionTimeByType(endSessionTime);
        }

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with <see cref="SpecificSessionHours"/>.
        /// </summary>
        /// <param name="specificSessionHours">the <see cref="SpecificSessionHours"/> to create the <see cref="SessionHours"/> class.</param>
        private SessionHours(SpecificSessionHours specificSessionHours)
        {
            this.specificSessionHours = specificSessionHours;
            this.BeginSessionTime = specificSessionHours.ToBeginSessionTime();
            this.EndSessionTime = specificSessionHours.ToEndSessionTime();
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with <see cref="SpecificSessionHours"/>.
        /// </summary>
        /// <param name="specificSessionHours">the <see cref="SpecificSessionHours"/> to create the <see cref="SessionHours"/> class.</param>
        /// <returns>A new instance of <see cref="SessionHours"/> class.</returns>
        public static SessionHours CreateSessionHoursByType(SpecificSessionHours specificSessionHours)
        {
            return new SessionHours(specificSessionHours);
        }

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with specific session times.
        /// </summary>
        /// <param name="beginSessionTime">The initial <see cref="SessionTime"/> of the <see cref="SessionHours"/> class.</param>
        /// <param name="endSessionTime">The final <see cref="SessionTime"/> of the <see cref="SessionHours"/> class.</param>
        /// <returns>A new instance of <see cref="SessionHours"/> class.</returns>
        public static SessionHours CreateSessionHoursBySessionTimes(SpecificSessionTime beginSessionTime, SpecificSessionTime endSessionTime)
        {
            return new SessionHours(beginSessionTime, endSessionTime);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the begin <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.
        /// </summary>
        /// <param name="currentDate">The current date time.</param>
        /// <param name="targetTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The begin <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.</returns>
        public DateTime GetBeginDateTime(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        {
            return currentDate + (targetTimeZoneInfo.BaseUtcOffset - BeginSessionTime.TimeZoneInfo.BaseUtcOffset) + BeginSessionTime.Time;
        }

        /// <summary>
        /// Gets the final <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.
        /// </summary>
        /// <param name="currentDate">The current date time.</param>
        /// <param name="targetTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The final <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.</returns>
        public DateTime GetEndDateTime(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        {
            return currentDate + (targetTimeZoneInfo.BaseUtcOffset - EndSessionTime.TimeZoneInfo.BaseUtcOffset) + EndSessionTime.Time;
        }

        /// <summary>
        /// Gets the begin <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.
        /// </summary>
        /// <param name="targetTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The begin <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.</returns>
        public DateTime GetBeginDateTime(TimeZoneInfo targetTimeZoneInfo)
        {
            return GetBeginDateTime(DateTime.Now.Date, targetTimeZoneInfo);
        }

        /// <summary>
        /// Gets the final <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.
        /// </summary>
        /// <param name="targetTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The final <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.</returns>
        public DateTime GetEndDateTime(TimeZoneInfo targetTimeZoneInfo)
        {
            var timeSpan = EndSessionTime.Time - BeginSessionTime.Time;
            if (timeSpan.Hours < 0)
                timeSpan += TimeSpan.FromHours(24);
            return GetBeginDateTime(targetTimeZoneInfo) + timeSpan;
        }

        /// <summary>
        /// Gets the begin <see cref="TimeSpan"/> structure of the <see cref="SessionHours"/>.
        /// </summary>
        /// <param name="targetTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The begin <see cref="TimeSpan"/> structure of the <see cref="SessionHours"/>.</returns>
        public TimeSpan GetBeginTime(TimeZoneInfo targetTimeZoneInfo)
        {
            return GetBeginDateTime(targetTimeZoneInfo).TimeOfDay;
        }

        /// <summary>
        /// Gets the final <see cref="TimeSpan"/> structure of the <see cref="SessionHours"/>.
        /// </summary>
        /// <param name="targetTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The final <see cref="TimeSpan"/> structure of the <see cref="SessionHours"/>.</returns>
        public TimeSpan GetEndTime(TimeZoneInfo targetTimeZoneInfo)
        {
            return GetEndDateTime(targetTimeZoneInfo).TimeOfDay;
        }

        /// <summary>
        /// Converts the begin <see cref="TimeSpan"/> of the <see cref="SessionHours"/> to integer.
        /// </summary>
        /// <param name="targetTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The integer that represents the initial <see cref="TimeSpan"/></returns>
        public int BeginTimeToInteger(TimeZoneInfo targetTimeZoneInfo)
        {
            TimeSpan time = GetBeginTime(targetTimeZoneInfo);
            return (time.Hours*10000)+(time.Minutes*100)+(time.Seconds);
        }

        /// <summary>
        /// Converts the final <see cref="TimeSpan"/> of the <see cref="SessionHours"/> to integer.
        /// </summary>
        /// <param name="targetTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The integer that represents the final <see cref="TimeSpan"/></returns>
        public int EndTimeToInteger(TimeZoneInfo targetTimeZoneInfo)
        {
            TimeSpan time = GetEndTime(targetTimeZoneInfo);
            return (time.Hours*10000)+(time.Minutes*100)+(time.Seconds);
        }

        #endregion

        #region Override methods

        /// <summary>
        /// Converts the <see cref="SessionHours"/> to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", GetBeginDateTime(TimeZoneInfo.Local).ToString(), "End Time: ", GetEndDateTime(TimeZoneInfo.Local).ToString());
        }

        #endregion

    }
}
