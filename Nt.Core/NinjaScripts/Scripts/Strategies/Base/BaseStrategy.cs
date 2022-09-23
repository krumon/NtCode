using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// Base class for any strategy.
    /// </summary>
    public abstract class BaseStrategy<TScript, TOptions,TBuilder> : BaseNinjascript<TScript, TOptions, TBuilder>, IStrategy<TScript,TOptions,TBuilder>
        where TScript : BaseStrategy<TScript, TOptions,TBuilder>, new()
        where TOptions : BaseStrategyOptions<TOptions>, new()
        where TBuilder : BaseStrategyBuilder<TScript,TOptions,TBuilder>, new()
    {
    }
}
