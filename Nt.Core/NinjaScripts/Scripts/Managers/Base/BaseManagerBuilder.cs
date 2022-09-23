using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascripts manager builders.
    /// </summary>
    public abstract class BaseManagerBuilder<TManagerScript, TManagerOptions, TManagerBuilder> : BaseBuilder<TManagerScript, TManagerOptions, TManagerBuilder>, IManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>
        where TManagerScript : BaseManager<TManagerScript, TManagerOptions,TManagerBuilder>, new()
        where TManagerOptions : BaseManagerOptions<TManagerOptions>, new()
        where TManagerBuilder : BaseManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, new()
    {
    }

}
