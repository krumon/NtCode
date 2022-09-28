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
        private List<INinjascript> scripts = new List<INinjascript>();

        #endregion

        #region Public methods

        /// <summary>
        /// Builds the <see cref="IManager"/> object and set default properties of ninjatrader script.
        /// </summary>
        /// <param name="ninjascript"></param>
        /// <returns></returns>
        public override INinjascript Build(NinjaScriptBase ninjascript = null)
        {

            // Create the script
            TManagerScript script = Activator.CreateInstance<TManagerScript>();

            // Set default properties of ninjatrader script.
            if (ninjascript != null)
                script.SetDefault(ninjascript);

            // Configure the manager object.
            script.SetOptions(Options);

            // Configure the ninjascripts
            script.SetScripts(scripts);

            // Return the script.
            return script;

        }

        /// <summary>
        /// Adds one <see cref="INinjascript"/> object to the ninjascripts collection.
        /// </summary>
        /// <typeparam name="Script">The <see cref="INinjascript"/> object to add object.</typeparam>
        /// <typeparam name="Options">The <see cref="INinjascript"/> configuration object.</typeparam>
        /// <param name="options">The specific configuration to add.</param>
        /// <returns></returns>
        public IManagerBuilder Add<Script, Options>(Action<Options> options)
            where Script : INinjascript
        {
            var script = Activator.CreateInstance<Script>().CreateBuilder().Configure<Script,Options>(options).Build();

            if (scripts == null)
                scripts = new List<INinjascript>();

            scripts.Add(script);

            return this;

        }

        #endregion
    }

}
