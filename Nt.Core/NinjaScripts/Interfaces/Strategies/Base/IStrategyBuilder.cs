namespace Nt.Core
{

    /// <summary>
    /// Interface for any strategy builder.
    /// </summary>
    public interface IStrategyBuilder : IBuilder
    {
    }

    /// <summary>
    /// Interface for any strategy builder.
    /// </summary>
    public interface IStrategyBuilder<TScript,TOptions,TBuilder> : IBuilder<TScript,TOptions,TBuilder>, IStrategyBuilder
        where TScript : IStrategy<TScript,TOptions,TBuilder>
        where TOptions : IStrategyOptions<TOptions>
        where TBuilder : IStrategyBuilder<TScript,TOptions,TBuilder>
    {
    }

}
