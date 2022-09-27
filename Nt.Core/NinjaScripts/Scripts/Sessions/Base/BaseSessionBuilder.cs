﻿namespace Nt.Core
{
    /// <summary>
    /// The base class to session scripts builders
    /// </summary>
    public abstract class BaseSessionBuilder<TSession,TSessionOptions,TSessionBuilder> : BaseBuilder<TSession,TSessionOptions,TSessionBuilder>, ISessionBuilder
        where TSession : BaseSession<TSession,TSessionOptions,TSessionBuilder>, ISession
        where TSessionOptions : BaseSessionOptions<TSessionOptions>, ISessionOptions
        where TSessionBuilder : BaseSessionBuilder<TSession, TSessionOptions, TSessionBuilder>, ISessionBuilder
    {
    }

}
