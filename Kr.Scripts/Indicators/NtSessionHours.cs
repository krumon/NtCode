using NinjaTrader.Core;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using Nt.Core;
using System;
using System.Collections.Generic;

namespace Ninjascripts
{
    /// <summary>
    /// Represents the SessionHours Indicator Core.
    /// </summary>
    public class NtSessionHours : KrSessionHours
    {

        #region Private members

        /// <summary>
        /// The ninjascript parent of the class.
        /// </summary>
        private NinjaScriptBase ninjascript;

        /// <summary>
        /// The session iterator to store the actual and next session data.
        /// </summary>
        private SessionIterator sessionIterator;

        /// <summary>
        /// The bars of the chart control.
        /// </summary>
        private Bars bars;

        /// <summary>
        /// The global general options <see cref="TimeZoneInfo"/>.
        /// </summary>
        private TimeZoneInfo platformTimeZoneInfo => Globals.GeneralOptions.TimeZoneInfo;

        /// <summary>
        /// The bars <see cref="TimeZoneInfo"/>.
        /// </summary>
        private TimeZoneInfo barsTimeZoneInfo => bars.TradingHours.TimeZoneInfo;

        /// <summary>
        /// Store the actual session begin
        /// </summary>
        private DateTime actualSessionBegin = Globals.MinDate;

        /// <summary>
        /// Store the actual session end.
        /// </summary>
        private DateTime actualSessionEnd = Globals.MinDate;

        /// <summary>
        /// The current session end in Bars TimeZoneInfo.
        /// </summary>
        private DateTime currentSessionEnd = Globals.MinDate;

        /// <summary>
        /// The session end time in Bars TimeZoneInfo.
        /// </summary>
        private DateTime sessionDateTmp = Globals.MinDate;

        /// <summary>
        /// Session bar indexs collection.
        /// </summary>
        private List<int> newSessionBarIdx = new List<int>();

        private Instrument instrument;

        #endregion

        #region Public properties

        /// <summary>
        /// The session iterator to store the actual and next session data.
        /// </summary>
        //public SessionIterator SessionIterator
        //{
        //    get
        //    {
        //        if (sessionIterator == null)
        //            sessionIterator = new SessionIterator(bars);

        //        return sessionIterator;
        //    }
        //}

        /// <summary>
        /// Is true when a new session is added to the sorted session list.
        /// </summary>
        public bool IsInNewSession { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Create a default instance of <see cref="SessionHoursStructure"/>.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="sessionIterator"></param>
        public NtSessionHours(NinjaScriptBase ninjascript, SessionIterator sessionIterator, Bars bars)
        {
            this.ninjascript = ninjascript;
            this.sessionIterator = sessionIterator;
            this.bars = bars;

            instrument = new Instrument
            {
                InstrumentCode = bars.Instrument.MasterInstrument.Name.ToInstrumentCode(),
            };
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
            if (time <= actualSessionEnd)
                return sessionDateTmp;

            if (!bars.BarsType.IsIntraday)
                return sessionDateTmp;

            sessionIterator.GetNextSession(time, true);

            actualSessionBegin = sessionIterator.ActualSessionBegin;
            actualSessionEnd = sessionIterator.ActualSessionEnd;
            
            sessionDateTmp = TimeZoneInfo.ConvertTime(actualSessionEnd.AddSeconds(-1), platformTimeZoneInfo,barsTimeZoneInfo);

            if (newSessionBarIdx.Count == 0 ||
                newSessionBarIdx.Count > 0 && ninjascript.CurrentBar > newSessionBarIdx[newSessionBarIdx.Count - 1])
                newSessionBarIdx.Add(ninjascript.CurrentBar);
                
            return sessionDateTmp;
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

        /// <summary>
        /// Method to update the last bar time.
        /// </summary>
        private void LastBarUpdate()
        {
            DateTime lastBarTimeStamp = GetLastBarSessionDate(ninjascript.Time[0]);
            bool isInNewSession = false;

            if (lastBarTimeStamp != currentSessionEnd)
            {
                currentSession = new SessionHoursStructure
                {
                    SessionBegin = SessionTime.CreateCustomSessionTime(actualSessionBegin.TimeOfDay, bars.TradingHours.TimeZoneInfo),
                    SessionEnd = SessionTime.CreateCustomSessionTime(actualSessionEnd.TimeOfDay,bars.TradingHours.TimeZoneInfo)
                };
                sortedSessionList.Add(currentSession);
                isInNewSession = true;
            }
            currentSessionEnd = lastBarTimeStamp;
            IsInNewSession = isInNewSession;
        }

        #endregion

    }


}
