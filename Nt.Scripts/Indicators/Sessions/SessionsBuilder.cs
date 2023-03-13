using Nt.Core.DependencyInjection;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Build the <see cref="SessionsIndicator"/> object.
    /// </summary>
    public class SessionsBuilder : ISessionsBuilder
    {

        public SessionsBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }

        //public SessionsBuilder ConfigureSessionsIterator(Action<SessionsIteratorOptions> configureOptions)
        //{
        //    if (configureOptions == null)
        //        throw new ArgumentNullException(nameof(configureOptions));
        //    _sessionsIteratorOptionsActions = configureOptions;

        //    return this;
        //}

        //public SessionsBuilder ConfigureFilters(Action<SessionsFiltersOptions> configureOptions)
        //{
        //    if (configureOptions == null)
        //        throw new ArgumentNullException(nameof(configureOptions));

        //    return this;
        //}

        //public void Build()
        //{
        //    //if (_sessionsIteratorOptionsActions == null) _sessionsIteratorOptionsActions = (options) => new SessionsIteratorOptions();
        //    //_services.AddSingleton<IConfigureOptions<SessionsIteratorOptions>>(new ConfigureSessionsIteratorOptions(_sessionsIteratorOptionsActions));
        //    //if (_sessionsFiltersOptionsActions == null) _sessionsFiltersOptionsActions = (options) => new SessionsFiltersOptions();
        //    //_services.AddSingleton<IConfigureOptions<SessionsFiltersOptions>>(new ConfigureSessionsFiltersOptions(_sessionsFiltersOptionsActions));
        //}

    }
}
