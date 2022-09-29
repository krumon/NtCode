using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// <see cref="SessionsManager"/> builder.
    /// </summary>
    public class SessionsManagerBuilder : BaseManagerBuilder<SessionsManager,SessionsManagerOptions,SessionsManagerBuilder>, ISessionsManagerBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="SessionHoursBuilder"/> default instance.
        /// </summary>
        public SessionsManagerBuilder(SessionsManagerOptions options, List<INinjascript> scripts) : base(options,scripts)
        {
            Options = options;
            Scripts = scripts;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Add <see cref="Sessionfilters"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public SessionsManagerBuilder AddSessionFilters(Action<SessionFiltersOptions> options)
            => (SessionsManagerBuilder)Add<SessionFilters, SessionFiltersOptions>(options);

        /// <summary>
        /// Add <see cref="SessionHours"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public SessionsManagerBuilder AddSessionHours(Action<SessionHoursOptions> options)
            => (SessionsManagerBuilder)Add<SessionHours, SessionHoursOptions>(options);

        /// <summary>
        /// Add <see cref="SessionHoursList"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public SessionsManagerBuilder AddSessionHoursList(Action<SessionHoursListOptions> options)
            => (SessionsManagerBuilder)Add<SessionHoursList, SessionHoursListOptions>(options);

        /// <summary>
        /// Add <see cref="SessionStats"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public SessionsManagerBuilder AddSessionStats(Action<SessionStatsOptions> options)
            => (SessionsManagerBuilder)Add<SessionStats, SessionStatsOptions>(options);

        /// <summary>
        /// Add <see cref="SessionsIterator"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public SessionsManagerBuilder AddSessionsIterator(Action<SessionsIteratorOptions> options)
            => (SessionsManagerBuilder)Add<SessionsIterator, SessionsIteratorOptions>(options);

        #endregion

    }
}
