using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript sessions
    /// </summary>
    public abstract class BaseSession : BaseScript
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
    /// <typeparam name="TSession">The session type</typeparam>
    /// <typeparam name="TOptions">The session options type to configure the object.</typeparam>
    public abstract class BaseSession<TSession,TOptions> : BaseScript<TSession,TOptions>
        where TSession : BaseSession<TSession,TOptions>, new()
        where TOptions : BaseOptions, new()
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

}
