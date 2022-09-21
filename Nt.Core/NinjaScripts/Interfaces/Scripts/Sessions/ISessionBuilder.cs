namespace Nt.Core
{
    /// <summary>
    /// Interface for any session builder.
    /// </summary>
    public interface ISessionBuilder<TScript,TOptions> : IScriptBuilder<TScript,TOptions>, ISession
        where TScript : IScriptBuilder<TScript,TOptions>
        where TOptions : IScriptOptions<TOptions>
    {
    }

    /// <summary>
    /// Interface for any session builder.
    /// </summary>
    public interface ISessionBuilder : IScriptBuilder
    {
    }
}
