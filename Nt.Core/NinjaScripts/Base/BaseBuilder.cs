using Kr.Core;
using NinjaTrader.NinjaScript;
using System;
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

        ///// <summary>
        ///// The script options.
        ///// </summary>
        //protected IOptions options;

        /// <summary>
        /// The script to build.
        /// </summary>
        protected TScript script;

        #endregion

        #region Public properties

        /// <summary>
        /// The script to build.
        /// </summary>
        public INinjascript Script => script;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseBuilder"/> default instance.
        /// </summary>
        //public BaseBuilder(TOptions options)
        //{
        //    Options = options;
        //}

        /// <summary>
        /// Creates <see cref="BaseBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public BaseBuilder(INinjascript script)
        {
            // Make sure parameter is not null.
            if (script == null)
                throw new ArgumentNullException(nameof(script));

            // Update the field
            this.script = (TScript)script;

            // Make sure configuration is not null
            if (Script.Configuration == null)
            {
                TOptions op = Activator.CreateInstance<TOptions>();
                Script.SetOptions(op);
            }
        }

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
            //TScript script = CreateNinjascriptInstance();
            
            //// Make sure the script has been constructed.
            //if (Script == null)
            //    throw new ArgumentNullException(nameof(Script));

            //// if options is null create default options
            //if (options == null)
            //    options = Activator.CreateInstance<TOptions>();

            //// Configure options
            //Script.SetOptions(Options);

            // Set the default ninjatrader script properties
            if (ninjascript != null)
                Script.SetDefault(ninjascript);

            // Configuration method to the listeners
            Build();

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
                //// Create default options to rewriter the new properties passed by the options object.
                //if (this.options == null)
                //    this.options = Activator.CreateInstance<TOptions>();

                // Add custom options and properties
                op?.Invoke((TOptions)script.Configuration);
            }

            return (TBuilder)this;
        }

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="options"/>.
        /// </summary>
        /// <param name="options">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        public IBuilder Configure<Script, Options>(Options options)
            where Options : IOptions
        {
            // Make sure options is a valid object and configure the script.
            if (typeof(Script) == typeof(TScript) && typeof(Options) == typeof(TOptions))
            {
                //// Create default options to rewriter the new properties passed by the options object.
                //if (this.options == null)
                //    this.options = Activator.CreateInstance<TOptions>();

                // Copy to the options object the options passed by parameter.
                script.SetOptions(options);
            }

            return (TBuilder)this;
        }

        /// <summary>
        /// Configure the ninjascript with the options passed by <see cref="Action"/> delegate.
        /// </summary>
        /// <param name="options">the options to configure the ninjascript.</param>
        /// <returns>The builder to continue construction the ninjascript.</returns>
        public TBuilder Configure(Action<TOptions> options) =>
            (TBuilder)Configure<TScript,TOptions>(options);

        /// <summary>
        /// Configure the ninjascript with the options passed by <see cref="SessionFiltersOptions"/> object.
        /// </summary>
        /// <param name="options">The options to configure the ninjascript.</param>
        /// <returns>The builder to continue construction the ninjascript.</returns>
        public TBuilder Configure(TOptions options) =>
            (TBuilder)Configure<TScript, TOptions>(options);


        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="op"/>.
        /// </summary>
        /// <param name="op">Delegate method with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        //public IBuilder Configure(Action<IOptions> op)
        //{
        //    // Create default options to rewriter the new properties passed by the options object.
        //    if (Options == null)
        //        Options = Activator.CreateInstance<TOptions>();

        //    // Add custom options and properties
        //    op?.Invoke((TOptions)Options);

        //    // Return the builder
        //    return this;
        //}

        /// <summary>
        /// Configure the ninjascript properties passed by the <paramref name="options"/>.
        /// </summary>
        /// <param name="options"><see cref="TOptions"/> object with the new properties to configure the script.</param>
        /// <returns>The script builder to continue the construction.</returns>
        //public IBuilder Configure(IOptions options)
        //{
        //    // Create default options to rewriter the new properties passed by the options object.
        //    if (Options == null)
        //        Options = Activator.CreateInstance<TOptions>();

        //    // Copy to the options object the options passed by parameter.
        //    this.Options = options;

        //    // Return the builder
        //    return this;
        //}

        #endregion

        #region Private methods

        /// <summary>
        /// Driven method to construct the ninjascript object.
        /// </summary>
        protected virtual void Build()
        {
        }

        /// <summary>
        /// Creates a new <see cref="INinjascript"/> instance.
        /// </summary>
        /// <returns></returns>
        protected TScript CreateNinjascriptInstance()
        {
            ConstructorInfo construct = typeof(TScript).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null);
            if (construct != null)
               return (TScript)construct.Invoke(new object[] {});
            else
                throw new NullReferenceException();
        }

        /// <summary>
        /// Creates a new <see cref="INinjascript"/> instance.
        /// </summary>
        /// <returns></returns>
        protected T CreateNinjascriptInstance<T>()
            where T : INinjascript
        {
            ConstructorInfo construct = typeof(T).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, Type.EmptyTypes, null);
            if (construct != null)
                return (T)construct.Invoke(new object[] { });
            else
                throw new NullReferenceException();
        }

        #endregion

    }
}
