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
    public interface IIndicatorBuilder<TScript,TOptions,TBuilder> : IBuilder<TScript,TOptions,TBuilder>, IIndicatorBuilder
        where TScript : IIndicator<TScript,TOptions,TBuilder>
        where TOptions : IIndicatorOptions<TOptions>
        where TBuilder : IIndicatorBuilder<TScript,TOptions,TBuilder>
    {
    }

}
