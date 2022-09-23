namespace Nt.Core
{
    /// <summary>
    /// The base class to ninjascripts manager options.
    /// </summary>
    public abstract class BaseManagerOptions<TOptions> : BaseOptions<TOptions>, IManagerOptions<TOptions>
        where TOptions : BaseManagerOptions<TOptions>, new()
    {
    }
}
