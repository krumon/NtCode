using NinjaTrader.Core;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using Nt.Core;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// Represents the core of the ninjascripts  session hours.
    /// </summary>
    public class NsSessionHours : NsIndicator
    {

        #region Private members

        /// <summary>
        /// Represents the ninjatrader session configure on the chart bars.
        /// </summary>
        private NsSession session;

        /// <summary>
        /// The ninjascript parent of the class.
        /// </summary>
        private NinjaScriptBase ninjascript;

        /// <summary>
        /// The session hours structure core.
        /// </summary>
        private SessionHoursStructure sessionHours;

        /// <summary>
        /// <see cref="SessionHoursStructure"/> sorted collection.
        /// </summary>
        private List<SessionHoursStructure> sessionHoursList = new List<SessionHoursStructure>();

        ///// <summary>
        ///// The current session end in Bars TimeZoneInfo.
        ///// </summary>
        //private DateTime currentSessionEnd = Globals.MinDate;

        ///// <summary>
        ///// The session end time in Bars TimeZoneInfo.
        ///// </summary>
        //private DateTime sessionDateTmp = Globals.MinDate;

        ///// <summary>
        ///// Session bar indexs collection.
        ///// </summary>
        //private List<int> newSessionBarIdx = new List<int>();

        ///// <summary>
        ///// The session number.
        ///// </summary>
        //private int count = 0;

        #endregion

        #region Public properties

        #endregion

        #region Constructor

        /// <summary>
        /// Create a default instance of <see cref="SessionHoursStructure"/>.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="sessionIterator"></param>
        public NsSessionHours(NinjaScriptBase ninjascript, SessionIterator sessionIterator, Bars bars)
        {
            this.ninjascript = ninjascript;
            session = new NsSession(ninjascript, sessionIterator, bars);

            session.SessionChanged += NtSession_SessionChanged;
            
            //this.bars = bars;

            //instrument = new Instrument
            //{
            //    InstrumentCode = bars.Instrument.MasterInstrument.Name.ToInstrumentCode(),
            //};
        }

        #endregion

        #region Public methods

        public override void Dispose()
        {
            session.SessionChanged -= NtSession_SessionChanged;
        }

        ///// <summary>
        ///// Returns the session last bar date.
        ///// </summary>
        ///// <param name="time"></param>
        ///// <param name="bars"></param>
        ///// <param name="platformTimeZoneInfo"></param>
        ///// <returns></returns>
        //public DateTime GetLastBarSessionDate(DateTime time)
        //{
        //    if (time <= actualSessionEnd)
        //        return sessionDateTmp;

        //    if (!bars.BarsType.IsIntraday)
        //        return sessionDateTmp;

        //    sessionIterator.GetNextSession(time, true);

        //    actualSessionBegin = sessionIterator.ActualSessionBegin;
        //    actualSessionEnd = sessionIterator.ActualSessionEnd;

        //    sessionDateTmp = TimeZoneInfo.ConvertTime(actualSessionEnd.AddSeconds(-1), platformTimeZoneInfo,barsTimeZoneInfo);

        //    if (newSessionBarIdx.Count == 0 ||
        //        newSessionBarIdx.Count > 0 && ninjascript.CurrentBar > newSessionBarIdx[newSessionBarIdx.Count - 1])
        //        newSessionBarIdx.Add(ninjascript.CurrentBar);

        //    return sessionDateTmp;
        //}

        #endregion

        #region Market Data methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public override void OnBarUpdate()
        {
            //LastBarUpdate();
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public override void OnMarketData()
        {
            //LastBarUpdate();
        }

        #endregion

        #region Private methods

        private void NtSession_SessionChanged(SessionChangedEventArgs obj)
        {
            sessionHours = new SessionHoursStructure
            {
                SessionBegin = SessionTime.CreateCustomSessionTime(session.ActualSessionBegin.TimeOfDay, session.BarsTimeZoneInfo),
                SessionEnd = SessionTime.CreateCustomSessionTime(session.ActualSessionEnd.TimeOfDay, session.BarsTimeZoneInfo)
            };
            sessionHoursList.Add(sessionHours);
        }

        ///// <summary>
        ///// Method to update the last bar time.
        ///// </summary>
        //private void LastBarUpdate()
        //{
        //    DateTime lastBarTimeStamp = GetLastBarSessionDate(ninjascript.Time[0]);
        //    bool isInNewSession = false;

        //    if (lastBarTimeStamp != currentSessionEnd)
        //    {
        //        currentSession = new SessionHoursStructure
        //        {
        //            SessionBegin = SessionTime.CreateCustomSessionTime(NtSession.ActualSessionBegin.TimeOfDay, NtSession.BarsTimeZoneInfo),
        //            SessionEnd = SessionTime.CreateCustomSessionTime(NtSession.ActualSessionEnd.TimeOfDay,NtSession.BarsTimeZoneInfo)
        //        };
        //        sortedSessionList.Add(currentSession);
        //        isInNewSession = true;
        //    }
        //    currentSessionEnd = lastBarTimeStamp;
        //    IsInNewSession = isInNewSession;
        //}

        #endregion

    }


}
