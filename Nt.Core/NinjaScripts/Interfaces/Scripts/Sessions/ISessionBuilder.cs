namespace Nt.Core
{

    /// <summary>
    /// Interface for any session builder.
    /// </summary>
    public interface ISessionBuilder : IBuilder
    {
    }

    /// <summary>
    /// Interface for any session builder.
    /// </summary>
    public interface ISessionBuilder<TScript,TOptions> : IBuilder<TScript,TOptions>
        where TScript : ISession<TScript,TOptions>
        where TOptions : ISessionOptions<TOptions>
    {
    }

}
