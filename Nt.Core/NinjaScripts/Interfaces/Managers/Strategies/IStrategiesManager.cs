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
    public interface IStrategiesManager<TStrategiesManager,TStrategiesManagerOptions> : IManager<TStrategiesManager,TStrategiesManagerOptions>, IStrategiesManager
        where TStrategiesManager : IStrategiesManager<TStrategiesManager,TStrategiesManagerOptions>
        where TStrategiesManagerOptions : IStrategiesManagerOptions<TStrategiesManagerOptions>
    {
    }

}
