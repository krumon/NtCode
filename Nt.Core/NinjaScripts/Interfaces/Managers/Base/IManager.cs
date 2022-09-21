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
    public interface IManager<TManager,TOptions> : INinjascript
        where TManager : IManager<TManager, TOptions>
        where TOptions : IOptions<TOptions>
    {
    }
}
