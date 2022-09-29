using NinjaTrader.Core;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents the trading hours session definition.
    /// </summary>
    public class SessionsIterator : BaseSession<SessionsIterator, SessionsIteratorOptions,SessionsIteratorBuilder>
    {

        #region Test Properties

        public string DisplayDayType { get; set; } 

        #endregion

        #region Private members

        /// <summary>
        /// The session iterator to store the actual and next session data.
        /// </summary>
        private SessionIterator sessionIterator;

        /// <summary>
        /// The current session end in Bars TimeZoneInfo.
        /// </summary>
        private DateTime currentSessionEnd = Globals.MinDate;

        /// <summary>
        /// The session end time in Bars TimeZoneInfo.
        /// </summary>
        private DateTime sessionDateTmp = Globals.MinDate;

        /// <summary>
        /// TradingSessionInfo bar indexs collection.
        /// </summary>
        private List<int> newSessionBarIdx = new List<int>();

        /// <summary>
        /// Flag to indicate whe the session changed to new session.
        /// </summary>
        private bool isNewSession;

        /// <summary>
        /// Represents a partial partialHoliday
        /// </summary>
        private PartialHoliday partialHoliday;

        #endregion

        #region Public events

        /// <summary>
        /// Event thats is raised when the sessoin changed.
        /// </summary>
        public event Action<SessionChangedEventArgs> SessionChanged = (e) => { };

        #endregion

        #region Public properties

        /// <summary>
        /// The session iterator to store the actual and next session data.
        /// </summary>
        public SessionIterator SessionIterator => sessionIterator;

        /// <summary>
        /// Represents the actual session start time converted to the user's configured Time Zone.
        /// </summary>
        public DateTime ActualSessionBegin { get; private set; } = Globals.MinDate;

        /// <summary>
        /// Represents the actual session end time converted to the user's configured Time Zone.
        /// </summary>
        public DateTime ActualSessionEnd { get; private set; } = Globals.MinDate;

        /// <summary>
        /// Represents the user's configured <see cref="TimeZoneInfo"/>.
        /// </summary>
        public TimeZoneInfo UserTimeZoneInfo { get; private set; }

        /// <summary>
        /// Represents the bar's configured <see cref="TimeZoneInfo"/>.
        /// </summary>
        public TimeZoneInfo BarsTimeZoneInfo { get; private set; }

        /// <summary>
        /// The sessions counter.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Is true when a new session bars enter in a new session.
        /// </summary>
        public bool IsNewSession 
        {
            get => isNewSession;
            private set
            {
                // Make sure value changed
                if (ninjascript == null || value == isNewSession)
                    return;

                // Update value.
                isNewSession = value;

                if (!value)
                    return;

                // Update the number of session counter.
                Count++;

                // Check if it's a partial holiday
                if (!bars.TradingHours.PartialHolidays.TryGetValue(ActualSessionEnd.Date, out partialHoliday))
                    partialHoliday = null;

                // TODO: Remove
                DisplayDayType = partialHoliday != null ? "Partial Holiday" : "--------------";

                // Create the event args.
                SessionChangedEventArgs e = new SessionChangedEventArgs
                {
                    Idx = this.ninjascript.CurrentBar,
                    N = this.Count,
                    BeginTime = this.ActualSessionBegin,
                    EndTime = this.ActualSessionEnd,
                    NewSessionTimeZoneInfo = this.UserTimeZoneInfo,
                    IsPartialHoliday = partialHoliday!= null,
                    IsLateBegin = partialHoliday != null && partialHoliday.IsLateBegin,
                    IsEarlyEnd = partialHoliday != null && partialHoliday.IsEarlyEnd,
                };

                // Call the listeners
                OnNtSessionChanged(e);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionsIterator"/> default instance.
        /// </summary>
        protected SessionsIterator() : base()
        {
        }

        #endregion

        #region State Changed methods

        public override void SetDefault(NinjaScriptBase ninjascript)
        {
            NinjaTrader.Data.TradingHours.Get("").CopyTo(bars.TradingHours);
            NinjaTrader.Data.TradingHours.String2TradingHours("").CopyTo(bars.TradingHours);
        }

        /// <summary>
        /// Loaded <see cref="SessionsIterator"/> in "OnStateChanged" method.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="o">Any object necesary to load the script.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Call to parent.
            base.Load(ninjascript, bars);

            // Create ninjatrader session iterator
            this.sessionIterator = new SessionIterator(bars);

            // Set the ninjatrader general options configure by the user
            UserTimeZoneInfo = Globals.GeneralOptions.TimeZoneInfo;

            // Sets the TimeZoneInfo configure by the user in the chart bars.
            BarsTimeZoneInfo = bars.TradingHours.TimeZoneInfo;

        }

        /// <summary>
        /// Free the memory of the script.
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
            LastBarUpdate();
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public override void OnMarketData()
        {
            LastBarUpdate();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns the session last bar date.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="bars"></param>
        /// <param name="platformTimeZoneInfo"></param>
        /// <returns></returns>
        public DateTime GetLastBarSessionDate(DateTime time)
        {
            // Make sure the session is terminated
            if (time <= ActualSessionEnd)
                return sessionDateTmp;

            // Make sure the bars type are not intraday
            if (!bars.BarsType.IsIntraday)
                return sessionDateTmp;

            // Get the next session values
            sessionIterator.GetNextSession(time, true);

            // Store the start and final time in user's configured time zone.
            ActualSessionBegin = sessionIterator.ActualSessionBegin;
            ActualSessionEnd = sessionIterator.ActualSessionEnd;

            // Converts the start and end time to bar's configured time zone.
            sessionDateTmp = TimeZoneInfo.ConvertTime(ActualSessionEnd.AddSeconds(-1), UserTimeZoneInfo, BarsTimeZoneInfo);

            // Store the bar index of the session.
            if (newSessionBarIdx.Count == 0 ||
                newSessionBarIdx.Count > 0 && ninjascript.CurrentBar > newSessionBarIdx[newSessionBarIdx.Count - 1])
                newSessionBarIdx.Add(ninjascript.CurrentBar);

            return sessionDateTmp;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Mthod to raise the event and driven methods.
        /// </summary>
        /// <param name="e"></param>
        private void OnNtSessionChanged(SessionChangedEventArgs e)
        {

            // Raise the handler method
            OnSessionChanged(e);

            // Raise the event
            SessionChanged?.Invoke(e);

        }

        /// <summary>
        /// Method to update the last bar time.
        /// </summary>
        private void LastBarUpdate()
        {
            DateTime lastBarTimeStamp = GetLastBarSessionDate(ninjascript.Time[0]);
            bool isNewSession = false;

            if (lastBarTimeStamp != currentSessionEnd)
                isNewSession = true;
            
            currentSessionEnd = lastBarTimeStamp;
            IsNewSession = isNewSession;
        }

        #endregion

    }
}
