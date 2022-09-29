using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascripts manager builders.
    /// </summary>
    public abstract class BaseManagerBuilder<TManagerScript, TManagerOptions, TManagerBuilder> : BaseBuilder<TManagerScript, TManagerOptions, TManagerBuilder>, IManagerBuilder
        where TManagerScript : BaseManager<TManagerScript, TManagerOptions,TManagerBuilder>, IManager
        where TManagerOptions : BaseManagerOptions<TManagerOptions>, IManagerOptions
        where TManagerBuilder : BaseManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, IManagerBuilder
    {

        #region Private members

        /// <summary>
        /// Ninjascripts collection.
        /// </summary>
        private List<INinjascript> scripts;

        #endregion

        #region Public methods

        /// <summary>
        /// Driven method to construct the ninjascript object.
        /// </summary>
        /// <param name="script">The ninjascript object to build.</param>
        /// <param name="ninjascript">The ninjatrader script.</param>
        protected override void OnBuild(TManagerScript script, NinjaScriptBase ninjascript)
        {
            // Call the parent.
            base.OnBuild(script, ninjascript);

            // Configure the ninjascripts
            script.SetScripts(scripts);

        }

        /// <summary>
        /// Adds one <see cref="INinjascript"/> object to the ninjascripts collection.
        /// </summary>
        /// <typeparam name="Script">The <see cref="INinjascript"/> object to add object.</typeparam>
        /// <typeparam name="Options">The <see cref="INinjascript"/> configuration object.</typeparam>
        /// <param name="options">The specific configuration to add.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public IManagerBuilder Add<Script, Options>(Action<Options> options)
            where Script : INinjascript
        {
            Script script = CreateNinjascriptInstance<Script>();
                
            script.CreateBuilder().Configure<Script,Options>(options).Build();

            if (scripts == null)
                scripts = new List<INinjascript>();

            scripts.Add(script);

            return this;

        }

        /// <summary>
        /// Add <see cref="Sessionfilters"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public IManagerBuilder AddSessionFilters(Action<SessionFiltersOptions> options) 
            => Add<SessionFilters, SessionFiltersOptions>(options);

        /// <summary>
        /// Add <see cref="SessionHours"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public IManagerBuilder AddSessionHours(Action<SessionHoursOptions> options) 
            => Add<SessionHours, SessionHoursOptions>(options);

        /// <summary>
        /// Add <see cref="SessionHoursList"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public IManagerBuilder AddSessionHoursList(Action<SessionHoursListOptions> options) 
            => Add<SessionHoursList, SessionHoursListOptions>(options);

        /// <summary>
        /// Add <see cref="SessionStats"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public IManagerBuilder AddSessionStats(Action<SessionStatsOptions> options) 
            => Add<SessionStats, SessionStatsOptions>(options);

        /// <summary>
        /// Add <see cref="SessionsIterator"/> to the manager object.
        /// </summary>
        /// <param name="options">The options to configure the manager object.</param>
        /// <returns>Returns the builder to continue building the object.</returns>
        public IManagerBuilder AddSessionsIterator(Action<SessionsIteratorOptions> options) 
            => Add<SessionsIterator, SessionsIteratorOptions>(options);

        #endregion
    }

}
