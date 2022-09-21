namespace Nt.Core
{

    /// <summary>
    /// Interface for any indicator builder.
    /// </summary>
    public interface IManagerBuilder : IBuilder
    {
    }

    /// <summary>
    /// Interface for any indicator builder.
    /// </summary>
    public interface IManagerBuilder<TScript,TOptions> : IBuilder<TScript,TOptions>, IManagerBuilder
        where TScript : IIndicatorBuilder<TScript,TOptions>
        where TOptions : IIndicatorOptions<TOptions>
    {
    }

}
