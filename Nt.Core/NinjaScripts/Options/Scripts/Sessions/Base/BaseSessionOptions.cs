namespace Nt.Core
{
    /// <summary>
    /// The generic class for session options
    /// </summary>
    public abstract class BaseSessionOptions<T> : BaseScriptOptions<T>
        where T : BaseSessionOptions<T>, new()
    {
    }
}
