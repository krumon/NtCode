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
    public interface IManagerBuilder<TManager,TManagerOptions> : IBuilder<TManager,TManagerOptions>, IManagerBuilder
        where TManager : IManager<TManager,TManagerOptions>
        where TManagerOptions : IManagerOptions<TManagerOptions>
    {
    }

}
