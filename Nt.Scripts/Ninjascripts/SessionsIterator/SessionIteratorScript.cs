using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.NinjaScript.DrawingTools;
using Nt.Core.Data;
using Nt.Core.Events;
using Nt.Core.Hosting;
using Nt.Core.Services;
using System;
using System.Runtime.CompilerServices;

namespace Nt.Scripts.Ninjascripts
{
    public class SessionIteratorScript : SessionIteratorService, ISessionIteratorScript
    {

        #region Private members

        private NinjaScriptBase _ninjascript;
        private Bars _bars;
        private SessionIterator _sessionIterator;
        private PartialHoliday _partialHoliday;
        private bool _isNewSession;

        #endregion

        #region Public properties

        public SessionIterator SessionIterator => _sessionIterator;
        public PartialHoliday PartialHoliday => _partialHoliday;
        public override bool? IsBarsIntraday => _bars?.BarsType.IsIntraday;

        public override bool IsNewSession
        {
            get => _isNewSession;
            protected set
            {
                // Make sure value changed
                if (_ninjascript == null || value == _isNewSession)
                    return;

                // Update value.
                _isNewSession = value;

                if (!value)
                    return;

                // Update the number of session counter.
                Count++;

                // Check if it's a partial holiday
                if (!_bars.TradingHours.PartialHolidays.TryGetValue(ActualSessionEnd.Date, out _partialHoliday))
                    _partialHoliday = null;

                // Create the event args.
                SessionChangedEventArgs e = new SessionChangedEventArgs
                {
                    Idx = _ninjascript.CurrentBar,
                    N = Count,
                    BeginTime = ActualSessionBegin,
                    EndTime = ActualSessionEnd,
                    NewSessionTimeZoneInfo = UserTimeZoneInfo,
                    IsPartialHoliday = _partialHoliday != null,
                    IsLateBegin = _partialHoliday != null && _partialHoliday.IsLateBegin,
                    IsEarlyEnd = _partialHoliday != null && _partialHoliday.IsEarlyEnd,
                };

                // Call the listeners
                //OnNtSessionChanged(e);
            }

        }

        #endregion

        #region Constructors

        public SessionIteratorScript(IGlobalDataScript globalDataScript) : base(globalDataScript)
        {
        }

        #endregion

        #region Public methods

        public override void Configure(object[] ninjascriptObjects)
        {
            base.Configure(ninjascriptObjects);

            IsConfigured = true;
        }

        public override void DataLoaded(object[] ninjascriptObjects)
        {
            if (!IsConfigured)
                return;

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
            }
            catch
            {
                throw new Exception("The SessionScript cannot be configured");
            }
        }

        public override void Dispose()
        {
        }

        #endregion

        #region Private methods



        #endregion
    }
}
