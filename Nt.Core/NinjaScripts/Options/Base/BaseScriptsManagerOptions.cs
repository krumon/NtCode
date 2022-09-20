namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseScriptsManagerOptions<TOptions> : BaseScriptOptions<TOptions>
        where TOptions : BaseScriptsManagerOptions<TOptions>, new()
    {
    }
}
