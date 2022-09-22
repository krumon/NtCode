using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// Base class for any ninjascript indicator.
    /// </summary>
    public abstract class BaseIndicator<TScript, TOptions> : BaseNinjascript<TScript, TOptions>
        where TScript : BaseIndicator<TScript, TOptions>, new()
        where TOptions : BaseIndicatorOptions<TOptions>, new()
    {
    }
}
