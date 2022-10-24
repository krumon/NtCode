using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nt.Core.Trading
{
    /// <summary>
    /// Contents trading session information.
    /// </summary>
    public class TradingSession : BaseElement, IComparable, IComparable<TradingSession>, IComparer, IComparer<TradingSession>
    {

        #region Consts

        DateTime TIME_REFERENCE = new DateTime(1978,9,20,0,0,0,DateTimeKind.Local);

        #endregion

        #region Private members

        /// <summary>
        /// The trading session type.
        /// </summary>
        private TradingSessionType _sessionType;

        #endregion

        #region Public properties

        /// <summary>
        /// The trading session type.
        /// </summary>
        public TradingSessionType SessionType
        {
            private set
            {
                _sessionType = value;

                if (_sessionType == TradingSessionType.Custom)
                {
                    Code = ToDefaultCode();
                    if (string.IsNullOrEmpty(Description))
                        Description = $"Custom TradingSessionInfo Hours {BeginSessionTime.LocalTime.TotalHours}.{EndSessionTime.LocalTime.TotalHours}.";
                }
                else
                {
                    Code = _sessionType.ToCode();
                    Description = _sessionType.ToDescription();
                }
            }
            get => _sessionType;
        }

        /// <summary>
        /// Gets the unique code of the <see cref="TradingSession"/>.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the description of the <see cref="TradingSession"/>.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The initial <see cref="TradingTime"/>.
        /// </summary>
        public TradingTime BeginSessionTime { get; set; }

        /// <summary>
        /// The final <see cref="TradingTime"/>.
        /// </summary>
        public TradingTime EndSessionTime { get; set; }

        /// <summary>
        /// TradingSessionInfo hours duration.
        /// </summary>
        public TimeSpan Duration => EndSessionTime >= BeginSessionTime ? EndSessionTime - BeginSessionTime : BeginSessionTime - EndSessionTime;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of <see cref="TradingSession"/> class.
        /// </summary>
        private TradingSession()
        {
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Create a new instance of <see cref="TradingSession"/> class with <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="sessionType">the <see cref="TradingSession"/> to create the <see cref="TradingSession"/> class.</param>
        /// <param name="instrumentCode">The unique code of the instrument.</param>
        /// <param name="beginTimeDisplacement">The minutes of the balance session.</param>
        /// <returns>A new instance of <see cref="TradingSession"/> class.</returns>
        public static TradingSession CreateTradingSessionByType(TradingSessionType sessionType, TradingInstrumentCode instrumentCode = TradingInstrumentCode.Default, int beginTimeDisplacement = 0, int endTimeDisplacement = 0)
        {
            return new TradingSession
            {
                BeginSessionTime = sessionType.ToBeginSessionTime(instrumentCode, beginTimeDisplacement),
                EndSessionTime = sessionType.ToEndSessionTime(instrumentCode, endTimeDisplacement),
                SessionType = sessionType,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTimeType"/> types and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginSessionTimeType">The initial <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="endSessionTimeType">The final <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomTradingSession(TradingTimeType beginSessionTimeType, TradingTimeType endSessionTimeType, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginSessionTimeType),
                EndSessionTime = TradingTime.CreateSessionTimeByType(endSessionTimeType),
                Description = description,
                SessionType = TradingSessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTime"/> object, <see cref="TradingTimeType"/> type and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTime">The initial <see cref="TradingTime"/> of the <see cref="TradingSession"/> object.</param>
        /// <param name="endTradingTimeType">The final <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomTradingSession(TradingTime beginTradingTime, TradingTimeType endTradingTimeType, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = beginTradingTime,
                EndSessionTime = TradingTime.CreateSessionTimeByType(endTradingTimeType),
                Description = description,
                SessionType = TradingSessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTime"/> object, <see cref="TradingTimeType"/> type and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTimeType">The initial <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="endTradingTime">The final <see cref="TradingTime"/> of the <see cref="TradingSession"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomTradingSession(TradingTimeType beginTradingTimeType, TradingTime endTradingTime, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginTradingTimeType),
                EndSessionTime = endTradingTime,
                Description = description,
                SessionType = TradingSessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTime"/> objects and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTime">The initial <see cref="TradingTime"/> of the <see cref="TradingSession"/> object.</param>
        /// <param name="endTradingTime">The final <see cref="TradingTime"/> of the <see cref="TradingSession"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomTradingSession(TradingTime beginTradingTime, TradingTime endTradingTime, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = beginTradingTime,
                EndSessionTime = endTradingTime,
                Description = description,
                SessionType = TradingSessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTimeType"/>, <see cref="TradingTime"/> properties and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTime">The initial <see cref="TimeSpan"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="beginTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTradingTimeType">The final <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomTradingSession(TimeSpan beginTime, TimeZoneInfo beginTimeZoneInfo, TradingTimeType endTradingTimeType, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = TradingTime.CreateCustomSessionTime(beginTime,beginTimeZoneInfo,description == "" ? "Custom Open Time" : description + " - Open"),
                EndSessionTime = TradingTime.CreateSessionTimeByType(endTradingTimeType),
                Description = description,
                SessionType = TradingSessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTimeType"/>, <see cref="TradingTime"/> properties and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTimeType">The initial <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="endTime">The initial <see cref="TimeSpan"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="description"></param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomTradingSession(TradingTimeType beginTradingTimeType, TimeSpan endTime, TimeZoneInfo endTimeZoneInfo, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginTradingTimeType),
                EndSessionTime = TradingTime.CreateCustomSessionTime(endTime,endTimeZoneInfo,description == "" ? "Custom Open Time" : description + " - Open"),
                Description = description,
                SessionType = TradingSessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTime"/> properties and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTime">The initial <see cref="TimeSpan"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="beginTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTime">The initial <see cref="TimeSpan"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="description"></param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomTradingSession(TimeSpan beginTime, TimeZoneInfo beginTimeZoneInfo, TimeSpan endTime, TimeZoneInfo endTimeZoneInfo, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = TradingTime.CreateCustomSessionTime(beginTime,beginTimeZoneInfo,description == "" ? "Custom TradingSessionInfo - Open Time" : description + " - Open"),
                EndSessionTime = TradingTime.CreateCustomSessionTime(endTime,endTimeZoneInfo,description == "" ? "Custom TradingSessionInfo - Close Time" : description + " - Close"),
                Description = description,
                SessionType = TradingSessionType.Custom,
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the initial <see cref="DateTime"/> of the <see cref="TradingSession"/>.
        /// </summary>
        /// <returns>The begin <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetBeginTime(DateTime time)
        {
            return BeginSessionTime.GetTime(time);
        }

        /// <summary>
        /// Gets the begin <see cref="DateTime"/> structure of the <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <returns>The begin <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetBeginTime(DateTime time, TimeZoneInfo sourceTimeZoneInfo)
        {
            return BeginSessionTime.GetTime(time,sourceTimeZoneInfo);
        }

        /// <summary>
        /// Gets the begin <see cref="DateTime"/> structure of the <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <param name="destinationTimeZoneInfo">The <see cref="TimeZoneInfo"/> to convert the date time structure.</param>
        /// <returns>The begin <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetBeginTime(
            DateTime time,
            TimeZoneInfo sourceTimeZoneInfo,
            TimeZoneInfo destinationTimeZoneInfo)
        {
            return BeginSessionTime.GetTime(time,sourceTimeZoneInfo,destinationTimeZoneInfo);
        }

        /// <summary>
        /// Gets the end <see cref="DateTime"/> structure of the <see cref="TradingSession"/>.
        /// </summary>
        /// <returns>The end <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetEndTime(DateTime time)
        {
            return EndSessionTime.GetTime(time);
        }

        /// <summary>
        /// Gets the end <see cref="DateTime"/> structure of the <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <returns>The end <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetEndTime(DateTime time, TimeZoneInfo sourceTimeZoneInfo)
        {
            return EndSessionTime.GetTime(time,sourceTimeZoneInfo);
        }

        /// <summary>
        /// Gets the end <see cref="DateTime"/> structure of the <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <param name="destinationTimeZoneInfo">The <see cref="TimeZoneInfo"/> to convert the date time structure.</param>
        /// <returns>The end <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetEndTime(
            DateTime time,
            TimeZoneInfo sourceTimeZoneInfo,
            TimeZoneInfo destinationTimeZoneInfo)
        {
            return EndSessionTime.GetTime(time,sourceTimeZoneInfo,destinationTimeZoneInfo);
        }

        // TODO: Terminar este método. ES COMPLICADO!!!!!!

        public DateTime? GetNextSessionEndTime(DateTime currentTime, DateTime actualSessionBeginTime, DateTime actualSessionEndTime)
        {
            TimeZoneInfo sourceTimeZoneInfo = 
                currentTime.Kind == DateTimeKind.Local ? TimeZoneInfo.Local : 
                currentTime.Kind == DateTimeKind.Utc ? TimeZoneInfo.Utc : null;

            if (sourceTimeZoneInfo != null)
            {
                DateTime beginTime = GetBeginTime(currentTime);
                DateTime endTime = GetEndTime(currentTime);

                bool dayChanges = actualSessionEndTime.Date - actualSessionBeginTime.Date != TimeSpan.Zero;

                if (endTime <= beginTime && dayChanges)
                    return GetEndTime(actualSessionEndTime);
                
                if (beginTime > actualSessionBeginTime)
                {

                }
            }
                // Returns the TradingTimeInfo TimeSpan for the date passed as parameter.
                //return GetTime(time, sourceTimeZoneInfo);

            throw new Exception("The kind of the " + nameof(currentTime) + " must be Local or Utc");

        }

        /// <summary>
        /// Gets the final <see cref="DateTime"/> structure of the <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="currentDate">The current date time.</param>
        /// <param name="destinationTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The final <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/>.</returns>
        public DateTime GetNextEndTime(
            DateTime currentDate,
            TimeZoneInfo sourceTimeZoneInfo = null,
            TimeZoneInfo destinationTimeZoneInfo = null)
        {
            DateTime beginDateTime = BeginSessionTime.GetTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);
            DateTime endDateTime = EndSessionTime.GetTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);

            if (endDateTime <= beginDateTime)
                return endDateTime.AddHours(24);

            return EndSessionTime.GetTime(currentDate);
        }

        /// <summary>
        /// Gets the final <see cref="DateTime"/> structure of the <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="currentDate">The current date time.</param>
        /// <param name="destinationTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The initial and final <see cref="DateTime"/> structures of the next session since the <paramref name="currentTime"/>.</returns>
        public DateTime[] GetNextDateTimes(
            DateTime currentDate,
            TimeZoneInfo sourceTimeZoneInfo = null,
            TimeZoneInfo destinationTimeZoneInfo = null,
            bool sessionComplete = false)
        {
            DateTime[] sessionDateTimes = new DateTime[2];
            DateTime beginDateTime = BeginSessionTime.GetTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);
            DateTime endDateTime = EndSessionTime.GetTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);

            if (sessionComplete && (endDateTime <= beginDateTime))
                endDateTime += TimeSpan.FromHours(24);

            sessionDateTimes[0] = beginDateTime;
            sessionDateTimes[1] = endDateTime;

            return sessionDateTimes;
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

        #region Equals methods

        /// <summary>
        /// Compare <see cref="TradingSession"/> objects and return true if the elements are equals.
        /// The <see cref="TradingSession"/> objects are equals if the times and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is TradingSession ts)
                return BeginSessionTime.Equals(ts.BeginSessionTime) && EndSessionTime.Equals(ts.EndSessionTime) && this.Code == ts.Code;

            return false;
        }

        /// <summary>
        /// Compare <see cref="TradingSession"/> objects and return true if the elements are equals.
        /// the <see cref="TradingSession"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="value">The <see cref="TradingSession"/> to compare with the instance.</param>
        /// <returns>True if the pair of <see cref="TradingSession"/> are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="TradingSession"/>object passed as parameter cannot be null.</exception>
        public bool Equals(TradingSession ts)
        {

            if (ts is null)
                return false;

            return BeginSessionTime.Equals(ts.BeginSessionTime) && EndSessionTime.Equals(ts.EndSessionTime) && this.Code == ts.Code;
        }

        /// <summary>
        /// Compare <see cref="TradingSession"/> objects and return true if the elements are equals.
        /// the <see cref="TradingSession"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="ts1">The first <see cref="TradingSession"/> object to compare with the second.</param>
        /// <param name="ts2">The second <see cref="TradingSession"/> object to compare with the first.</param>
        /// <returns>True if <see cref="TradingSession"/> objects are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="TradingSession"/>objects passed as parameter cannot be null.</exception>
        public static bool Equals(TradingSession ts1, TradingSession ts2)
        {

            if (ts1 is null && ts2 is null)
                return true;

            if (ts1 is null || ts2 is null)
                return false;

            return TradingTime.Equals(ts1.BeginSessionTime, ts2.BeginSessionTime) && TradingTime.Equals(ts1.EndSessionTime, ts2.EndSessionTime) && ts1.Code == ts2.Code;

        }



        #endregion

        #region Compare methods

        /// <summary>
        /// Compare <paramref name="value1"/> and <paramref name="value2"/> objects and 
        /// return 3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// return -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// return 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// return -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// return 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// return -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value1">The first object to compare with the second.</param>
        /// <param name="value2">The second object to compare with the first.</param>
        /// <returns>3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// 0 if the objects are equals.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="TradingSession"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="TradingSession"/> objects.</exception>
        public int Compare(object value1, object value2)
        {
            if (!(value1 is null && value2 is null))
            {
                if (value1 is TradingSession ts1)
                {
                    if (value2 is TradingSession ts2)
                    {
                        return Compare(ts1, ts2);
                    }
                    else
                        throw new ArgumentException(nameof(value2));
                }
                else
                    throw new ArgumentException(nameof(value1));
            }

            throw new ArgumentNullException("The value1 and value2 cannot be null.");
        }

        /// <summary>
        /// Compare <see cref="TradingSession"/> objects and 
        /// return 3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// return -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// return 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// return -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// return 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// return -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value1">The first <see cref="TradingSession"/> object to compare with the second.</param>
        /// <param name="value2">The second <see cref="TradingSession"/> object to compare with the first.</param>
        /// <returns>3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// 0 if the objects are equals.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="TradingSession"/>objects passed as parameter cannot be null.</exception>
        public int Compare(TradingSession value1, TradingSession value2)
        {
            if (value1 == null || value2 == null)
                throw new ArgumentNullException("The arguments cannot be null.");

            return (int)CompareSessions(value1, value2);
            
        }

        /// <summary>
        /// Compare the <see cref="TradingSession"/> to <paramref name="value"/> object and 
        /// return 3 if <see cref="TradingSession"/> is the parent of the <paramref name="value"/>, 
        /// return -3 if <see cref="TradingSession"/> is the parent,
        /// return 2 if <see cref="TradingSession"/> is major and inner on the <paramref name="value"/>,
        /// return -2 if <see cref="TradingSession"/> is minor and inner on the <paramref name="value"/>,
        /// return 1 if <see cref="TradingSession"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="TradingSession"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="TradingSession"/>.</param>
        /// <returns>3 if <see cref="TradingSession"/> is the parent of the <paramref name="value"/>, 
        /// -3 if <see cref="TradingSession"/> is the parent,
        /// 2 if <see cref="TradingSession"/> is major and inner on the <paramref name="value"/>,
        /// -2 if <see cref="TradingSession"/> is minor and inner on the <paramref name="value"/>,
        /// 1 if <see cref="TradingSession"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="TradingSession"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="TradingSession"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="TradingSession"/> objects.</exception>
        public int CompareTo(object value)
        {
            if (!(value is null))
            {
                if (value is TradingSession ts)
                {
                    return Compare(this, ts);
                }
                else
                    throw new ArgumentException(nameof(value));
            }

            throw new ArgumentNullException("The value cannot be null.");
        }

        /// <summary>
        /// Compare the <see cref="TradingSession"/> to <see cref="TradingSession"/> object and 
        /// return 3 if <see cref="TradingSession"/> is the parent of the <paramref name="value"/>, 
        /// return -3 if <see cref="TradingSession"/> is the parent,
        /// return 2 if <see cref="TradingSession"/> is major and inner on the <paramref name="value"/>,
        /// return -2 if <see cref="TradingSession"/> is minor and inner on the <paramref name="value"/>,
        /// return 1 if <see cref="TradingSession"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="TradingSession"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="TradingSession"/>.</param>
        /// <returns>3 if <see cref="TradingSession"/> is the parent of the <paramref name="value"/>, 
        /// -3 if <see cref="TradingSession"/> is the parent,
        /// 2 if <see cref="TradingSession"/> is major and inner on the <paramref name="value"/>,
        /// -2 if <see cref="TradingSession"/> is minor and inner on the <paramref name="value"/>,
        /// 1 if <see cref="TradingSession"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="TradingSession"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="TradingSession"/>objects passed as parameter cannot be null.</exception>
        public int CompareTo(TradingSession value)
        {
            if (value == null)
                throw new ArgumentException("Argument cannot be null");

            return (int)CompareSessions(this, value);
        }

        /// <summary>
        /// Compare <paramref name="value1"/> and <paramref name="value2"/> objects and 
        /// return 3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// return -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// return 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// return -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// return 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// return -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value1">The first object to compare with the second.</param>
        /// <param name="value2">The second object to compare with the first.</param>
        /// <returns>3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// 0 if the objects are equals.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="TradingSession"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="TradingSession"/> objects.</exception>
        public TradingSessionCompareResult CompareSession(object value1, object value2)
        {
            if (!(value1 is null && value2 is null))
            {
                if (value1 is TradingSession ts1)
                {
                    if (value2 is TradingSession ts2)
                    {
                        return CompareSession(ts1, ts2);
                    }
                    else
                        throw new ArgumentException(nameof(value2));
                }
                else
                    throw new ArgumentException(nameof(value1));
            }

            throw new ArgumentNullException("The value1 and value2 cannot be null.");
        }

        /// <summary>
        /// Compare <see cref="TradingSession"/> objects and 
        /// return 3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// return -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// return 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// return -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// return 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// return -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value1">The first <see cref="TradingSession"/> object to compare with the second.</param>
        /// <param name="value2">The second <see cref="TradingSession"/> object to compare with the first.</param>
        /// <returns>3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// 0 if the objects are equals.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="TradingSession"/>objects passed as parameter cannot be null.</exception>
        public TradingSessionCompareResult CompareSession(TradingSession value1, TradingSession value2)
        {
            if (value1 == null || value2 == null)
                throw new ArgumentNullException("The arguments cannot be null.");

            return CompareSessions(value1, value2);
            
        }

        /// <summary>
        /// Compare the <see cref="TradingSession"/> to <paramref name="value"/> object and 
        /// return 3 if <see cref="TradingSession"/> is the parent of the <paramref name="value"/>, 
        /// return -3 if <see cref="TradingSession"/> is the parent,
        /// return 2 if <see cref="TradingSession"/> is major and inner on the <paramref name="value"/>,
        /// return -2 if <see cref="TradingSession"/> is minor and inner on the <paramref name="value"/>,
        /// return 1 if <see cref="TradingSession"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="TradingSession"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="TradingSession"/>.</param>
        /// <returns>3 if <see cref="TradingSession"/> is the parent of the <paramref name="value"/>, 
        /// -3 if <see cref="TradingSession"/> is the parent,
        /// 2 if <see cref="TradingSession"/> is major and inner on the <paramref name="value"/>,
        /// -2 if <see cref="TradingSession"/> is minor and inner on the <paramref name="value"/>,
        /// 1 if <see cref="TradingSession"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="TradingSession"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="TradingSession"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="TradingSession"/> objects.</exception>
        public TradingSessionCompareResult CompareSessionTo(object value)
        {
            if (!(value is null))
            {
                if (value is TradingSession ts)
                {
                    return CompareSession(this, ts);
                }
                else
                    throw new ArgumentException(nameof(value));
            }

            throw new ArgumentNullException("The value cannot be null.");
        }

        /// <summary>
        /// Compare the <see cref="TradingSession"/> to <see cref="TradingSession"/> object and 
        /// return 3 if <see cref="TradingSession"/> is the parent of the <paramref name="value"/>, 
        /// return -3 if <see cref="TradingSession"/> is the parent,
        /// return 2 if <see cref="TradingSession"/> is major and inner on the <paramref name="value"/>,
        /// return -2 if <see cref="TradingSession"/> is minor and inner on the <paramref name="value"/>,
        /// return 1 if <see cref="TradingSession"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="TradingSession"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="TradingSession"/>.</param>
        /// <returns>3 if <see cref="TradingSession"/> is the parent of the <paramref name="value"/>, 
        /// -3 if <see cref="TradingSession"/> is the parent,
        /// 2 if <see cref="TradingSession"/> is major and inner on the <paramref name="value"/>,
        /// -2 if <see cref="TradingSession"/> is minor and inner on the <paramref name="value"/>,
        /// 1 if <see cref="TradingSession"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="TradingSession"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="TradingSession"/>objects passed as parameter cannot be null.</exception>
        public TradingSessionCompareResult CompareSessionTo(TradingSession value)
        {
            if (value == null)
                throw new ArgumentException("Argument cannot be null");

            return CompareSession(this, value);
        }

        #endregion

        #region Opertator methods

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> that is greater than another specified.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> is greater than <paramref name="ts2"/>; otherwise, false.</returns>
        public static bool operator >(TradingSession ts1, TradingSession ts2)
        {
            if (ts1 is null)
                throw new ArgumentNullException($"the argument {nameof(ts1)} cannot be null.");

            if (ts2 is null)
                throw new ArgumentNullException($"the argument {nameof(ts2)} cannot be null.");

            TradingSessionCompareResult result = CompareSessions(ts1, ts2);
            if (result == TradingSessionCompareResult.Major || result == TradingSessionCompareResult.MajorAndInner)
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> that is earlier than another specified.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> is less than <paramref name="ts2"/>; otherwise, false.</returns>
        public static bool operator <(TradingSession ts1, TradingSession ts2)
        {
            if (ts1 is null)
                throw new ArgumentNullException($"the argument {nameof(ts1)} cannot be null.");

            if (ts2 is null)
                throw new ArgumentNullException($"the argument {nameof(ts2)} cannot be null.");

            TradingSessionCompareResult result = CompareSessions(ts1, ts2);
            if (result == TradingSessionCompareResult.Minor || result == TradingSessionCompareResult.MinorAndInner)
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> that is the parent of another specified.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> is parent of <paramref name="ts2"/>; otherwise, false.</returns>
        public static bool operator >=(TradingSession ts1, TradingSession ts2)
        {
            if (ts1 is null)
                throw new ArgumentNullException($"the argument {nameof(ts1)} cannot be null.");

            if (ts2 is null)
                throw new ArgumentNullException($"the argument {nameof(ts2)} cannot be null.");

            TradingSessionCompareResult result = CompareSessions(ts1, ts2);
            if (result == TradingSessionCompareResult.Parent)
                return true;

            return false;
        }
        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> that is the child of another specified.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> is the child of <paramref name="ts2"/>; otherwise, false.</returns>
        public static bool operator <=(TradingSession ts1, TradingSession ts2)
        {

            if (ts1 is null)
                throw new ArgumentNullException($"the argument {nameof(ts1)} cannot be null.");

            if (ts2 is null)
                throw new ArgumentNullException($"the argument {nameof(ts2)} cannot be null.");

            TradingSessionCompareResult result = CompareSessions(ts1, ts2);
            if (result == TradingSessionCompareResult.Child)
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> have the same <see cref="Time"/>.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> and <paramref name="ts2"/> represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator ==(TradingSession ts1, TradingSession ts2)
        {
            if (ts1 is null)
                throw new ArgumentNullException($"the argument {nameof(ts1)} cannot be null.");

            if (ts2 is null)
                throw new ArgumentNullException($"the argument {nameof(ts2)} cannot be null.");

            TradingSessionCompareResult result = CompareSessions(ts1, ts2);
            if (result == TradingSessionCompareResult.Equals)
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> haven't the same <see cref="Time"/>.
        /// </summary>
        /// <param name="ts1">The first object to compare.</param>
        /// <param name="ts2">The second object to compare.</param>
        /// <returns>True if <paramref name="ts1"/> and <paramref name="ts2"/> do not represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator !=(TradingSession ts1, TradingSession ts2)
        {
            if (ts1 is null)
                throw new ArgumentNullException($"the argument {nameof(ts1)} cannot be null.");

            if (ts2 is null)
                throw new ArgumentNullException($"the argument {nameof(ts2)} cannot be null.");

            TradingSessionCompareResult result = CompareSessions(ts1, ts2);
            if (result != TradingSessionCompareResult.Equals)
                return true;

            return false;
        }

        /// <summary>
        /// Adds a specified session time to a specified session time, generating a new time span.
        /// </summary>
        /// <param name="sh1">TradingSessionInfo time value to add.</param>
        /// <param name="sh2">TradingSessionInfo time value to add.</param>
        /// <returns><see cref="TimeSpan"/> that is the sum of the values ​​of <paramref name="sh1"/> and <paramref name="sh2"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator +(TradingSession sh1, TradingSession sh2)
        {
            if (sh1 is null)
                throw new ArgumentNullException($"the argument {nameof(sh1)} cannot be null.");

            if (sh2 is null)
                throw new ArgumentNullException($"the argument {nameof(sh2)} cannot be null.");

            return sh1.BeginSessionTime.UtcTime + sh2.EndSessionTime.UtcTime;
        }

        /// <summary>
        /// Adds a specified session time to a specified time span, generating a new time span.
        /// </summary>
        /// <param name="sh">TradingSessionInfo time value to add.</param>
        /// <param name="ts">Time span value to add.</param>
        /// <returns><see cref="TimeSpan"/> that is the sum of the values ​​of <paramref name="sh"/> and <paramref name="ts"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator +(TradingSession sh, TimeSpan ts)
        {
            if (sh is null)
                throw new ArgumentNullException($"the argument {nameof(sh)} cannot be null.");

            return new TimeSpan((sh.EndSessionTime.UtcTime + ts).Ticks);
        }

        /// <summary>
        /// Subtracts a specified session time from a specified session time value and returns a newtime span.
        /// </summary>
        /// <param name="sh1">TradingSessionInfo time value to substract.</param>
        /// <param name="sh2">TradingSessionInfo time value to substract.</param>
        /// <returns>An <see cref="TimeSpan"/> whose value is the value of <paramref name="sh1"/> minus the value of <paramref name="sh2"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator -(TradingSession sh1, TradingSession sh2)
        {
            if (sh1 is null)
                throw new ArgumentNullException($"the argument {nameof(sh1)} cannot be null.");

            if (sh2 is null)
                throw new ArgumentNullException($"the argument {nameof(sh2)} cannot be null.");

            return new TimeSpan((sh1.EndSessionTime.UtcTime - sh2.BeginSessionTime.UtcTime).Ticks);
        }

        /// <summary>
        /// Subtracts a specified time span from a specified session time value and returns a newtime span.
        /// </summary>
        /// <param name="sh">TradingSessionInfo time value to add.</param>
        /// <param name="ts">Time span value to add.</param>
        /// <returns>An <see cref="TimeSpan"/> whose value is the value of <paramref name="sh"/> minus the value of <paramref name="ts"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator -(TradingSession sh, TimeSpan ts)
        {
            if (sh is null)
                throw new ArgumentNullException($"the argument {nameof(sh)} cannot be null.");

            return new TimeSpan((sh.BeginSessionTime.UtcTime - ts).Ticks);
        }

        #endregion

        #region ToString methods

        /// <summary>
        /// Converts the <see cref="TradingSession"/> to string.
        /// </summary>
        /// <returns></returns>
        public string ToString(bool onlyActualSession)
        {
            DateTime[] sessionDateTimes = GetNextDateTimes(DateTime.Now);
            string sessions = String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", sessionDateTimes[0].ToString(), "End Time: ", sessionDateTimes[1].ToString());
            if (!onlyActualSession)
            {
                //if (HasSessionHours)
                //    for (int i = 0; i < SessionHours.Count; i++)
                //        sessionHoursList += Environment.NewLine + SessionHours[i].ToString(onlyActualSession);
            }

            return sessions;
        }

        /// <summary>
        /// Converts the <see cref="TradingSession"/> to string.
        /// </summary>
        /// <returns></returns>
        public string ToString(DateTime referenceDateTime)
        {
            DateTime[] sessionDateTimes = GetNextDateTimes(referenceDateTime);
            return String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", sessionDateTimes[0].ToString(), "End Time: ", sessionDateTimes[1].ToString());
        }

        /// <summary>
        /// Converts the <see cref="TradingSession"/> to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format($"{Description} || Begin Time: {BeginSessionTime.ToString("Local")} || End Time: {EndSessionTime.ToShortString()}");
        }

        /// <summary>
        /// Returns the string that represents the <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="format">The specific time to convert. The time can be Utc, Local or Unspecific.</param>
        /// <returns></returns>
        public string ToString(string format)
        {
            string f = format.ToUpper();

            return String.Format($"{Description} || Begin Time: {BeginSessionTime.ToShortString(f)} || End Time: {EndSessionTime.ToShortString(f)}");
        }

        /// <summary>
        /// Converts the <see cref="TradingSession"/> to short string.
        /// </summary>
        /// <returns></returns>
        public string ToShortString()
        {
            return String.Format($"{BeginSessionTime.ToShortString()} => {EndSessionTime.ToShortString()}");
        }

        /// <summary>
        /// Returns the string that represents the <see cref="Time"/> of the <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="format">The specific time to convert. The time can be Utc, Local or Unspecific.</param>
        /// <returns></returns>
        public string ToShortString(string format)
        {
            string f = format.ToUpper();

            return String.Format($"{BeginSessionTime.ToShortString(f)} => {EndSessionTime.ToShortString(f)}");
        }

        /// <summary>
        /// Converts the <see cref="TradingSession"/> to long string.
        /// </summary>
        /// <returns></returns>
        public string ToLongString()
        {
            return String.Format($"{Code} || {Description} || Begin Time: {BeginSessionTime.ToLongString()} || End Time: {EndSessionTime.ToLongString()}");
        }

        /// <summary>
        /// Returns the string that represents the <see cref="Time"/> of the <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="format">The specific time to convert. The time can be Utc, Local or Unspecific.</param>
        /// <returns></returns>
        public string ToLongString(string format)
        {
            string f = format.ToUpper();

            return String.Format($"{Code} || {Description} || Begin Time: {BeginSessionTime.ToLongString(f)} || End Time: {EndSessionTime.ToLongString(f)}");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns default code for custom session times.
        /// </summary>
        /// <returns>Returns a string with the hours and minutes in specific time zone info and utc time zone info.</returns>
        private string ToDefaultCode()
        {
            return $"CTM-{BeginSessionTime.Time.TotalHours}{EndSessionTime.Time.TotalHours}-{BeginSessionTime.UtcTime.TotalHours}{EndSessionTime.UtcTime.TotalHours}";
        }

        private static TradingSessionCompareResult CompareSessions(TradingSession ts1, TradingSession ts2)
        {
            if (
                ts1.BeginSessionTime == ts2.BeginSessionTime &&
                ts1.EndSessionTime == ts2.EndSessionTime
                )
                // Returns 0
                return TradingSessionCompareResult.Equals;
            else if (
                ts1.BeginSessionTime >= ts2.BeginSessionTime &&
                ts1.EndSessionTime <= ts2.EndSessionTime
                )
                // Returns -3
                return TradingSessionCompareResult.Child;
            else if (
                ts1.BeginSessionTime <= ts2.BeginSessionTime &&
                ts1.EndSessionTime >= ts2.EndSessionTime
                )
                // Returns 3
                return TradingSessionCompareResult.Parent;
            else if (
                ts1.BeginSessionTime < ts2.BeginSessionTime &&
                ts1.EndSessionTime <= ts2.BeginSessionTime
                )
                // Returns -2
                return TradingSessionCompareResult.Minor;
            else if (
                ts1.BeginSessionTime >= ts2.EndSessionTime &&
                ts1.EndSessionTime > ts2.EndSessionTime
                )
                // Returns 2
                return TradingSessionCompareResult.Major;
            else if (
                ts1.BeginSessionTime < ts2.BeginSessionTime &&
                ts1.EndSessionTime > ts2.BeginSessionTime &&
                ts1.EndSessionTime <= ts2.EndSessionTime
                )
                // Returns -1
                return TradingSessionCompareResult.MinorAndInner;
            else if (
                ts1.BeginSessionTime >= ts2.BeginSessionTime &&
                ts1.BeginSessionTime < ts2.EndSessionTime &&
                ts1.EndSessionTime > ts2.EndSessionTime
                )
                // Returns 1
                return TradingSessionCompareResult.MajorAndInner;

            // Alert an calculate error
            Debug.Assert(false);
            return TradingSessionCompareResult.Major;
        }

        #endregion

        #region Helpèr methods

        public TradingSession GetRegularSession()
        {
            return CreateTradingSessionByType(TradingSessionType.Regular);
        }

        public TradingSession GetOvernightSession()
        {
            return CreateTradingSessionByType(TradingSessionType.OVN);
        }

        public TradingSession GetEuropeanSession()
        {
            return CreateTradingSessionByType(TradingSessionType.European);
        }

        public TradingSession GetAsianSession()
        {
            return CreateTradingSessionByType(TradingSessionType.Asian);
        }

        public TradingSession GetAmericanSession()
        {
            return CreateTradingSessionByType(TradingSessionType.American);
        }

        public TradingSession GetAmericanAndEuropeanSession()
        {
            return CreateTradingSessionByType(TradingSessionType.AmericanAndEuropean);
        }

        public TradingSession GetAmericanResidualSession()
        {
            return CreateTradingSessionByType(TradingSessionType.American_RS); ;
        }

        public TradingSession GetAsianResidualSession()
        {
            return CreateTradingSessionByType(TradingSessionType.Asian_RS); ;
        }

        public TradingSession GetAmericanResidualExtraTimeSession()
        {
            return CreateTradingSessionByType(TradingSessionType.American_RS_EXT); ;
        }

        public TradingSession GetAmericanResidualEndOfDaySession()
        {
            return CreateTradingSessionByType(TradingSessionType.American_RS_EOD); ;
        }

        public TradingSession GetAmericanResidualNewDaySession()
        {
            return CreateTradingSessionByType(TradingSessionType.American_RS_NWD); ;
        }

        public List<TradingSession> GetAmericanSessions()
        {
            List<TradingSession> sessions = new List<TradingSession>
            {
                CreateTradingSessionByType(TradingSessionType.AmericanAndEuropean),
                CreateTradingSessionByType(TradingSessionType.American),
            };

            return sessions;
        }

        public List<TradingSession> GetAmericanResidualSessions()
        {
            List<TradingSession> sessions = new List<TradingSession>
            {
                CreateTradingSessionByType(TradingSessionType.American_RS_EXT),
                CreateTradingSessionByType(TradingSessionType.American_RS_EXT),
                CreateTradingSessionByType(TradingSessionType.American_RS_NWD)
            };

            return sessions;
        }

        #endregion

    }
}
