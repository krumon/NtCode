namespace Nt.Core
{
    /// <summary>
    /// The generic class for session options
    /// </summary>
    public abstract class BaseSessionOptions<TSessionOptions> : BaseOptions<TSessionOptions>, ISessionOptions<TSessionOptions>
        where TSessionOptions : BaseSessionOptions<TSessionOptions>, new()
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
