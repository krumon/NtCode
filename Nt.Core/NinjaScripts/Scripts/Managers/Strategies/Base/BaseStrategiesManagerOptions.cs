namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseStrategiesManagerOptions<TManagerOptions> : BaseManagerOptions<TManagerOptions>, IStrategiesManagerOptions<TManagerOptions>
        where TManagerOptions : BaseStrategiesManagerOptions<TManagerOptions>, new()
    {
    }
}
