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
        /// The <see cref="SessionTimeZoneInfo"/> configute on ninjatrader plattaform.
        /// All ninjascript times are reference to this TimeZoneInfo.
        /// </summary>
        private TimeZoneInfo plattaformTimeZoneInfo;

        /// <summary>
        /// The <see cref="SessionTimeZoneInfo"/> configure in the specific Chartcontrol.
        /// This property must be used to draw the times in the correct place on the chart.
        /// </summary>
        private TimeZoneInfo tradingHoursTimeZoneInfo;

        /// <summary>
        /// The instrument code to calculate the session time.
        /// This code represents de instrument represents on the chart.
        /// </summary>
        private InstrumentCode instrumentCode;

        /// <summary>
        /// The last <see cref="DateTime"/> of the session.
        /// </summary>
        private DateTime actualNextTime = DateTime.MinValue; 

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
        /// Gets or sets the <see cref="TimeZoneInfo"/> of the trading hour.
        /// </summary>
        public TimeZoneInfo SessionTimeZoneInfo { get; set; }

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
                if (SessionTimeZoneInfo == null)
                    throw new ArgumentNullException(nameof(SessionTimeZoneInfo));
                if (Time == null)
                    throw new ArgumentNullException(nameof(Time));

                TimeSpan utcTime = Time - SessionTimeZoneInfo.BaseUtcOffset;

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
                if (SessionTimeZoneInfo == null)
                    throw new ArgumentNullException(nameof(SessionTimeZoneInfo));
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

        /// <summary>
        /// Create new instance of <see cref="SessionTime"/> with sepecific parameters.
        /// </summary>
        /// <param name="tradingTime"></param>
        /// <param name="instrumentCode"></param>
        /// <param name="tradingTimeDisplacement"></param>
        //private SessionTime(TradingTime tradingTime, InstrumentCode instrumentCode = InstrumentCode.Default, int tradingTimeDisplacement = 0)
        //{
        //    this.tradingTime = tradingTime;
        //    this.TimeZoneInfo = tradingTime.ToTimeZoneInfo(instrumentCode);
        //    this.Time = tradingTime.ToTime(instrumentCode, tradingTimeDisplacement);
        //}

        /// <summary>
        /// Create a new instance instance of <see cref="SessionTime"/> class whith custom values.
        /// </summary>
        /// <param name="timeZoneInfo"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        //private SessionTime(TimeZoneInfo timeZoneInfo, int hour, int minute, int seconds, string code, string description = "")
        //{
        //    tradingTime = TradingTime.Custom;
        //    this.code = code;
        //    this.description = description;
        //    this.TimeZoneInfo = timeZoneInfo;
        //    this.Time = new TimeSpan(hour,minute,seconds);
        //}

        #endregion

        #region Instance methods

        /// <summary>
        /// Create a default instance of <see cref="SessionTime"/> by specific <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="tradingTime">The specific session time to create the instance.</param>
        /// <param name="instrumentCode">The unique code of the financial instrument session.</param>
        /// <param name="tradingTimeDisplacement">The offset of the <see cref="DateTime"/> in minutes.</param>
        /// <returns>The session time instance.</returns>
        public static SessionTime CreateSessionTimeByType(TradingTime tradingTime, InstrumentCode instrumentCode = InstrumentCode.Default, int tradingTimeDisplacement = 0)
        {
            return new SessionTime 
            {
                tradingTime = tradingTime,
                SessionTimeZoneInfo = tradingTime.ToTimeZoneInfo(instrumentCode),
                Time = tradingTime.ToTime(instrumentCode, tradingTimeDisplacement)
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
        public static SessionTime CreateCustomSessionTime(TimeSpan time, TimeZoneInfo timeZoneInfo = null, string code = "CUSTOM", string description = "")
        {
            return new SessionTime
            {
                tradingTime = TradingTime.Custom,
                code = code,
                description = description,
                SessionTimeZoneInfo = timeZoneInfo,
                Time = time
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
                SessionTimeZoneInfo = timeZoneInfo,
                Time = new TimeSpan(hour, minute, seconds)
            };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Converts the <see cref="Time"/> to integer.
        /// </summary>
        /// <param name="destinationTimeZoneInfo">The target <see cref="SessionTimeZoneInfo"/>. The default values is <see cref="TimeZoneInfo.Local"/>.</param>
        /// <returns>The integer that represents the session <see cref="Time"/></returns>
        public int ToInteger(TimeZoneInfo destinationTimeZoneInfo = null)
        {
            TimeSpan time = GetTime(DateTime.Now, destinationTimeZoneInfo);
            return (time.Hours * 10000) + (time.Minutes * 100) + (time.Seconds);
        }

        /// <summary>
        /// Gets the trading time <see cref="DateTime"/>.
        /// </summary>
        /// <param name="time">The current date to create the date time structure.</param>
        /// <param name="sourceTimeZoneInfo">The TimeZoneInfo of the time passed by the nijascript.</param>
        /// <param name="destinationTimeZoneInfo">The time zone info to convert the date time structure.</param>
        /// <returns></returns>
        public DateTime GetDateTime(
            DateTime time,
            TimeZoneInfo sourceTimeZoneInfo = null,
            TimeZoneInfo destinationTimeZoneInfo = null)
        {

            if (sourceTimeZoneInfo == null)
                sourceTimeZoneInfo = plattaformTimeZoneInfo;

            if (destinationTimeZoneInfo == null)
                destinationTimeZoneInfo = plattaformTimeZoneInfo;

            // Converts the time to the SessionTime.TimeZoneInfo
            DateTime currentTime = TimeZoneInfo.ConvertTime(time, sourceTimeZoneInfo, SessionTimeZoneInfo);
            // Calculate the next time.
            DateTime sessionDateTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, Time.Hours, Time.Minutes, Time.Seconds, DateTimeKind.Unspecified);
            // Converts de time to the destinationTimeZoneInfo passed as parameter.
            DateTime destinationDateTime = TimeZoneInfo.ConvertTime(sessionDateTime, SessionTimeZoneInfo, destinationTimeZoneInfo);

            return destinationDateTime;

        }

        /// <summary>
        /// Gets the trading time <see cref="DateTime"/>.
        /// </summary>
        /// <param name="time">The current date to create the date time structure.</param>
        /// <param name="destinationTimeZoneInfo">The time zone info to convert the date time structure.</param>
        /// <returns></returns>
        public TimeSpan GetTime(
            DateTime time,
            TimeZoneInfo sourceTimeZoneInfo = null,
            TimeZoneInfo destinationTimeZoneInfo = null)
        {
            return GetDateTime(time,sourceTimeZoneInfo,destinationTimeZoneInfo).TimeOfDay;
        }

        /// <summary>
        /// Gets the trading time <see cref="DateTime"/>.
        /// </summary>
        /// <param name="time">The current date to create the date time structure.</param>
        /// <param name="sourceTimeZoneInfo">The <see cref="SessionTimeZoneInfo"/> that represents <paramref name="time"/>"/></param>
        /// <param name="destinationTimeZoneInfo">The <see cref="SessionTimeZoneInfo"/> to convert the date time structure.</param>
        /// <returns>The <see cref="DateTime"/> of the next session since the <paramref name="time"/></returns>
        public DateTime GetNextTime(DateTime time)
        {

            //if (sourceTimeZoneInfo == null)
            //{
            //    if (currentTime.Kind == DateTimeKind.Local)
            //        sourceTimeZoneInfo = TimeZoneInfo.Local;

            //    else if (currentTime.Kind == DateTimeKind.Utc)
            //        sourceTimeZoneInfo = TimeZoneInfo.Utc;

            //    else if (currentTime.Kind == DateTimeKind.Unspecified)
            //        throw new ArgumentNullException(nameof(sourceTimeZoneInfo) + " cannot be null if the " + nameof(DateTimeKind) + " is Unnespecified");
            //}

            //if (destinationTimeZoneInfo == null)
            //    destinationTimeZoneInfo = TimeZoneInfo.Local;

            // Converts the time to the SessionTime.TimeZoneInfo
            DateTime currentTime = TimeZoneInfo.ConvertTime(time, plattaformTimeZoneInfo, SessionTimeZoneInfo);
            // Calculate the next time.
            DateTime nextDateTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, Time.Hours, Time.Minutes, Time.Seconds, DateTimeKind.Unspecified);

            // Make sure the next time changed.
            if (currentTime.Date <= nextDateTime)
                // Return de actualNextTime converts to Bars.TradingHours.TimeZoneInfo
                return TimeZoneInfo.ConvertTime(actualNextTime, plattaformTimeZoneInfo, tradingHoursTimeZoneInfo);

            // Converts the next time to PlattaformTimeZoneInfo.
            actualNextTime = TimeZoneInfo.ConvertTime(nextDateTime, SessionTimeZoneInfo, plattaformTimeZoneInfo);

            // Return de actual next time converts to Bars.TradingHours.TimeZoneInfo
            return TimeZoneInfo.ConvertTime(actualNextTime, plattaformTimeZoneInfo, tradingHoursTimeZoneInfo);
        }

        #endregion

        #region Ninjatrader Methods

        public void Configure(string masterInstrument = "Default", TimeZoneInfo plattaformTimeZoneInfo = null, TimeZoneInfo tradingHoursTimeZoneInfo = null)
        {
            this.instrumentCode = masterInstrument.ToInstrumentCode();
            this.plattaformTimeZoneInfo = plattaformTimeZoneInfo ?? TimeZoneInfo.Local;
            this.tradingHoursTimeZoneInfo = tradingHoursTimeZoneInfo ?? TimeZoneInfo.Local;
        }

        /// <summary>
        /// Update the values when the bar of the char is updated.
        /// The parameter are passed in <see cref="plattaformTimeZoneInfo"/> time.
        /// </summary>
        /// <param name="time">The time passed by ninjascript.</param>
        public void OnBarUpdate(DateTime time)
        {
            GetNextTime(time);
        }

        #endregion
    }
}
