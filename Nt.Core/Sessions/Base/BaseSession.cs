using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript sessions
    /// </summary>
    public abstract class BaseSession : NtScript
    {
        #region Public properties

        public bool IsConfigured { get; private set; }

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

        #region Configure Methods

        /// <summary>
        /// Add options to configure the session.
        /// </summary>
        /// <typeparam name="TSession"></typeparam>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public TSession Configure<TSession,TOptions>(Action<TOptions> options)
            where TSession : BaseSession, new()
            where TOptions : class,new()
        {
            // Create default session options.
            var sessionOptions = new TOptions();

            // If options is not null...invoke delegate to update the options configure by the user.
            if (options != null)
                options.Invoke(sessionOptions);

            // Mapper the sesion filters with the session filters options.
            Mapper(sessionOptions);

            // Update the configure flag
            if (!IsConfigured)
                IsConfigured = true;

            return (TSession)this;
        }

        /// <summary>
        /// Add options to configure the session.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public TSession Configure<TSession,TOptions>(TOptions options = null)
            where TSession : BaseSession, new()
            where TOptions : class,new()
        {
            // If options is null...create a default options...
            if (options == null)
                options = new TOptions();

            // Mapper the sesion filters with the session filters options.
            Mapper(options);

            // Update the configure flag
            if (!IsConfigured)
                IsConfigured = true;

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

        #region Public methods

        protected virtual void Mapper<T>(T options)
        {
        }

        #endregion
        

    }
}
