using NinjaTrader.NinjaScript;
using Nt.Core.Logging;
using Nt.Scripts.Services;
using System;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Represents a <see cref="ISessionsIndicator"/> service.
    /// </summary>
    public class SessionsIndicator : ISessionsIndicator
    {
        private readonly INinjascriptBase _ninjascript;
        private readonly IGlobalsData _globalsData;
        private readonly ILogger _logger;

        private ISessionsIterator _sessionsIterator;
        //private bool _isNewSession;
        
        public ISessionsIterator SessionsIterator => _sessionsIterator;
        public bool IsConfigured { get; internal set; }
        public bool IsNewSession { get; set; }
        //{
        //    get => _isNewSession;
        //    set
        //    {
        //        if (value == _isNewSession)
        //            return;

        //        _isNewSession = value;

        //        if (_isNewSession)
        //            OnSessionUpdate();
        //    }
        //}

        public SessionsIndicator(INinjascriptBase ninjascript, IGlobalsData globalsData, ILogger<SessionsIndicator> logger)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
            _globalsData = globalsData ?? throw new ArgumentNullException(nameof(globalsData));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sessionsIterator = new SessionsIterator(this, _ninjascript, _globalsData, _logger);
        }

        internal SessionsIndicator()
        {
        }

        public void Configure() 
        {
            if (_ninjascript.State == State.SetDefaults)
                return;
            else
            {
                _sessionsIterator.Configure();

            }

            if (_sessionsIterator.IsConfigured)
                IsConfigured = true;
        }

        public virtual void OnBarUpdate()
        {
            _sessionsIterator.OnBarUpdate();
            //if (Filters.IsEnabled)
            //{

            //}

            // Reset the new session value when after the first bar update
            if (IsNewSession)
                IsNewSession = false;
        }

        public virtual void OnSessionChanged(SessionChangedEventArgs args)
        {
            _logger.LogInformation(_sessionsIterator.ToString());
        }

        public bool IsEnabled(Ninjascripts.NinjascriptLevel ninjascriptLevel)
        {
            throw new NotImplementedException();
        }

        public void Calculate()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
