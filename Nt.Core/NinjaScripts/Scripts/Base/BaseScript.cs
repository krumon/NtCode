using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using System;

namespace Nt.Core
{
    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <typeparam name="TScript">The ninjascript.</typeparam>
    /// <typeparam name="TOptions">The ninjascript options.</typeparam>
    public abstract class BaseScript<TScript, TOptions> : BaseNinjascript<TScript,TOptions, NinjaScriptBase>
        where TScript : BaseScript<TScript,TOptions>, new()
        where TOptions : BaseScriptOptions<TOptions>, new()
    {
    }
}
