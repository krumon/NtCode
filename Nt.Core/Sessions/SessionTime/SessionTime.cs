using Kr.Core;
using System;

namespace Nt.Core
{
    public class SessionTime
    {
        #region Private members

        /// <summary>
        /// The trading time type.
        /// </summary>
        private TradingTime tradingTime;

        #endregion

        #region Public properties

        /// <summary>
        /// The trading time type.
        /// </summary>
        public TradingTime TradingTime 
        {
            private set 
            { 
                tradingTime = value;

                if (tradingTime == TradingTime.Custom)
                {
                    Code = ToDefaultCode();
                    if (string.IsNullOrEmpty(Description))
                        Description = $"Custom Session Time {LocalTime.TotalHours}.";
                }
                else
                {
                    Code = tradingTime.ToCode();
                    Description = tradingTime.ToDescription();
                }
            }
            get => tradingTime;
        }

        /// <summary>
        /// Gets the unique code of the <see cref="SessionTime"/>.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the description of the <see cref="SessionTime"/>.
        /// </summary>
        public string Description {get; private set; }

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
        private SessionTime()
        {
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Create a default instance of <see cref="SessionTime"/> by specific <see cref="Nt.Core.TradingTime"/>.
        /// </summary>
        /// <param name="tradingTime">The specific session time to create the instance.</param>
        /// <param name="instrumentCode">The unique code of the financial instrument session.</param>
        /// <param name="specificTimeOffset">The offset of the <see cref="DateTime"/> in minutes.</param>
        /// <returns>The session time instance.</returns>
        public static SessionTime CreateSessionTimeByType(TradingTime tradingTime, InstrumentCode instrumentCode = InstrumentCode.Default, int specificTimeOffset = 0)
        {
            return new SessionTime 
            {
                TimeZoneInfo = tradingTime.ToTimeZoneInfo(instrumentCode),
                Time = tradingTime.ToTime(instrumentCode, specificTimeOffset),
                TradingTime = tradingTime
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
        public static SessionTime CreateCustomSessionTime(TimeSpan time, TimeZoneInfo timeZoneInfo = null, string description = "")
        {
            return new SessionTime
            {
                Time = time,
                TimeZoneInfo = timeZoneInfo,
                Description = description,
                TradingTime = TradingTime.Custom,
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
        public static SessionTime CreateCustomSessionTime(int hour, int minute, int seconds, TimeZoneInfo timeZoneInfo = null, string description = "")
        {
            return new SessionTime
            {
                Time = new TimeSpan(hour, minute, seconds),
                TimeZoneInfo = timeZoneInfo,
                Description = description,
                TradingTime = TradingTime.Custom,
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

            throw new Exception("The kind of the " + nameof(time) + " must be Local or Utc");
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

        /// <summary>
        /// Added the session time to the date passed as parameter and returns it.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>The <see cref="DateTime"/> that represents the session time.</returns>
        public DateTime ToSessionDateTime(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, Time.Hours, date.Minute, date.Second, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Indicates if <paramref name="st"/> exists in the <see cref="TradingTime"/> enum.
        /// </summary>
        /// <param name="st">Session time object to check exists.</param>
        /// <returns>True if the session time object exists in the trading time type.</returns>
        public bool Exist()
        {
            bool exist = false;
            SessionTime st = CreateSessionTimeByType(TradingTime.American_Open);
            SessionTime st_tmp;

            EnumHelpers.Iterator<TradingTime>((t) =>
            {
                if (!exist)
                {
                    if (t == TradingTime.Custom)
                        return;

                    st_tmp = CreateSessionTimeByType(t);
                    if (st.Equals(st_tmp))
                        exist = true;
                }
            });

            return exist;
        }

        /// <summary>
        /// Returns the string that represents the <see cref="Time"/> of the <see cref="SessionTime"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Code} | {Time}";
        }

        /// <summary>
        /// Returns the string that represents the <see cref="Time"/> of the <see cref="SessionTime"/>.
        /// </summary>
        /// <param name="format">The specific time to convert. The time can be Utc, Local or Unspecific.</param>
        /// <returns></returns>
        public string ToString(string format = "")
        {
            string f = format.ToUpper();

            if (f == "UTC")
                return $"{Code} | {UtcTime} Utc";

            if (f == "LOCAL")
                return $"{Code} | {LocalTime} Local";

            return $"{Code} | {Time}";
        }

        /// <summary>
        /// Returns the string that represents the <see cref="Code"/> and the <see cref="Time"/> of the <see cref="SessionTime"/>.
        /// </summary>
        /// <returns></returns>
        public string ToShortString()
        {
            return Time.ToString();
        }

        /// <summary>
        /// Returns the string that represents the <see cref="Code"/>, <see cref="Description"/>, 
        /// <see cref="Time"/> and <see cref="TimeZoneInfo"/> of the <see cref="SessionTime"/>. 
        /// </summary>
        /// <returns></returns>
        public string ToLongString()
        {
            return $"Code: {Code} | Description: {Description} | Time: {Time} | TimeZoneInfo: {TimeZoneInfo.DisplayName}";
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
        /// Compare <see cref="SessionTime"/> objects and return true if the elements are equals.
        /// the <see cref="SessionTime"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the objects are equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is SessionTime st)
                return this.Time == st.Time && this.UtcTime == st.UtcTime && this.Code == st.Code;

            return false;
        }

        /// <summary>
        /// Compare <see cref="SessionTime"/> objects and return true if the elements are equals.
        /// the <see cref="SessionTime"/> objects are equals if the <see cref="Time"/> and <see cref="TimeZoneInfo"/> are equals.
        /// </summary>
        /// <param name="value">The <see cref="SessionTime"/> to compare with the instance.</param>
        /// <returns>True if the pair of <see cref="SessionTime"/> are equals.</returns>
        /// <exception cref="ArgumentException">The <see cref="SessionTime"/>object passed as parameter cannot be null.</exception>
        public bool Equals(SessionTime st)
        {

            if (st is null)
                return false;

            return this.Time == st.Time && this.UtcTime == st.UtcTime && this.Code == st.Code;
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

            if (st1 is null && st2 is null)
                return true;

            if (st1 is null || st2 is null)
                return false;

            return st1.Time == st2.Time && st1.UtcTime == st2.UtcTime & st1.Code == st2.Code;

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
        public static int Compare(SessionTime st1, SessionTime st2)
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
                throw new ArgumentException("Argument cannot be null");
            }

            if (!(value is SessionTime))
            {
                throw new ArgumentException("Argument must be SessionTime");
            }

            TimeSpan valueUtcTime = ((SessionTime)value).UtcTime;

            if (this.UtcTime > valueUtcTime)
            {
                return 1;
            }

            if (this.UtcTime < valueUtcTime)
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
                throw new ArgumentException("Argument cannot be null");
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
        /// Determines whether two specified instances of <see cref="SessionTime"/> that is greater than another specified.
        /// </summary>
        /// <param name="st1">The first object to compare.</param>
        /// <param name="st2">The second object to compare.</param>
        /// <returns>True if <paramref name="st1"/> is greater than <paramref name="st2"/>; otherwise, false.</returns>
        public static bool operator >(SessionTime st1, SessionTime st2)
        {
            if (st1 is null)
                throw new ArgumentNullException($"the argument {nameof(st1)} cannot be null.");

            if (st2 is null)
                throw new ArgumentNullException($"the argument {nameof(st2)} cannot be null.");

            return st1.UtcTime > st2.UtcTime;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="SessionTime"/> that is earlier than another specified.
        /// </summary>
        /// <param name="st1">The first object to compare.</param>
        /// <param name="st2">The second object to compare.</param>
        /// <returns>True if <paramref name="st1"/> is less than <paramref name="st2"/>; otherwise, false.</returns>
        public static bool operator <(SessionTime st1, SessionTime st2)
        {
            if (st1 is null)
                throw new ArgumentNullException($"the argument {nameof(st1)} cannot be null.");

            if (st2 is null)
                throw new ArgumentNullException($"the argument {nameof(st2)} cannot be null.");

            return st1.UtcTime < st2.UtcTime;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="SessionTime"/> that is the same as or greater than another specified.
        /// </summary>
        /// <param name="st1">The first object to compare.</param>
        /// <param name="st2">The second object to compare.</param>
        /// <returns>True if <paramref name="st1"/> is equal to or greater than <paramref name="st2"/>; otherwise, false.</returns>
        public static bool operator >=(SessionTime st1, SessionTime st2)
        {
            if (st1 is null)
                throw new ArgumentNullException($"the argument {nameof(st1)} cannot be null.");

            if (st2 is null)
                throw new ArgumentNullException($"the argument {nameof(st2)} cannot be null.");

            return st1.UtcTime >= st2.UtcTime;
        }
        /// <summary>
        /// Determines whether two specified instances of <see cref="SessionTime"/> that is the same as or earlier than another specified.
        /// </summary>
        /// <param name="st1">The first object to compare.</param>
        /// <param name="st2">The second object to compare.</param>
        /// <returns>True if <paramref name="st1"/> is equal to or less than <paramref name="st2"/>; otherwise, false.</returns>
        public static bool operator <=(SessionTime st1, SessionTime st2)
        {

            if (st1 is null)
                throw new ArgumentNullException($"the argument {nameof(st1)} cannot be null.");

            if (st2 is null)
                throw new ArgumentNullException($"the argument {nameof(st2)} cannot be null.");

            return st1.UtcTime <= st2.UtcTime;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="SessionTime"/> have the same <see cref="Time"/>.
        /// </summary>
        /// <param name="st1">The first object to compare.</param>
        /// <param name="st2">The second object to compare.</param>
        /// <returns>True if <paramref name="st1"/> and <paramref name="st2"/> represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator ==(SessionTime st1, SessionTime st2)
        {
            if (st1 is null && st2 is null)
                return true;

            if (st1 is null || st2 is null)
                return false;

            return st1.UtcTime == st2.UtcTime;
        }

        /// <summary>
        /// Determines whether two specified instances of <see cref="SessionTime"/> haven't the same <see cref="Time"/>.
        /// </summary>
        /// <param name="st1">The first object to compare.</param>
        /// <param name="st2">The second object to compare.</param>
        /// <returns>True if <paramref name="st1"/> and <paramref name="st2"/> do not represent the same <see cref="Time"/>; otherwise, false.</returns>
        public static bool operator !=(SessionTime st1, SessionTime st2)
        {
            if (st1 is null && st2 is null)
                return false;

            if (st1 is null && !(st2 is null))
                return true;

            if (!(st1 is null) || st2 is null)
                return true;

            return st1.UtcTime != st2.UtcTime;
        }

        /// <summary>
        /// Adds a specified session time to a specified session time, generating a new time span.
        /// </summary>
        /// <param name="st1">Session time value to add.</param>
        /// <param name="st2">Session time value to add.</param>
        /// <returns><see cref="TimeSpan"/> that is the sum of the values ​​of <paramref name="st1"/> and <paramref name="st2"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator+(SessionTime st1, SessionTime st2)
        {
            if (st1 is null)
                throw new ArgumentNullException($"the argument {nameof(st1)} cannot be null.");

            if (st2 is null)
                throw new ArgumentNullException($"the argument {nameof(st2)} cannot be null.");

            return st1.UtcTime + st2.UtcTime;
        }

        /// <summary>
        /// Adds a specified session time to a specified time span, generating a new time span.
        /// </summary>
        /// <param name="st">Session time value to add.</param>
        /// <param name="ts">Time span value to add.</param>
        /// <returns><see cref="TimeSpan"/> that is the sum of the values ​​of <paramref name="st"/> and <paramref name="ts"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator+(SessionTime st, TimeSpan ts)
        {
            if (st is null)
                throw new ArgumentNullException($"the argument {nameof(st)} cannot be null.");

            return new TimeSpan((st.UtcTime + ts).Ticks);
        }

        /// <summary>
        /// Subtracts a specified session time from a specified session time value and returns a newtime span.
        /// </summary>
        /// <param name="st1">Session time value to substract.</param>
        /// <param name="st2">Session time value to substract.</param>
        /// <returns>An <see cref="TimeSpan"/> whose value is the value of <paramref name="st1"/> minus the value of <paramref name="st2"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator-(SessionTime st1, SessionTime st2)
        {
            if (st1 is null)
                throw new ArgumentNullException($"the argument {nameof(st1)} cannot be null.");

            if (st2 is null)
                throw new ArgumentNullException($"the argument {nameof(st2)} cannot be null.");

            return new TimeSpan((st1.UtcTime - st2.UtcTime).Ticks);
        }

        /// <summary>
        /// Subtracts a specified time span from a specified session time value and returns a newtime span.
        /// </summary>
        /// <param name="st">Session time value to add.</param>
        /// <param name="ts">Time span value to add.</param>
        /// <returns>An <see cref="TimeSpan"/> whose value is the value of <paramref name="st"/> minus the value of <paramref name="ts"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TimeSpan operator -(SessionTime st, TimeSpan ts)
        {
            if (st is null)
                throw new ArgumentNullException($"the argument {nameof(st)} cannot be null.");

            return new TimeSpan((st.UtcTime - ts).Ticks);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns default code for custom session times.
        /// </summary>
        /// <returns>Returns a string with the hours and minutes in specific time zone info and utc time zone info.</returns>
        private string ToDefaultCode()
        {
            return $"CTM-{Time.Hours}{Time.Minutes}-{UtcTime.Hours}{UtcTime.Minutes}";
        }

        #endregion
    }
}
