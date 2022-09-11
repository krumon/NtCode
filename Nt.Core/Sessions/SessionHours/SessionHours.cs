using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents the SessionHours of the day trading.
    /// </summary>
    public class SessionHours : BaseSession
    {

        #region Private members

        /// <summary>
        /// Represents the generic sessionHoursList configure.
        /// </summary>
        private readonly GenericSessionsConfigure genericConfigure;

        /// <summary>
        /// Represents the custom sessionHoursList configure.
        /// </summary>
        private readonly CustomSessionsConfigure customConfigure;

        /// <summary>
        /// Store the sessionHoursList sessionHoursList.
        /// </summary>
        private List<TradingSessionInfo> sessions;

        #endregion

        #region Public properties

        /// <summary>
        /// The <see cref="DateTime"/> object of the actual ninjatrader session begin.
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// The <see cref="DateTime"/> object of the actual ninjatrader session end.
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Collection of <see cref="TradingSessionInfo"/>.
        /// </summary>
        public List<TradingSessionInfo> Sessions 
        {
            get 
            {
                if (sessions == null) 
                    sessions = new List<TradingSessionInfo>();

                return sessions;
            }
        }

        /// <summary>
        /// Indicates if the <see cref="SessionHours"/> has sessionHoursList.
        /// </summary>
        public bool HasSessions => Sessions != null && Sessions.Count >= 1;

        /// <summary>
        /// Indicates if the trading hours is a partial partialHoliday.
        /// </summary>
        public bool IsPartialHoliday { get; private set; } // => PartialHoliday != null; // {get; private set;}

        /// <summary>
        /// Indicates if the partial partialHoliday has a late begin time.
        /// </summary>
        public bool IsLateBegin { get; private set; } //=> IsPartialHoliday && PartialHoliday.IsLateBegin;

        /// <summary>
        /// Indicates if the partial partialHoliday has a early end.
        /// </summary>
        public bool IsEarlyEnd { get; private set; } //=> IsPartialHoliday && PartialHoliday.IsEarlyEnd;

        /// <summary>
        /// The number of main sessionHoursList stored.
        /// </summary>
        public int N { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Create a default instance of <see cref="SessionHours"/>.
        /// </summary>
        public SessionHours()
        {
        }

        /// <summary>
        /// Create a default instance of <see cref="SessionHours"/> with configure parameters.
        /// </summary>
        /// <param name="genericConfigure">The generic sessionHoursList configure object.</param>
        /// <param name="customConfigure">The custom sessionHoursList configure object.</param>
        public SessionHours(GenericSessionsConfigure genericConfigure, CustomSessionsConfigure customConfigure)
        {
            this.genericConfigure = genericConfigure;
            this.customConfigure = customConfigure;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// If the trading hours is in partial partialHoliday, gets the Partial Holiday object, otherwise, partial partialHoliday is null.
        /// </summary>
        /// <param name="e"></param>
        public void Load(SessionChangedEventArgs e)
        {
            Idx = e.Idx;
            BeginTime = e.NewSessionBeginTime;
            EndTime = e.NewSessionEndTime;
            IsPartialHoliday = e.IsPartialHoliday;
            IsEarlyEnd = e.IsEarlyEnd;
            IsLateBegin = e.IsLateBegin;

            // TODO: Add Generic SessionHours existing in the configuration object. (American, Assian, Custom,...)
            //       Add Custom SessionHours existing in the configuration object. 
        }

        public void AddSession( 
            TradingSession sessionType,
             InstrumentCode instrumentCode = InstrumentCode.Default,
             int includeInitialBalance = 0,
             int includeFinalBalance = 0)
        {

            if (Sessions == null)
                sessions = new List<TradingSessionInfo>();

            Add(sessionType.ToSessionHours(instrumentCode));
        }

        public void Remove(TradingSessionInfo session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            if (Sessions == null)
                throw new ArgumentNullException(nameof(SessionHours));

            Sessions.Remove(session);

            if (Sessions.Count == 0)
                sessions = null;

        }

        public void Clear()
        {
            Sessions.Clear();

            sessions = null;
        }

        /// <summary>
        /// Converts the <see cref="TradingSessionInfo"/> to string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string holidayText = !IsPartialHoliday ? 
                "Regular Day." : IsLateBegin ? 
                "Partial Holiday - Late Begin." : "Partial Holiday - Early End.";
            return 
                $"N: {N} " +
                $"| Begin: {BeginTime.ToShortDateString()} {BeginTime.ToLongTimeString()} " +
                $"| End: {EndTime.ToShortDateString()} {EndTime.ToLongTimeString()} " +
                $"| {holidayText}";
            
        }

        //public void Iterator(Action<TradingSessionInfo> action = null)
        //{
        //    action(this);
        //    if (HasSessionHours)
        //        for (int i = 0; i < SessionHours.Count; i++)
        //            SessionHours[i].Iterator(action);
        //}

        #endregion

        #region Market Data methods

        #endregion

        #region Private methods


        // TODO: Codificar el método "Add" para añadir sesiones conforme al enum TradingSession
        //       y organizando según queramos que se vean las sesiones.
        private void Add(TradingSessionInfo session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            //if (HasSessionHours)
            //{
            //    DateTime[] nextDateTimes = session.GetNextDateTimes(DateTime.Now);
            //    foreach (var s in SessionHours)
            //    {
            //        session.IsInnerSession(s);
            //    }
            //}
            //else
            //    SessionHours.Add(session);

            Sessions.Add(session);
        }

        #endregion

    }


}
