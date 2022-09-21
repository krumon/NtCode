namespace Nt.Core
{
    /// <summary>
    /// Interface for any indicator builder.
    /// </summary>
    public interface IIndicatorBuilder<TScript,TOptions> : IScriptBuilder<TScript,TOptions>, IIndicatorBuilder
        where TScript : IIndicatorBuilder<TScript,TOptions>
        where TOptions : IIndicatorOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any indicator builder.
    /// </summary>
    public interface IIndicatorBuilder : IScriptBuilder
    {
    }
}
