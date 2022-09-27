namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseIndicatorsManagerBuilder<TManagerScript, TManagerOptions,TManagerBuilder> : BaseManagerBuilder<TManagerScript, TManagerOptions,TManagerBuilder>, IIndicatorsManagerBuilder
        where TManagerScript : BaseIndicatorsManager<TManagerScript, TManagerOptions, TManagerBuilder>, IIndicatorsManager
        where TManagerOptions : BaseIndicatorsManagerOptions<TManagerOptions>, IIndicatorsManagerOptions
        where TManagerBuilder : BaseIndicatorsManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, IIndicatorsManagerBuilder
    {
    }

}
