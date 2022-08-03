using System;
using System.Collections.Generic;

namespace NtCore
{
    /// <summary>
    /// Contents properties and methods of specific trading time zone.
    /// </summary>
    public class SessionHours
    {

        #region Consts

        DateTime TIME_REFERENCE = new DateTime(1978,9,20,0,0,0,DateTimeKind.Local);

        #endregion

        #region Private members

        private TradingSession sessionType;
        private BalanceSession balanceSession;

        //private InstrumentCode instrumentCode;

        #endregion

        #region Public properties

        /// <summary>
        /// The type of the session.
        /// </summary>
        public TradingSession SessionType => sessionType;

        /// <summary>
        /// Gets the unique code of the <see cref="SessionHours"/>.
        /// </summary>
        public string Code => sessionType.ToCode();

        /// <summary>
        /// Gets the description of the <see cref="SessionHours"/>.
        /// </summary>
        public string Description => sessionType.ToDescription();

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
        public bool HasSessions => Sessions != null && Sessions.Count >= 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of <see cref="SessionHours"/> class.
        /// </summary>
        protected SessionHours()
        {
        }

        #endregion

        #region Instance methods

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="tradingSession">the <see cref="TradingSession"/> to create the <see cref="SessionHours"/> class.</param>
        /// <param name="instrumentCode">The unique code of the instrument.</param>
        /// <param name="balanceMinutes">The minutes of the balance session.</param>
        /// <returns>A new instance of <see cref="SessionHours"/> class.</returns>
        public static SessionHours CreateSessionHoursByType(TradingSession tradingSession, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
        {
            return new SessionHours
            {
                sessionType = tradingSession,
                BeginSessionTime = tradingSession.ToBeginSessionTime(instrumentCode, balanceMinutes),
                EndSessionTime = tradingSession.ToEndSessionTime(instrumentCode, balanceMinutes),
            };
        }

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with <see cref="BalanceSession"/>.
        /// </summary>
        /// <param name="balanceSession">the <see cref="BalanceSession"/> to create the <see cref="SessionHours"/>.</param>
        /// <returns>A new instance of <see cref="SessionHours"/> class.</returns>
        public static SessionHours CreateSessionBalanceByType(BalanceSession balanceSession, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
        {
            return new SessionHours
            {
                balanceSession = balanceSession,
                sessionType = balanceSession.ToTradingSession(),
                BeginSessionTime = balanceSession.ToBeginSessionTime(instrumentCode, balanceMinutes),
                EndSessionTime = balanceSession.ToEndSessionTime(instrumentCode, balanceMinutes),
            };
        }

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with specific session times.
        /// </summary>
        /// <param name="beginSessionTime">The initial <see cref="SessionTime"/> of the <see cref="SessionHours"/> class.</param>
        /// <param name="endSessionTime">The final <see cref="SessionTime"/> of the <see cref="SessionHours"/> class.</param>
        /// <returns>A new instance of <see cref="SessionHours"/> class.</returns>
        public static SessionHours CreateCustomSessionHours(TradingTime beginSessionTime, TradingTime endSessionTime)
        {
            return new SessionHours
            {
                BeginSessionTime = SessionTime.CreateSessionTimeByType(beginSessionTime),
                EndSessionTime = SessionTime.CreateSessionTimeByType(endSessionTime),
            };
        }


        #endregion

        #region Public Methods

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
            return BeginSessionTime.GetNextTime(currentDate);
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
            TimeZoneInfo destinationTimeZoneInfo = null,
            bool sessionComplete = false)
        {
            DateTime beginDateTime = BeginSessionTime.GetNextTime(currentDate);
            DateTime endDateTime = EndSessionTime.GetNextTime(currentDate);

            if (sessionComplete && (endDateTime <= beginDateTime))
                return EndSessionTime.GetNextTime(currentDate) + TimeSpan.FromHours(24);

            return EndSessionTime.GetNextTime(currentDate);
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
            TimeZoneInfo destinationTimeZoneInfo = null,
            bool sessionComplete = false)
        {
            DateTime[] sessionDateTimes = new DateTime[2];
            DateTime beginDateTime = BeginSessionTime.GetNextTime(currentDate);
            DateTime endDateTime = EndSessionTime.GetNextTime(currentDate);

            if (sessionComplete && (endDateTime <= beginDateTime))
                endDateTime += TimeSpan.FromHours(24);

            sessionDateTimes[0] = beginDateTime;
            sessionDateTimes[1] = endDateTime;

            return sessionDateTimes;
        }

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

        public SessionHours GetRegularSession()
        {
            return CreateSessionHoursByType(TradingSession.Regular);
        }

        public SessionHours GetOvernightSession()
        {
            return CreateSessionHoursByType(TradingSession.OVN);
        }

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
        
        public SessionHours GetAmericanAndEuropeanSession()
        {
            return CreateSessionHoursByType(TradingSession.AmericanAndEuropean);
        }

        public SessionHours GetAmericanResidualSession()
        {
            return CreateSessionHoursByType(TradingSession.American_RS); ;
        }

        public SessionHours GetAsianResidualSession()
        {
            return CreateSessionHoursByType(TradingSession.Asian_RS); ;
        }

        public SessionHours GetAmericanResidualExtraTimeSession()
        {
            return CreateSessionHoursByType(TradingSession.American_RS_EXT); ;
        }

        public SessionHours GetAmericanResidualEndOfDaySession()
        {
            return CreateSessionHoursByType(TradingSession.American_RS_EOD); ;
        }

        public SessionHours GetAmericanResidualNewDaySession()
        {
            return CreateSessionHoursByType(TradingSession.American_RS_NWD); ;
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

        public void AddSession(
            TradingSession sessionType, 
            InstrumentCode instrumentCode = InstrumentCode.Default, 
            int includeInitialBalance = 0, 
            int includeFinalBalance = 0)
        {

            if (Sessions == null)
                Sessions = new List<SessionHours>();

            Add(sessionType.ToSessionHours(instrumentCode, includeInitialBalance));
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

        public void Iterator(Action<SessionHours> action = null)
        {
            action(this);
            if (HasSessions)
                for (int i = 0; i < Sessions.Count; i++)
                    Sessions[i].Iterator(action);
        }

        //public bool IsBetween(DateTime currentDateTime, DateTime minorDateTime, DateTime majorDateTime)
        //{
        //    if (minorDateTime >= majorDateTime)
        //        throw new Exception("The monor date time cannot be bigger than major date time.");

        //    DateTime[] nextTimes = GetNextDateTimes(currentDateTime,null,null,true);

        //    return true;
        //}

        #endregion

        #region Private methods

        // TODO: Codificar el método "Add" para añadir sesiones conforme al enum TradingSession
        //       y organizando según queramos que se vean las sesiones.
        private void Add(SessionHours session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            //if (HasSessions)
            //{
            //    DateTime[] nextDateTimes = session.GetNextDateTimes(DateTime.Now);
            //    foreach (var s in Sessions)
            //    {
            //        session.IsInnerSession(s);
            //    }
            //}
            //else
            //    Sessions.Add(session);

            Sessions.Add(session);
        }



        #endregion

    }
}
