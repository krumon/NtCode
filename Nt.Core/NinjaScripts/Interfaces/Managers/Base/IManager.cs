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
    public interface IManager<TManager,TManagerOptions> : INinjascript<TManager,TManagerOptions>, IManager
        where TManager : IManager<TManager, TManagerOptions>
        where TManagerOptions : IOptions<TManagerOptions>
    {
    }
}
