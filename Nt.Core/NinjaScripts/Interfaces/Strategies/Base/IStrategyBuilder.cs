namespace Nt.Core
{
    /// <summary>
    /// Interface for any strategy builder.
    /// </summary>
    public interface IStrategyBuilder<TScript,TOptions> : IBuilder<TScript,TOptions>, IStrategy
        where TScript : IStrategyBuilder<TScript,TOptions>
        where TOptions : IStrategyOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any strategy builder.
    /// </summary>
    public interface IStrategyBuilder : IBuilder
    {
    }
}
