using NinjaTrader.NinjaScript;

namespace Nt.Core
{
    /// <summary>
    /// The base class for all ninjascripts options to configure.
    /// </summary>
    public abstract class BaseOptions<TOptions>
        where TOptions : BaseOptions<TOptions>, new()
    {

        #region Public methods

        /// <summary>
        /// Copy options to ninjascript options
        /// </summary>
        /// <param name="options"></param>
        public virtual void CopyTo(TOptions options)
        {
        }

        #endregion

    }


}
