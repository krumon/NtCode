namespace Nt.Core
{

    /// <summary>
    /// Interface for any manager builder.
    /// </summary>
    public interface IManagerBuilder : IBuilder
    {
    }

    /// <summary>
    /// Interface for any manager builder.
    /// </summary>
    public interface IManagerBuilder<TManager,TManagerOptions,TManagerBuilder> : IBuilder<TManager,TManagerOptions,TManagerBuilder>, IManagerBuilder
        where TManager : IManager<TManager,TManagerOptions,TManagerBuilder>
        where TManagerOptions : IManagerOptions<TManagerOptions>
        where TManagerBuilder : IManagerBuilder<TManager,TManagerOptions,TManagerBuilder>
    {
    }

}
