namespace Nt.Core
{
    /// <summary>
    /// Interface for any strategies manager.
    /// </summary>
    public interface IStrategiesManager : IManager
    {
    }

    /// <summary>
    /// Interface for any strategies manager.
    /// </summary>
    public interface IStrategiesManager<TScript,TOptions> : IManager<TScript,TOptions>, IStrategiesManager
        where TScript : IStrategiesManager<TScript,TOptions>
        where TOptions : IOptions<TOptions>
    {
    }

}
