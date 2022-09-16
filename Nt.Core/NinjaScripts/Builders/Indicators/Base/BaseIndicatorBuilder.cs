using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class to script builders
    /// </summary>
    public abstract class BaseIndicatorBuilder<TScript,TOptions> : BaseNinjascriptBuilder<TScript,TOptions,IndicatorBase>
        where TScript : BaseIndicator<TScript,TOptions>, new()
        where TOptions : BaseIndicatorOptions<TOptions>, new()
    {

        #region Public methods

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <returns>The script object created by the builder.</returns>
        public override TScript Build(IndicatorBase ninjascript)
        {
            // Build the parent script
            return base.Build(ninjascript);
        }

        #endregion
    }
}
