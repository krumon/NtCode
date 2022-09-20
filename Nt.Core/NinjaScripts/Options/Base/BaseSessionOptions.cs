namespace Nt.Core
{
    /// <summary>
    /// The generic class for session options
    /// </summary>
    public abstract class BaseSessionOptions<TOptions> : BaseScriptOptions<TOptions>
        where TOptions : BaseSessionOptions<TOptions>, new()
    {
    }
}
