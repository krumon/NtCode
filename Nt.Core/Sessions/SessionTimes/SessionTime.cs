using System;

namespace NtCore
{
    public class SessionTime
    {
        #region Private members

        /// <summary>
        /// The unique code of the trading time.
        /// </summary>
        private string code;

        /// <summary>
        /// The description of the trading time.
        /// </summary>
        private string description;

        /// <summary>
        /// The trading time type.
        /// </summary>
        private TradingTime tradingTime;

        /// <summary>
        /// The <see cref="TimeZoneInfo"/> configute on ninjatrader plattaform.
        /// All ninjascript times are reference to this TimeZoneInfo.
        /// </summary>
        private TimeZoneInfo plattaformTimeZoneInfo;

        /// <summary>
        /// The <see cref="TimeZoneInfo"/> configure in the specific Chartcontrol.
        /// This property must be used to draw the times in the correct place on the chart.
        /// </summary>
        private TimeZoneInfo barsTimeZoneInfo;

        /// <summary>
        /// The instrument code to calculate the session time.
        /// This code represents de instrument represents on the chart.
        /// </summary>
        private InstrumentCode instrumentCode;

        /// <summary>
        /// The last <see cref="DateTime"/> of the session.
        /// </summary>
        private DateTime actualSessionTime = DateTime.MinValue; 

        #endregion

        #region Public properties

        /// <summary>
        /// The trading time type.
        /// </summary>
        public TradingTime TradingTimeType => tradingTime;

        /// <summary>
        /// Gets the unique code of the <see cref="SessionTime"/>.
        /// </summary>
        public string Code => tradingTime == TradingTime.Custom ? code : tradingTime.ToCode();

        /// <summary>
        /// Gets the description of the <see cref="SessionTime"/>.
        /// </summary>
        public string Description => tradingTime == TradingTime.Custom ? description : tradingTime.ToDescription();

        /// <summary>
        /// Gets or sets the <see cref="System.TimeZoneInfo"/> of the trading hour.
        /// </summary>
        public TimeZoneInfo TimeZoneInfo { get; set; }

        /// <summary>
        /// Gets or sets the time of the trading hour.
        /// </summary>
        public TimeSpan Time { get; set; }

