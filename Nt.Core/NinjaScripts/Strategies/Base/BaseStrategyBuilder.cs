namespace Nt.Core
{
    /// <summary>
    /// The base class to strategy builders
    /// </summary>
    public abstract class BaseStrategyBuilder<TScript,TOptions,TBuilder> : BaseBuilder<TScript,TOptions,TBuilder>, IStrategyBuilder
        where TScript : BaseStrategy<TScript,TOptions,TBuilder>, IStrategy
        where TOptions : BaseStrategyOptions<TOptions>, IStrategyOptions
        where TBuilder : BaseStrategyBuilder<TScript,TOptions,TBuilder>, IStrategyBuilder
    {

        #region Constructors


        /// <summary>
        /// Creates <see cref="BaseStrategyBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public BaseStrategyBuilder(INinjascript script) : base(script)
        {
        }

        #endregion

    }
}
