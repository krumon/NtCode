using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class to indicator builders
    /// </summary>
    public abstract class BaseIndicatorBuilder<TScript,TOptions,TBuilder> : BaseBuilder<TScript,TOptions,TBuilder>, IIndicatorBuilder<TScript,TOptions,TBuilder>
        where TScript : BaseIndicator<TScript,TOptions,TBuilder>, new()
        where TOptions : BaseIndicatorOptions<TOptions>, new()
        where TBuilder : BaseIndicatorBuilder<TScript,TOptions,TBuilder>, new()
    {
    }
}
