using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core.Ninjascript
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseStrategiesManagerBuilder<TManagerScript, TManagerOptions,TManagerBuilder> : BaseManagerBuilder<TManagerScript, TManagerOptions,TManagerBuilder>, IStrategiesManagerBuilder
        where TManagerScript : BaseStrategiesManager<TManagerScript, TManagerOptions,TManagerBuilder>, IStrategiesManager
        where TManagerOptions : BaseStrategiesManagerOptions<TManagerOptions>, IStrategiesManagerOptions
        where TManagerBuilder : BaseStrategiesManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, IStrategiesManagerBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseStrategiesManagerBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public BaseStrategiesManagerBuilder(IManager script) : base(script)
        {
        }

        #endregion

    }

}
