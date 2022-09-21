namespace Nt.Core
{
    /// <summary>
    /// Interface for any indicators manager.
    /// </summary>
    public interface IIndicatorsMAnager<TScript,TOptions> : IScriptsManager<TScript,TOptions>, IIndicator
        where TScript : IIndicatorsMAnager<TScript,TOptions>
        where TOptions : IIndicatorOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any indicators manager.
    /// </summary>
    public interface IIndicatorsManager : IScriptsManager
    {
    }
}
