using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class to strategy builders
    /// </summary>
    public abstract class BaseStrategyBuilder<TScript,TOptions> : BaseBuilder<TScript,TOptions>
        where TScript : BaseStrategy<TScript,TOptions>, new()
        where TOptions : BaseStrategyOptions<TOptions>, new()
    {
    }
}
