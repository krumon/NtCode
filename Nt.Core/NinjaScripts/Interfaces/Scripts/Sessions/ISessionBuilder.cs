namespace Nt.Core
{
    /// <summary>
    /// Interface for any session builder.
    /// </summary>
    public interface ISessionBuilder<TScript,TOptions> : IBuilder<TScript,TOptions>, ISession
        where TScript : IBuilder<TScript,TOptions>
        where TOptions : IOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any session builder.
    /// </summary>
    public interface ISessionBuilder : IBuilder
    {
    }
}
