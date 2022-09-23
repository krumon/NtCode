namespace Nt.Core
{

    /// <summary>
    /// Interface for any script strategy.
    /// </summary>
    public interface IStrategy : INinjascript
    {
    }

    /// <summary>
    /// Interface for any script strategy.
    /// </summary>
    public interface IStrategy<TScript,TOptions,TBuilder> : INinjascript<TScript,TOptions,TBuilder>, IStrategy
        where TScript : IStrategy<TScript,TOptions,TBuilder>
        where TOptions : IStrategyOptions<TOptions>
        where TBuilder : IStrategyBuilder<TScript,TOptions,TBuilder>
    {
    }

}
