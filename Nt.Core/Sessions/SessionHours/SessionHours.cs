using System;
using System.Collections.Generic;

namespace NtCore
{
    /// <summary>
    /// Contents properties and methods of specific trading time zone.
    /// </summary>
    public class SessionHours
    {

        #region Private members

        private readonly TradingSession tradingSession;
        private readonly BalanceSession balanceSession;

        //private InstrumentCode instrumentCode;

        #endregion

        #region Public properties

        /// <summary>
        /// The type of the session.
        /// </summary>
        public TradingSession SessionType => tradingSession;

        /// <summary>
        /// Gets the unique code of the <see cref="SessionHours"/>.
        /// </summary>
        public string Code => tradingSession.ToCode();

        /// <summary>
        /// Gets the description of the <see cref="SessionHours"/>.
        /// </summary>
        public string Description => tradingSession.ToDescription();

        /// <summary>
        /// The initial <see cref="SessionTime"/>.
        /// </summary>
        public SessionTime BeginSessionTime { get; set; }

        /// <summary>
        /// The final <see cref="SessionTime"/>.
        /// </summary>
        public SessionTime EndSessionTime { get; set; }

        /// <summary>
        /// Collection of minor sessions in the <see cref="SessionHours"/>.
        /// </summary>
        public List<SessionHours> Sessions { get; set; }

        /// <summary>
        /// Collection of balance sessions in the <see cref="SessionHours"/>.
        /// </summary>
        public List<SessionHours> BalanceSessions { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with specific session times.
        /// </summary>
        /// <param name="beginSessionTime">The initial <see cref="SessionTime"/> of the <see cref="SessionHours"/> class.</param>
        /// <param name="endSessionTime">The final <see cref="SessionTime"/> of the <see cref="SessionHours"/> class.</param>
        private SessionHours(TradingTime beginSessionTime, TradingTime endSessionTime)
        {
            this.BeginSessionTime = SessionTime.CreateSessionTimeByType(beginSessionTime);
            this.EndSessionTime = SessionTime.CreateSessionTimeByType(endSessionTime);
        }

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="tradingSession">the <see cref="TradingSession"/> to create the <see cref="SessionHours"/> class.</param>
        private SessionHours(TradingSession tradingSession, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
        {
            this.tradingSession = tradingSession;
            this.BeginSessionTime = tradingSession.ToBeginSessionTime(instrumentCode, balanceMinutes);
            this.EndSessionTime = tradingSession.ToEndSessionTime(instrumentCode, balanceMinutes);
        }

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="balanceSession">The <see cref="BalanceSession"/> to create the <see cref="SessionHours"/> class.</param>
        private SessionHours(BalanceSession balanceSession, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
        {
            this.balanceSession = balanceSession;
            this.tradingSession = balanceSession.ToTradingSession();
            this.BeginSessionTime = balanceSession.ToBeginSessionTime(instrumentCode, balanceMinutes);
            this.EndSessionTime = balanceSession.ToEndSessionTime(instrumentCode,balanceMinutes);
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="tradingSession">the <see cref="TradingSession"/> to create the <see cref="SessionHours"/> class.</param>
        /// <returns>A new instance of <see cref="SessionHours"/> class.</returns>
        public static SessionHours CreateSessionHoursByType(TradingSession tradingSession, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
        {
            return new SessionHours(tradingSession, instrumentCode, balanceMinutes);
        }

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with <see cref="BalanceSession"/>.
        /// </summary>
        /// <param name="balanceSession">the <see cref="BalanceSession"/> to create the <see cref="SessionHours"/>.</param>
        /// <returns>A new instance of <see cref="SessionHours"/> class.</returns>
        public static SessionHours CreateSessionBalanceByType(BalanceSession balanceSession, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
        {
            return new SessionHours(balanceSession, instrumentCode, balanceMinutes);
        }

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with specific session times.
        /// </summary>
        /// <param name="beginSessionTime">The initial <see cref="SessionTime"/> of the <see cref="SessionHours"/> class.</param>
        /// <param name="endSessionTime">The final <see cref="SessionTime"/> of the <see cref="SessionHours"/> class.</param>
        /// <returns>A new instance of <see cref="SessionHours"/> class.</returns>
        public static SessionHours CreateCustomSessionHours(TradingTime beginSessionTime, TradingTime endSessionTime)
        {
            return new SessionHours(beginSessionTime, endSessionTime);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets the begin <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <param name="destinationTimeZoneInfo">The <see cref="TimeZoneInfo"/> to convert the date time structure.</param>
        /// <returns>The begin <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetSessionBeginDateTime(
            DateTime currentDate, 
            TimeZoneInfo sourceTimeZoneInfo = null, 
            TimeZoneInfo destinationTimeZoneInfo = null)
        {
            return BeginSessionTime.GetNextSessionTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);
        }

        /// <summary>
        /// Gets the final <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.
        /// </summary>
        /// <param name="currentDate">The current date time.</param>
        /// <param name="destinationTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The final <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/>.</returns>
        public DateTime GetSessionEndDateTime(
            DateTime currentDate,
            TimeZoneInfo sourceTimeZoneInfo = null,
            TimeZoneInfo destinationTimeZoneInfo = null)
        {
            DateTime beginDateTime = BeginSessionTime.GetNextSessionTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);
            DateTime endDateTime = EndSessionTime.GetNextSessionTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);

            if (endDateTime > beginDateTime)
                return EndSessionTime.GetNextSessionTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);

            return EndSessionTime.GetNextSessionTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo) + TimeSpan.FromHours(24);
        }

        /// <summary>
        /// Gets the final <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.
        /// </summary>
        /// <param name="currentDate">The current date time.</param>
        /// <param name="destinationTimeZoneInfo">The target <see cref="TimeZoneInfo"/>.</param>
        /// <returns>The initial and final <see cref="DateTime"/> structures of the next session since the <paramref name="currentTime"/>.</returns>
        public DateTime[] GetNextSessionDateTimes(
            DateTime currentDate,
            TimeZoneInfo sourceTimeZoneInfo = null,
            TimeZoneInfo destinationTimeZoneInfo = null)
        {
            DateTime[] sessionDateTimes = new DateTime[2];
            DateTime beginDateTime = BeginSessionTime.GetNextSessionTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);
            DateTime endDateTime = EndSessionTime.GetNextSessionTime(currentDate, sourceTimeZoneInfo, destinationTimeZoneInfo);
            
            if (endDateTime <= beginDateTime)
                endDateTime += TimeSpan.FromHours(24);

            sessionDateTimes[0] = beginDateTime;
            sessionDateTimes[1] = endDateTime;

            return sessionDateTimes;
        }

        #endregion

        #region ToString methods

        /// <summary>
        /// Converts the <see cref="SessionHours"/> to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            DateTime[] sessionDateTimes = GetNextSessionDateTimes(DateTime.Now);
            return String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", sessionDateTimes[0].ToString(), "End Time: ", sessionDateTimes[1].ToString());
        }

        /// <summary>
        /// Converts the <see cref="SessionHours"/> to string.
        /// </summary>
        /// <returns></returns>
        public string ToString(DateTime referenceDateTime)
        {
            DateTime[] sessionDateTimes = GetNextSessionDateTimes(referenceDateTime);
            return String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", sessionDateTimes[0].ToString(), "End Time: ", sessionDateTimes[1].ToString());
        }

        #endregion

    }
}
