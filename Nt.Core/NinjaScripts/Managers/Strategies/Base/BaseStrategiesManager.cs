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
    public abstract class BaseStrategiesManager<TManagerScript, TManagerConfiguration,TManagerBuilder> : BaseManager<TManagerScript, TManagerConfiguration,TManagerBuilder>, IStrategiesManager
        where TManagerScript : BaseStrategiesManager<TManagerScript, TManagerConfiguration,TManagerBuilder>, IStrategiesManager
        where TManagerConfiguration : BaseStrategiesManagerConfiguration<TManagerConfiguration>, IStrategiesManagerConfiguration
        where TManagerBuilder : BaseStrategiesManagerBuilder<TManagerScript,TManagerConfiguration,TManagerBuilder>, IStrategiesManagerBuilder
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
