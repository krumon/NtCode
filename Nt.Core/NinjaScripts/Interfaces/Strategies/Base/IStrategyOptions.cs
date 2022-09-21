namespace Nt.Core
{
    /// <summary>
    /// Interface for any script strategy options.
    /// </summary>
    public interface IStrategyOptions<TOptions> : IOptions<TOptions>, IStrategyOptions
        where TOptions : IStrategyOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any script strategy options.
    /// </summary>
    public interface IStrategyOptions : IOptions
    {
    }
}
