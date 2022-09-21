namespace Nt.Core
{
    /// <summary>
    /// Interface for any session options.
    /// </summary>
    public interface ISessionOptions<TOptions> : IOptions<TOptions>, ISessionOptions
        where TOptions : ISessionOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any session options.
    /// </summary>
    public interface ISessionOptions : IOptions
    {
    }
}
