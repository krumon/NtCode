using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Represents the properties and methods to built a <see cref="SessionsIteratorService"/>.
    /// </summary>
    public interface ISessionsBuilder
    {

        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where ninjascript services are configured.
        /// </summary>
        IServiceCollection Services { get; }

        //SessionsBuilder ConfigureSessionsIterator(Action<SessionsIteratorOptions> configureOptions);
        //SessionsBuilder ConfigureFilters(Action<SessionsFiltersOptions> configureOptions);
        //void Build();

    }
}
