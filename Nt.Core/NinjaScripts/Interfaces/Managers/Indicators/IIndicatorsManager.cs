namespace Nt.Core
{

    /// <summary>
    /// Interface for any indicators manager.
    /// </summary>
    public interface IIndicatorsManager : IManager
    {
    }

    /// <summary>
    /// Interface for any indicators manager.
    /// </summary>
    public interface IIndicatorsManager<TManagerScript,TManagerOptions,TManagerBuilder> : IManager<TManagerScript,TManagerOptions,TManagerBuilder>, IIndicatorsManager
        where TManagerScript : IIndicatorsManager<TManagerScript,TManagerOptions,TManagerBuilder>
        where TManagerOptions : IIndicatorsManagerOptions<TManagerOptions>
        where TManagerBuilder : IIndicatorsManagerBuilder<TManagerScript, TManagerOptions, TManagerBuilder>
    {
    }

}
