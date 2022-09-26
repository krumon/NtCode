﻿using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseBuilder<TScript, TOptions,TBuilder>: IBuilder<TScript, TOptions,TBuilder>
        where TScript : BaseNinjascript<TScript, TOptions,TBuilder> 
        where TOptions : BaseOptions<TOptions>
        where TBuilder : BaseBuilder<TScript,TOptions,TBuilder>
    {

        #region Protected members

        /// <summary>
        /// The script options.
        /// </summary>
        protected TOptions options;

        #endregion

        #region Public methods

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <returns>The script object created by the builder.</returns>
        public virtual TScript Build(NinjaScriptBase ninjascript)
        {
            // Create the script
            TScript script = Activator.CreateInstance<TScript>(); // new TScript();

            // Configure options
            script.Configure(options);

            // Set the default properties or the default actions of the session
            script.SetDefault(ninjascript);

            // Return the script.
            return script;
        }

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <returns>The script object created by the builder.</returns>
        public virtual TScript Build()
        {
            // Create the script
            TScript script = Activator.CreateInstance<TScript>();  // new TScript();

            // Configure options
            script.Configure(options);

            // Return the script.
            return script;
        }

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="op"/>.
        /// </summary>
        /// <param name="op">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        public BaseBuilder<TScript, TOptions,TBuilder> Configure(Action<TOptions> op)
        {
            // Create default options to rewriter the new properties passed by the options object.
            if (options == null)
                options = Activator.CreateInstance<TOptions>();  // new TOptions();

            // Add custom options and properties
            op?.Invoke(options);

            // Return the builder
            return this;
        }

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="op"/>.
        /// </summary>
        /// <param name="op"><see cref="TOptions"/> object with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        public BaseBuilder<TScript, TOptions,TBuilder>Configure(TOptions op)
        {
            // Create default options to rewriter the new properties passed by the options object.
            if (options == null)
                options = Activator.CreateInstance<TOptions>();  // new TOptions();

            // Copy to the options object the options passed by parameter.
            op.CopyTo(options);

            // Return the builder
            return this;
        }

        #endregion

    }

    public abstract class BaseBuilder : BaseBuilder<INinjascript, IOptions, IBuilder>, INinjascript
    {
    }
}