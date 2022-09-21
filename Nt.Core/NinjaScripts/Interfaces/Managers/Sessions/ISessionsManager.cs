namespace Nt.Core
{

    /// <summary>
    /// Interface for any sessions manager.
    /// </summary>
    public interface ISessionsManager : IManager
    {
    }

    /// <summary>
    /// Interface for any sessions manager.
    /// </summary>
    public interface ISessionsManager<TScript,TOptions> : IManager<TScript,TOptions>, ISessionsManager
        where TScript : IManager<TScript,TOptions>
        where TOptions : IManagerOptions<TOptions>
    {
    }

}
