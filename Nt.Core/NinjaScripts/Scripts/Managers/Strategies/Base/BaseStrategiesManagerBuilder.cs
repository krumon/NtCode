using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseStrategiesManagerBuilder<TManagerScript, TManagerOptions> : BaseBuilder<TManagerScript, TManagerOptions>
        where TManagerScript : BaseStrategiesManager<TManagerScript, TManagerOptions>, new()
        where TManagerOptions : BaseStrategiesManagerOptions<TManagerOptions>, new()
    {
    }

}
