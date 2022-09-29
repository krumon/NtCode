using Kr.Core;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;
using System.Reflection;

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
        public IOptions Options { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseBuilder"/> default instance.
        /// </summary>
        public BaseBuilder(TOptions options)
        {
            Options = options;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <param name="ninjascript">The ninjatrader ninjascript.</param>
        /// <returns>The script object created by the builder.</returns>
        public INinjascript Build(NinjaScriptBase ninjascript = null)
        {
            // Create the script
            TScript script = CreateNinjascriptInstance();
            
            // Make sure the script has been constructed.
            if (script == null)
                throw new ArgumentNullException(nameof(script));

            // if options is null create default options
            if (Options == null)
                Options = Activator.CreateInstance<TOptions>();

            // Configure options
            script.SetOptions(Options);

            // Set the default ninjatrader script properties
            if (ninjascript != null)
                script.SetDefault(ninjascript);

            // Configuration method to the listeners
            OnBuild(script, ninjascript);

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
            {
                // Create default options to rewriter the new properties passed by the options object.
                if (this.Options == null)
                    this.Options = Activator.CreateInstance<TOptions>();

                // Add custom options and properties
                op?.Invoke((TOptions)this.Options);
            }

            return (TBuilder)this;
        }

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="options"/>.
        /// </summary>
        /// <param name="options">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        public IBuilder Configure<Script, Options>(IOptions options)
            where Options : IOptions
        {
            // Make sure options is a valid object and configure the script.
            if (typeof(Script) == typeof(TScript) && typeof(Options) == typeof(TOptions))
            {
                // Create default options to rewriter the new properties passed by the options object.
                if (this.Options == null)
                    this.Options = Activator.CreateInstance<TOptions>();

                // Copy to the options object the options passed by parameter.
                Mapper.Auto(options, this.Options);
            }

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

        #region Private methods

        /// <summary>
        /// Driven method to construct the ninjascript object.
        /// </summary>
        /// <param name="script">The ninjascript object to build.</param>
        /// <param name="ninjascript">The ninjatrader script.</param>
        protected virtual void OnBuild(TScript script, NinjaScriptBase ninjascript)
        {
        }

        /// <summary>
        /// Creates a new <see cref="TScript"/> instance.
        /// </summary>
        /// <returns></returns>
        protected TScript CreateNinjascriptInstance()
        {
            ConstructorInfo construct = typeof(TScript).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            if (construct != null)
               return (TScript)construct.Invoke(new object[] {});
            else
                throw new NullReferenceException();
        }

        /// <summary>
        /// Creates a new <see cref="TScript"/> instance.
        /// </summary>
        /// <returns></returns>
        protected T CreateNinjascriptInstance<T>()
        {
            ConstructorInfo construct = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
            if (construct != null)
                return (T)construct.Invoke(new object[] { });
            else
                throw new NullReferenceException();
        }

        /// <summary>
        /// Creates a new <see cref="TScript"/> instance.
        /// </summary>
        /// <returns></returns>
        protected TScript CreateNinjascriptBuilderInstance(TOptions configuration)
        {
            ConstructorInfo construct = typeof(TScript).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[] {typeof(TBuilder)}, null);
            if (construct != null)
               return (TScript)construct.Invoke(new object[] {configuration});
            else
                throw new NullReferenceException();
        }

        /// <summary>
        /// Creates a new <see cref="TScript"/> instance.
        /// </summary>
        /// <returns></returns>
        protected TScript CreateManagerBuilderInstance(TOptions configuration, List<INinjascript> scripts)
        {
            ConstructorInfo construct = typeof(TScript).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[] {typeof(TBuilder),typeof(List<INinjascript>)}, null);
            if (construct != null)
               return (TScript)construct.Invoke(new object[] {configuration, scripts});
            else
                throw new NullReferenceException();
        }

        #endregion

    }
}
