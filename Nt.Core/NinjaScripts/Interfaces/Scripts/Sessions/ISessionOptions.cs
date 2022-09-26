namespace Nt.Core
{
    /// <summary>
    /// Interface for any session options.
    /// </summary>
    public interface ISessionOptions<TOptions> : IOptions<TOptions>
        where TOptions : ISessionOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any session options.
    /// </summary>
    public interface ISessionOptions : ISessionOptions<ISessionOptions>
    {
    }
}
