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
    public interface ISessionsManager<TSessionsManager,TSessionsManagerOptions> : IManager<TSessionsManager,TSessionsManagerOptions>, ISessionsManager
        where TSessionsManager : ISessionsManager<TSessionsManager,TSessionsManagerOptions>
        where TSessionsManagerOptions : ISessionsManagerOptions<TSessionsManagerOptions>
    {
    }

}
