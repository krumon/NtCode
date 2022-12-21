using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using Nt.Core.Hosting;
using Nt.Core.Services;
using System;

namespace Nt.Scripts.Ninjascripts
{
    public class SessionsIteratorScript : SessionsIteratorService
    {

        #region Private members

        private NinjaScriptBase _ninjascript;
        private Bars _bars;
        private SessionIterator _sessionIterator;
        private PartialHoliday _partialHoliday;

        #endregion

        #region Public properties

        public SessionIterator SessionIterator => _sessionIterator;
        public PartialHoliday PartialHoliday => _partialHoliday;
        public override bool? BarsTypeIsIntraday => _bars?.BarsType.IsIntraday;
        //public override int CurrentBar { get; protected set; }
        //public override DateTime CurrentTime { get; protected set; }
        public override bool? IsPartialHoliday => _partialHoliday != null;
        public override bool? IsLateBegin => _partialHoliday?.IsLateBegin;
        public override bool? IsEarlyEnd => _partialHoliday?.IsEarlyEnd;

        #endregion

        #region Constructors

        public SessionsIteratorScript(IGlobalsDataService globalsData) : base(globalsData)
        {
        }

        #endregion

        #region Public methods

        public override void DataLoaded(object[] ninjascriptObjects)
        {
            // Make sure the script is configured
            if (!IsConfigured)
                return;
            // Initialize variables
            _currentSessionEnd = _globalsDataService.MinDate;
            _sessionDateTmp = _globalsDataService.MinDate;
            UserTimeZoneInfo = _globalsDataService.UserConfigureTimeZoneInfo;
            ActualSessionBegin = _globalsDataService.MinDate;
            ActualSessionEnd = _globalsDataService.MaxDate;
            // Gets the necesary ninjascript object for the script.
            this.TryGetObject<NinjaScriptBase>(ninjascriptObjects, out _ninjascript);
            this.TryGetObject<Bars>(ninjascriptObjects, out _bars);
            //Make sure the ninjascripts objects can be loaded.
            if (_ninjascript == null)
                // Logger critical error loading the NinjascrptBase object in method SessionsIteratorScript.DataLoaded
                throw new ArgumentNullException(nameof(_ninjascript));
            if (_bars == null)
                // Logger critical error loading the Bars object in method SessionsIteratorScript.DataLoaded
                throw new ArgumentNullException(nameof(_bars));

            if (_ninjascript == null || _bars == null)
                return;

            try
            {
                BarsTimeZoneInfo = _bars.TradingHours.TimeZoneInfo;
                _sessionIterator = new SessionIterator(_bars);
                IsDataLoaded = true;
            }
            catch
            {
                throw new Exception("The SessionsIteratorScript cannot be configured when data is loaded.");
            }
        }

        public override void Dispose()
        {
        }

        public override void OnBarUpdate()
        {
            CurrentBar = _ninjascript.CurrentBar;
            CurrentTime = _ninjascript.Time[0];

            base.OnBarUpdate();
        }
        public override void GetAndUpdateNextSessionValues(DateTime time, bool includeEndTimeStamp = true)
        {
            // Get the next session value.
            _sessionIterator.GetNextSession(time,includeEndTimeStamp);
            // Update the next session value.
            ActualSessionBegin = _sessionIterator.ActualSessionBegin;
            ActualSessionEnd = _sessionIterator.ActualSessionEnd;
        }

        public override bool? TryGetPartialHoliday() 
            => _bars.TradingHours.PartialHolidays.TryGetValue(ActualSessionEnd.Date, out _partialHoliday);

        #endregion

    }
}
