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
        /// The trading time type.
        /// </summary>
        private readonly TradingTime tradingTime;

        #endregion

        #region Public properties

        /// <summary>
        /// The trading time type.
        /// </summary>
        public TradingTime Type => tradingTime;

        /// <summary>
        /// Gets the unique code of the <see cref="SessionTime"/>.
        /// </summary>
        public string Code => tradingTime == TradingTime.Custom ? code : tradingTime.ToCode();

        /// <summary>
        /// Gets the description of the <see cref="SessionTime"/>.
        /// </summary>
        public string Description => tradingTime == TradingTime.Custom ? description : tradingTime.ToDescription();

        /// <summary>
        /// Gets or sets the <see cref="TimeZoneInfo"/> of the trading hour.
        /// </summary>
        public TimeZoneInfo TimeZoneInfo { get; set; }

        /// <summary>
        /// Gets or sets the time of the trading hour.
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Converts the Time property to UTC time.
        /// </summary>
        public TimeSpan ToUtcTime
        {
            get
            {
                if (TimeZoneInfo == null)
                    throw new ArgumentNullException(nameof(TimeZoneInfo));
                if (Time == null)
                    throw new ArgumentNullException(nameof(Time));

                TimeSpan utcTime = Time - TimeZoneInfo.BaseUtcOffset;

                if (utcTime.TotalHours >= 24)
                {
                    utcTime -= TimeSpan.FromHours(24);
                }
                if (utcTime.TotalHours < 0)
                {
                    utcTime += TimeSpan.FromHours(24);
                }
                return utcTime;
            }
        }

        /// <summary>
        /// Converts the Time property to UTC time.
        /// </summary>
        public TimeSpan ToLocalTime
        {
            get
            {
                if (TimeZoneInfo == null)
                    throw new ArgumentNullException(nameof(TimeZoneInfo));
                if (Time == null)
                    throw new ArgumentNullException(nameof(Time));

                TimeSpan localTime = this.ToUtcTime + TimeZoneInfo.Local.BaseUtcOffset;

                if (localTime.TotalHours >= 24)
                {
                    localTime -= TimeSpan.FromHours(24);
                }
                if (localTime.TotalHours < 0)
                {
                    localTime += TimeSpan.FromHours(24);
                }
                return localTime;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create instance of <see cref="SessionTime"/> class.
        /// </summary>
        /// <param name="tradingTime"></param>
        private SessionTime(TradingTime tradingTime, InstrumentCode instrumentCode = InstrumentCode.Default, int offset = 0)
        {
            this.tradingTime = tradingTime;
            this.TimeZoneInfo = tradingTime.ToTimeZoneInfo(instrumentCode);
            this.Time = tradingTime.ToTime(instrumentCode, offset);
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
            tradingTime = TradingTime.Custom;
            this.code = code;
            this.description = description;
            this.TimeZoneInfo = timeZoneInfo;
            this.Time = new TimeSpan(hour,minute,seconds);
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Create default instance of <see cref="SessionTime"/> class.
        /// </summary>
        /// <param name="tradingTime">The specific session time to create the instance.</param>
        /// <returns>The session time instance.</returns>
        public static SessionTime CreateSessionTimeByType(TradingTime tradingTime, InstrumentCode instrumentCode = InstrumentCode.Default, int offset = 0)
        {
            return new SessionTime(tradingTime,instrumentCode,offset);
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
        public static SessionTime CreateCustomSessionTime(TimeZoneInfo timeZoneInfo, int hour, int minute, int seconds, string code="CUSTOM", string description = "")
        {
            return new SessionTime(timeZoneInfo,hour,minute,seconds,code, description);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Converts the <see cref="Time"/> to integer.
        /// </summary>
        /// <param name="destinationTimeZoneInfo">The target <see cref="TimeZoneInfo"/>. The default values is <see cref="TimeZoneInfo.Local"/>.</param>
        /// <returns>The integer that represents the session <see cref="Time"/></returns>
        public int ToInteger(TimeZoneInfo destinationTimeZoneInfo = null)
        {
            TimeSpan time = GetTime(DateTime.Now, destinationTimeZoneInfo);
            return (time.Hours * 10000) + (time.Minutes * 100) + (time.Seconds);
        }

        /// <summary>
        /// Gets the trading time <see cref="DateTime"/>.
        /// </summary>
        /// <param name="currentTime">The current date to create the date time structure.</param>
        /// <param name="destinationTimeZoneInfo">The time zone info to convert the date time structure.</param>
        /// <returns></returns>
        public DateTime GetDateTime(
            DateTime currentTime, 
            TimeZoneInfo destinationTimeZoneInfo = null)
        {

            if (destinationTimeZoneInfo == null)
                destinationTimeZoneInfo = TimeZoneInfo.Local;

            DateTime sessionDateTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, Time.Hours, Time.Minutes, Time.Seconds, DateTimeKind.Unspecified);
            DateTime destinationDateTime = TimeZoneInfo.ConvertTime(sessionDateTime, this.TimeZoneInfo, destinationTimeZoneInfo);

            return destinationDateTime;

        }

        /// <summary>
        /// Gets the trading time <see cref="DateTime"/>.
        /// </summary>
        /// <param name="currentTime">The current date to create the date time structure.</param>
        /// <param name="destinationTimeZoneInfo">The time zone info to convert the date time structure.</param>
        /// <returns></returns>
        public TimeSpan GetTime(
            DateTime currentTime, 
            TimeZoneInfo destinationTimeZoneInfo = null)
        {
            return GetDateTime(currentTime,destinationTimeZoneInfo).TimeOfDay;
        }

        /// <summary>
        /// Gets the trading time <see cref="DateTime"/>.
        /// </summary>
        /// <param name="currentTime">The current date to create the date time structure.</param>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <param name="destinationTimeZoneInfo">The <see cref="TimeZoneInfo"/> to convert the date time structure.</param>
        /// <returns>The <see cref="DateTime"/> of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetNextDateTime(
            DateTime currentTime, 
            TimeZoneInfo sourceTimeZoneInfo = null, 
            TimeZoneInfo destinationTimeZoneInfo = null)
        {

            if (sourceTimeZoneInfo == null)
            {
                if (currentTime.Kind == DateTimeKind.Local)
                    sourceTimeZoneInfo = TimeZoneInfo.Local;
                
                else if (currentTime.Kind == DateTimeKind.Utc)
                    sourceTimeZoneInfo = TimeZoneInfo.Utc;

                else if (currentTime.Kind == DateTimeKind.Unspecified)
                    throw new ArgumentNullException(nameof(sourceTimeZoneInfo) + " cannot be null if the " + nameof(DateTimeKind) + " is Unnespecified");
            }

            if (destinationTimeZoneInfo == null)
                destinationTimeZoneInfo = TimeZoneInfo.Local;

            DateTime sessionTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, Time.Hours, Time.Minutes, Time.Seconds, DateTimeKind.Unspecified);

            DateTime currentDateTime = TimeZoneInfo.ConvertTime(currentTime, sourceTimeZoneInfo, destinationTimeZoneInfo);
            DateTime nextDateTime = TimeZoneInfo.ConvertTime(sessionTime, this.TimeZoneInfo, destinationTimeZoneInfo);

            if (nextDateTime > currentDateTime)
                return nextDateTime;
            else 
                return nextDateTime + TimeSpan.FromHours(24);

        }

        #endregion


    }
}
