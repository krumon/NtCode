using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the properties and methods to built a <see cref="SessionsService"/>.
    /// </summary>
    public interface ISessionsBuilder
    {
        SessionsBuilder ConfigureSessions(Action<SessionsOptions> configureOptions);
        SessionsBuilder ConfigureFilters(Action<SessionsFiltersOptions> configureOptions);

        //ISessionsBuilder AddGenericSessions();
        //ISessionsBuilder AddCustomSessions();
        //ISessionsBuilder AddStats();
        //TImplementation Build<TImplementation>() where TImplementation: ISessionsService, new();

    }
}
