using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript sessions
    /// </summary>
    public abstract class BaseSession : NtScript
    {
        #region Market data Delegates

        /// <summary>
        /// Delegate to execute in OnBarUpdate method.
        /// </summary>
        public Action BarUpdateAction;

        /// <summary>
        /// Delegate to execute in OnMarketData method.
        /// </summary>
        public Action MarketDataAction;

        /// <summary>
        /// Delegate to execute in OnSessionHoursChanged method.
        /// </summary>
        public Action SessionHoursChangedAction;

        #endregion

        #region State changed methods

        /// <summary>
        /// Load the <see cref="SessionFilters"/>.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Make sure the parameters are not null.
            if (ninjascript == null || bars == null)
                throw new Exception($"{nameof(SessionFilters)} load parameters can not be null"); // return null;

            // Set values.
            this.ninjascript = ninjascript;
            this.bars = bars;

        }

        #endregion

        #region Market data methods

        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnSessionChanged(SessionChangedEventArgs e)
        {
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

        /// <summary>
        /// Execute the delegate in the OnSessionHoursChanged method
        /// </summary>
        /// <param name="action">The delegate to execute.</param>
        protected void ExecuteInSessionHoursChangedMethod(Action action)
        {
            action?.Invoke();
        }

        #endregion

    }

    /// <summary>
    /// The base class to ninjascript sessions
    /// </summary>
    /// <typeparam name="TOptions">The session options to configure the object.</typeparam>
    public abstract class BaseSession<TSession,TOptions> : BaseSession
        where TSession : BaseSession<TSession,TOptions>, new()
        where TOptions : BaseSessionOptions, new()
    {

        #region Protected members

        /// <summary>
        /// Indicates if the session is configured
        /// </summary>
        protected bool isConfigured;

        #endregion

        #region State changed methods

        /// <summary>
        /// Load the <see cref="SessionFilters"/>.
        /// </summary>
        /// <param name="ninjascript">The ninjascript.</param>
        /// <param name="bars">The bars.</param>
        public override void Load(NinjaScriptBase ninjascript, Bars bars)
        {
            // Call parent method
            base.Load(ninjascript, bars);

            // Make sure the session is configured
            if (!isConfigured)
                Configure();
        }

        #endregion

        #region Configure Methods

        /// <summary>
        /// Add options to configure the session.
        /// </summary>
        /// <typeparam name="TSession"></typeparam>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public TSession Configure(Action<TOptions> options)
        {
            // Create default session options.
            var sessionOptions = new TOptions();

            // If options is not null...invoke delegate to update the options configure by the user.
            if (options != null)
                options.Invoke(sessionOptions);

            // Mapper the sesion filters with the session filters options.
            Mapper(sessionOptions);

            // Update the configure flag
            if (!isConfigured)
                isConfigured = true;

            return (TSession)this;
        }

        /// <summary>
        /// Add options to configure the session.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public TSession Configure(TOptions options = null)
        {
            // If options is null...create a default options...
            if (options == null)
                options = new TOptions();

            // Mapper the sesion filters with the session filters options.
            Mapper(options);

            // Update the configure flag
            if (!isConfigured)
                isConfigured = true;

            return (TSession)this;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Mapper the <see cref="BaseSession"/> from the <see cref="BaseSessionOptions"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        protected virtual void Mapper(TOptions options)
        {
        }

        #endregion

    }

}
