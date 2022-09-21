using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseManagerBuilder<TManagerScript, TManagerOptions> : BaseNinjascriptBuilder<TManagerScript, TManagerOptions>
        where TManagerScript : BaseManager<TManagerScript, TManagerOptions>, new()
        where TManagerOptions : BaseManagerOptions<TManagerOptions>, new()
    {


    }

}
