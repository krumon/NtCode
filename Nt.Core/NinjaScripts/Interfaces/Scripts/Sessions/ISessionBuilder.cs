namespace Nt.Core
{

    /// <summary>
    /// Interface for any session builder.
    /// </summary>
    public interface ISessionBuilder<TScript,TOptions,TBuilder> : IBuilder<TScript,TOptions,TBuilder>
        where TScript : ISession<TScript,TOptions,TBuilder>
        where TOptions : ISessionOptions<TOptions>
        where TBuilder : ISessionBuilder<TScript,TOptions,TBuilder>
    {
    }

    /// <summary>
    /// Interface for any session builder.
    /// </summary>
    public interface ISessionBuilder : ISessionBuilder<ISession, ISessionOptions, ISessionBuilder>
    {
    }

}
