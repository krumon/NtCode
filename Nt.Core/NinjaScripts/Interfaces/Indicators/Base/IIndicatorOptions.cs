namespace Nt.Core
{
    /// <summary>
    /// Interface for any indicator options.
    /// </summary>
    public interface IIndicatorOptions<TOptions> : IScriptOptions<TOptions>, IIndicatorOptions
        where TOptions : IIndicatorOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any indicator options.
    /// </summary>
    public interface IIndicatorOptions : IScriptOptions
    {
    }
}
