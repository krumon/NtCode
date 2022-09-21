namespace Nt.Core
{
    /// <summary>
    /// Interface for any script strategy.
    /// </summary>
    public interface IStrategy<TScript,TOptions> : IScript<TScript,TOptions>, IStrategy
        where TScript : IStrategy<TScript,TOptions>
        where TOptions : IStrategyOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any script strategy.
    /// </summary>
    public interface IStrategy : IScript
    {
    }
}
