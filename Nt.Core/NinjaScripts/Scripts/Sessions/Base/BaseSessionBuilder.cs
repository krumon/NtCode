namespace Nt.Core
{
    /// <summary>
    /// The base class to session scripts builders
    /// </summary>
    public abstract class BaseSessionBuilder<TSession,TSessionOptions,TSessionBuilder> : BaseBuilder<TSession,TSessionOptions,TSessionBuilder>, ISessionBuilder<TSession,TSessionOptions,TSessionBuilder>
        where TSession : BaseSession<TSession,TSessionOptions,TSessionBuilder>, new()
        where TSessionOptions : BaseSessionOptions<TSessionOptions>, new()
        where TSessionBuilder : BaseSessionBuilder<TSession, TSessionOptions, TSessionBuilder>, new()
    {
    }
}
