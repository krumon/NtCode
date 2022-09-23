namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascript builders
    /// </summary>
    public abstract class BaseIndicatorsManagerBuilder<TManagerScript, TManagerOptions,TManagerBuilder> : BaseManagerBuilder<TManagerScript, TManagerOptions,TManagerBuilder>, IIndicatorsManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>
        where TManagerScript : BaseIndicatorsManager<TManagerScript, TManagerOptions, TManagerBuilder>, new()
        where TManagerOptions : BaseIndicatorsManagerOptions<TManagerOptions>, new()
        where TManagerBuilder : BaseIndicatorsManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, new()
    {
    }

}
