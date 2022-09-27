using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class to indicator builders
    /// </summary>
    public abstract class BaseIndicatorBuilder<TScript,TOptions,TBuilder> : BaseBuilder<TScript,TOptions,TBuilder>, IIndicatorBuilder
        where TScript : BaseIndicator<TScript,TOptions,TBuilder>, IIndicator
        where TOptions : BaseIndicatorOptions<TOptions>, IIndicatorOptions
        where TBuilder : BaseIndicatorBuilder<TScript,TOptions,TBuilder>, IIndicatorBuilder
    {
    }
}
