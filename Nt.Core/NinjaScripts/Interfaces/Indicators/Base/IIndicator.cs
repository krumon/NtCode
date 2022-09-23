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
    public interface IIndicator<TScript,TOptions,TBuilder> : INinjascript<TScript,TOptions,TBuilder>, IIndicator
        where TScript : IIndicator<TScript,TOptions,TBuilder>
        where TOptions : IIndicatorOptions<TOptions>
        where TBuilder : IIndicatorBuilder<TScript,TOptions,TBuilder>
    {
    }

}
