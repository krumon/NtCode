namespace Nt.Core
{

    /// <summary>
    /// Interface for any manager options.
    /// </summary>
    public interface IManagerOptions : IOptions
    {
    }

    /// <summary>
    /// Interface for any manager options.
    /// </summary>
    public interface IManagerOptions<TOptions> : IOptions<TOptions>, IManagerOptions
        where TOptions : IManagerOptions<TOptions>
    {
    }

}
