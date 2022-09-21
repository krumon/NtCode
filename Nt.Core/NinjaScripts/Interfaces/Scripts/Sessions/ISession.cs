namespace Nt.Core
{
    /// <summary>
    /// Interface for any script session.
    /// </summary>
    public interface ISession<TScript,TOptions> : INinjascript<TScript,TOptions>, ISession
        where TScript : INinjascript<TScript,TOptions>
        where TOptions : IOptions<TOptions>
    {
    }

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
}
