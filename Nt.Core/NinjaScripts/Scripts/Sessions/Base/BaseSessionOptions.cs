namespace Nt.Core
{
    /// <summary>
    /// The generic class for session options
    /// </summary>
    public abstract class BaseSessionOptions<TOptions> : BaseOptions<TOptions>, ISessionOptions<TOptions>
        where TOptions : BaseSessionOptions<TOptions>, new()
    {
    }

    /// <summary>
    /// The generic class for session options
    /// </summary>
    public class SessionOptions : BaseSessionOptions<SessionOptions>, ISessionOptions
    {
        public void CopyTo(ISessionOptions options)
        {
            base.CopyTo((SessionOptions)options);
        }
    }
}
