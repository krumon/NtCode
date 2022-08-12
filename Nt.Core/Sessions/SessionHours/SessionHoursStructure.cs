using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents the SessionHours Indicator Core.
    /// </summary>
    public class SessionHoursStructure : NsIndicator
    {

        #region Private members

        /// <summary>
        /// The current session.
        /// </summary>
        protected SessionHoursStructure currentSession;

        /// <summary>
        /// Session sorted list.
        /// </summary>
        protected List<SessionHoursStructure> sortedSessionList = new List<SessionHoursStructure>();

        /// <summary>
        /// Collection of sessions hours.
        /// </summary>
        protected List<SessionHours> sessions = new List<SessionHours>();

        #endregion

        #region Public properties

        /// <summary>
        /// Collection of <see cref="SessionHours"/>.
        /// </summary>
        public List<SessionHours> Sessions => sessions;

        /// <summary>
        /// Indicates if the <see cref="SessionHours"/> has sessions.
        /// </summary>
        public bool HasSessions => Sessions != null && Sessions.Count >= 1;

        /// <summary>
        /// The number of session stored.
        /// </summary>
        public int N => sortedSessionList.Count;

        /// <summary>
        /// The <see cref="SessionTime"/> object of the actual session begin.
        /// </summary>
        public SessionTime SessionBegin { get; set; }

        /// <summary>
        /// The <see cref="SessionTime"/> object of the actual session end.
        /// </summary>
        public SessionTime SessionEnd { get;set; }

        public bool IsPartialHoliday { get;set; }

        public bool IsLateBegin { get; set; }

        public bool IsEarlyEnd { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Create a default instance of <see cref="SessionHoursStructure"/>.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="sessionIterator"></param>
        public SessionHoursStructure()
        {
        }

        #endregion

        #region Instance methods


        #endregion

        #region Public methods

        public void AddSession( 
            TradingSession sessionType,
             InstrumentCode instrumentCode = InstrumentCode.Default,
             int includeInitialBalance = 0,
             int includeFinalBalance = 0)
        {

            if (Sessions == null)
                sessions = new List<SessionHours>();

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
                sessions = null;

        }

        public void Clear()
        {
            Sessions.Clear();

            sessions = null;
        }

        /// <summary>
        /// Converts the <see cref="SessionHours"/> to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //DateTime[] sessionDateTimes = GetNextDateTimes(DateTime.Now);

            //string sessions = String.Format("{0}{1,12}{2,20}{3,1}{4,20}{5,1}", "", Code, "Begin Time: ", sessionDateTimes[0].ToString(), "End Time: ", sessionDateTimes[1].ToString());

            //if (HasSessions)
            //    for (int i = 0; i < Sessions.Count; i++)
            //        sessions += Environment.NewLine + Sessions[i].ToString();

            return ""; // sessions;
        }

        //public void Iterator(Action<SessionHours> action = null)
        //{
        //    action(this);
        //    if (HasSessions)
        //        for (int i = 0; i < Sessions.Count; i++)
        //            Sessions[i].Iterator(action);
        //}

        /// <summary>
        /// Returns the string with the number of the session, the begin time and the end time.
        /// </summary>
        /// <returns></returns>
        //public override string ToString()
        //{
        //    return $"Session {N}: Begin: {currentSession.SessionBegin.ToString()} | End: {currentSession.SessionEnd.ToString()}";
        //}

        // TODO:    - Método para añadir las sesiones.
        //          - Método para saber si una sesión es menor o mayor que otra.

        /// <summary>
        /// Returns the session last bar date.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="bars"></param>
        /// <param name="platformTimeZoneInfo"></param>
        /// <returns></returns>
        //public DateTime GetLastBarSessionDate(DateTime time)
        //{
        //    if (time <= actualSessionEnd)
        //        return sessionDateTmp;

        //    if (!bars.BarsType.IsIntraday)
        //        return sessionDateTmp;

        //    sessionIterator.GetNextSession(time, true);

        //    actualSessionBegin = sessionIterator.ActualSessionBegin;
        //    actualSessionEnd = sessionIterator.ActualSessionEnd;

        //    sessionDateTmp = TimeZoneInfo.ConvertTime(actualSessionEnd.AddSeconds(-1), platformTimeZoneInfo,bars.TradingHours.TimeZoneInfo);

        //    if (newSessionBarIdx.Count == 0 ||
        //        newSessionBarIdx.Count > 0 && ninjascript.CurrentBar > newSessionBarIdx[newSessionBarIdx.Count - 1])
        //        newSessionBarIdx.Add(ninjascript.CurrentBar);

        //    return sessionDateTmp;
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

        #region Helper methods




        #endregion

        #region Para revisar

        //public SessionHoursStructure AddDefaultSessions(InstrumentCode instrumentCode = InstrumentCode.Default)
        //{
        //    // TODO: Borrar. Es KrSessionHours la que tiene que tener el método AddSession.
        //    //       AddSession(TradingSession.Regular, instrumentCode, 0, 0);

        //    // Main Sessions
        //    Sessions[0].AddSession(TradingSession.Regular, instrumentCode, 0, 0);
        //    Sessions[0].AddSession(TradingSession.OVN, instrumentCode, 0, 0);
        //    // Regular Sessions
        //    Sessions[0].Sessions[0].AddSession(TradingSession.AmericanAndEuropean, instrumentCode, 0, 0);
        //    Sessions[0].Sessions[0].AddSession(TradingSession.American, instrumentCode, 0, 0);
        //    // Overnight Sessions
        //    Sessions[0].Sessions[1].AddSession(TradingSession.American_RS, instrumentCode, 0, 0);
        //    Sessions[0].Sessions[1].AddSession(TradingSession.Asian, instrumentCode, 0, 0);
        //    Sessions[0].Sessions[1].AddSession(TradingSession.Asian_RS, instrumentCode, 0, 0);
        //    Sessions[0].Sessions[1].AddSession(TradingSession.European, instrumentCode, 0, 0);
        //    // Minor Sessions
        //    Sessions[0].Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_EXT, instrumentCode, 0, 0);
        //    Sessions[0].Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_EOD, instrumentCode, 0, 0);
        //    Sessions[0].Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_NWD, instrumentCode, 0, 0);

        //    return this;
        //}

        protected bool IsInSession(DateTime time)
        {
            CurrentTime = time;

            if (CurrentTime > NextBeginDateTime)
            {
                if (CurrentTime > NextEndDateTime)
                {
                    //NextBeginDateTime = GetNextBeginDateTime(CurrentTime);
                    //NextEndDateTime = GetNextEndDateTime(CurrentTime);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Create a new instance of <see cref="SessionHours"/> class with <see cref="TradingSession"/>.
        /// </summary>
        /// <param name="tradingSession">the <see cref="TradingSession"/> to create the <see cref="SessionHours"/> class.</param>
        /// <param name="instrumentCode">The unique code of the instrument.</param>
        /// <param name="balanceMinutes">The minutes of the balance session.</param>
        /// <returns>A new instance of <see cref="SessionHours"/> class.</returns>
        //public static KrSessionHours CreateSessionHours(TradingSession tradingSession, InstrumentCode instrumentCode = InstrumentCode.Default, int balanceMinutes = 0)
        //{
        //return new KrSessionHours
        //{
        //BeginSessionTime = tradingSession.ToBeginSessionTime(instrumentCode, balanceMinutes),
        //EndSessionTime = tradingSession.ToEndSessionTime(instrumentCode, balanceMinutes),
        //};
        //}

        public DateTime CurrentTime { get; set; } = DateTime.MinValue;

        public DateTime NextBeginDateTime { get; set; } = DateTime.MinValue;

        public DateTime NextEndDateTime { get; set; } = DateTime.MinValue;

        #endregion

    }


}
