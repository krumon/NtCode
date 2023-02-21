namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents a <see cref="SessionsService"/>.
    /// </summary>
    public interface ISessions
    {
        ISessionsIterator Iterator { get; }
        ISessionsFilters Filters { get; }
        bool IsInNewSession { get; }
        
    }
}
