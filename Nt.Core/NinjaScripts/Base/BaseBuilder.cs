using Kr.Core;
using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseBuilder<TScript, TOptions,TBuilder>: IBuilder
        where TScript : BaseNinjascript<TScript, TOptions,TBuilder>, INinjascript
        where TOptions : BaseOptions<TOptions>, IOptions
        where TBuilder : BaseBuilder<TScript,TOptions,TBuilder>, IBuilder
    {

        #region Protected members

        /// <summary>
        /// The script options.
        /// </summary>
        public IOptions Options { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <returns>The script object created by the builder.</returns>
        public virtual INinjascript Build(NinjaScriptBase ninjascript = null)
        {
            // Create the script
            TScript script = Activator.CreateInstance<TScript>();

            // Configure options
            script.SetOptions(Options);

            if (ninjascript != null)
                // Set the default properties or the default actions of the session
                script.SetDefault(ninjascript);

            // Return the script.
            return script;
        }

        /// <summary>
        /// Configure options into the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public IBuilder Configure<Script, Options>(Action<Options> options)
        {
            // Make sure options is a valid object and configure the script.
            if (typeof(Script) == typeof(TScript) && options is Action<TOptions> op)
                Configure(op as Action<IOptions>);

            return (TBuilder)this;
        }

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="options"/>.
        /// </summary>
        /// <param name="options">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        public IBuilder Configure<Script, Options>(Options options)
        {
            // Make sure options is a valid object and configure the script.
            if (typeof(Script) == typeof(TScript) && typeof(Options) == typeof(TOptions))
                Configure(options as IOptions);

            return (TBuilder)this;
        }

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="op"/>.
        /// </summary>
        /// <param name="op">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        public IBuilder Configure(Action<IOptions> op)
        {
            // Create default options to rewriter the new properties passed by the options object.
            if (Options == null)
                Options = Activator.CreateInstance<TOptions>();

            // Add custom options and properties
            op?.Invoke((TOptions)Options);

            // Return the builder
            return this;
        }

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="options"/>.
        /// </summary>
        /// <param name="options"><see cref="TOptions"/> object with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        public IBuilder Configure(IOptions options)
        {
            // Create default options to rewriter the new properties passed by the options object.
            if (Options == null)
                Options = Activator.CreateInstance<TOptions>();

            // Copy to the options object the options passed by parameter.
            Mapper.Auto(options,Options);

            // Return the builder
            return this;
        }

        #endregion

    }
}
