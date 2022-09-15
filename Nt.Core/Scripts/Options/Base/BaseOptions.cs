using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class for all options
    /// </summary>
    public abstract class BaseOptions<T>
        where T : BaseOptions<T>, new()
    {
        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public virtual void CopyTo(T options)
        {
        }

        /// <summary>
        /// Copy options to ninjatrader properties
        /// </summary>
        /// <param name="ninjascript"></param>
        public virtual void CopyTo(NinjaScriptBase ninjascript)
        {
        }

    }
}
