using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using Nt.Core.Events;
using Nt.Core.Hosting;
using Nt.Core.Services;
using System;

namespace Nt.Scripts.Ninjascripts
{
    public class SessionScript : SessionService, ISessionScript
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
        public override int CurrentBar => _ninjascript.CurrentBar;
        public override DateTime CurrentTime => _ninjascript.Time[0];
        public override bool? IsPartialHoliday => _partialHoliday != null;
        public override bool? IsLateBegin => _partialHoliday?.IsLateBegin;
        public override bool? IsEarlyEnd => _partialHoliday?.IsEarlyEnd;

        #endregion

        #region Constructors

        public SessionScript(IGlobalDataScript globalDataScript) : base(globalDataScript)
        {
        }

        #endregion

        #region Public methods

        public override void DataLoaded(object[] ninjascriptObjects)
        {
            if (!IsConfigured)
                return;

            _currentSessionEnd = _globalDataService.MinDate;
            _sessionDateTmp = _globalDataService.MinDate;
            UserTimeZoneInfo = _globalDataService.UserConfigureTimeZoneInfo;

            this.TryGet<NinjaScriptBase>(ninjascriptObjects, out _ninjascript);
            this.TryGet<Bars>(ninjascriptObjects, out _bars);

            if (_ninjascript == null)
                // Change for logger and go out
                throw new ArgumentNullException(nameof(_ninjascript));
            if (_bars == null)
                // Change for logger and go out
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
                throw new Exception("The SessionScript cannot be configured when data is loaded.");
            }
        }

        public override void Dispose()
        {
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
