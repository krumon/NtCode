using Nt.Core.Events;
using System;

namespace Nt.Core.Ninjascript
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

        #region Public methods

        /// <summary>
        /// Execute the ninjascript handler methods.
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="e"></param>
        public override void ExecuteHandlerMethod(EventType eventType, SessionChangedEventArgs e = null)
        {

            if (eventType == EventType.SessionChanged)
            {
                OnSessionChanged(e);
                return;
            }
            // Call to parent
            base.ExecuteHandlerMethod(eventType, e);
        }

        #endregion

    }

}
