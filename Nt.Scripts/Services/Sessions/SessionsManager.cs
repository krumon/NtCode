using Nt.Core.Hosting;
using Nt.Core.Logging;
using System;

namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents a <see cref="ISessionsManager"/> service.
    /// </summary>
    public class SessionsManager : ISessionsManager
    {
        private readonly INinjascript _ninjascript;
        private readonly IGlobalsData _globalsData;
        private readonly ILogger _logger;

        private readonly ISessionsIterator _sessionsIterator;
        private bool _isNewSession;
        
        public ISessionsIterator SessionsIterator => _sessionsIterator;
        public bool IsConfigured { get; internal set; }
        public bool IsNewSession 
        {
            get => _isNewSession;
            set
            {
                if (value == _isNewSession)
                    return;

                _isNewSession = value;

                if (_isNewSession)
                    OnSessionUpdate();
            }
        }

        public SessionsManager(INinjascript ninjascript, IGlobalsData globalsData, ILogger<SessionsManager> logger)
        {
            _ninjascript = ninjascript ?? throw new ArgumentNullException(nameof(ninjascript));
            _globalsData = globalsData ?? throw new ArgumentNullException(nameof(globalsData));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sessionsIterator = new SessionsIterator(this, _ninjascript, _globalsData, _logger);
        }

        internal SessionsManager()
        {
        }

        public void Configure() 
        {
            _sessionsIterator.Configure();
            

            if (_sessionsIterator.IsConfigured)
                IsConfigured = true;
        }

        public virtual void OnBarUpdate()
        {
            _sessionsIterator.OnBarUpdate();
            //if (Filters.IsEnabled)
            //{

            //}
        }

        public virtual void OnSessionUpdate()
        {
            _logger.LogInformation(_sessionsIterator.ToString());
        }
    }
}
