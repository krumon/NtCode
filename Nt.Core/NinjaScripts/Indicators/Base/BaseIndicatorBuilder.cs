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

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseIndicatorBuilder{TScript, TOptions, TBuilder}"/> default instance.
        /// </summary>
        public BaseIndicatorBuilder(TOptions options) : base(options)
        {
            Options = options;
        }

        #endregion

    }
}
