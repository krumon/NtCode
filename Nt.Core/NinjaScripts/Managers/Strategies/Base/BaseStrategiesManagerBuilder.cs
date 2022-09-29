using NinjaTrader.NinjaScript;
using System;
using System.Collections.Generic;

namespace Nt.Core
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
        /// Creates <see cref="BaseStrategiesManagerBuilder{TManagerScript, TManagerOptions, TManagerBuilder}"/> default instance.
        /// </summary>
        public BaseStrategiesManagerBuilder(TManagerOptions options, List<INinjascript> scripts) : base(options,scripts)
        {
            Options = options;
            Scripts = scripts;
        }

        #endregion

    }

}
