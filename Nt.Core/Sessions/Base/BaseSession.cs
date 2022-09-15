using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript sessions
    /// </summary>
    //public abstract class BaseSession : BaseScript
    //{
    //    #region Market data Delegates

    //    /// <summary>
    //    /// Delegate to execute in OnBarUpdate method.
    //    /// </summary>
    //    public Action BarUpdateAction;

    //    /// <summary>
    //    /// Delegate to execute in OnMarketData method.
    //    /// </summary>
    //    public Action MarketDataAction;

    //    /// <summary>
    //    /// Delegate to execute in OnSessionHoursChanged method.
    //    /// </summary>
    //    public Action SessionHoursChangedAction;

    //    #endregion

    //    #region Market data methods

    //    /// <summary>
    //    /// Event driven method which is called for every new session. 
    //    /// </summary>
    //    /// <param name="e"></param>
    //    public virtual void OnSessionChanged(SessionChangedEventArgs e)
    //    {
    //    }

    //    #endregion

    //    #region Delegate methods

    //    /// <summary>
    //    /// Execute the delegate in the OnBarUpdate method
    //    /// </summary>
    //    /// <param name="action">The delegate to execute.</param>
    //    protected void ExecuteInBarUpdateMethod(Action action)
    //    {
    //        action?.Invoke();
    //    }

    //    /// <summary>
    //    /// Execute the delegate in the OnBarUpdate method
    //    /// </summary>
    //    /// <param name="action">The delegate to execute.</param>
    //    protected void ExecuteInMarketDataMethod(Action action)
    //    {
    //        action?.Invoke();
    //    }

    //    /// <summary>
    //    /// Execute the delegate in the OnSessionHoursChanged method
    //    /// </summary>
    //    /// <param name="action">The delegate to execute.</param>
    //    protected void ExecuteInSessionHoursChangedMethod(Action action)
    //    {
    //        action?.Invoke();
    //    }

    //    #endregion

    //}

    /// <summary>
    /// The base class to ninjascript sessions
    /// </summary>
    /// <typeparam name="TSession">The session type</typeparam>
    /// <typeparam name="TOptions">The session options type to configure the object.</typeparam>
    public abstract class BaseSession<TSession,TOptions,TProperties> : BaseScript<TSession,TOptions, TProperties>
        where TSession : BaseSession<TSession,TOptions,TProperties>, new()
        where TOptions : SessionOptions, new()
        where TProperties : SessionProperties, new()
    {
        public List<TSession> UseSessions { get; set; } = new List<TSession>();

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

        #region Configure methods

        /// <summary>
        /// Add <see cref="SessionOptions"/> to Session configure.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public TSession ConfigureSession<T,O,P>(T session, Action<O,P> config = null)
            where T : BaseSession<T,O,P>, new()
            where O : TOptions, new()
            where P : TProperties, new()
        {
            // Make sure session filters is not null.
            if (session == null)
                session = new T();

            // Configure....options and properties
            session.Configure(config);

            return (TSession)this;
        }

        public TSession ConfigureUseSession(TSession session, TOptions options, TProperties properties)
        {
            if (UseSessions.Contains(session))

            if (session != null)
                session = new TSession();

            session.Configure(options, properties);

            UseSessions.Add(session);

            return (TSession)this;
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
