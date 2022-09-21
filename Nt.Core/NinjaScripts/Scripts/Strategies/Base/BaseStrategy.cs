using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// Base class for any ninjascript indicator.
    /// </summary>
    public abstract class BaseStrategy<TScript, TOptions> : BaseNinjascript<TScript, TOptions>
        where TScript : BaseStrategy<TScript, TOptions>, new()
        where TOptions : BaseStrategyOptions<TOptions>, new()
    {

    }
}
