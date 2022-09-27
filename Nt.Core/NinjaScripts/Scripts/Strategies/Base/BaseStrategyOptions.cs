using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class to strategy options.
    /// </summary>
    public abstract class BaseStrategyOptions<TOptions> : BaseOptions<TOptions>, IStrategyOptions
        where TOptions : BaseStrategyOptions<TOptions>, IStrategyOptions
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
        public override void CopyTo(IOptions options)
        {
            TOptions op = (TOptions)options;

            // Sets the parent options values.
            base.CopyTo(op);
        }

        /// <summary>
        /// Copy options to ninjatrader indicator properties
        /// </summary>
        /// <param name="ninjascript"></param>
        public void CopyTo(IndicatorBase ninjascript)
        {
            // Ninjascript properties
            CopyToNinjascript(ninjascript);
            // Strategy properties

        }

        #endregion

    }
}
