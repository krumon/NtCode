using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
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
        /// The session hours structure core.
        /// </summary>
        private NsSessionHoursStructure sessionHoursStructure;

        /// <summary>
        /// <see cref="SessionStructure"/> sorted collection.
        /// </summary>
        private List<NsSessionHoursStructure> sessionHoursStructureList = new List<NsSessionHoursStructure>();

        /// <summary>
        /// Represents the ninjatrader session configure on the chart bars.
        /// </summary>
        public NsSession ninjatraderSession;

        #endregion

        #region Public properties

        /// <summary>
        /// Represents the ninjatrader session configure on the chart bars.
        /// </summary>
        public NsSession NinjatraderSession
        {
            get
            {
                if (ninjatraderSession == null)
                    ninjatraderSession = new NsSession(ninjascript, SessionIterator, bars);

                return ninjatraderSession;
            }
        }

        /// <summary>
        /// Represents the session hours structure.
        /// </summary>
        public NsSessionHoursStructure SessionHoursStructure => sessionHoursStructure;

        /// <summary>
        /// The session iterator to store the actual and next session data.
        /// </summary>
        public SessionIterator SessionIterator
        {
            get
            {
                if (sessionIterator == null)
                    sessionIterator = new SessionIterator(bars);

                return sessionIterator;
            }
        }

        public DateTime BeginTime => NinjatraderSession.ActualSessionBegin;
        public DateTime EndTime => NinjatraderSession.ActualSessionEnd;
        public int Count => sessionHoursStructureList.Count;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a default instance of <see cref="SessionStructure"/>.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <param name="sessionIterator"></param>
        public NsSessionHours(NinjaScriptBase ninjascript, SessionIterator sessionIterator, Bars bars)
        {
            this.ninjascript = ninjascript;
            this.bars = bars;

            NinjatraderSession.SessionChanged += NinjatraderSessionChanged;
        }

        #endregion

        #region StateChanged Methods

        /// <summary>
        /// Method to execute when de ninjascript terminated.
        /// </summary>
        public void OnTerminated()
        {
            Dispose();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Free the memory of the handler methods.
        /// </summary>
        public override void Dispose()
        {
            ninjatraderSession.SessionChanged -= NinjatraderSessionChanged;
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
            NinjatraderSession.OnBarUpdate();
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public override void OnMarketData()
        {
            NinjatraderSession.OnMarketData();
        }

        #endregion

        #region Private methods

        private void NinjatraderSessionChanged(SessionChangedEventArgs e)
        {
            sessionHoursStructure = new NsSessionHoursStructure
            {
                Idx = Count,
                BeginTime = this.BeginTime,
                EndTime = this.NinjatraderSession.ActualSessionEnd
            };
            sessionHoursStructureList.Add(sessionHoursStructure);
        }

        #endregion

    }


}
