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
    public abstract class BaseIndicatorsManager<TManagerScript, TManagerOptions,TManagerBuilder> : BaseManager<TManagerScript, TManagerOptions,TManagerBuilder>, IIndicatorsManager
        where TManagerScript : BaseIndicatorsManager<TManagerScript, TManagerOptions,TManagerBuilder>, IIndicatorsManager
        where TManagerOptions : BaseIndicatorsManagerOptions<TManagerOptions>, IIndicatorsManagerOptions
        where TManagerBuilder : BaseIndicatorsManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, IIndicatorsManagerBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseIndicatorsManager{TManagerScript, TManagerOptions, TManagerBuilder}"/> default instance.
        /// </summary>
        protected BaseIndicatorsManager() : base()
        {
        }

        #endregion

    }

}
