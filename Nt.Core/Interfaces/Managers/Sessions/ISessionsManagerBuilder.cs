using System;

namespace Nt.Core
{

    /// <summary>
    /// Interface for any sessions manager.
    /// </summary>
    public interface ISessionsManagerBuilder : IManagerBuilder
    {

        /// <summary>
        /// Configure the ninjascript with the options passed by <see cref="Action{SessionsManagerOptions}"/> delegate.
        /// </summary>
        /// <param name="options">The options to configure the ninjascript.</param>
        /// <returns>The builder to continue construction the ninjascript.</returns>
        SessionsManagerBuilder Configure(Action<SessionsManagerOptions> options);

        /// <summary>
        /// Configure the ninjascript with the options passed by <see cref="SessionFiltersOptions"/> object.
        /// </summary>
        /// <param name="options">The options to configure the ninjascript.</param>
        /// <returns>The builder to continue construction the ninjascript.</returns>
        SessionsManagerBuilder Configure(SessionsManagerOptions options);

        /// <summary>
        /// Add <see cref="Sessionfilters"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        ISessionsManagerBuilder AddSessionFilters(Action<SessionFiltersOptions> options);

        /// <summary>
        /// Add <see cref="SessionHours"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        ISessionsManagerBuilder AddSessionHours(Action<SessionHoursOptions> options);

        /// <summary>
        /// Add <see cref="SessionHoursList"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        ISessionsManagerBuilder AddSessionHoursList(Action<SessionHoursListOptions> options);

        /// <summary>
        /// Add <see cref="SessionStats"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        ISessionsManagerBuilder AddSessionStats(Action<SessionStatsOptions> options);

        /// <summary>
        /// Add <see cref="SessionsIterator"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        ISessionsManagerBuilder AddSessionsIterator(Action<SessionsIteratorOptions> options);

    }

}
