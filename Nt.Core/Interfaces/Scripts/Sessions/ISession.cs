using System;

namespace Nt.Core
{

    /// <summary>
    /// Interface for any session script.
    /// </summary>
    public interface ISession : INinjascript
    {
        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        void OnSessionChanged(SessionChangedEventArgs e);

    }

    /// <summary>
    /// Interface for any sessions iterator.
    /// </summary>
    public interface ISessionsIterator : ISession
    {

        /// <summary>
        /// Event thats is raised when the sessoin changed.
        /// </summary>
        event Action<SessionChangedEventArgs> SessionChanged;

    }
}
