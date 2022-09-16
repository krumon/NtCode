using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class for all ninjascripts options to configure.
    /// </summary>
    public abstract class BaseOptions<T,N>
        where T : BaseOptions<T,N>, new()
        where N : NinjaScriptBase
    {

        #region Public methods

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
        public virtual void CopyTo(N ninjascript)
        {
        }

        #endregion

    }
}
