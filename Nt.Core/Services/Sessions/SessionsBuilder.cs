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

        private SessionsOptions _options;
        private readonly IServiceCollection _services;

        public SessionsBuilder(IServiceCollection services)
        {
            _services = services;
        }

        //public ISessionsBuilder ConfigureFilters(Action<SessionsFiltersOptions>)
        //{
        //    return this;
        //}

        public SessionsBuilder ConfigureSessions(Action<SessionsOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            _services.Add<IOptions<SessionsOptions>>(new SessionsOptions(configureOptions));
            return this;
        }

        public SessionsBuilder ConfigureFilters(Action<SessionsFiltersOptions> configureOptions)
        {
            if (configureOptions == null)
                throw new ArgumentNullException(nameof(configureOptions));

            _services.Add<IOptions<SessionsFiltersOptions>>(new SessionsFiltersOptions(configureOptions));
            return this;
        }

        public ISessionsBuilder AddGenericSessions()
        {
            return this;
        }

        public ISessionsBuilder AddCustomSessions()
        {
            return this;
        }

        public ISessionsBuilder AddStats()
        {
            return this;
        }

        public T Build<T>() 
            where T : ISessionsService, new() //=> new TImplementation();
        {
            var service = new T();
            // Add to ISessionService the Filters, generic sessions, custom sessions,...
            // service.Filters = _builderFilters
            return service;
        } 

    }
}
