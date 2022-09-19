using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseNinjascriptsManagerBuilder<TManagerScript, TManagerOptions> : BaseNinjascriptBuilder<TManagerScript, TManagerOptions>
        where TManagerScript : BaseNinjascriptsManager<TManagerScript, TManagerOptions>, new()
        where TManagerOptions : BaseNinjascriptsManagerOptions<TManagerOptions>, new()
    {


    }

}
