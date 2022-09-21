namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseStrategiesManagerOptions<TOptions> : BaseStrategyOptions<TOptions>
        where TOptions : BaseStrategiesManagerOptions<TOptions>, new()
    {
    }
}
