namespace Nt.Core
{
    /// <summary>
    /// The base class to strategy builders
    /// </summary>
    public abstract class BaseStrategyBuilder<TScript,TOptions,TBuilder> : BaseBuilder<TScript,TOptions,TBuilder>, IStrategyBuilder<TScript,TOptions,TBuilder>
        where TScript : BaseStrategy<TScript,TOptions,TBuilder>, new()
        where TOptions : BaseStrategyOptions<TOptions>, new()
        where TBuilder : BaseStrategyBuilder<TScript,TOptions,TBuilder>, new()
    {
    }
}
