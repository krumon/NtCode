namespace Nt.Core
{

    /// <summary>
    /// Interface for any sessions manager options.
    /// </summary>
    public interface ISessionsManagerOptions : IManagerOptions
    {
    }

    /// <summary>
    /// Interface for any indicator options.
    /// </summary>
    public interface ISessionsManagerOptions<TSessionsManagerOptions> : IManagerOptions<TSessionsManagerOptions>, ISessionsManagerOptions
        where TSessionsManagerOptions : ISessionsManagerOptions<TSessionsManagerOptions>
    {
    }

}
