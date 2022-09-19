using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseScriptsManagerBuilder<TManagerScript, TManagerOptions> : BaseNinjascriptBuilder<TManagerScript, TManagerOptions>
        where TManagerScript : BaseScriptsManager<TManagerScript, TManagerOptions>, new()
        where TManagerOptions : BaseScriptsManagerOptions<TManagerOptions>, new()
    {


    }

}
