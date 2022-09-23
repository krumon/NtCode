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
    public interface IStrategiesManagerOptions<TManagerOptions> : IManagerOptions<TManagerOptions>, IStrategiesManagerOptions
        where TManagerOptions : IStrategiesManagerOptions<TManagerOptions>
    {
    }

}
