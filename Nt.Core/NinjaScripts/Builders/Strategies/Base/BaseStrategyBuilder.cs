﻿using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class to script builders
    /// </summary>
    public abstract class BaseStrategyBuilder<TScript,TOptions> : BaseNinjascriptBuilder<TScript,TOptions>
        where TScript : BaseStrategy<TScript,TOptions>, new()
        where TOptions : BaseStrategyOptions<TOptions>, new()
    {

        #region Public methods

        /// <summary>
        /// Method to build any script.
        /// </summary>
        /// <returns>The script object created by the builder.</returns>
        public override TScript Build(NinjaScriptBase ninjascript)
        {
            // Build the parent script
            return base.Build(ninjascript);
        }

        #endregion
    }
}