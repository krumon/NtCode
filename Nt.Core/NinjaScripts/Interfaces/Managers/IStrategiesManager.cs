namespace Nt.Core
{
    /// <summary>
    /// Interface for any strategies manager.
    /// </summary>
    public interface IStrategiesManager<TScript,TOptions> : IScriptsManager<TScript,TOptions>, IStrategy
        where TScript : IStrategiesManager<TScript,TOptions>
        where TOptions : IStrategyOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any strategies manager.
    /// </summary>
    public interface IStrategiesManager : IScript
    {
    }
}
