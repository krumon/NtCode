namespace Nt.Core
{
    /// <summary>
    /// The generic class for session options
    /// </summary>
    public class SessionOptions<T> : ScriptOptions<T>
        where T : SessionOptions<T>, new()
    {
    }
}
