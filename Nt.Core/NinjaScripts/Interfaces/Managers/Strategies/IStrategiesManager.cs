﻿namespace Nt.Core
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
    public interface IStrategiesManager<TManagerScript,TManagerOptions,TManagerBuilder> : IManager<TManagerScript,TManagerOptions,TManagerBuilder>, IStrategiesManager
        where TManagerScript : IStrategiesManager<TManagerScript,TManagerOptions,TManagerBuilder>
        where TManagerOptions : IStrategiesManagerOptions<TManagerOptions>
        where TManagerBuilder : IStrategiesManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>
    {
    }

}
