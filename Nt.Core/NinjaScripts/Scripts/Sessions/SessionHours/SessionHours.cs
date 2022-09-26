﻿using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents the SessionHours of the day trading.
    /// </summary>
    public class SessionHours : BaseSession<SessionHours,SessionHoursOptions,SessionHoursBuilder>
    {

        #region Private members

        /// <summary>
        /// Store the minor sessions in the main session.
        /// </summary>
        private List<TradingSessionInfo> sessions;

        /// <summary>
        /// String that contents the type of day (regular or partial holiday).
        /// </summary>
        string holidayText;

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
        /// Represents the actual session begin <see cref="DayOfWeek"/>.
        /// </summary>
        public DayOfWeek BeginDay => BeginTime.DayOfWeek;

        /// <summary>
        /// Represents the actual session end <see cref="DayOfWeek"/>.
        /// </summary>
        public DayOfWeek EndDay => EndTime.DayOfWeek;

        /// <summary>
        /// Represents the actual session <see cref="DayOfWeek"/>.
        /// The day represents the <see cref="DayOfWeek"/> of the EndTime./>
        /// </summary>
        public DayOfWeek TradingDay => EndTime.DayOfWeek;

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
        /// Indicates if the current time is in session.
        /// </summary>
        public bool IsInSession { get;private set; }

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

        #endregion

        #region State changed methods

        /// <summary>
        /// Method to set default properties
        /// This method is executed when "ConfigureProperties" methods is raised.
        /// </summary>
        /// <param name="ninjascript">The ninjascript parent object.</param>
        public override void SetDefault(NinjaScriptBase ninjascript)
        {
        }

        /// <summary>
        /// AddValues the <see cref="SessionFilters"/>.
        /// </summary>
        /// <param name="ninjascript">The ninjascript parent object.</param>
        /// <param name="bars">The bars loaded in the graphics.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            base.Load(ninjascript, bars);
        }

        /// <summary>
        /// Method used to free memory when the script is terminate.
        /// </summary>
        public override void Terminated()
        {
        }

        #endregion

        #region Market Data methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public override void OnBarUpdate()
        {
            if (ninjascript.Time[0] > BeginTime && ninjascript.Time[0] <= EndTime)
                IsInSession = true;
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public override void OnMarketData()
        {
            if (ninjascript.Time[0] > BeginTime && ninjascript.Time[0] <= EndTime)
                IsInSession = true;
        }

        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        public override void OnSessionChanged(SessionChangedEventArgs e)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// If the trading hours is in partial partialHoliday, gets the Partial Holiday object, otherwise, partial partialHoliday is null.
        /// </summary>
        /// <param name="e"></param>
        public void SetValues(SessionChangedEventArgs e)
        {
            Idx = e.Idx;
            N = e.N;
            BeginTime = e.BeginTime;
            EndTime = e.EndTime;
            IsPartialHoliday = e.IsPartialHoliday;
            IsEarlyEnd = e.IsEarlyEnd;
            IsLateBegin = e.IsLateBegin;

            holidayText = !IsPartialHoliday ? "Regular Day." : IsLateBegin ? "Partial Holiday - Late Begin." : "Partial Holiday - Early End.";

            // TODO: Add Generic SessionHours existing in the configuration object. (American, Assian, Custom,...)
            //       Add Custom SessionHours existing in the configuration object. 
        }

        /// <summary>
        /// Adds <see cref="TradingSessionInfo"/> list to session hours sessions.
        /// </summary>
        /// <param name="sessionsInfo"></param>
        public void AddSessions(params TradingSessionInfo[] sessionsInfo)
        {
            foreach (var sessionInfo in sessionsInfo)
                Sessions.Add(sessionInfo);
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
            return 
                $"TNinjaScript: {N}. {TradingDay} \t" +
                $"{BeginTime.ToShortDateString()} at {BeginTime.ToLongTimeString()} --> " +
                $"{EndTime.ToShortDateString()} at {EndTime.ToLongTimeString()} \t" +
                $"{holidayText}";
            
        }

        //public void Iterator(Action<TradingSessionInfo> action = null)
        //{
        //    action(this);
        //    if (HasSessionHours)
        //        for (int i = 0; i < SessionHours.Count; i++)
        //            SessionHours[i].Iterator(action);
        //}

        #endregion

        #region Private methods

        /// <summary>
        /// Add <see cref="TradingSessionInfo"/> to the session hours sessions in the correct place.
        /// </summary>
        /// <param name="sessionType"></param>
        /// <param name="instrumentCode"></param>
        /// <param name="includeInitialBalance"></param>
        /// <param name="includeFinalBalance"></param>
        private void AddSession(
            TradingSession sessionType,
            InstrumentCode instrumentCode = InstrumentCode.Default,
            int includeInitialBalance = 0,
            int includeFinalBalance = 0)
        {

            Sessions.Add(sessionType.ToSessionHours(instrumentCode));
        }


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