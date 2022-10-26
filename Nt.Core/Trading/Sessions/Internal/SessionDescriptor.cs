using System;
using System.Diagnostics;

namespace Nt.Core.Trading.Internal
{
    internal class SessionDescriptor
    {

        #region Consts

        DateTime TIME_REFERENCE = new DateTime(1978, 9, 20, 0, 0, 0, DateTimeKind.Local);

        #endregion

        #region Private members

        private SessionCode _sessionType;
        private TradingTime _endSessionTime;

        #endregion

        #region Public properties

        /// <summary>
        /// The session type.
        /// </summary>
        public SessionCode SessionType
        {
            private set
            {
                _sessionType = value;

                if (_sessionType == SessionCode.Custom)
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
        /// Gets the unique code of the <see cref="SessionDescriptor"/>.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the name of the <see cref="SessionDescriptor"/>.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the description of the <see cref="SessionDescriptor"/>.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The initial <see cref="TradingTime"/>.
        /// </summary>
        public TradingTime BeginSessionTime { get; set; }

        /// <summary>
        /// The final <see cref="TradingTime"/>.
        /// </summary>
        public TradingTime EndSessionTime
        {
            get => _endSessionTime;
            set
            {
                _endSessionTime = value;

                if (_endSessionTime <= BeginSessionTime)
                    _endSessionTime += TimeSpan.FromDays(1);
            }
        }

        /// <summary>
        /// Session duration.
        /// </summary>
        public TimeSpan Duration => EndSessionTime.Time - BeginSessionTime.Time;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of <see cref="SessionDescriptor"/> class.
        /// </summary>
        private SessionDescriptor()
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
        public static SessionDescriptor CreateTradingSessionByType(SessionCode sessionType, TradingInstrumentCode instrumentCode = TradingInstrumentCode.Default, int beginTimeDisplacement = 0, int endTimeDisplacement = 0)
        {
            return new SessionDescriptor
            {
                BeginSessionTime = sessionType.ToBeginSessionTime(instrumentCode, beginTimeDisplacement),
                EndSessionTime = sessionType.ToEndSessionTime(instrumentCode, endTimeDisplacement),
                SessionType = sessionType,
            };
        }

        /// <summary>
        /// Create a new instance of <see cref="SessionDescriptor"/> collection with <see cref="SessionType"/> collection.
        /// </summary>
        /// <param name="sessionTypes">The <see cref="SessionType"/> collection to create the <see cref="SessionDescriptor"/> collection.</param>
        /// <param name="instrumentCode">The unique code of the instrument.</param>
        /// <param name="beginTimeDisplacement">The displacement minutes to the intial balance of the session.</param>
        /// <param name="endTimeDisplacement">The displacement minutes to the final balance of the session.</param>
        /// <returns>A new instance of <see cref="SessionDescriptor"/> collection.</returns>
        public static SessionDescriptor[] CreateTradingSessionByTypes(SessionCode[] sessionTypes, TradingInstrumentCode instrumentCode = TradingInstrumentCode.Default, int beginTimeDisplacement = 0, int endTimeDisplacement = 0)
        {
            if (sessionTypes == null || sessionTypes.Length < 1)
                throw new ArgumentNullException(nameof(sessionTypes));

            SessionDescriptor[] tradingSessions = new SessionDescriptor[sessionTypes.Length];
            for (int i = 0; i < sessionTypes.Length; i++)
            {
                SessionDescriptor ts = CreateTradingSessionByType(sessionTypes[i]);
                tradingSessions[i] = ts;
            }

            return tradingSessions;
        }

        /// <summary>
        /// Create a new custom instance of <see cref="SessionDescriptor"/> objects with specific <see cref="TradingTimeType"/> types and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginSessionTimeType">The initial <see cref="TradingTimeType"/> type of the <see cref="SessionDescriptor"/> object.</param>
        /// <param name="endSessionTimeType">The final <see cref="TradingTimeType"/> type of the <see cref="SessionDescriptor"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="SessionDescriptor"/> object.</returns>
        public static SessionDescriptor CreateCustomTradingSession(TradingTimeType beginSessionTimeType, TradingTimeType endSessionTimeType, string description = "")
        {
            return new SessionDescriptor
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginSessionTimeType),
                EndSessionTime = TradingTime.CreateSessionTimeByType(endSessionTimeType),
                Description = description,
                SessionType = SessionCode.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="TradingSession"/> objects with specific <see cref="TradingTime"/> object, <see cref="TradingTimeType"/> type and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTime">The initial <see cref="TradingTime"/> of the <see cref="TradingSession"/> object.</param>
        /// <param name="endTradingTimeType">The final <see cref="TradingTimeType"/> type of the <see cref="TradingSession"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static SessionDescriptor CreateCustomTradingSession(TradingTime beginTradingTime, TradingTimeType endTradingTimeType, string description = "")
        {
            return new SessionDescriptor
            {
                BeginSessionTime = beginTradingTime,
                EndSessionTime = TradingTime.CreateSessionTimeByType(endTradingTimeType),
                Description = description,
                SessionType = SessionCode.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="SessionDescriptor"/> objects with specific <see cref="TradingTime"/> object, <see cref="TradingTimeType"/> type and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTimeType">The initial <see cref="TradingTimeType"/> type of the <see cref="SessionDescriptor"/> object.</param>
        /// <param name="endTradingTime">The final <see cref="TradingTime"/> of the <see cref="SessionDescriptor"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static SessionDescriptor CreateCustomTradingSession(TradingTimeType beginTradingTimeType, TradingTime endTradingTime, string description = "")
        {
            return new SessionDescriptor
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginTradingTimeType),
                EndSessionTime = endTradingTime,
                Description = description,
                SessionType = SessionCode.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="SessionDescriptor"/> objects with specific <see cref="TradingTime"/> objects and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTime">The initial <see cref="TradingTime"/> of the <see cref="SessionDescriptor"/> object.</param>
        /// <param name="endTradingTime">The final <see cref="TradingTime"/> of the <see cref="SessionDescriptor"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="TradingSession"/> object.</returns>
        public static SessionDescriptor CreateCustomTradingSession(TradingTime beginTradingTime, TradingTime endTradingTime, string description = "")
        {
            return new SessionDescriptor
            {
                BeginSessionTime = beginTradingTime,
                EndSessionTime = endTradingTime,
                Description = description,
                SessionType = SessionCode.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="SessionDescriptor"/> objects with specific <see cref="TradingTimeType"/>, <see cref="TradingTime"/> properties and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTime">The initial <see cref="TimeSpan"/> of the <see cref="SessionDescriptor"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="beginTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="SessionDescriptor"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTradingTimeType">The final <see cref="TradingTimeType"/> type of the <see cref="SessionDescriptor"/> object.</param>
        /// <param name="description">Custom session hours description.</param>
        /// <returns>A new custom instance of <see cref="SessionDescriptor"/> object.</returns>
        public static SessionDescriptor CreateCustomTradingSession(TimeSpan beginTime, TimeZoneInfo beginTimeZoneInfo, TradingTimeType endTradingTimeType, string description = "")
        {
            return new SessionDescriptor
            {
                BeginSessionTime = TradingTime.CreateCustomSessionTime(beginTime, beginTimeZoneInfo, description == "" ? "Custom Open Time" : description + " - Open"),
                EndSessionTime = TradingTime.CreateSessionTimeByType(endTradingTimeType),
                Description = description,
                SessionType = SessionCode.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="SessionDescriptor"/> objects with specific <see cref="TradingTimeType"/>, <see cref="TradingTime"/> properties and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTradingTimeType">The initial <see cref="TradingTimeType"/> type of the <see cref="SessionDescriptor"/> object.</param>
        /// <param name="endTime">The initial <see cref="TimeSpan"/> of the <see cref="SessionDescriptor"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="SessionDescriptor"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="description"></param>
        /// <returns>A new custom instance of <see cref="SessionDescriptor"/> object.</returns>
        public static SessionDescriptor CreateCustomTradingSession(TradingTimeType beginTradingTimeType, TimeSpan endTime, TimeZoneInfo endTimeZoneInfo, string description = "")
        {
            return new SessionDescriptor
            {
                BeginSessionTime = TradingTime.CreateSessionTimeByType(beginTradingTimeType),
                EndSessionTime = TradingTime.CreateCustomSessionTime(endTime, endTimeZoneInfo, description == "" ? "Custom Open Time" : description + " - Open"),
                Description = description,
                SessionType = SessionCode.Custom,
            };
        }

        /// <summary>
        /// Create a new custom instance of <see cref="SessionDescriptor"/> objects with specific <see cref="TradingTime"/> properties and <paramref name="description"/>.
        /// </summary>
        /// <param name="beginTime">The initial <see cref="TimeSpan"/> of the <see cref="SessionDescriptor"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="beginTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="SessionDescriptor"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTime">The initial <see cref="TimeSpan"/> of the <see cref="SessionDescriptor"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="endTimeZoneInfo">The initial <see cref="TimeZoneInfo"/> of the <see cref="SessionDescriptor"/> <see cref="BeginSessionTime"/>.</param>
        /// <param name="description"></param>
        /// <returns>A new custom instance of <see cref="SessionDescriptor"/> object.</returns>
        public static SessionDescriptor CreateCustomTradingSession(TimeSpan beginTime, TimeZoneInfo beginTimeZoneInfo, TimeSpan endTime, TimeZoneInfo endTimeZoneInfo, string description = "")
        {
            return new SessionDescriptor
            {
                BeginSessionTime = TradingTime.CreateCustomSessionTime(beginTime, beginTimeZoneInfo, description == "" ? "Custom TradingSessionInfo - Open Time" : description + " - Open"),
                EndSessionTime = TradingTime.CreateCustomSessionTime(endTime, endTimeZoneInfo, description == "" ? "Custom TradingSessionInfo - Close Time" : description + " - Close"),
                Description = description,
                SessionType = SessionCode.Custom,
            };
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the initial <see cref="DateTime"/> of the <see cref="SessionDescriptor"/>.
        /// </summary>
        /// <returns>The begin <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetBeginTime(DateTime time)
        {
            return BeginSessionTime.GetTime(time);
        }

        /// <summary>
        /// Gets the begin <see cref="DateTime"/> structure of the <see cref="SessionDescriptor"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <returns>The begin <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetBeginTime(DateTime time, TimeZoneInfo sourceTimeZoneInfo)
        {
            return BeginSessionTime.GetTime(time, sourceTimeZoneInfo);
        }

        /// <summary>
        /// Gets the begin <see cref="DateTime"/> structure of the <see cref="SessionDescriptor"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <param name="destinationTimeZoneInfo">The <see cref="TimeZoneInfo"/> to convert the date time structure.</param>
        /// <returns>The begin <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetBeginTime(
            DateTime time,
            TimeZoneInfo sourceTimeZoneInfo,
            TimeZoneInfo destinationTimeZoneInfo)
        {
            return BeginSessionTime.GetTime(time, sourceTimeZoneInfo, destinationTimeZoneInfo);
        }

        /// <summary>
        /// Gets the end <see cref="DateTime"/> structure of the <see cref="SessionDescriptor"/>.
        /// </summary>
        /// <returns>The end <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetEndTime(DateTime time)
        {
            return EndSessionTime.GetTime(time);
        }

        /// <summary>
        /// Gets the end <see cref="DateTime"/> structure of the <see cref="SessionDescriptor"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <returns>The end <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetEndTime(DateTime time, TimeZoneInfo sourceTimeZoneInfo)
        {
            return EndSessionTime.GetTime(time, sourceTimeZoneInfo);
        }

        /// <summary>
        /// Gets the end <see cref="DateTime"/> structure of the <see cref="SessionDescriptor"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <param name="destinationTimeZoneInfo">The <see cref="TimeZoneInfo"/> to convert the date time structure.</param>
        /// <returns>The end <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetEndTime(
            DateTime time,
            TimeZoneInfo sourceTimeZoneInfo,
            TimeZoneInfo destinationTimeZoneInfo)
        {
            return EndSessionTime.GetTime(time, sourceTimeZoneInfo, destinationTimeZoneInfo);
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
        /// Gets the final <see cref="DateTime"/> structure of the <see cref="SessionDescriptor"/>.
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
        /// Gets the final <see cref="DateTime"/> structure of the <see cref="SessionDescriptor"/>.
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
        /// Compare <see cref="SessionDescriptor"/> objects and return true if the elements are equals.
        /// The <see cref="SessionDescriptor"/> objects are equals if the times and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is SessionDescriptor descriptor)
                return BeginSessionTime.Equals(descriptor.BeginSessionTime) && EndSessionTime.Equals(descriptor.EndSessionTime);

            return false;
        }

        /// <summary>
        /// Compare <see cref="SessionDescriptor"/> objects and return true if the elements are equals.
        /// the <see cref="SessionDescriptor"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="value">The <see cref="SessionDescriptor"/> to compare with the instance.</param>
        /// <returns>True if the pair of <see cref="SessionDescriptor"/> are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="SessionDescriptor"/>object passed as parameter cannot be null.</exception>
        public bool Equals(SessionDescriptor descriptor)
        {

            if (descriptor is null)
                return false;

            return BeginSessionTime.Equals(descriptor.BeginSessionTime) && EndSessionTime.Equals(descriptor.EndSessionTime);

        }

        /// <summary>
        /// Compare <see cref="SessionDescriptor"/> objects and return true if the elements are equals.
        /// the <see cref="SessionDescriptor"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="d1">The first <see cref="SessionDescriptor"/> object to compare with the second.</param>
        /// <param name="d2">The second <see cref="SessionDescriptor"/> object to compare with the first.</param>
        /// <returns>True if <see cref="SessionDescriptor"/> objects are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="SessionDescriptor"/>objects passed as parameter cannot be null.</exception>
        public static bool Equals(SessionDescriptor d1, SessionDescriptor d2)
        {

            if (d1 is null && d2 is null)
                return true;

            if (d1 is null || d2 is null)
                return false;

            return TradingTime.Equals(d1.BeginSessionTime, d2.BeginSessionTime) && TradingTime.Equals(d1.EndSessionTime, d2.EndSessionTime);

        }

        #endregion

        #region Compare methods

        /// <summary>
        /// Compare <paramref name="value1"/> and <paramref name="value2"/> objects and 
        /// return 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// return -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value1">The first object to compare with the second.</param>
        /// <param name="value2">The second object to compare with the first.</param>
        /// <returns>1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// 0 if the objects are equals.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="SessionDescriptor"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="SessionDescriptor"/> objects.</exception>
        public int Compare(object value1, object value2)
        {
            if (!(value1 is null && value2 is null))
            {
                if (value1 is SessionDescriptor d1)
                {
                    if (value2 is SessionDescriptor d2)
                    {
                        return Compare(d1, d2);
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
        /// Compare <see cref="SessionDescriptor"/> objects and 
        /// return 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// return -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value1">The first <see cref="SessionDescriptor"/> object to compare with the second.</param>
        /// <param name="value2">The second <see cref="SessionDescriptor"/> object to compare with the first.</param>
        /// <returns>1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// 0 if the objects are equals.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="SessionDescriptor"/>objects passed as parameter cannot be null.</exception>
        public int Compare(SessionDescriptor value1, SessionDescriptor value2)
        {
            if (value1 == null || value2 == null)
                throw new ArgumentNullException("The arguments cannot be null.");

            return value1 < value2 ? -1 : value1 > value2 ? 1 : 0;

        }

        /// <summary>
        /// Compare the <see cref="SessionDescriptor"/> to <paramref name="value"/> object and 
        /// return 1 if <see cref="SessionDescriptor"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="SessionDescriptor"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="SessionDescriptor"/>.</param>
        /// <returns>1 if <see cref="TradingSession"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="TradingSession"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="SessionDescriptor"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="SessionDescriptor"/> objects.</exception>
        public int CompareTo(object value)
        {
            if (!(value is null))
            {
                if (value is SessionDescriptor d)
                {
                    return Compare(this, d);
                }
                else
                    throw new ArgumentException(nameof(value));
            }

            throw new ArgumentNullException("The value cannot be null.");
        }

        /// <summary>
        /// Compare the <see cref="SessionDescriptor"/> to <see cref="SessionDescriptor"/> object and 
        /// return 1 if <see cref="SessionDescriptor"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="SessionDescriptor"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="SessionDescriptor"/>.</param>
        /// <returns>1 if <see cref="SessionDescriptor"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="SessionDescriptor"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="SessionDescriptor"/>objects passed as parameter cannot be null.</exception>
        public int CompareTo(SessionDescriptor value)
        {
            if (value == null)
                throw new ArgumentException("Argument cannot be null");

            return Compare(this, value);
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
        /// <exception cref="ArgumentNullException">The <see cref="SessionDescriptor"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="SessionDescriptor"/> objects.</exception>
        public SessionCompareResult CompareSession(object value1, object value2)
        {
            if (!(value1 is null && value2 is null))
            {
                if (value1 is SessionDescriptor d1)
                {
                    if (value2 is SessionDescriptor d2)
                    {
                        return CompareSession(d1, d2);
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
        /// <param name="value1">The first <see cref="SessionDescriptor"/> object to compare with the second.</param>
        /// <param name="value2">The second <see cref="SessionDescriptor"/> object to compare with the first.</param>
        /// <returns>3 if <paramref name="value1"/> is the parent of <paramref name="value2"/>, 
        /// -3 if <paramref name="value1"/>is the child of the <paramref name="value2"/>,
        /// 2 if <paramref name="value1"/>is major and inner on the <paramref name="value2"/>,
        /// -2 if <paramref name="value1"/>is minor and inner on the <paramref name="value2"/>,
        /// 1 if <paramref name="value1"/>is greater than <paramref name="value2"/>,
        /// -1 if <paramref name="value1"/>is minor than <paramref name="value2"/>,
        /// 0 if the objects are equals.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="SessionDescriptor"/>objects passed as parameter cannot be null.</exception>
        public SessionCompareResult CompareSession(SessionDescriptor value1, SessionDescriptor value2)
        {
            if (value1 == null || value2 == null)
                throw new ArgumentNullException("The arguments cannot be null.");

            return CompareSessions(value1,value2);

        }

        /// <summary>
        /// Compare the <see cref="SessionDescriptor"/> to <paramref name="value"/> object and 
        /// return 3 if <see cref="SessionDescriptor"/> is the parent of the <paramref name="value"/>, 
        /// return -3 if <see cref="SessionDescriptor"/> is the parent,
        /// return 2 if <see cref="SessionDescriptor"/> is major and inner on the <paramref name="value"/>,
        /// return -2 if <see cref="SessionDescriptor"/> is minor and inner on the <paramref name="value"/>,
        /// return 1 if <see cref="SessionDescriptor"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="SessionDescriptor"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="SessionDescriptor"/>.</param>
        /// <returns>3 if <see cref="SessionDescriptor"/> is the parent of the <paramref name="value"/>, 
        /// -3 if <see cref="SessionDescriptor"/> is the parent,
        /// 2 if <see cref="SessionDescriptor"/> is major and inner on the <paramref name="value"/>,
        /// -2 if <see cref="SessionDescriptor"/> is minor and inner on the <paramref name="value"/>,
        /// 1 if <see cref="SessionDescriptor"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="SessionDescriptor"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="SessionDescriptor"/>objects passed as parameter cannot be null.</exception>
        /// <exception cref="ArgumentException">The objects passed as parameter must be <see cref="SessionDescriptor"/> objects.</exception>
        public SessionCompareResult CompareSessionTo(object value)
        {
            if (!(value is null))
            {
                if (value is SessionDescriptor ts)
                {
                    return CompareSession(this, ts);
                }
                else
                    throw new ArgumentException(nameof(value));
            }

            throw new ArgumentNullException("The value cannot be null.");
        }

        /// <summary>
        /// Compare the <see cref="SessionDescriptor"/> to <see cref="SessionDescriptor"/> object and 
        /// return 3 if <see cref="SessionDescriptor"/> is the parent of the <paramref name="value"/>, 
        /// return -3 if <see cref="SessionDescriptor"/> is the parent,
        /// return 2 if <see cref="SessionDescriptor"/> is major and inner on the <paramref name="value"/>,
        /// return -2 if <see cref="SessionDescriptor"/> is minor and inner on the <paramref name="value"/>,
        /// return 1 if <see cref="SessionDescriptor"/> is greater than <paramref name="value"/>,
        /// return -1 if <see cref="SessionDescriptor"/> is minor than <paramref name="value"/>,
        /// return 0 if the objects are equals.
        /// </summary>
        /// <param name="value">The object to compare with the <see cref="SessionDescriptor"/>.</param>
        /// <returns>3 if <see cref="SessionDescriptor"/> is the parent of the <paramref name="value"/>, 
        /// -3 if <see cref="SessionDescriptor"/> is the parent,
        /// 2 if <see cref="SessionDescriptor"/> is major and inner on the <paramref name="value"/>,
        /// -2 if <see cref="SessionDescriptor"/> is minor and inner on the <paramref name="value"/>,
        /// 1 if <see cref="SessionDescriptor"/> is greater than <paramref name="value"/>,
        /// -1 if <see cref="SessionDescriptor"/> is minor than <paramref name="value"/>,
        /// 0 if the objects are equals.
        /// <exception cref="ArgumentNullException">The <see cref="SessionDescriptor"/>objects passed as parameter cannot be null.</exception>
        public SessionCompareResult CompareSessionTo(SessionDescriptor value)
        {
            if (value == null)
                throw new ArgumentException("Argument cannot be null");

            return CompareSession(this, value);
        }

        #endregion

        #region Opertator methods

        /// <summary>
        /// Determines whether two specified instances of <see cref="SessionDescriptor"/> that is greater than another specified.
        /// </summary>
        /// <param name="d1">The first object to compare.</param>
        /// <param name="d2">The second object to compare.</param>
        /// <returns>True if <paramref name="d1"/> is greater than <paramref name="d2"/>; otherwise, false.</returns>
        public static bool operator >(SessionDescriptor d1, SessionDescriptor d2)
        {
            if (d1 is null)
                throw new ArgumentNullException($"the argument {nameof(d1)} cannot be null.");

            if (d2 is null)
                throw new ArgumentNullException($"the argument {nameof(d2)} cannot be null.");

            SessionCompareResult result = CompareSessions(d1, d2);
            if (
                result == SessionCompareResult.Later ||
                result == SessionCompareResult.InnerAndLater
               )
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="SessionDescriptor"/> that is earlier than another specified.
        /// </summary>
        /// <param name="d1">The first object to compare.</param>
        /// <param name="d2">The second object to compare.</param>
        /// <returns>True if <paramref name="d1"/> is less than <paramref name="d2"/>; otherwise, false.</returns>
        public static bool operator <(SessionDescriptor d1, SessionDescriptor d2)
        {
            if (d1 is null)
                throw new ArgumentNullException($"the argument {nameof(d1)} cannot be null.");

            if (d2 is null)
                throw new ArgumentNullException($"the argument {nameof(d2)} cannot be null.");

            SessionCompareResult result = CompareSessions(d1, d2);
            if (
                result == SessionCompareResult.Before ||
                result == SessionCompareResult.BeforeAndInner
               )
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="SessionDescriptor"/> that is the parent of another specified.
        /// </summary>
        /// <param name="d1">The first object to compare.</param>
        /// <param name="d2">The second object to compare.</param>
        /// <returns>True if <paramref name="d1"/> is parent of <paramref name="d2"/>; otherwise, false.</returns>
        public static bool operator >=(SessionDescriptor d1, SessionDescriptor d2)
        {
            if (d1 is null)
                throw new ArgumentNullException($"the argument {nameof(d1)} cannot be null.");

            if (d2 is null)
                throw new ArgumentNullException($"the argument {nameof(d2)} cannot be null.");

            SessionCompareResult result = CompareSessions(d1, d2);
            if (
                result == SessionCompareResult.Equals ||
                result == SessionCompareResult.Later ||
                result == SessionCompareResult.InnerAndLater
               )
                return true;

            return false;
        }
        /// <summary>
        /// Determines whether two specified instances of <see cref="SessionDescriptor"/> that is the child of another specified.
        /// </summary>
        /// <param name="d1">The first object to compare.</param>
        /// <param name="d2">The second object to compare.</param>
        /// <returns>True if <paramref name="d1"/> is the child of <paramref name="d2"/>; otherwise, false.</returns>
        public static bool operator <=(SessionDescriptor d1, SessionDescriptor d2)
        {

            if (d1 is null)
                throw new ArgumentNullException($"the argument {nameof(d1)} cannot be null.");

            if (d2 is null)
                throw new ArgumentNullException($"the argument {nameof(d2)} cannot be null.");

            SessionCompareResult result = CompareSessions(d1, d2);
            if (
                result == SessionCompareResult.Equals ||
                result == SessionCompareResult.Before ||
                result == SessionCompareResult.BeforeAndInner
               )
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="SessionDescriptor"/> have the same <see cref="Time"/>.
        /// </summary>
        /// <param name="d1">The first object to compare.</param>
        /// <param name="d2">The second object to compare.</param>
        /// <returns>True if <paramref name="d1"/> and <paramref name="d2"/> represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator ==(SessionDescriptor d1, SessionDescriptor d2)
        {
            if (d1 is null && d2 is null)
                return true;

            if (d1 is null || d2 is null)
                return false;

            SessionCompareResult result = CompareSessions(d1, d2);
            if (result == SessionCompareResult.Equals)
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="TradingSession"/> haven't the same <see cref="Time"/>.
        /// </summary>
        /// <param name="d1">The first object to compare.</param>
        /// <param name="d2">The second object to compare.</param>
        /// <returns>True if <paramref name="d1"/> and <paramref name="d2"/> do not represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator !=(SessionDescriptor d1, SessionDescriptor d2)
        {
            return !(d1 == d2);
        }

        /// <summary>
        /// Adds a specified session time to a specified session time, generating a new time span.
        /// </summary>
        /// <param name="d1">TradingSessionInfo time value to add.</param>
        /// <param name="d2">TradingSessionInfo time value to add.</param>
        /// <returns><see cref="TimeSpan"/> that is the sum of the values ​​of <paramref name="d1"/> and <paramref name="d2"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator +(SessionDescriptor d1, SessionDescriptor d2)
        {
            if (d1 is null)
                throw new ArgumentNullException($"the argument {nameof(d1)} cannot be null.");

            if (d2 is null)
                throw new ArgumentNullException($"the argument {nameof(d2)} cannot be null.");

            return d1.BeginSessionTime.UtcTime + d2.EndSessionTime.UtcTime;
        }

        /// <summary>
        /// Adds a specified session time to a specified time span, generating a new time span.
        /// </summary>
        /// <param name="d"><see cref="SessionDescriptor"/> object to add.</param>
        /// <param name="t">Time span value to add.</param>
        /// <returns><see cref="TimeSpan"/> that is the sum of the values ​​of <paramref name="d"/> and <paramref name="t"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator +(SessionDescriptor d, TimeSpan t)
        {
            if (d is null)
                throw new ArgumentNullException($"the argument {nameof(d)} cannot be null.");

            return new TimeSpan((d.EndSessionTime.UtcTime + t).Ticks);
        }

        /// <summary>
        /// Subtracts a specified session time from a specified session time value and returns a newtime span.
        /// </summary>
        /// <param name="d1"><see cref="SessionDescriptor"/> object to substract.</param>
        /// <param name="d2"><see cref="SessionDescriptor"/> object to substract.</param>
        /// <returns>An <see cref="TimeSpan"/> whose value is the value of <paramref name="d1"/> minus the value of <paramref name="d2"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator -(SessionDescriptor d1, SessionDescriptor d2)
        {
            if (d1 is null)
                throw new ArgumentNullException($"the argument {nameof(d1)} cannot be null.");

            if (d2 is null)
                throw new ArgumentNullException($"the argument {nameof(d2)} cannot be null.");

            return new TimeSpan((d1.EndSessionTime.UtcTime - d2.BeginSessionTime.UtcTime).Ticks);
        }

        /// <summary>
        /// Subtracts a specified time span from a specified session time value and returns a newtime span.
        /// </summary>
        /// <param name="d"><see cref="SessionDescriptor"/> object to add.</param>
        /// <param name="t"><see cref="TimeSpan"/> object to add.</param>
        /// <returns>An <see cref="TimeSpan"/> whose value is the value of <paramref name="d"/> minus the value of <paramref name="t"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator -(SessionDescriptor d, TimeSpan t)
        {
            if (d is null)
                throw new ArgumentNullException($"the argument {nameof(d)} cannot be null.");

            return new TimeSpan((d.BeginSessionTime.UtcTime - t).Ticks);
        }

        #endregion

        #region ToString methods

        /// <summary>
        /// Converts the <see cref="TradingSession"/> to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => ToString("U");

        /// <summary>
        /// Returns the string that represents the <see cref="SessionDescriptor"/>.
        /// </summary>
        /// <param name="format">The specific time to convert. The time can be Utc, Local or Unspecific.</param>
        /// <returns></returns>
        public string ToString(string format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            string f = format.ToUpper();
            bool showTimeZoneInfo = true;

            if (f == "U" || f == "UTC" || f == "L" || f == "LOCAL")
                showTimeZoneInfo = false;
            else if (BeginSessionTime.TimeZoneInfo == EndSessionTime.TimeZoneInfo)
                showTimeZoneInfo = false;

            return $"{SessionType.ToName()} - Begin: {BeginSessionTime.ToShortString(format, showTimeZoneInfo)} - End: {EndSessionTime.ToShortString(format, true)}";
        }

        /// <summary>
        /// Converts the <see cref="SessionDescriptor"/> to short string.
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
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            string f = format.ToUpper();
            bool showTimeZoneInfo = true;

            if (f == "U" || f == "UTC" || f == "L" || f == "LOCAL")
                showTimeZoneInfo = false;
            else if (BeginSessionTime.TimeZoneInfo == EndSessionTime.TimeZoneInfo)
                showTimeZoneInfo = false;

            return $"{SessionType.ToName()} - Begin: {BeginSessionTime.ToShortString(format, showTimeZoneInfo)} - End: {EndSessionTime.ToShortString(format, true)}";

        }

        /// <summary>
        /// Converts the <see cref="SessionDescriptor"/> to long string.
        /// </summary>
        /// <returns></returns>
        public string ToLongString() => ToLongString("U");

        /// <summary>
        /// Returns the string that represents the <see cref="Time"/> of the <see cref="TradingTime"/>.
        /// </summary>
        /// <param name="format">The specific time to convert. The time can be Utc, Local or Unspecific.</param>
        /// <returns></returns>
        public string ToLongString(string format)
        {
            if (format == null)
                throw new ArgumentNullException(nameof(format));

            string f = format.ToUpper();
            bool showTimeZoneInfo = true;

            if (f == "U" || f == "UTC" || f == "L" || f == "LOCAL")
                showTimeZoneInfo = false;
            else if (BeginSessionTime.TimeZoneInfo == EndSessionTime.TimeZoneInfo)
                showTimeZoneInfo = false;

            return $"{SessionType.ToDescription()} - Begin: {BeginSessionTime.ToShortString(format, showTimeZoneInfo)} - End: {EndSessionTime.ToShortString(format, true)}";
        }

        /// <summary>
        /// Converts the <see cref="SessionDescriptor"/> to string.
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
        /// Converts the <see cref="SessionDescriptor"/> to string.
        /// </summary>
        /// <returns></returns>
        public string ToString(DateTime referenceDateTime)
        {
            DateTime[] sessionDateTimes = GetNextDateTimes(referenceDateTime);
            return String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", sessionDateTimes[0].ToString(), "End Time: ", sessionDateTimes[1].ToString());
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

        private static SessionCompareResult CompareSessions(SessionDescriptor d1, SessionDescriptor d2)
        {
            if (
                d1.BeginSessionTime == d2.BeginSessionTime &&
                d1.EndSessionTime == d2.EndSessionTime
                )
                // Returns 0
                return SessionCompareResult.Equals;
            else if (
                d1.BeginSessionTime >= d2.BeginSessionTime &&
                d1.EndSessionTime <= d2.EndSessionTime
                )
                // Returns -3
                return SessionCompareResult.Inner;
            else if (
                d1.BeginSessionTime <= d2.BeginSessionTime &&
                d1.EndSessionTime >= d2.EndSessionTime
                )
                // Returns 3
                return SessionCompareResult.Outer;
            else if (
                d1.BeginSessionTime < d2.BeginSessionTime &&
                d1.EndSessionTime <= d2.BeginSessionTime
                )
                // Returns -2
                return SessionCompareResult.Before;
            else if (
                d1.BeginSessionTime >= d2.EndSessionTime &&
                d1.EndSessionTime > d2.EndSessionTime
                )
                // Returns 2
                return SessionCompareResult.Later;
            else if (
                d1.BeginSessionTime < d2.BeginSessionTime &&
                d1.EndSessionTime > d2.BeginSessionTime &&
                d1.EndSessionTime <= d2.EndSessionTime
                )
                // Returns -1
                return SessionCompareResult.BeforeAndInner;
            else if (
                d1.BeginSessionTime >= d2.BeginSessionTime &&
                d1.BeginSessionTime < d2.EndSessionTime &&
                d1.EndSessionTime > d2.EndSessionTime
                )
                // Returns 1
                return SessionCompareResult.InnerAndLater;

            // Alert an calculate error
            Debug.Assert(false);
            return SessionCompareResult.Later;
        }

        #endregion
    }
}
