namespace Nt.Core
{
    /// <summary>
    /// The script options
    /// </summary>
    public abstract class BaseNinjascriptsManagerOptions<TOptions> : BaseNinjascriptOptions<TOptions>
        where TOptions : BaseNinjascriptsManagerOptions<TOptions>, new()
    {
    }
}
