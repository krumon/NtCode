using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseIndicatorsManagerBuilder<TManagerScript, TManagerOptions> : BaseNinjascriptBuilder<TManagerScript, TManagerOptions>
        where TManagerScript : BaseIndicatorsManager<TManagerScript, TManagerOptions>, new()
        where TManagerOptions : BaseIndicatorsManagerOptions<TManagerOptions>, new()
    {


    }

}
