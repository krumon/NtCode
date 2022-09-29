using System;

namespace Nt.Core
{

    /// <summary>
    /// Interface for any sessions manager.
    /// </summary>
    public interface ISessionsManagerBuilder : IManagerBuilder
    {
        /// <summary>
        /// Add <see cref="Sessionfilters"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        SessionsManagerBuilder AddSessionFilters(Action<SessionFiltersOptions> options);

        /// <summary>
        /// Add <see cref="SessionHours"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        SessionsManagerBuilder AddSessionHours(Action<SessionHoursOptions> options);

        /// <summary>
        /// Add <see cref="SessionHoursList"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        SessionsManagerBuilder AddSessionHoursList(Action<SessionHoursListOptions> options);

        /// <summary>
        /// Add <see cref="SessionStats"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        SessionsManagerBuilder AddSessionStats(Action<SessionStatsOptions> options);

        /// <summary>
        /// Add <see cref="SessionsIterator"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        SessionsManagerBuilder AddSessionsIterator(Action<SessionsIteratorOptions> options);

    }

}
