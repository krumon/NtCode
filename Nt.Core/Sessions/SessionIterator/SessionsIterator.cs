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
    public class SessionsIterator : NtScript
    {

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
        /// Represents a partial holiday
        /// </summary>
        private PartialHoliday holiday;

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
        /// Represents the actual session begin
        /// </summary>
        public DateTime ActualSessionBegin { get; set; } = Globals.MinDate;

        /// <summary>
        /// Represents the actual session end.
        /// </summary>
        public DateTime ActualSessionEnd { get; set; } = Globals.MinDate;

        /// <summary>
        /// Represents the <see cref="TimeZoneInfo"/> configure on the platform.
        /// </summary>
        public TimeZoneInfo UserTimeZoneInfo { get; private set; }

        /// <summary>
        /// Represents the <see cref="TimeZoneInfo"/> of the chart bars.
        /// </summary>
        public TimeZoneInfo BarsTimeZoneInfo { get; private set; }

        /// <summary>
        /// The session number.
        /// </summary>
        //public int Count { get; private set; }

        /// <summary>
        /// Is true when a new session is added to the sorted session list.
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

                // Update the counter.
                //Count++;
                if (!(bars.TradingHours.PartialHolidays.TryGetValue(ActualSessionEnd, out var holidays)))
                    holiday = null;

                // Create the event args.
                SessionChangedEventArgs e = new SessionChangedEventArgs 
                { 
                    Idx = this.ninjascript.CurrentBar,
                    NewSessionBeginTime = this.ActualSessionBegin,
                    NewSessionEndTime = this.ActualSessionEnd,
                    NewSessionTimeZoneInfo = this.UserTimeZoneInfo,
                    IsPartialHoliday = holiday!= null,
                    IsLateBegin = holiday != null && holiday.IsLateBegin,
                    IsEarlyEnd = holiday != null && holiday.IsEarlyEnd,
                };

                // Call the listeners
                OnNtSessionChanged(e);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of <see cref="SessionsIterator"/>.
        /// </summary>
        public SessionsIterator()
        {
        }

        /// <summary>
        /// Create a default instance with parameters of <see cref="SessionsIterator"/>.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="bars"></param>
        private SessionsIterator(NinjaScriptBase ninjascript, Bars bars)
        {
            this.ninjascript = ninjascript;
            this.bars = bars;
            this.sessionIterator = new SessionIterator(bars);

            UserTimeZoneInfo = Globals.GeneralOptions.TimeZoneInfo;
            BarsTimeZoneInfo = bars.TradingHours.TimeZoneInfo;
        }

        #endregion

        #region State Changed methods

        /// <summary>
        /// Load the Script.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        /// <param name="o">Any object necesary to load the script.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Make sure session manager can be loaded.
            if (ninjascript == null || bars == null)
                throw new Exception("Parameters can not be null"); // return null;

            // Set values.
            this.ninjascript = ninjascript;
            this.bars = bars;

            // Create ninjatrader session iterator
            this.sessionIterator = new SessionIterator(bars);

            // Set the ninjatrader general options configure by the user
            UserTimeZoneInfo = Globals.GeneralOptions.TimeZoneInfo;

            // Sets the TimeZoneInfo configure by the user in the chart bars.
            BarsTimeZoneInfo = bars.TradingHours.TimeZoneInfo;

            // Aggregate delegates to the events.
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

        #region Event driven methods

        /// <summary>
        /// Event driven method which is called whenever a session hours is changed.
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnSessionChanged(SessionChangedEventArgs e)
        {
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
            if (time <= ActualSessionEnd)
                return sessionDateTmp;

            if (!bars.BarsType.IsIntraday)
                return sessionDateTmp;

            sessionIterator.GetNextSession(time, true);

            ActualSessionBegin = sessionIterator.ActualSessionBegin;
            ActualSessionEnd = sessionIterator.ActualSessionEnd;

            sessionDateTmp = TimeZoneInfo.ConvertTime(ActualSessionEnd.AddSeconds(-1), UserTimeZoneInfo, BarsTimeZoneInfo);

            // TODO: Esto no lo entiendo.
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
