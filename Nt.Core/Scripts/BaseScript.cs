using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    public abstract class BaseScript : BaseElement
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
                throw new Exception($"{nameof(BaseScript)} load parameters can not be null"); // return null;

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
        /// Automapper from <see cref="BaseScript"/> to <see cref="NinjaScriptBase"/>.
        /// </summary>
        /// <param name="ninjascript"></param>
        protected virtual void SetNinjascriptProperties(NinjaScriptBase ninjascript)
        {
        }

        #endregion

    }

    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <typeparam name="TScript">The ninjascript.</typeparam>
    /// <typeparam name="TOptions">The ninjascript options.</typeparam>
    public abstract class BaseScript<TScript, TOptions, TProperties> : BaseScript
        where TScript : BaseScript<TScript,TOptions, TProperties>
        where TOptions : ScriptOptions, new()
        where TProperties: ScriptProperties, new()
    {
        #region Protected members

        /// <summary>
        /// Indicates if the session is configured
        /// </summary>
        protected bool isConfigured;

        #endregion

        #region Public properties and options

        /// <summary>
        /// Gets the script properties
        /// </summary>
        public TProperties Properties { get; }

        /// <summary>
        /// Gets the script options
        /// </summary>
        public TOptions Options { get; }

        #endregion

        #region State changed methods

        /// <summary>
        /// AddValues the <see cref="BaseScript"/>.
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
        /// Add properties to configure the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public TScript Configure(Action<TOptions> options)
        {
            // Create default session properties.
            var sessionOptions = new TOptions();

            // If properties is not null...invoke delegate to update the properties configure by the user.
            if (options != null)
                options.Invoke(sessionOptions);

            // Mapper the sesion filters with the session filters properties.
            Mapper(sessionOptions);

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

            // Mapper the sesion filters with the session filters properties.
            Mapper(options);

            // Update the configure flag
            if (!isConfigured)
                isConfigured = true;

            return (TScript)this;
        }

        /// <summary>
        /// Add properties to configure the script.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public TScript Configure(Action<TProperties> properties)
        {
            // Create default session properties.
            var sessionProperties = new TProperties();

            // If properties is not null...invoke delegate to update the properties configure by the user.
            if (properties != null)
                properties.Invoke(sessionProperties);

            // Mapper the sesion filters with the session filters properties.
            Mapper(sessionProperties);

            return (TScript)this;
        }

        /// <summary>
        /// Add properties to configure the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public TScript Configure(TProperties properties)
        {
            // If properties is null...create a default prperties...
            if (properties == null)
                properties = new TProperties();

            // Set the properties.
            Mapper(properties);

            return (TScript)this;
        }

        /// <summary>
        /// Add properties to configure the script.
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public TScript Configure(Action<TOptions,TProperties> config)
        {
            // Create default session properties.
            var sessionProperties = new TProperties();
            var sessionOptions = new TOptions();

            // If properties is not null...invoke delegate to update the properties configure by the user.
            if (config != null)
                config.Invoke(sessionOptions,sessionProperties);

            // Mapper the sesion filters with the session filters properties.
            Mapper(sessionOptions);
            Mapper(sessionProperties);

            return (TScript)this;
        }

        /// <summary>
        /// Add properties to configure the script.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public TScript Configure(TOptions options, TProperties properties = null)
        {
            // If options is null...create a default options...
            if (properties == null)
                properties = new TProperties();

            // If properties is null...create a default properties...
            if (properties == null)
                properties = new TProperties();

            // Set the properties.
            Mapper(options);
            Mapper(properties);

            return (TScript)this;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Mapper the <see cref="BaseScript"/> from the <see cref="ScriptOptions"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        protected virtual void Mapper(TOptions options)
        {
        }

        /// <summary>
        /// Mapper the <see cref="BaseScript"/> from the <see cref="ScriptProperties"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        protected virtual void Mapper(TProperties properties)
        {
            Properties.Description = properties.Description;
            Properties.Name = properties.Name;
            Properties.Calculate = properties.Calculate;
        }

        #endregion

    }
}
