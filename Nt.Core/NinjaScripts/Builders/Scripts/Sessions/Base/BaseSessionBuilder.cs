namespace Nt.Core
{
    /// <summary>
    /// The base class to session scripts builders
    /// </summary>
    public abstract class BaseSessionBuilder<TScript,TOptions> : BaseScriptBuilder<TScript,TOptions>
        where TScript : BaseSession<TScript,TOptions>, new()
        where TOptions : BaseSessionOptions<TOptions>, new()
    {
    }
}
