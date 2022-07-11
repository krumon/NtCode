using System;

namespace NtCore
{
    public class SessionTime
    {
        #region Private members

        /// <summary>
        /// The unique code of the trading time.
        /// </summary>
        private readonly string code;

        /// <summary>
        /// The description of the trading time.
        /// </summary>
        private readonly string description;

        /// <summary>
        /// The specific session time.
        /// </summary>
        protected SpecificSessionTime specificSessionTime;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the unique code of the trading time.
        /// </summary>
        public string Code => specificSessionTime == SpecificSessionTime.Custom ? code : specificSessionTime.ToCode();

        /// <summary>
        /// Gets the description of the trading time.
        /// </summary>
        public string Description => specificSessionTime == SpecificSessionTime.Custom ? description : specificSessionTime.ToDescription();

        /// <summary>
        /// Gets or sets the <see cref="TimeZoneInfo"/> of the trading hour.
        /// </summary>
        public TimeZoneInfo TimeZoneInfo { get; set; }

        /// <summary>
        /// Gets or sets the time of the trading hour.
        /// </summary>
        public TimeSpan Time { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create instance of <see cref="SessionTime"/> class.
        /// </summary>
        /// <param name="specificSessionTime"></param>
        private SessionTime(SpecificSessionTime specificSessionTime, InstrumentCode code = InstrumentCode.Default)
        {
            this.TimeZoneInfo = specificSessionTime.ToTimeZoneInfo(code);
            this.Time = specificSessionTime.ToTimeSpan(code);
        }

        /// <summary>
        /// Create instance of <see cref="SessionTime"/> class whith custom values.
        /// </summary>
        /// <param name="timeZoneInfo"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        private SessionTime(TimeZoneInfo timeZoneInfo, int hour, int minute, int seconds, string code, string description = "")
        {
            specificSessionTime = SpecificSessionTime.Custom;
            this.code = code;
            this.description = description;
            this.TimeZoneInfo = timeZoneInfo;
            this.Time = new TimeSpan(hour,minute,seconds);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Create default instance of <see cref="SessionTime"/> class.
        /// </summary>
        /// <param name="specificSessionTime">The specific session time to create the instance.</param>
        /// <returns>The session time instance.</returns>
        public static SessionTime CreateSessionTimeByType(SpecificSessionTime specificSessionTime, InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            return new SessionTime(specificSessionTime,instrumentCode);
        }

        /// <summary>
        /// Create default instance of <see cref="SessionTime"/> class whith custom values.
        /// </summary>
        /// <param name="timeZoneInfo"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="seconds"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static SessionTime CreateCustomSessionTime(TimeZoneInfo timeZoneInfo, int hour, int minute, int seconds, string code, string description = "")
        {
            return new SessionTime(timeZoneInfo,hour,minute,seconds,code, description);
        }

        /// <summary>
        /// Gets the trading time <see cref="DateTime"/>.
        /// </summary>
        /// <param name="currentDate">The current date to create the date time structure.</param>
        /// <param name="targetTimeZoneInfo">The time zone info to convert the date time structure.</param>
        /// <returns></returns>
        public DateTime GetTradingTime(DateTime currentDate, TimeZoneInfo targetTimeZoneInfo)
        {
            return TimeZoneInfo.ConvertTime(currentDate.Date + Time,TimeZoneInfo,targetTimeZoneInfo);
        }

        #endregion

    }
}
