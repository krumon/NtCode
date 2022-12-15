using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the properties and methods to built a <see cref="SessionsIteratorService"/>.
    /// </summary>
    public interface ISessionsBuilder
    {
        SessionsBuilder ConfigureSessionsIterator(Action<SessionsIteratorOptions> configureOptions);
        SessionsBuilder ConfigureFilters(Action<SessionsFiltersOptions> configureOptions);

        //ISessionsBuilder AddGenericSessions();
        //ISessionsBuilder AddCustomSessions();
        //ISessionsBuilder AddStats();
        //TImplementation Build<TImplementation>() where TImplementation: ISessionsService, new();

    }
}
