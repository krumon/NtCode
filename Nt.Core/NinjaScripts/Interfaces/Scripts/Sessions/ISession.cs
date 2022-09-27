using System;

namespace Nt.Core
{

    /// <summary>
    /// Interface for any script session.
    /// </summary>
    public interface ISession : INinjascript
    {
        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        void OnSessionChanged(SessionChangedEventArgs e);

    }

    ///// <summary>
    ///// Interface for any script session.
    ///// </summary>
    //public interface ISession<TScript,TOptions,TBuilder> : INinjascript<TScript,TOptions,TBuilder>
    //    where TScript : ISession<TScript,TOptions,TBuilder>
    //    where TOptions : ISessionOptions<TOptions>
    //    where TBuilder : ISessionBuilder<TScript, TOptions, TBuilder>
    //{
    //}

    //public interface ISession : ISession<ISession, ISessionOptions, ISessionBuilder>
    //{

    //}

}
