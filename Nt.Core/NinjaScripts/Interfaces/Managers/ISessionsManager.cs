namespace Nt.Core
{
    /// <summary>
    /// Interface for any sessions manager.
    /// </summary>
    public interface ISessionsManager<TScript,TOptions> : IScriptsManager<TScript,TOptions>, ISession
        where TScript : IScriptsManager<TScript,TOptions>
        where TOptions : IScriptOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any sessions manager.
    /// </summary>
    public interface ISessionsManager : IScriptsManager
    {
    }
}
