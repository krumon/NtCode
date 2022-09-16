using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The generic class for session options
    /// </summary>
    public abstract class BaseManagerOptions<T> : BaseScriptOptions<T>
        where T : BaseManagerOptions<T>, new()
    {
    }
}
