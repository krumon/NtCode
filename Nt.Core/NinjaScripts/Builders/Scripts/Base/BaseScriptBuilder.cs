using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class to script builders
    /// </summary>
    public abstract class BaseScriptBuilder<TScript,TOptions> : BaseNinjascriptBuilder<TScript,TOptions,NinjaScriptBase>
        where TScript : BaseScript<TScript,TOptions>, new()
        where TOptions : BaseScriptOptions<TOptions>, new()
    {
    }
}
