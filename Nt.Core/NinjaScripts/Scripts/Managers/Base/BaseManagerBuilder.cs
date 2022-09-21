using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascripts manager builders.
    /// </summary>
    public abstract class BaseManagerBuilder<TManagerScript, TManagerOptions> : BaseBuilder<TManagerScript, TManagerOptions>
        where TManagerScript : BaseManager<TManagerScript, TManagerOptions>, new()
        where TManagerOptions : BaseManagerOptions<TManagerOptions>, new()
    {
    }

}
