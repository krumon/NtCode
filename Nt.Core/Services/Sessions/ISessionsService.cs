namespace Nt.Core.Services
{
    /// <summary>
    /// Represents a <see cref="SessionsService"/>.
    /// </summary>
    public interface ISessionsService
    {
        ISessionsIteratorService Iterator { get; }
        ISessionsFiltersService Filters { get; }

    }
}
