namespace Nt.Core
{
    /// <summary>
    /// Interface for any indicator.
    /// </summary>
    public interface IIndicator<TScript,TOptions> : IScript<TScript,TOptions>, IIndicator
        where TScript : IIndicator<TScript,TOptions>
        where TOptions : IIndicatorOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any indicator.
    /// </summary>
    public interface IIndicator : IScript
    {
    }
}
