namespace Nt.Core
{
    /// <summary>
    /// Interface for any session script.
    /// </summary>
    public interface ISession<TScript,TOptions> : IScript<TScript,TOptions>, ISession
        where TScript : IScript<TScript,TOptions>
        where TOptions : IScriptOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any session script.
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// Event driven method which is called for every new session. 
        /// </summary>
        /// <param name="e"></param>
        void OnSessionChanged(SessionChangedEventArgs e);
    }
}
