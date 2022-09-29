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

        #region Public properties

        /// <summary>
        /// Ninjascripts collection.
        /// </summary>
        public List<INinjascript> Scripts { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseManagerBuilder"/> default instance.
        /// </summary>
        public BaseManagerBuilder(TManagerOptions options, List<INinjascript> scripts) : base(options)
        {
            Options = options;
            Scripts = scripts;
        }

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
            script.SetScripts(Scripts);

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

            if (Scripts == null)
                Scripts = new List<INinjascript>();

            Scripts.Add(script);

            return this;

        }

        #endregion

    }
}
