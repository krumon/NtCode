namespace Nt.Core
{

    /// <summary>
    /// Interface for any strategies manager options.
    /// </summary>
    public interface IStrategiesManagerOptions : IManagerOptions
    {
    }

    /// <summary>
    /// Interface for any strategies manager options.
    /// </summary>
    public interface IStrategiesManagerOptions<TStrategiesManagerOptions> : IManagerOptions<TStrategiesManagerOptions>, IStrategiesManagerOptions
        where TStrategiesManagerOptions : IStrategiesManagerOptions<TStrategiesManagerOptions>
    {
    }

}