        /// <summary>
        /// Converts the Time property to UTC time.
        /// </summary>
        public TimeSpan UtcTime
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
        public TimeSpan LocalTime
        {
            get
            {
                if (TimeZoneInfo == null)
                    throw new ArgumentNullException(nameof(TimeZoneInfo));
                if (Time == null)
                    throw new ArgumentNullException(nameof(Time));

                TimeSpan localTime = this.UtcTime + TimeZoneInfo.Local.BaseUtcOffset;

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
        /// Create a default instance of <see cref="SessionTime"/>.
        /// </summary>
        protected SessionTime()
        {

        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Create a default instance of <see cref="SessionTime"/> by specific <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="tradingTime">The specific session time to create the instance.</param>
        /// <param name="instrumentCode">The unique code of the financial instrument session.</param>
        /// <param name="timeDisplacement">The offset of the <see cref="DateTime"/> in minutes.</param>
        /// <returns>The session time instance.</returns>
        public static SessionTime CreateSessionTimeByType(TradingTime tradingTime, InstrumentCode instrumentCode = InstrumentCode.Default, int timeDisplacement = 0)
        {
            return new SessionTime 
            {
                tradingTime = tradingTime,
                TimeZoneInfo = tradingTime.ToTimeZoneInfo(instrumentCode),
                Time = tradingTime.ToTime(instrumentCode, timeDisplacement)
            };
        }

        /// <summary>
        /// Create a cutom instance of <see cref="SessionTime"/> with specific <see cref="TimeSpan"/> and <see cref="System.TimeZoneInfo"/>.
        /// </summary>
        /// <param name="time">The specific time.</param>
        /// <param name="timeZoneInfo">The specific time zone info.</param>
        /// <param name="code">The code of the custom session.</param>
        /// <param name="description">The description of the custom session time.</param>
        /// <returns>A new instance of the <see cref="SessionTime"/> with the specific parameters.</returns>
        public static SessionTime CreateCustomSessionTime(TimeSpan time, TimeZoneInfo timeZoneInfo = null, string code = "CUSTOM", string description = "My Custom Session Time.")
        {
            return new SessionTime
            {
                tradingTime = TradingTime.Custom,
                Time = time,
                TimeZoneInfo = timeZoneInfo,
                code = code,
                description = description
            };
        }

        /// <summary>
        /// Create a cutom instance of <see cref="SessionTime"/> with specific <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="hour">The session <see cref="TimeSpan"/> hour.</param>
        /// <param name="minute">The session <see cref="TimeSpan"/> minute.</param>
        /// <param name="seconds">The session <see cref="TimeSpan"/> seconds.</param>
        /// <param name="timeZoneInfo">The specific time zone info.</param>
        /// <param name="code">The custom session code.</param>
        /// <param name="description">The custom session time description.</param>
        /// <returns></returns>
        public static SessionTime CreateCustomSessionTime(int hour, int minute, int seconds, TimeZoneInfo timeZoneInfo = null, string code = "CUSTOM", string description = "")
        {
            return new SessionTime
            {
                tradingTime = TradingTime.Custom,
                code = code,
                description = description,
                TimeZoneInfo = timeZoneInfo,
                Time = new TimeSpan(hour, minute, seconds)
            };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the trading time <see cref="DateTime"/>.
        /// </summary>
        /// <param name="sourceTime">The current date to create the date time structure.</param>
        /// <param name="sourceTimeZoneInfo">The TimeZoneInfo of the time passed by the nijascript.</param>
        /// <param name="destinationTimeZoneInfo">The time zone info to convert the date time structure.</param>
        /// <returns></returns>
        public DateTime GetSessionTime(DateTime sourceTime, TimeZoneInfo sourceTimeZoneInfo)
        {

            // Converts the time to the SessionTime.TimeZoneInfo
            DateTime time = TimeZoneInfo.ConvertTime(sourceTime, sourceTimeZoneInfo, TimeZoneInfo);
            // Calculate the session time for the date passed as parameter.
            DateTime sessionTime = new DateTime(time.Year, time.Month, time.Day, Time.Hours, Time.Minutes, Time.Seconds, DateTimeKind.Unspecified);
            // Converts the time to the sourceTimeZoneInfo.
            return TimeZoneInfo.ConvertTime(sessionTime, TimeZoneInfo, sourceTimeZoneInfo);

        }

        /// <summary>
        /// Gets the trading time <see cref="DateTime"/>.
        /// </summary>
        /// <param name="sourceTime">The current date to create the date time structure.</param>
        /// <param name="sourceTimeZoneInfo">The TimeZoneInfo of the time passed by the nijascript.</param>
        /// <param name="destinationTimeZoneInfo">The time zone info to convert the date time structure.</param>
        /// <returns></returns>
        public DateTime GetSessionTime(DateTime sourceTime, TimeZoneInfo sourceTimeZoneInfo, TimeZoneInfo destinationTimeZoneInfo)
        {

            // Converts the time to the SessionTime.TimeZoneInfo
            DateTime time = TimeZoneInfo.ConvertTime(sourceTime, sourceTimeZoneInfo, TimeZoneInfo);
            // Calculate the session time for the date passed as parameter.
            DateTime sessionTime = new DateTime(time.Year, time.Month, time.Day, Time.Hours, Time.Minutes, Time.Seconds, DateTimeKind.Unspecified);
            // Converts the time to the destination TimeZoneInfo.
            return TimeZoneInfo.ConvertTime(sessionTime, TimeZoneInfo, destinationTimeZoneInfo);

        }

        ///// <summary>
        ///// Gets the trading time <see cref="DateTime"/>.
        ///// </summary>
        ///// <param name="time">The current date to create the date time structure.</param>
        ///// <returns>The <see cref="DateTime"/> of the next session since the <paramref name="time"/></returns>
        //public DateTime GetNextSessionTime(DateTime time, TimeZoneInfo sourceTimeZoneInfo)
        //{
        //    // Gets the current session time.
        //    DateTime sessionTime = GetSessionTime(time,sourceTimeZoneInfo);

        //    if (sessionTime > time)
        //        return sessionTime;

        //    return sessionTime.AddDays(1);
        //}

        #endregion

        #region Public Methods

        public DateTime ToSessionTime(DateTime time, TimeZoneInfo timeZoneInfo = null)
        {
            if (time == null)
                throw new ArgumentNullException(nameof(time) + "can not be null");

            if (time.Kind != DateTimeKind.Local)
                throw new ArgumentException(nameof(time) + " kind must be Local");


            return TimeZoneInfo.ConvertTime(time, TimeZoneInfo.Local, TimeZoneInfo);
        }

        public DateTime ToLocalTime(DateTime time)
        {
            return new DateTime();
        }

        public DateTime ToUtcTime(DateTime time)
        {
            return new DateTime();
        }

        public DateTime ToUnspecificTime(DateTime time)
        {
            return new DateTime();
        }

        //public void Configure(string masterInstrument = "Default", TimeZoneInfo plattaformTimeZoneInfo = null, TimeZoneInfo barsTimeZoneInfo = null)
        //{
        //    this.instrumentCode = masterInstrument.ToInstrumentCode();
        //    this.plattaformTimeZoneInfo = plattaformTimeZoneInfo ?? TimeZoneInfo.Local;
        //    this.barsTimeZoneInfo = barsTimeZoneInfo ?? TimeZoneInfo.Local;
        //}

        /// <summary>
        /// Update the values when the bar of the char is updated.
        /// The parameter are passed in <see cref="plattaformTimeZoneInfo"/> time.
        /// </summary>
        /// <param name="time">The time passed by ninjascript.</param>
        //public void OnBarUpdate(DateTime time)
        //{
        //    if (time < actualSessionTime)
        //        return;

        //    if (actualSessionTime.Date == time.Date)
        //        return;

        //    actualSessionTime = GetNextSessionTime(time);
        //}

        #endregion

        #region Helper methods

        /// <summary>
        /// Returns true if the objects have the same <see cref="TimeSpan"/> and the same <see cref="TimeZoneInfo"/>.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is SessionTime time)
                return time.Time == Time && time.TimeZoneInfo == TimeZoneInfo;

            return false;
        }

        public bool Equals(SessionTime value)
        {
            if (value != null)
                return value.Time == Time && value.TimeZoneInfo == TimeZoneInfo;

            throw new ArgumentException("The argument can not be null.");
        }

        public static bool Equals(SessionTime st1, SessionTime st2)
        {
            if (st1 != null && st2 != null)
                return st1.Time == st2.Time && st1.TimeZoneInfo == st2.TimeZoneInfo;

            throw new ArgumentException("The arguments can not be null.");
        }

        public int Compare(SessionTime st1, SessionTime st2)
        {
            if (st1 == null || st2 == null)
                throw new ArgumentException("The arguments can not be null.");

            TimeSpan st1UtcTime = st1.UtcTime;
            TimeSpan st2UtcTime = st2.UtcTime;

            if (st1UtcTime > st2UtcTime)
            {
                return 1;
            }

            if (st1UtcTime < st2UtcTime)
            {
                return -1;
            }

            return 0;
        }

        public int CompareTo(object value)
        {
            if (value == null)
            {
                return 1;
            }

            if (!(value is SessionTime))
            {
                throw new ArgumentException("Argument must be SessionTime");
            }

            TimeSpan valueUtcTime = ((SessionTime)value).UtcTime;

            if (UtcTime > valueUtcTime)
            {
                return 1;
            }

            if (UtcTime < valueUtcTime)
            {
                return -1;
            }

            return 0;
        }

        public int CompareTo(SessionTime value)
        {
            if (value == null)
            {
                return 1;
            }

            TimeSpan valueUtcTime = ((SessionTime)value).UtcTime;

            if (UtcTime > valueUtcTime)
            {
                return 1;
            }

            if (UtcTime < valueUtcTime)
            {
                return -1;
            }

            return 0;
        }

        public static SessionTime operator +(SessionTime st1, SessionTime st2)
        {
            if (st1 == null || st2 == null)
            {
                throw new ArgumentException("The argument can not be null.");
            }

            TimeSpan sum = st1.UtcTime + st2.UtcTime;

            TimeSpan st2UtcTime = st2.UtcTime;

            return new SessionTime();
        }

        public static SessionTime operator -(SessionTime st1, SessionTime st2)
        {
            return new SessionTime();
        }

        /// <summary>
        /// Returns the string that represents the <see cref="Time"/> of the <see cref="SessionTime"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Time.ToString();
        }

        /// <summary>
        /// Returns object hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}
