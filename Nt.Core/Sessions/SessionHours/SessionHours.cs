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

        /// <summary>
        /// Indicates if the <see cref="SessionHours"/> has sessions.
        /// </summary>
        public bool HasSessions => Sessions == null ? false : Sessions.Count < 1 ? false : true;

        public DateTime CurrentTime { get; set; } = DateTime.MinValue;

        public DateTime NextBeginDateTime { get; set; } = DateTime.MinValue;

        public DateTime NextEndDateTime { get; set; } = DateTime.MinValue;
        
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

        #region Handler methods

        public bool IsInSession(DateTime time)
        {
            CurrentTime = time;

            if (CurrentTime > NextBeginDateTime)
            {
                if (CurrentTime > NextEndDateTime)
                {
                    NextBeginDateTime = GetNextBeginDateTime(CurrentTime);
                    NextEndDateTime = GetNextEndDateTime(CurrentTime);
                    return true;
                }
            }
            return false;
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

        public static SessionHours CreateMundialSessions(InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            SessionHours sessionHours = CreateSessionHoursByType(TradingSession.Electronic);
            // Main Sessions
            sessionHours.Add(CreateSessionHoursByType(TradingSession.Regular));
            sessionHours.Add(CreateSessionHoursByType(TradingSession.OVN));
            // Regular Sessions
            sessionHours.Sessions[0].Add(CreateSessionHoursByType(TradingSession.AmericanAndEuropean));
            sessionHours.Sessions[0].Add(CreateSessionHoursByType(TradingSession.American));
            // Overnight Sessions
            sessionHours.Sessions[1].Add(CreateSessionHoursByType(TradingSession.American_RS));
            sessionHours.Sessions[1].Add(CreateSessionHoursByType(TradingSession.Asian));
            sessionHours.Sessions[1].Add(CreateSessionHoursByType(TradingSession.Asian_RS));
            sessionHours.Sessions[1].Add(CreateSessionHoursByType(TradingSession.European));
            // Minor Sessions
            sessionHours.Sessions[1].Sessions[0].Add(CreateSessionHoursByType(TradingSession.American_RS_EXT));
            sessionHours.Sessions[1].Sessions[0].Add(CreateSessionHoursByType(TradingSession.American_RS_EOD));
            sessionHours.Sessions[1].Sessions[0].Add(CreateSessionHoursByType(TradingSession.American_RS_NWD));

            return sessionHours;
        }

        #endregion

        #region Getter Methods

        public SessionHours GetEuropeanSession()
        {
            return CreateSessionHoursByType(TradingSession.European);
        }

        public SessionHours GetAsianSession()
        {
            return CreateSessionHoursByType(TradingSession.Asian);
        }

        public SessionHours GetAmericanSession()
        {
            return CreateSessionHoursByType(TradingSession.American);
        }

        public List<SessionHours> GetAmericanSessions()
        {
            List<SessionHours> sessions = new List<SessionHours>
            {
                CreateSessionHoursByType(TradingSession.AmericanAndEuropean),
                CreateSessionHoursByType(TradingSession.American),
            };

            return sessions;
        }

        public List<SessionHours> GetAmericanResidualSessions()
        {
            List<SessionHours> sessions = new List<SessionHours>
            {
                CreateSessionHoursByType(TradingSession.American_RS_EXT),
                CreateSessionHoursByType(TradingSession.American_RS_EXT),
                CreateSessionHoursByType(TradingSession.American_RS_NWD)
            };

            return sessions;
        }

        #endregion

        #region Session Time Methods

        /// <summary>
        /// Gets the begin <see cref="DateTime"/> structure of the <see cref="SessionHours"/>.
        /// </summary>
        /// <param name="sourceTimeZoneInfo">The <see cref="TimeZoneInfo"/> that represents <paramref name="currentTime"/>"/></param>
        /// <param name="destinationTimeZoneInfo">The <see cref="TimeZoneInfo"/> to convert the date time structure.</param>
        /// <returns>The begin <see cref="DateTime"/> structure of the next session since the <paramref name="currentTime"/></returns>
        public DateTime GetNextBeginDateTime(
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
        public DateTime GetNextEndDateTime(
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
        public DateTime[] GetNextDateTimes(
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

        #region Session Collection methods

        public void Add(SessionHours sessionHours)
        {
            if (sessionHours == null)
                throw new ArgumentNullException(nameof(sessionHours));

            if (Sessions == null)
                Sessions = new List<SessionHours>();

            Sessions.Add(sessionHours);
        }

        public void Remove(SessionHours sessionHours)
        {
            if (sessionHours == null)
                throw new ArgumentNullException(nameof(sessionHours));

            if (Sessions == null)
                throw new ArgumentNullException(nameof(Sessions));

            Sessions.Remove(sessionHours);

            if (Sessions.Count == 0)
                Sessions = null;

        }

        public void Clear()
        {
            Sessions.Clear();

            Sessions = null;
        }

        #endregion

        #region Helper methods

        public void Iterator(Action<SessionHours> action = null)
        {
            action(this);
            if (HasSessions)
                for (int i=0; i < Sessions.Count; i++)
                    Sessions[i].Iterator(action);
        }

        #endregion

        #region ToString methods

        /// <summary>
        /// Converts the <see cref="SessionHours"/> to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            DateTime[] sessionDateTimes = GetNextDateTimes(DateTime.Now);

            string sessions = String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", sessionDateTimes[0].ToString(), "End Time: ", sessionDateTimes[1].ToString());

            if (HasSessions)
                for (int i = 0; i < Sessions.Count; i++)
                    sessions += Environment.NewLine + Sessions[i].ToString();

            return sessions;
        }

        /// <summary>
        /// Converts the <see cref="SessionHours"/> to string.
        /// </summary>
        /// <returns></returns>
        public string ToString(bool onlyActualSession = true)
        {
            DateTime[] sessionDateTimes = GetNextDateTimes(DateTime.Now);
            string sessions = String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", sessionDateTimes[0].ToString(), "End Time: ", sessionDateTimes[1].ToString());
            if (!onlyActualSession)
            {
                if (HasSessions)
                    for (int i = 0; i < Sessions.Count; i++)
                        sessions += Environment.NewLine + Sessions[i].ToString(onlyActualSession);
            }

            return sessions;
        }

        /// <summary>
        /// Converts the <see cref="SessionHours"/> to string.
        /// </summary>
        /// <returns></returns>
        public string ToString(DateTime referenceDateTime)
        {
            DateTime[] sessionDateTimes = GetNextDateTimes(referenceDateTime);
            return String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", sessionDateTimes[0].ToString(), "End Time: ", sessionDateTimes[1].ToString());
        }

        #endregion

    }
}
