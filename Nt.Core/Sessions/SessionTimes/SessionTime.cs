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
        /// <param name="time">The current date to create the date time structure.</param>
        /// <param name="sourceTimeZoneInfo">The TimeZoneInfo of the time passed by the nijascript.</param>
        /// <param name="destinationTimeZoneInfo">The time zone info to convert the date time structure.</param>
        /// <returns></returns>
        public DateTime GetTime(DateTime time)
        {

            // Converts the time to the SessionTime.TimeZoneInfo
            DateTime currentTime = TimeZoneInfo.ConvertTime(time, plattaformTimeZoneInfo, TimeZoneInfo);
            // Calculate the next time.
            DateTime sessionTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, Time.Hours, Time.Minutes, Time.Seconds, DateTimeKind.Unspecified);
            // Converts de time to the destinationTimeZoneInfo passed as parameter.
            DateTime destinationTime = TimeZoneInfo.ConvertTime(sessionTime, TimeZoneInfo, plattaformTimeZoneInfo);

            return destinationTime;

        }

        /// <summary>
        /// Gets the trading time <see cref="DateTime"/>.
        /// </summary>
        /// <param name="time">The current date to create the date time structure.</param>
        /// <returns>The <see cref="DateTime"/> of the next session since the <paramref name="time"/></returns>
        public DateTime GetNextSessionTime(DateTime time)
        {
            // Gets the current session time.
            DateTime sessionTime = GetTime(time);

            if (sessionTime > time)
                return sessionTime;

            return sessionTime.AddDays(1);
        }

        #endregion

        #region Ninjatrader Methods

        public void Configure(string masterInstrument = "Default", TimeZoneInfo plattaformTimeZoneInfo = null, TimeZoneInfo barsTimeZoneInfo = null)
        {
            this.instrumentCode = masterInstrument.ToInstrumentCode();
            this.plattaformTimeZoneInfo = plattaformTimeZoneInfo ?? TimeZoneInfo.Local;
            this.barsTimeZoneInfo = barsTimeZoneInfo ?? TimeZoneInfo.Local;
        }

        /// <summary>
        /// Update the values when the bar of the char is updated.
        /// The parameter are passed in <see cref="plattaformTimeZoneInfo"/> time.
        /// </summary>
        /// <param name="time">The time passed by ninjascript.</param>
        public void OnBarUpdate(DateTime time)
        {
            if (time < actualSessionTime)
                return;

            if (actualSessionTime.Date == time.Date)
                return;

            actualSessionTime = GetNextSessionTime(time);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Converts the <see cref="Time"/> to integer.
        /// </summary>
        /// <returns>The integer that represents the session <see cref="Time"/></returns>
        public int ToInteger(DateTime time)
        {
            return (time.Hour * 10000) + (time.Minute * 100) + (time.Second);
        }

        /// <summary>
        /// Converts the <see cref="Int32"/> to <see cref="DateTime"/>.
        /// </summary>
        /// <returns>The date thats represents the session date.</returns>
        public DateTime FromInteger(int time)
        {
            throw new Exception("This method must be executed");
        }

        /// <summary>
        /// Returns true if the object has the same <see cref="TimeSpan"/> and the same <see cref="TimeZoneInfo"/>.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is SessionTime)
            {
                if (((SessionTime)obj).Time == Time && ((SessionTime)obj).TimeZoneInfo == TimeZoneInfo)
                    return true;
                return false;
            }
            return false;
        }

        /// <summary>
        /// Returns the string that represents the <see cref="Time"/> of the <see cref="SessionTime"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Time.ToString();
        }

        #endregion
    }
}
