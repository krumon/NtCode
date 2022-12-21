using Nt.Core.DependencyInjection;
using Nt.Core.Options;
using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Build the <see cref="SessionsService"/> objects.
    /// </summary>
    public class SessionsBuilder : ISessionsBuilder
    {

        private readonly IServiceCollection _services;
        private Action<SessionsIteratorOptions> _sessionsIteratorOptionsActions;
        private Action<SessionsFiltersOptions> _sessionsFiltersOptionsActions;

        public SessionsBuilder(IServiceCollection services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public SessionsBuilder ConfigureSessionsIterator(Action<SessionsIteratorOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));
            _sessionsIteratorOptionsActions = configureOptions;
            
            return this;
        }

        public SessionsBuilder ConfigureFilters(Action<SessionsFiltersOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));
            
            return this;
        }

        public void Build()
        {
            if (_sessionsIteratorOptionsActions == null) _sessionsIteratorOptionsActions = (options) => new SessionsIteratorOptions();
            _services.Add<IConfigureOptions<SessionsIteratorOptions>>(new ConfigureSessionsIteratorOptions(_sessionsIteratorOptionsActions));
            if (_sessionsFiltersOptionsActions == null) _sessionsFiltersOptionsActions = (options) => new SessionsFiltersOptions();
            _services.Add<IConfigureOptions<SessionsFiltersOptions>>(new ConfigureSessionsFiltersOptions(_sessionsFiltersOptionsActions));
        }

    }
}
