namespace Nt.Core
{
    /// <summary>
    /// The interfece for any ninjascripts manager.
    /// </summary>
    public interface IManager : INinjascript
    {
    }

    /// <summary>
    /// The interfece for any ninjascripts manager.
    /// </summary>
    public interface IManager<TManager,TManagerOptions,TManagerBuilder> : INinjascript<TManager,TManagerOptions,TManagerBuilder>, IManager
        where TManager : IManager<TManager, TManagerOptions,TManagerBuilder>
        where TManagerOptions : IManagerOptions<TManagerOptions>
        where TManagerBuilder : IManagerBuilder<TManager, TManagerOptions,TManagerBuilder>
    {
    }
}
