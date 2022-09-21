namespace Nt.Core
{
    /// <summary>
    /// Interface for any strategy builder.
    /// </summary>
    public interface IStrategyBuilder<TScript,TOptions> : IScriptBuilder<TScript,TOptions>, IStrategy
        where TScript : IStrategyBuilder<TScript,TOptions>
        where TOptions : IStrategyOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any strategy builder.
    /// </summary>
    public interface IStrategyBuilder : IScriptBuilder
    {
    }
}
