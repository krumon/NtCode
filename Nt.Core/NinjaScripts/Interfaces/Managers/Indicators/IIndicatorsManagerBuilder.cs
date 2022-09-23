namespace Nt.Core
{

    /// <summary>
    /// Interface for any indicators manager builder.
    /// </summary>
    public interface IIndicatorsManagerBuilder : IManagerBuilder
    {
    }

    /// <summary>
    /// Interface for any indicators manager.
    /// </summary>
    public interface IIndicatorsManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder> : IManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, IIndicatorsManagerBuilder
        where TManagerScript : IIndicatorsManager<TManagerScript,TManagerOptions,TManagerBuilder>
        where TManagerOptions : IIndicatorsManagerOptions<TManagerOptions>
        where TManagerBuilder : IIndicatorsManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>
    {
    }

}
