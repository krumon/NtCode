using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{

    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    public abstract class BaseNinjascript : BaseElement
    {
        #region Private members

        /// <summary>
        /// The ninjascript parent of the class.
        /// </summary>
        protected NinjaScriptBase ninjascript;

        /// <summary>
        /// The bars of the chart control.
        /// </summary>
        protected Bars bars;

        #region Public properties

        /// <summary>
        /// The ninjascript parent of the class.
        /// </summary>
        public NinjaScriptBase Ninjascript => ninjascript;

        /// <summary>
        /// The bars of the chart control.
        /// </summary>
        public Bars Bars => bars;

        #endregion

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
        /// Loaded the Script in OnStateChanged method.
        /// </summary>
        /// <param name="ninjascript">The parent ninjascript object.</param>
        /// <param name="bars">The chart bars object loaded.</param>
        public virtual void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Make sure the parameters are not null.
            if (ninjascript == null || bars == null)
                throw new Exception($"{nameof(BaseNinjascript)} load parameters can not be null"); // return null;

            // Set values.
            this.ninjascript = ninjascript;
            this.bars = bars;

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
        }

        /// <summary>
        /// Event driven method which is called and guaranteed to be in the correct sequence 
        /// for every change in level one market data for the underlying instrument. 
        /// OnMarketData() can include but is not limited to the bid, ask, last price and volume.
        /// </summary>
        public virtual void OnMarketData()
        {
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Automapper from <see cref="BaseNinjascript"/> to <see cref="NinjaScriptBase"/>.
        /// </summary>
        /// <param name="ninjascript"></param>
        //protected virtual void SetNinjascriptProperties(NinjaScriptBase ninjascript)
        //{
        //}

        #endregion

    }

    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <typeparam name="TScript">The ninjascript.</typeparam>
    /// <typeparam name="TOptions">The ninjascript options.</typeparam>
    public abstract class BaseNinjascript<TScript, TOptions> : BaseNinjascript, INinjascript<TScript,TOptions>
        where TScript : BaseNinjascript<TScript,TOptions>, new()
        where TOptions : BaseNinjascriptOptions<TOptions>, new()
    {
        #region Protected members

        /// <summary>
        /// Indicates if the session is configured
        /// </summary>
        protected bool isConfigured;

        #endregion

        #region Market data Delegates

        /// <summary>
        /// Delegate to execute in OnBarUpdate method.
        /// </summary>
        public Action BarUpdateAction;

        /// <summary>
        /// Delegate to execute in OnMarketData method.
        /// </summary>
        public Action MarketDataAction;

        #endregion

        #region Public properties and options

        /// <summary>
        /// Gets the script options
        /// </summary>
        public TOptions Options { get; } = new TOptions();

        #endregion

        #region State changed methods

        /// <summary>
        /// AddValues the <see cref="BaseNinjascript"/>.
        /// </summary>
        /// <param name="ninjascript">The parent ninjascript.</param>
        /// <param name="bars">The chart bars object.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Call parent method
            base.Load(ninjascript, bars);

            // Make sure the session is configured
            if (!isConfigured)
                Configure();
        }

        #endregion

        #region Configure methods

        /// <summary>
        /// Configure options into the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public TScript Configure(Action<TOptions> options)
        {
            // Create default session properties.
            var sessionOptions = new TOptions();

            // If options is not null...invoke delegate to update the configure options by the user.
            if (options != null)
                options.Invoke(sessionOptions);

            // Copy the options into the class options.
            sessionOptions.CopyTo(Options);

            // Update the configure flag
            if (!isConfigured)
                isConfigured = true;

            return (TScript)this;
        }

        /// <summary>
        /// Add properties to configure the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public TScript Configure(TOptions options = null)
        {
            // If properties is null...create a default properties...
            if (options == null)
                options = new TOptions();

            // Copy the options into the class options.
            options.CopyTo(Options);

            // Update the configure flag
            if (!isConfigured)
                isConfigured = true;

            return (TScript)this;
        }

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

        #region Builder methods

        /// <summary>
        /// Creates a script builder default instance.
        /// </summary>
        public static TBuilder CreateBuilder<TBuilder>()
            where TBuilder : BaseNinjascriptBuilder<TScript, TOptions>, new()
        {
            return new TBuilder();
        }

        #endregion

    }

}
