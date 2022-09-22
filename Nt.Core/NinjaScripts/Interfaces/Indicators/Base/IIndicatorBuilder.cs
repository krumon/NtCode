namespace Nt.Core
{

    /// <summary>
    /// Interface for any indicator builder.
    /// </summary>
    public interface IIndicatorBuilder : IBuilder
    {
    }

    /// <summary>
    /// Interface for any indicator builder.
    /// </summary>
    public interface IIndicatorBuilder<TScript,TOptions> : IBuilder<TScript,TOptions>, IIndicatorBuilder
        where TScript : IIndicator<TScript,TOptions>
        where TOptions : IIndicatorOptions<TOptions>
    {
    }

}
