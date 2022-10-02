using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript sessions
    /// </summary>
    /// <typeparam name="TSession">The session type</typeparam>
    /// <typeparam name="TSessionOptions">The session options type to configure the object.</typeparam>
    /// <typeparam name="TSessionBuilder">The session builder to construct the object.</typeparam>
    public abstract class BaseSession<TSession,TSessionOptions,TSessionBuilder> : BaseNinjascript<TSession,TSessionOptions,TSessionBuilder>, ISession
        where TSession : BaseSession<TSession,TSessionOptions,TSessionBuilder>, ISession
        where TSessionOptions : BaseSessionOptions<TSessionOptions>, ISessionOptions
        where TSessionBuilder : BaseSessionBuilder<TSession,TSessionOptions,TSessionBuilder>, ISessionBuilder
    {


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
        /// Add <see cref="BaseSessionOptions{T}"/> to Session configure.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        //public TSession ConfigureSession<T,O>(T session, Action<O> options = null)
        //    where T : BaseSession<TSession, TOptions>, new()
        //    where O : TOptions, new()
        //{
        //    // Make sure session filters is not null.
        //    if (session == null)
        //        session = new T();

        //    // Configure....options and properties
        //    session.Configure(options);

        //    return (TSession)this;
        //}

        #endregion

        #region Constructors

        ///// <summary>
        ///// Creates <see cref="BaseSession{TSession, TSessionOptions, TSessionBuilder}"/> default instance.
        ///// </summary>
        //public BaseSession() : base()
        //{
        //}

        #endregion

        #region Market data Delegates

        /// <summary>
        /// Delegate to execute in OnSessionHoursChanged method.
        /// </summary>
        public Action SessionChangedAction;

        #endregion

        #region Delegate methods

        /// <summary>
        /// Execute the delegate in the OnSessionHoursChanged method
        /// </summary>
        /// <param name="action">The delegate to execute.</param>
        protected void ExecuteInSessionChangedMethod(Action action)
        {
            action?.Invoke();
        }

        #endregion

    }

}
