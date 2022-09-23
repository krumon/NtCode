namespace Nt.Core
{

    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <typeparam name="TScript">The ninjascript.</typeparam>
    /// <typeparam name="TOptions">The ninjascript options.</typeparam>
    public abstract class BaseStrategiesManager<TManagerScript, TManagerOptions,TManagerBuilder> : BaseManager<TManagerScript, TManagerOptions,TManagerBuilder>, IStrategiesManager<TManagerScript,TManagerOptions,TManagerBuilder>
        where TManagerScript : BaseStrategiesManager<TManagerScript, TManagerOptions,TManagerBuilder>, new()
        where TManagerOptions : BaseStrategiesManagerOptions<TManagerOptions>, new()
        where TManagerBuilder : BaseStrategiesManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, new()
    {
    }

}
