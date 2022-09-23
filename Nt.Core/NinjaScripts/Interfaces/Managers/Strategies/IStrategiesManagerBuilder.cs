namespace Nt.Core
{
    /// <summary>
    /// Interface for any strategies manager builder.
    /// </summary>
    public interface IStrategiesManagerBuilder : IManagerBuilder
    {
    }

    /// <summary>
    /// Interface for any strategies manager builder.
    /// </summary>
    public interface IStrategiesManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder> : IManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, IStrategiesManagerBuilder
        where TManagerScript : IStrategiesManager<TManagerScript,TManagerOptions,TManagerBuilder>
        where TManagerOptions : IStrategiesManagerOptions<TManagerOptions>
        where TManagerBuilder : IStrategiesManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>
    {
    }

}
