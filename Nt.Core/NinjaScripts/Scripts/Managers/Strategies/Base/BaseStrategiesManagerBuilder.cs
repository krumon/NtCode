using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseStrategiesManagerBuilder<TManagerScript, TManagerOptions,TManagerBuilder> : BaseManagerBuilder<TManagerScript, TManagerOptions,TManagerBuilder>, IStrategiesManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>
        where TManagerScript : BaseStrategiesManager<TManagerScript, TManagerOptions,TManagerBuilder>, new()
        where TManagerOptions : BaseStrategiesManagerOptions<TManagerOptions>, new()
        where TManagerBuilder : BaseStrategiesManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, new()
    {
    }

}
