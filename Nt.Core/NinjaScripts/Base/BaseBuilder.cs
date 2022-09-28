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
        public IOptions options { get; private set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <returns>The script object created by the builder.</returns>
        public virtual INinjascript Build(NinjaScriptBase ninjascript)
        {
            //// Create the script
            //TScript script = Activator.CreateInstance<TScript>(); // new TScript();

            //// Configure options
            //script.Configure(options);

            //// Set the default properties or the default actions of the session
            //script.SetDefault(ninjascript);

            //// Return the script.
            //return script;
            return Builder(ninjascript);
        }

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <returns>The script object created by the builder.</returns>
        public virtual INinjascript Build()
        {
            //// Create the script
            //TScript script = Activator.CreateInstance<TScript>();  // new TScript();

            //// Configure options
            //script.Configure(options);

            //// Return the script.
            //return script;
            return Builder();
        }

        private TScript Builder(NinjaScriptBase ninjascript = null)
        {
            // Create the script
            TScript script = Activator.CreateInstance<TScript>(); // new TScript();

            // Configure options
            script.Configure(options);

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
            // Create default session properties.
            //var sessionOptions = new TOptions();
            if (typeof(Script) == typeof(TScript) && options is Action<TOptions> op)
            {
                if (this.options == null)
                    this.options = Activator.CreateInstance<TOptions>();

                // If options is not null...invoke delegate to update the configure options by the user.
                if (op != null)
                    op.Invoke((TOptions)this.options);

            }

            return this;
        }

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="op"/>.
        /// </summary>
        /// <param name="op">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        public IBuilder Configure<Script, Options>(Options op)
        {
            return this;
        }

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="op"/>.
        /// </summary>
        /// <param name="op">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        public IBuilder Configure(Action<IOptions> op)
        {
            // Create default options to rewriter the new properties passed by the options object.
            if (options == null)
                options = Activator.CreateInstance<TOptions>();

            // Add custom options and properties
            op?.Invoke((TOptions)options);

            // Return the builder
            return this;
        }

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="op"/>.
        /// </summary>
        /// <param name="op"><see cref="TOptions"/> object with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        public IBuilder Configure(IOptions op)
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

    ///// <summary>
    ///// The base class to ninjascript builders
    ///// </summary>
    //public abstract class BaseBuilder : BaseBuilder<BaseNinjascript, BaseOptions, BaseBuilder>, IBuilder
    //{
    //}
}
