namespace Nt.Core
{
    /// <summary>
    /// The base class to session scripts builders
    /// </summary>
    public abstract class BaseSessionBuilder<TSession,TSessionOptions> : BaseBuilder<TSession,TSessionOptions>
        where TSession : BaseSession<TSession,TSessionOptions>, new()
        where TSessionOptions : BaseSessionOptions<TSessionOptions>, new()
    {
    }
}
