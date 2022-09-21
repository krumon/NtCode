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
    public interface IIndicatorsManager<TScript,TOptions> : IManager<TScript,TOptions>, IIndicatorsManager
        where TScript : IIndicatorsManager<TScript,TOptions>
        where TOptions : IIndicatorOptions<TOptions>
    {
    }

}
