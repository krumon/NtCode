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
        void Build();

    }
}
