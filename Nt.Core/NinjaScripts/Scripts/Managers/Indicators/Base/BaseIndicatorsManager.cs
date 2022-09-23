using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System.Collections.Generic;

namespace Nt.Core
{

    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <typeparam name="TScript">The ninjascript.</typeparam>
    /// <typeparam name="TOptions">The ninjascript options.</typeparam>
    public abstract class BaseIndicatorsManager<TManagerScript, TManagerOptions,TManagerBuilder> : BaseManager<TManagerScript, TManagerOptions,TManagerBuilder>, IIndicatorsManager<TManagerScript,TManagerOptions,TManagerBuilder>
        where TManagerScript : BaseIndicatorsManager<TManagerScript, TManagerOptions,TManagerBuilder>, new()
        where TManagerOptions : BaseIndicatorsManagerOptions<TManagerOptions>, new()
        where TManagerBuilder : BaseIndicatorsManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, new()
    {
    }

}
