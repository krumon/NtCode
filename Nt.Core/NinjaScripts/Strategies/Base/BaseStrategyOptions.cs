namespace Nt.Core.Ninjascript
{
    /// <summary>
    /// The base class to strategy options.
    /// </summary>
    public abstract class BaseStrategyOptions<TOptions> : BaseOptions<TOptions>, IStrategyOptions
        where TOptions : BaseStrategyOptions<TOptions>, IStrategyOptions
    {
    }
}
