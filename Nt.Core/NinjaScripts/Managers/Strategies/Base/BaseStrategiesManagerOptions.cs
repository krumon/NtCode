namespace Nt.Core.Ninjascript
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseStrategiesManagerOptions<TManagerOptions> : BaseManagerOptions<TManagerOptions>, IStrategiesManagerOptions
        where TManagerOptions : BaseStrategiesManagerOptions<TManagerOptions>, IStrategiesManagerOptions
    {
    }
}
