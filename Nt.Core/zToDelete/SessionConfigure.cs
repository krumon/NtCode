namespace Nt.Core
{
    /// <summary>
    /// Contains the objects to confegure a session
    /// </summary>
    public class SessionConfigure<TOptions, TProperties>
        where TOptions : SessionOptions, new()
        where TProperties : SessionProperties, new()
    {
        public TOptions Options { get; set; }
        public TProperties Properties { get; set; }
    }
}
