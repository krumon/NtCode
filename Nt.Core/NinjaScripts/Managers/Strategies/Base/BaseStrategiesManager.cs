namespace Nt.Core.Ninjascript
{

    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <summary>
    /// Base class for any ninjascript.
    /// </summary>
    /// <typeparam name="TScript">The ninjascript.</typeparam>
    /// <typeparam name="TOptions">The ninjascript options.</typeparam>
    public abstract class BaseStrategiesManager<TManagerScript, TManagerOptions,TManagerBuilder> : BaseManager<TManagerScript, TManagerOptions,TManagerBuilder>, IStrategiesManager
        where TManagerScript : BaseStrategiesManager<TManagerScript, TManagerOptions,TManagerBuilder>, IStrategiesManager
        where TManagerOptions : BaseStrategiesManagerOptions<TManagerOptions>, IStrategiesManagerOptions
        where TManagerBuilder : BaseStrategiesManagerBuilder<TManagerScript,TManagerOptions,TManagerBuilder>, IStrategiesManagerBuilder
    {

        #region Constructors

        ///// <summary>
        ///// Creates <see cref="BaseStrategiesManager{TManagerScript, TManagerOptions, TManagerBuilder}"/> default instance.
        ///// </summary>
        //protected BaseStrategiesManager() : base()
        //{
        //}

        #endregion

    }

}
