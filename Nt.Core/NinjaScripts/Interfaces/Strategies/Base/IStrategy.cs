namespace Nt.Core
{
    /// <summary>
    /// Interface for any script strategy.
    /// </summary>
    public interface IStrategy<TScript,TOptions> : INinjascript<TScript,TOptions>, IStrategy
        where TScript : IStrategy<TScript,TOptions>
        where TOptions : IStrategyOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any script strategy.
    /// </summary>
    public interface IStrategy : INinjascript
    {
    }
}
