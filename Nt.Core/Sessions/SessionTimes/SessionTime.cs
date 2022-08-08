﻿using System;

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

        ///// <summary>
        ///// The <see cref="TimeZoneInfo"/> configute on ninjatrader plattaform.
        ///// All ninjascript times are reference to this TimeZoneInfo.
        ///// </summary>
        //private TimeZoneInfo plattaformTimeZoneInfo;

        ///// <summary>
        ///// The <see cref="TimeZoneInfo"/> configure in the specific Chartcontrol.
        ///// This property must be used to draw the times in the correct place on the chart.
        ///// </summary>
        //private TimeZoneInfo barsTimeZoneInfo;

        ///// <summary>
        ///// The instrument code to calculate the session time.
        ///// This code represents de instrument represents on the chart.
        ///// </summary>
        //private InstrumentCode instrumentCode;

        ///// <summary>
        ///// The last <see cref="DateTime"/> of the session.
        ///// </summary>
        //private DateTime actualSessionTime = DateTime.MinValue; 

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

                //TimeSpan utcTime = Time - TimeZoneInfo.BaseUtcOffset;

                //if (utcTime.TotalHours >= 24)
                //{
                //    utcTime -= TimeSpan.FromHours(24);
                //}
                //if (utcTime.TotalHours < 0)
                //{
                //    utcTime += TimeSpan.FromHours(24);
                //}

                return Time - TimeZoneInfo.BaseUtcOffset; // utcTime;
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

                //TimeSpan localTime = this.UtcTime + TimeZoneInfo.Local.BaseUtcOffset;

                //if (localTime.TotalHours >= 24)
                //{
                //    localTime -= TimeSpan.FromHours(24);
                //}
                //if (localTime.TotalHours < 0)
                //{
                //    localTime += TimeSpan.FromHours(24);
                //}

                return UtcTime + TimeZoneInfo.Local.BaseUtcOffset; // localTime;
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
        /// Gets the session <see cref="DateTime"/> for the time passed as parameter.
        /// </summary>
        /// <param name="time">The current date to create the date time structure.</param>
        /// <returns></returns>
        public DateTime GetTime(DateTime time)
        {
            TimeZoneInfo sourceTimeZoneInfo = 
                time.Kind == DateTimeKind.Local ? TimeZoneInfo.Local : 
                time.Kind == DateTimeKind.Utc ? TimeZoneInfo.Utc : null;

            if (sourceTimeZoneInfo != null)
                // Returns the SessionTime TimeSpan for the date passed as parameter.
                return GetTime(time,sourceTimeZoneInfo);

            throw new Exception("The kind of the time must be Local or Utc");
        }

        /// <summary>
        /// Gets the session <see cref="DateTime"/> for the <see cref="DateTime"/> and <see cref="TimeZoneInfo"/> passed as parameters.
        /// </summary>
        /// <param name="sourceTime">The specific time to convert in session <see cref="DateTime"/>.</param>
        /// <param name="sourceTimeZoneInfo">The source TimeZoneInfo.</param>
        /// <returns></returns>
        public DateTime GetTime(DateTime sourceTime, TimeZoneInfo sourceTimeZoneInfo)
        {

            // Converts the time to the SessionTime.TimeZoneInfo
            DateTime date = TimeZoneInfo.ConvertTime(sourceTime, sourceTimeZoneInfo, TimeZoneInfo);
            // Calculate the session time for the date passed as parameter.
            DateTime sessionTime = ToSessionDateTime(date);
            // Converts the time to the sourceTimeZoneInfo.
            return TimeZoneInfo.ConvertTime(sessionTime, TimeZoneInfo, sourceTimeZoneInfo);

        }

        /// <summary>
        /// Gets the session <see cref="DateTime"/> respect destination <see cref="TimeZoneInfo"/> passed as parameter.
        /// </summary>
        /// <param name="sourceTime">The current date to create the date time structure.</param>
        /// <param name="sourceTimeZoneInfo">The source <see cref="TimeZoneInfo"/>.</param>
        /// <param name="destinationTimeZoneInfo">The destination <see cref="TimeZoneInfo"/>.</param>
        /// <returns></returns>
        public DateTime GetTime(DateTime sourceTime, TimeZoneInfo sourceTimeZoneInfo, TimeZoneInfo destinationTimeZoneInfo)
        {

            // Converts the time to the SessionTime.TimeZoneInfo
            DateTime date = TimeZoneInfo.ConvertTime(sourceTime, sourceTimeZoneInfo, TimeZoneInfo);
            // Calculate the session time for the date passed as parameter.
            DateTime sessionTime = ToSessionDateTime(date);
            // Converts the time to the destination TimeZoneInfo.
            return TimeZoneInfo.ConvertTime(sessionTime, TimeZoneInfo, destinationTimeZoneInfo);

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Added the session time to the date passed as parameter and returns it.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>The <see cref="DateTime"/> that represents the session time.</returns>
        public DateTime ToSessionDateTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, Time.Hours, date.Minute, date.Second, DateTimeKind.Unspecified);
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
        /// Compare <see cref="SessionTime"/> objects and return true if the elements are equals.
        /// the <see cref="SessionTime"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is SessionTime time)
                return time.Time == Time && time.TimeZoneInfo == TimeZoneInfo;

            return false;
        }

        /// <summary>
        /// Compare <see cref="SessionTime"/> objects and return true if the elements are equals.
        /// the <see cref="SessionTime"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="value">The <see cref="SessionTime"/> to compare with the instance.</param>
        /// <returns>True if the pair of <see cref="SessionTime"/> are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="SessionTime"/>object passed as parameter cannot be null.</exception>
        public bool Equals(SessionTime value)
        {
            if (value != null)
                return value.Time == Time && value.TimeZoneInfo == TimeZoneInfo;

            throw new ArgumentException("The argument can not be null.");
        }

        /// <summary>
        /// Compare <see cref="SessionTime"/> objects and return true if the elements are equals.
        /// the <see cref="SessionTime"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="st1">The first <see cref="SessionTime"/> object to compare with the second.</param>
        /// <param name="st2">The second <see cref="SessionTime"/> object to compare with the first.</param>
        /// <returns>True if <see cref="SessionTime"/> objects are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="SessionTime"/>objects passed as parameter cannot be null.</exception>
        public static bool Equals(SessionTime st1, SessionTime st2)
        {
            if (st1 != null && st2 != null)
                return st1.Time == st2.Time && st1.TimeZoneInfo == st2.TimeZoneInfo;

            throw new ArgumentException("The arguments can not be null.");
        }

        /// <summary>
        /// Compare <see cref="SessionTime"/> objects and return 1 if <paramref name="st1"/> is major than <paramref name="st2"/>, 
        /// otherwise returns -1 and 0 if the objects are equals.
        /// </summary>
        /// <param name="st1">The first <see cref="SessionTime"/> object to compare with the second.</param>
        /// <param name="st2">The second <see cref="SessionTime"/> object to compare with the first.</param>
        /// <returns>1 if <paramref name="st1"/>is major than <paramref name="st2"/>,
        /// -1 if <paramref name="st1"/>is minor than <paramref name="st1"/>,
        /// otherwise are equals and returns 0.</returns>
        /// <exception cref="ArgumentException">The <see cref="SessionTime"/>objects passed as parameter cannot be null.</exception>
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

        /// <summary>
        /// Compare <paramref name="value"/> object with the object instance and return 1 if the instance is major than the second, 
        /// otherwise returns -1 and 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare.</param>
        /// <returns>1 if <paramref name="value"/>is minor than the object instance,
        /// -1 if <paramref name="value"/>is major than <paramref name="st1"/>,
        /// otherwise are equals and returns 0.</returns>
        /// <exception cref="ArgumentException">The object passed as parameter cannot be null.</exception>
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

        /// <summary>
        /// Compare <see cref="SessionTime"/> to <see cref="SessionTime"/> instance and return 1 
        /// if the instance is major than the second, 
        /// otherwise returns -1 and 0 if the objects are equals.
        /// <param name="value">The target object to compare.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Added the <see cref="SessionTime"/> objects passed by parameters.
        /// </summary>
        /// <param name="st1"></param>
        /// <param name="st2"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">The object passed as parameter cannot be null.</exception>
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

        public static SessionTime operator >(SessionTime st1, SessionTime st2)
        {
            return new SessionTime();
        }

        public static SessionTime operator <(SessionTime st1, SessionTime st2)
        {
            return new SessionTime();
        }

        public static SessionTime operator >=(SessionTime st1, SessionTime st2)
        {
            return new SessionTime();
        }

        public static SessionTime operator <=(SessionTime st1, SessionTime st2)
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
        /// Returns a long string of the <see cref="SessionTime"/>. 
        /// The string include: Code, Description and the Time.
        /// </summary>
        /// <returns></returns>
        public string ToLongString()
        {
            return $"Code: {Code} | Description: {Description} | Time: {Time}";
        }

        /// <summary>
        /// Returns the hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

    }
}
