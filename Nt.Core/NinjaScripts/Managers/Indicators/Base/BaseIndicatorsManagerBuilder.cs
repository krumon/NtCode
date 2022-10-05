using System.Collections.Generic;

namespace Nt.Core.Ninjascript
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseIndicatorsManagerBuilder<TManagerScript, TManagerOptions,TManagerBuilder> : BaseManagerBuilder<TManagerScript, TManagerOptions,TManagerBuilder>, IIndicatorsManagerBuilder
        where TManagerScript : BaseIndicatorsManager<TManagerScript, TManagerOptions, TManagerBuilder>, IIndicatorsManager
        where TManagerOptions : BaseIndicatorsManagerOptions<TManagerOptions>, IIndicatorsManagerOptions
        where TManagerBuilder : BaseIndicatorsManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, IIndicatorsManagerBuilder
    {

        #region Constructors

        /// <summary>
        /// Creates <see cref="BaseIndicatorsManagerBuilder"/> default instance.
        /// </summary>
        /// <param name="script">The script to build.</param>
        public BaseIndicatorsManagerBuilder(IManager script) : base(script)
        {
        }

        #endregion

    }

}
