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
    public interface ISessionsManager<TSessionsManager,TSessionsManagerOptions,TSessionsManagerBuilder> : IManager<TSessionsManager,TSessionsManagerOptions,TSessionsManagerBuilder>, ISessionsManager
        where TSessionsManager : ISessionsManager<TSessionsManager,TSessionsManagerOptions,TSessionsManagerBuilder>
        where TSessionsManagerOptions : ISessionsManagerOptions<TSessionsManagerOptions>
        where TSessionsManagerBuilder : ISessionsManagerBuilder<TSessionsManager,TSessionsManagerOptions,TSessionsManagerBuilder>
    {
    }

}
