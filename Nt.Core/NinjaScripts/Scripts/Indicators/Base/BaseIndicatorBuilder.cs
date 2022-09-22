using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class to indicator builders
    /// </summary>
    public abstract class BaseIndicatorBuilder<TScript,TOptions> : BaseBuilder<TScript,TOptions>
        where TScript : BaseIndicator<TScript,TOptions>, new()
        where TOptions : BaseIndicatorOptions<TOptions>, new()
    {
    }
}
