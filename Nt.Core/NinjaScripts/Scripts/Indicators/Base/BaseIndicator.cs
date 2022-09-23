using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// Base class for any ninjascript indicator.
    /// </summary>
    public abstract class BaseIndicator<TScript, TOptions,TBuilder> : BaseNinjascript<TScript, TOptions, TBuilder>, IIndicator<TScript, TOptions,TBuilder>
        where TScript : BaseIndicator<TScript, TOptions,TBuilder>, new()
        where TOptions : BaseIndicatorOptions<TOptions>, new()
        where TBuilder : BaseIndicatorBuilder<TScript,TOptions,TBuilder>, new()
    {
    }
}
