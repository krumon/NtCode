using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// Base class for any strategy.
    /// </summary>
    public abstract class BaseStrategy<TScript, TOptions,TBuilder> : BaseNinjascript<TScript, TOptions, TBuilder>, IStrategy
        where TScript : BaseStrategy<TScript, TOptions,TBuilder>, IStrategy
        where TOptions : BaseStrategyOptions<TOptions>, IStrategyOptions
        where TBuilder : BaseStrategyBuilder<TScript,TOptions,TBuilder>, IStrategyBuilder
    {

        #region Constructors

        ///// <summary>
        ///// Creates <see cref="BaseStrategy{TScript, TOptions, TBuilder}"/> default instance.
        ///// </summary>
        //protected BaseStrategy() : base()
        //{
        //}

        #endregion

    }
}
