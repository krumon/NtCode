namespace Nt.Core
{

    /// <summary>
    /// Interface for any indicator.
    /// </summary>
    public interface IIndicator : INinjascript
    {
    }

    /// <summary>
    /// Interface for any indicator.
    /// </summary>
    public interface IIndicator<TScript,TOptions> : INinjascript<TScript,TOptions>, IIndicator
        where TScript : IIndicator<TScript,TOptions>
        where TOptions : IIndicatorOptions<TOptions>
    {
    }

}
