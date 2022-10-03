using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using Nt.Core.Exceptions;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Nt.Core
{

    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    public abstract class BaseNinjascript : BaseElement
    {

        #region Protected members

        /// <summary>
        /// The ninjascript parent of the class.
        /// </summary>
        protected NinjaScriptBase ninjascript;

        /// <summary>
        /// The bars of the chart control.
        /// </summary>
        protected Bars bars;

        #endregion

        #region Public properties

        /// <summary>
        /// The ninjascript parent of the class.
        /// </summary>
        public NinjaScriptBase Ninjascript => ninjascript;

        /// <summary>
        /// The bars of the chart control.
        /// </summary>
        public Bars Bars => bars;

        /// <summary>
        /// Indicates if the session is configured.
        /// </summary>
        public bool IsLoaded { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseNinjascript"/> default instance.
        /// </summary>
        public BaseNinjascript()
        {
        }

        #endregion

        #region State changed methods

        /// <summary>
        /// Method to set default properties in the script in "OnStateChanged.Configure" method.
        /// </summary>
        /// <param name="ninjascript">The ninjascript parent object.</param>
        public virtual void SetDefault(NinjaScriptBase ninjascript)
        {
        }

        /// <summary>
        /// AddValues the <see cref="BaseNinjascript"/>.
        /// </summary>
        /// <param name="ninjascript">The parent ninjascript.</param>
        /// <param name="bars">The chart bars object.</param>
        public virtual void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            try
            {
                // Sets ninjascript value.
                this.ninjascript = ninjascript ?? throw new ArgumentNullException(NinjascriptErrors.NinjascriptLoadParametersException);
                // Sets bars value.
                this.bars = bars ?? throw new ArgumentNullException(NinjascriptErrors.NinjascriptLoadParametersException);
                // Indicates that the ninjascript is loaded.
                IsLoaded = true;
            }
            catch (Exception e)
            {
                // TODO: ILogger implementation to register errors.
                Console.WriteLine(e.Message);
                if (ninjascript == null)
                    Console.WriteLine($"If {nameof(ninjascript)} parameter is null, the {nameof(BaseNinjascript)} object never entry on event driven methods.");
                if (bars == null)
                    Console.WriteLine($"If {nameof(bars)} parameter is null, the {nameof(BaseNinjascript)} object cannot use some objects. For example the \"SessionsIterator\" object cannot be used.");
            }
        }

        /// <summary>
        /// Free the memory of the script.
        /// </summary>
        public virtual void Terminated() { }

        #endregion

        #region Market Data methods

        /// <summary>
        /// Event driven method which is called whenever a bar is updated. 
        /// The frequency in which OnBarUpdate is called will be determined by the "Calculate" property. 
        /// OnBarUpdate() is the method where all of your script's core bar based calculation logic should be contained.
        /// </summary>
        public virtual void OnBarUpdate()
        {
            if (!IsLoaded)
                throw new Exception(NinjascriptErrors.NinjascriptLoadedException);
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public virtual void OnMarketData()
        {
            if (!IsLoaded)
                throw new Exception(NinjascriptErrors.NinjascriptLoadedException);
        }

        #endregion

        #region Action delegates

        /// <summary>
        /// Delegate to execute in "OnBarUpdate" method.
        /// </summary>
        public Action BarUpdateAction;

        /// <summary>
        /// Delegate to execute in "OnMarketData" method.
        /// </summary>
        public Action MarketDataAction;

        #endregion

        #region Delegate methods

        /// <summary>
        /// Execute the delegate in the OnBarUpdate method
        /// </summary>
        /// <param name="action">The delegate to execute.</param>
        protected void ExecuteInBarUpdateMethod(Action action)
        {
            action?.Invoke();
        }

        /// <summary>
        /// Execute the delegate in the OnBarUpdate method
        /// </summary>
        /// <param name="action">The delegate to execute.</param>
        protected void ExecuteInMarketDataMethod(Action action)
        {
            action?.Invoke();
        }

        #endregion

    }


    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <typeparam name="TScript">The ninjascript object.</typeparam>
    /// <typeparam name="TOptions">The ninjascript options object.</typeparam>
    /// <typeparam name="TBuilder">The ninjascript bulder object.</typeparam>
    public abstract class BaseNinjascript<TScript, TOptions,TBuilder> : BaseNinjascript, INinjascript
        where TScript : BaseNinjascript<TScript,TOptions,TBuilder>, INinjascript
        where TOptions : BaseOptions<TOptions>, IOptions
        where TBuilder : BaseBuilder<TScript,TOptions,TBuilder>, IBuilder
    {

        #region Private members

        /// <summary>
        /// The script configure.
        /// </summary>
        protected TOptions configuration;

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the script configure.
        /// </summary>
        public IOptions Configuration => configuration;

        /// <summary>
        /// Indicates if the session is configured.
        /// </summary>
        public bool IsConfigured => Configuration != null;

        #endregion

        #region Configure methods

        /// <summary>
        /// Method to set default properties in the script in "OnStateChanged.Configure" method.
        /// </summary>
        /// <param name="ninjascript">The ninjascript parent object.</param>
        public override void SetDefault(NinjaScriptBase ninjascript)
        {
            try
            {
                Exception e = new Exception("\"SetDefault\" Exceptions: ");

                if (!IsConfigured)
                    e.Data["ConfigureException"] = NinjascriptErrors.NinjascriptConfigureException;

                if (ninjascript == null)
                    e.Data["NinjaTraderException"] = NinjascriptErrors.NinjaTraderNinjaScriptNullReferenceException;

                if (e.Data.Count > 0)
                    throw e;

                // Copy the new options
                ninjascript.Name = Configuration.Name;
                ninjascript.Calculate = Configuration.Calculate;
                ninjascript.BarsRequiredToPlot = Configuration.BarsRequiredToPlot;
            }
            catch(Exception e)
            {
                // TODO: Implementar ILogger para registrar los errores.
                Console.WriteLine(e);
                Console.WriteLine("  {0}", e.Data["ConfigureException"]);
                Console.WriteLine("  {0}", e.Data["NinjaTraderException"]);
            }
        }

        /// <summary>
        /// Sets the script configuration
        /// </summary>
        public void SetOptions(IOptions options, [CallerMemberName] string methodName = null)
        {
            if (methodName == ".ctor" || methodName == "Configure" || methodName == "Build")
                configuration = (TOptions)options;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates ninjascript builder to construct the script.
        /// </summary>
        /// <typeparam name="Script">The ninhascript to construct.</typeparam>
        /// <typeparam name="Builder">The ninjascript builder.</typeparam>
        /// <returns>The ninjascript builder.</returns>
        /// <exception cref="Exception">Any type is wrong.</exception>
        public virtual Builder CreateBuilder<Script,Builder>()
            where Script : INinjascript
            where Builder : IBuilder
        {
            // Make sure the types are correct.
            //if (typeof(Script) == typeof(TScript) && typeof(Builder) == typeof(TBuilder))
            if (typeof(Script).IsAssignableFrom(typeof(TScript)) && typeof(Builder).IsAssignableFrom(typeof(TBuilder)))
                return (Builder)CreateNinjascriptBuilderInstance((TScript)this);

            throw new Exception("The builder can not be created");
        }

        /// <summary>
        /// Returns the type of the script.
        /// </summary>
        /// <returns></returns>
        public Type GetScriptType()
        {
            return typeof(TScript);
        }

        /// <summary>
        /// Returns the type of the options.
        /// </summary>
        /// <returns></returns>
        public Type GetOptionsType()
        {
            return typeof(TOptions);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Creates a new <see cref="IBuilder"/> instance.
        /// </summary>
        /// <returns>The constructor can not be invoke.</returns>
        protected IBuilder CreateNinjascriptBuilderInstance(TScript script)
        {
            ConstructorInfo construct = typeof(TBuilder).GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[] { typeof(TScript) }, null);
            if (construct != null)
                return (TBuilder)construct.Invoke(new object[] { this });
            else
                throw new NullReferenceException();
        }

        #endregion

    }

}
