﻿using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class to strategy options.
    /// </summary>
    public abstract class BaseStrategyOptions<TOptions> : BaseOptions<TOptions>
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
        public void CopyTo(IndicatorBase ninjascript)
        {
            // Ninjascript properties
            CopyToNinjascript(ninjascript);
            // Strategy properties

        }

        #endregion

    }
}
