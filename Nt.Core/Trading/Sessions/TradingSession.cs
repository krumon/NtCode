using System;
using System.Collections.Generic;

namespace Nt.Core.Trading
{
    /// <summary>
    /// Contents trading session information.
    /// </summary>
    public class TradingSession : BaseElement
    {

        #region Consts

        DateTime TIME_REFERENCE = new DateTime(1978,9,20,0,0,0,DateTimeKind.Local);

        #endregion

        #region Private members


        /// <summary>
        /// The trading session type.
        /// </summary>
        private TradingSessionType genericSession;

        #endregion

        #region Public properties

        /// <summary>
        /// The trading session type.
        /// </summary>
        public TradingSessionType GenericSession
        {
            private set
            {
                genericSession = value;

                if (genericSession == TradingSessionType.Custom)
                {
                    Code = ToDefaultCode();
                    if (string.IsNullOrEmpty(Description))
                        Description = $"Custom TradingSessionInfo Hours {BeginSessionTime.LocalTime.TotalHours}.{EndSessionTime.LocalTime.TotalHours}.";
                }
                else
                {
                    Code = genericSession.ToCode();
                    Description = genericSession.ToDescription();
                }
            }
            get => genericSession;
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
        /// Create a new instance of <see cref="TradingSession"/> class with <see cref="Core.TradingSession"/>.
        /// </summary>
        /// <param name="genericSession">the <see cref="Core.TradingSession"/> to create the <see cref="TradingSession"/> class.</param>
        /// <param name="instrumentCode">The unique code of the instrument.</param>
        /// <param name="beginTimeDisplacement">The minutes of the balance session.</param>
        /// <returns>A new instance of <see cref="TradingSession"/> class.</returns>
        public static TradingSession CreateSessionHoursByType(TradingSessionType genericSession, TradingInstrumentCode instrumentCode = TradingInstrumentCode.Default, int beginTimeDisplacement = 0, int endTimeDisplacement = 0)
        {
            return new TradingSession
            {
                BeginSessionTime = genericSession.ToBeginSessionTime(instrumentCode, beginTimeDisplacement),
                EndSessionTime = genericSession.ToEndSessionTime(instrumentCode, endTimeDisplacement),
                GenericSession = genericSession,
            };
        }


        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTimeType"/> types and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingSession">The initial <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="endTradingSession">The final <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomSessionHours(TradingTimeType beginTradingSession, TradingTimeType endTradingSession, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginTradingSession),
                EndSessionTime = TradingTime.CreateSessionTimeByType(endTradingSession),
                Description = description,
                GenericSession = TradingSessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTime"/> object, <see cref="TradingTimeType"/> type and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginSessionTime">The initial <see cref="TradingTime"/> of the <see cref="TradingSession"/> object.</param>
        /// <param name="endTradingSession">The final <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomSessionHours(TradingTime beginSessionTime, TradingTimeType endTradingSession, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = beginSessionTime,
                EndSessionTime = TradingTime.CreateSessionTimeByType(endTradingSession),
                Description = description,
                GenericSession = TradingSessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTime"/> object, <see cref="TradingTimeType"/> type and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingSession">The initial <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="endSessionTime">The final <see cref="TradingTime"/> of the <see cref="TradingSession"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomSessionHours(TradingTimeType beginTradingSession, TradingTime endSessionTime, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginTradingSession),
                EndSessionTime = endSessionTime,
                Description = description,
                GenericSession = TradingSessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTime"/> objects and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginSessionTime">The initial <see cref="TradingTime"/> of the <see cref="TradingSession"/> object.</param>
        /// <param name="endSessionTime">The final <see cref="TradingTime"/> of the <see cref="TradingSession"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomSessionHours(TradingTime beginSessionTime, TradingTime endSessionTime, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = beginSessionTime,
                EndSessionTime = endSessionTime,
                Description = description,
                GenericSession = TradingSessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTimeType"/>, <see cref="TradingTime"/> properties and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTime">The initial <see cref="TimeSpan"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="beginTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTradingTime">The final <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomSessionHours(TimeSpan beginTime, TimeZoneInfo beginTimeZoneInfo, TradingTimeType endTradingTime, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = TradingTime.CreateCustomSessionTime(beginTime,beginTimeZoneInfo,description == "" ? "Custom Open Time" : description + " - Open"),
                EndSessionTime = TradingTime.CreateSessionTimeByType(endTradingTime),
                Description = description,
                GenericSession = TradingSessionType.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTimeType"/>, <see cref="TradingTime"/> properties and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTime">The initial <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="endTime">The initial <see cref="TimeSpan"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="TradingSession"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="description"></param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static TradingSession CreateCustomSessionHours(TradingTimeType beginTradingTime, TimeSpan endTime, TimeZoneInfo endTimeZoneInfo, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginTradingTime),
                EndSessionTime = TradingTime.CreateCustomSessionTime(endTime,endTimeZoneInfo,description == "" ? "Custom Open Time" : description + " - Open"),
                Description = description,
                GenericSession = TradingSessionType.Custom,
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
        public static TradingSession CreateCustomSessionHours(TimeSpan beginTime, TimeZoneInfo beginTimeZoneInfo, TimeSpan endTime, TimeZoneInfo endTimeZoneInfo, string description = "")
        {
            return new TradingSession
            {
                BeginSessionTime = TradingTime.CreateCustomSessionTime(beginTime,beginTimeZoneInfo,description == "" ? "Custom TradingSessionInfo - Open Time" : description + " - Open"),
                EndSessionTime = TradingTime.CreateCustomSessionTime(endTime,endTimeZoneInfo,description == "" ? "Custom TradingSessionInfo - Close Time" : description + " - Close"),
                Description = description,
                GenericSession = TradingSessionType.Custom,
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
        public DateTime GetNextEndDateTime(
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

        #region Operator, Compare and Equity methods

        /// <summary>
        /// Compare <see cref="TradingTime"/> objects and return true if the elements are equals.
        /// the <see cref="TradingTime"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is TradingSession sh)
                return BeginSessionTime.Equals(sh.BeginSessionTime) && EndSessionTime.Equals(sh.EndSessionTime) && this.Code == sh.Code;

            return false;
        }

        /// <summary>
        /// Compare <see cref="TradingTime"/> objects and return true if the elements are equals.
        /// the <see cref="TradingTime"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="value">The <see cref="TradingTime"/> to compare with the instance.</param>
        /// <returns>True if the pair of <see cref="TradingTime"/> are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="TradingTime"/>object passed as parameter cannot be null.</exception>
        public bool Equals(TradingSession sh)
        {

            if (sh is null)
                return false;

            return BeginSessionTime.Equals(sh.BeginSessionTime) && EndSessionTime.Equals(sh.EndSessionTime) && this.Code == sh.Code;
        }

        /// <summary>
        /// Compare <see cref="TradingTime"/> objects and return true if the elements are equals.
        /// the <see cref="TradingTime"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="sh1">The first <see cref="TradingTime"/> object to compare with the second.</param>
        /// <param name="sh2">The second <see cref="TradingTime"/> object to compare with the first.</param>
        /// <returns>True if <see cref="TradingTime"/> objects are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="TradingTime"/>objects passed as parameter cannot be null.</exception>
        public static bool Equals(TradingSession sh1, TradingSession sh2)
        {

            if (sh1 is null && sh2 is null)
                return true;

            if (sh1 is null || sh2 is null)
                return false;

            return TradingTime.Equals(sh1.BeginSessionTime, sh2.BeginSessionTime) && TradingTime.Equals(sh1.EndSessionTime, sh2.EndSessionTime) && sh1.Code == sh2.Code;

        }

        /// <summary>
        /// Compare <see cref="TradingSession"/> objects and return 1 if <paramref name="sh1"/> is greater than <paramref name="sh2"/>, 
        /// otherwise returns -1 and 0 if the objects are equals.
        /// </summary>
        /// <param name="sh1">The first <see cref="TradingSession"/> object to compare with the second.</param>
        /// <param name="sh2">The second <see cref="TradingSession"/> object to compare with the first.</param>
        /// <returns>1 if <paramref name="sh1"/>is greater than <paramref name="sh2"/>,
        /// -1 if <paramref name="sh1"/>is minor than <paramref name="sh1"/>,
        /// otherwise are equals and returns 0.</returns>
        /// <exception cref="ArgumentException">The <see cref="TradingSession"/>objects passed as parameter cannot be null.</exception>
        public static int Compare(TradingSession sh1, TradingSession sh2)
        {
            if (sh1 == null || sh2 == null)
                throw new ArgumentException("The arguments can not be null.");

            TimeSpan sh1UtcTime = sh1.BeginSessionTime.Time + sh1.EndSessionTime.Time;
            TimeSpan sh2UtcTime = sh2.EndSessionTime.Time + sh2.EndSessionTime.Time;

            if (sh1UtcTime > sh2UtcTime)
            {
                return 1;
            }

            if (sh1UtcTime < sh2UtcTime)
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
                throw new ArgumentException("Argument cannot be null");
            }

            if (value is TradingSession sh)
            {
                TimeSpan utcTime = this.BeginSessionTime.Time + this.EndSessionTime.Time;
                TimeSpan shUtcTime = sh.EndSessionTime.Time + sh.EndSessionTime.Time;

                if (utcTime > shUtcTime)
                {
                    return 1;
                }

                if (utcTime < shUtcTime)
                {
                    return -1;
                }

                return 0;
            }

            throw new ArgumentException("Argument must be TradingSessionInfo object.");
        }

        /// <summary>
        /// Compare <see cref="TradingSession"/> to <see cref="TradingSession"/> instance and return 1 
        /// if the instance is major than the second, 
        /// otherwise returns -1 and 0 if the objects are equals.
        /// <param name="value">The target object to compare.</param>
        /// <returns></returns>
        public int CompareTo(TradingSession value)
        {
            if (value == null)
            {
                throw new ArgumentException("Argument cannot be null");
            }

            TimeSpan thisUtcTime = this.BeginSessionTime.Time + this.EndSessionTime.Time;
            TimeSpan valueUtcTime = ((TradingSession)value).EndSessionTime.Time + ((TradingSession)value).EndSessionTime.Time;

            if (thisUtcTime > valueUtcTime)
            {
                return 1;
            }

            if (thisUtcTime < valueUtcTime)
            {
                return -1;
            }

            return 0;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> that is greater than another specified.
        /// </summary>
        /// <param name="sh1">The first object to compare.</param>
        /// <param name="sh2">The second object to compare.</param>
        /// <returns>True if <paramref name="sh1"/> is greater than <paramref name="sh2"/>; otherwise, false.</returns>
        public static bool operator >(TradingSession sh1, TradingSession sh2)
        {
            if (sh1 is null)
                throw new ArgumentNullException($"the argument {nameof(sh1)} cannot be null.");

            if (sh2 is null)
                throw new ArgumentNullException($"the argument {nameof(sh2)} cannot be null.");

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> that is earlier than another specified.
        /// </summary>
        /// <param name="sh1">The first object to compare.</param>
        /// <param name="sh2">The second object to compare.</param>
        /// <returns>True if <paramref name="sh1"/> is less than <paramref name="sh2"/>; otherwise, false.</returns>
        public static bool operator <(TradingSession sh1, TradingSession sh2)
        {
            if (sh1 is null)
                throw new ArgumentNullException($"the argument {nameof(sh1)} cannot be null.");

            if (sh2 is null)
                throw new ArgumentNullException($"the argument {nameof(sh2)} cannot be null.");

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> that is the same as or greater than another specified.
        /// </summary>
        /// <param name="sh1">The first object to compare.</param>
        /// <param name="sh2">The second object to compare.</param>
        /// <returns>True if <paramref name="sh1"/> is equal to or greater than <paramref name="sh2"/>; otherwise, false.</returns>
        public static bool operator >=(TradingSession sh1, TradingSession sh2)
        {
            if (sh1 is null)
                throw new ArgumentNullException($"the argument {nameof(sh1)} cannot be null.");

            if (sh2 is null)
                throw new ArgumentNullException($"the argument {nameof(sh2)} cannot be null.");

            return false;
        }
        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> that is the same as or earlier than another specified.
        /// </summary>
        /// <param name="sh1">The first object to compare.</param>
        /// <param name="sh2">The second object to compare.</param>
        /// <returns>True if <paramref name="sh1"/> is equal to or less than <paramref name="sh2"/>; otherwise, false.</returns>
        public static bool operator <=(TradingSession sh1, TradingSession sh2)
        {

            if (sh1 is null)
                throw new ArgumentNullException($"the argument {nameof(sh1)} cannot be null.");

            if (sh2 is null)
                throw new ArgumentNullException($"the argument {nameof(sh2)} cannot be null.");

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> have the same <see cref="Time"/>.
        /// </summary>
        /// <param name="sh1">The first object to compare.</param>
        /// <param name="sh2">The second object to compare.</param>
        /// <returns>True if <paramref name="sh1"/> and <paramref name="sh2"/> represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator ==(TradingSession sh1, TradingSession sh2)
        {
            if (sh1 is null && sh2 is null)
                return true;

            if (sh1 is null || sh2 is null)
                return false;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> haven't the same <see cref="Time"/>.
        /// </summary>
        /// <param name="sh1">The first object to compare.</param>
        /// <param name="sh2">The second object to compare.</param>
        /// <returns>True if <paramref name="sh1"/> and <paramref name="sh2"/> do not represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator !=(TradingSession sh1, TradingSession sh2)
        {
            if (sh1 is null && sh2 is null)
                return false;

            if (sh1 is null && !(sh2 is null))
                return true;

            if (!(sh1 is null) || sh2 is null)
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
        /// Returns the string that represents the <see cref="Time"/> of the <see cref="TradingTime"/>.
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

        #endregion

        #region Helpèr methods

        public TradingSession GetRegularSession()
        {
            return CreateSessionHoursByType(TradingSessionType.Regular);
        }

        public TradingSession GetOvernightSession()
        {
            return CreateSessionHoursByType(TradingSessionType.OVN);
        }

        public TradingSession GetEuropeanSession()
        {
            return CreateSessionHoursByType(TradingSessionType.European);
        }

        public TradingSession GetAsianSession()
        {
            return CreateSessionHoursByType(TradingSessionType.Asian);
        }

        public TradingSession GetAmericanSession()
        {
            return CreateSessionHoursByType(TradingSessionType.American);
        }

        public TradingSession GetAmericanAndEuropeanSession()
        {
            return CreateSessionHoursByType(TradingSessionType.AmericanAndEuropean);
        }

        public TradingSession GetAmericanResidualSession()
        {
            return CreateSessionHoursByType(TradingSessionType.American_RS); ;
        }

        public TradingSession GetAsianResidualSession()
        {
            return CreateSessionHoursByType(TradingSessionType.Asian_RS); ;
        }

        public TradingSession GetAmericanResidualExtraTimeSession()
        {
            return CreateSessionHoursByType(TradingSessionType.American_RS_EXT); ;
        }

        public TradingSession GetAmericanResidualEndOfDaySession()
        {
            return CreateSessionHoursByType(TradingSessionType.American_RS_EOD); ;
        }

        public TradingSession GetAmericanResidualNewDaySession()
        {
            return CreateSessionHoursByType(TradingSessionType.American_RS_NWD); ;
        }

        public List<TradingSession> GetAmericanSessions()
        {
            List<TradingSession> sessions = new List<TradingSession>
            {
                CreateSessionHoursByType(TradingSessionType.AmericanAndEuropean),
                CreateSessionHoursByType(TradingSessionType.American),
            };

            return sessions;
        }

        public List<TradingSession> GetAmericanResidualSessions()
        {
            List<TradingSession> sessions = new List<TradingSession>
            {
                CreateSessionHoursByType(TradingSessionType.American_RS_EXT),
                CreateSessionHoursByType(TradingSessionType.American_RS_EXT),
                CreateSessionHoursByType(TradingSessionType.American_RS_NWD)
            };

            return sessions;
        }

        #endregion

    }
}
