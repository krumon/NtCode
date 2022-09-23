namespace Nt.Core
{

    /// <summary>
    /// The interfece for any ninjascript builder.
    /// </summary>
    public interface IBuilder
    {
    }

    /// <summary>
    /// The interfece for any ninjascript builder.
    /// </summary>
    /// <typeparam name="TScript"></typeparam>
    /// <typeparam name="TOptions"></typeparam>
    public interface IBuilder<TScript, TOptions, TBuilder> : IBuilder
        where TScript : INinjascript<TScript, TOptions, TBuilder>
        where TOptions : IOptions<TOptions>
        where TBuilder : IBuilder<TScript,TOptions, TBuilder>
    {
    }

}
