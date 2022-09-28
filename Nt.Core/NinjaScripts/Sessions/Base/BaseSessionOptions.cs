namespace Nt.Core
{
    /// <summary>
    /// The generic class for session options
    /// </summary>
    public abstract class BaseSessionOptions<TSessionOptions> : BaseOptions<TSessionOptions>, ISessionOptions
        where TSessionOptions : BaseSessionOptions<TSessionOptions>, ISessionOptions
    {
    }
}
