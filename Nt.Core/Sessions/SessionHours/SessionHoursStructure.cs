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
        /// The number of session stored.
        /// </summary>
        public int N => sortedSessionList.Count;

        /// <summary>
        /// Collection of sessions hours.
        /// </summary>
        public List<SessionHours> Sessions => sessions;

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

        /// <summary>
        /// Returns the string with the number of the session, the begin time and the end time.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Session {N}: Begin: {currentSession.SessionBegin.ToString()} | End: {currentSession.SessionEnd.ToString()}";
        }

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

        #region Market Data methods

        ///// <summary>
        ///// Event driven method which is called whenever a bar is updated. 
        ///// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        ///// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        ///// </summary>
        //public virtual void OnBarUpdate()
        //{
        //    LastBarTimeUpdate();
        //}

        ///// <summary>
        ///// Event driven method which is called and guaranteed to be in the correct sequence 
        ///// for every change in level one market data for the underlying instrument. 
        ///// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        ///// </summary>
        //public virtual void OnMarketData()
        //{
        //    LastBarTimeUpdate();
        //}

        //#endregion

        //#region Private methods

        ///// <summary>
        ///// Method to update the last bar time.
        ///// </summary>
        //private void LastBarTimeUpdate()
        //{
        //    DateTime lastBarTimeStamp = GetLastBarSessionDate(ninjascript.Time[0]);

        //    if (lastBarTimeStamp != currentSessionEnd)
        //    {
        //        cacheDictionary = new Dictionary<int, SessionHours>();
        //        sortedDicList.Add(cacheDictionary);
        //    }

        //    currentSessionEnd = lastBarTimeStamp;
        //}

        #endregion

        #region Para revisar

        public SessionHoursStructure AddDefaultSessions(InstrumentCode instrumentCode = InstrumentCode.Default)
        {
            // TODO: Borrar. Es KrSessionHours la que tiene que tener el método AddSession.
            //       AddSession(TradingSession.Regular, instrumentCode, 0, 0);

            // Main Sessions
            Sessions[0].AddSession(TradingSession.Regular, instrumentCode, 0, 0);
            Sessions[0].AddSession(TradingSession.OVN, instrumentCode, 0, 0);
            // Regular Sessions
            Sessions[0].Sessions[0].AddSession(TradingSession.AmericanAndEuropean, instrumentCode, 0, 0);
            Sessions[0].Sessions[0].AddSession(TradingSession.American, instrumentCode, 0, 0);
            // Overnight Sessions
            Sessions[0].Sessions[1].AddSession(TradingSession.American_RS, instrumentCode, 0, 0);
            Sessions[0].Sessions[1].AddSession(TradingSession.Asian, instrumentCode, 0, 0);
            Sessions[0].Sessions[1].AddSession(TradingSession.Asian_RS, instrumentCode, 0, 0);
            Sessions[0].Sessions[1].AddSession(TradingSession.European, instrumentCode, 0, 0);
            // Minor Sessions
            Sessions[0].Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_EXT, instrumentCode, 0, 0);
            Sessions[0].Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_EOD, instrumentCode, 0, 0);
            Sessions[0].Sessions[1].Sessions[0].AddSession(TradingSession.American_RS_NWD, instrumentCode, 0, 0);

            return this;
        }

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
