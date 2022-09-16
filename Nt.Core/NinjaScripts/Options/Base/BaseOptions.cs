using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class for all ninjascripts options to configure.
    /// </summary>
    public abstract class BaseOptions<TOptions,TNinjaScript>
        where TOptions : BaseOptions<TOptions,TNinjaScript>, new()
        where TNinjaScript : NinjaScriptBase
    {

        #region Public methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public virtual void CopyTo(TOptions options)
        {
        }

        /// <summary>
        /// Copy options to ninjatrader properties
        /// </summary>
        /// <param name="ninjascript"></param>
        public virtual void CopyTo(TNinjaScript ninjascript)
        {
        }

        #endregion

    }
}
