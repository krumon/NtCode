using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The indicator options
    /// </summary>
    public abstract class BaseStrategyOptions<TOptions> : BaseNinjascriptOptions<TOptions,StrategyBase>
        where TOptions : BaseStrategyOptions<TOptions>, new()
    {

        #region Private members / Default value


        #endregion

        #region Properties


        #endregion

        #region Public methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public override void CopyTo(TOptions options)
        {
            // Sets the parent options values.
            base.CopyTo(options);
        }

        /// <summary>
        /// Copy options to ninjatrader indicator properties
        /// </summary>
        /// <param name="ninjascript"></param>
        public sealed override void CopyTo(StrategyBase ninjascript)
        {
            base.CopyTo(ninjascript);
        }

        #endregion

    }
}
