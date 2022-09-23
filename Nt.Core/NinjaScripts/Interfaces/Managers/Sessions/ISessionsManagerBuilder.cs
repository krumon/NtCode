namespace Nt.Core
{

    /// <summary>
    /// Interface for any sessions manager.
    /// </summary>
    public interface ISessionsManagerBuilder : IManagerBuilder
    {
    }

    /// <summary>
    /// Interface for any sessions manager.
    /// </summary>
    public interface ISessionsManagerBuilder<TSessionsManager,TSessionsManagerOptions,TSessionManagerBuilder> : IManagerBuilder<TSessionsManager,TSessionsManagerOptions,TSessionManagerBuilder>, ISessionsManagerBuilder
        where TSessionsManager : ISessionsManager<TSessionsManager,TSessionsManagerOptions,TSessionManagerBuilder>
        where TSessionsManagerOptions : ISessionsManagerOptions<TSessionsManagerOptions>
        where TSessionManagerBuilder : ISessionsManagerBuilder<TSessionsManager,TSessionsManagerOptions,TSessionManagerBuilder>
    {
    }

}
