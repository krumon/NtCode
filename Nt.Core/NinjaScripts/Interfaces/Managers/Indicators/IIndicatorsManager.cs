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
    public interface IIndicatorsManager<TIndicatorsManager,TIndicatorsManagerOptionsOptions> : IManager<TIndicatorsManager,TIndicatorsManagerOptionsOptions>, IIndicatorsManager
        where TIndicatorsManager : IIndicatorsManager<TIndicatorsManager,TIndicatorsManagerOptionsOptions>
        where TIndicatorsManagerOptionsOptions : IIndicatorsManagerOptions<TIndicatorsManagerOptionsOptions>
    {
    }

}
