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
        /// The ninjascript parent of the class.
        /// </summary>
        private NinjaScriptBase ninjascript;

        /// <summary>
        /// The bars of the chart control.
        /// </summary>
        private Bars bars;

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

        //private PartialHoliday partialHoliday;

        #endregion

        #region Public events

        /// <summary>
        /// Event thats is raised when the sessoin changed.
        /// </summary>
        public event Action<SessionChangedEventArgs> SessionChanged = (e) => { };

        #endregion

        #region Public properties

        /// <summary>
        /// The ninjascript parent of the class.
        /// </summary>
        public NinjaScriptBase Ninjascript => ninjascript;

        /// <summary>
        /// The bars of the chart control.
        /// </summary>
        public Bars Bars => bars;

        /// <summary>
        /// The session iterator to store the actual and next session data.
        /// </summary>
        public SessionIterator SessionIterator => sessionIterator;

        ///// <summary>
        ///// Represents the chart bars <see cref="Instrument"/>.
        ///// </summary>
        //public Instrument Instrument { get; private set; }

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
                if (value == isNewSession)
                    return;

                // Update value.
                isNewSession = value;

                if (!value)
                    return;

                // Update the counter.
                //Count++;

                // if date is a partial hoilday...store the partial holiday
                //if (!(bars.TradingHours.PartialHolidays.TryGetValue(ActualSessionBegin, out partialHoliday)))
                //    partialHoliday = null;

                // Create the event args.
                SessionChangedEventArgs e = new SessionChangedEventArgs 
                { 
                    Idx = this.ninjascript.CurrentBar,
                    NewSessionBeginTime = this.ActualSessionBegin,
                    NewSessionEndTime = this.ActualSessionEnd,
                    NewSessionTimeZoneInfo = this.UserTimeZoneInfo,
                    //IsPartialHoliday = this.IsPartialHoliday,
                    //IsLateBegin = this.IsLateBegin,
                    //IsEarlyEnd = this.IsEarlyEnd,
                };

                // Call the listeners
                OnNtSessionChanged(e);

            }
        }

        /// <summary>
        /// Indicates if the trading hours is a partial holiday.
        /// </summary>
        //public bool IsPartialHoliday => partialHoliday != null;

        /// <summary>
        /// Indicates if the partial holiday has a late begin time.
        /// </summary>
        //public bool IsLateBegin => IsPartialHoliday && partialHoliday.IsLateBegin;

        /// <summary>
        /// Indicates if the partial holiday has a early end.
        /// </summary>
        //public bool IsEarlyEnd => IsPartialHoliday && partialHoliday.IsEarlyEnd;

        #endregion

        #region Constructors

        /// <summary>
        /// Create a default instance of <see cref="SessionsIterator"/>.
        /// </summary>
        /// <param name="sessionIterator"></param>
        /// <param name="bars"></param>
        /// <param name="ninjascript"></param>
        public SessionsIterator(NinjaScriptBase ninjascript, Bars bars)
        {
            this.ninjascript = ninjascript;
            this.bars = bars;
            this.sessionIterator = new SessionIterator(bars);

            UserTimeZoneInfo = Globals.GeneralOptions.TimeZoneInfo;
            BarsTimeZoneInfo = bars.TradingHours.TimeZoneInfo;
            //Instrument = new Instrument
            //{
            //    InstrumentCode = bars.Instrument.MasterInstrument.Name.ToInstrumentCode(),
            //};
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

        #region Event driven methods

        public virtual void OnSessionChanged(SessionChangedEventArgs e)
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

        #region Private methods

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
