namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseManagerOptions<TOptions> : BaseScriptOptions<TOptions>
        where TOptions : BaseManagerOptions<TOptions>, new()
    {
    }
}
