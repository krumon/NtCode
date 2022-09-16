using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The generic class for session options
    /// </summary>
    public abstract class BaseManagerOptions<TOptions> : BaseScriptOptions<TOptions>
        where TOptions : BaseManagerOptions<TOptions>, new()
    {
    }
}
