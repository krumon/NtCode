namespace Nt.Core
{
    /// <summary>
    /// Options to create <see cref="SessionsManager"/> object.
    /// </summary>
    public class SessionManagerOptions
    {
        public bool UseSessionIterator { get; set; }
        public bool UseSessionFilters { get; set; }
        public bool UseSessionStats { get; set; }
        public bool IncludeGenericSessions { get; set; }
        public bool IncludeCustomSessions { get; set; }
    }
}
