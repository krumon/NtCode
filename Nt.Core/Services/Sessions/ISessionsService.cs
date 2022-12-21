using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents a <see cref="SessionsService"/>.
    /// </summary>
    public interface ISessionsService : IOnBarUpdateService, IOnMarketDataService, IOnSessionUpdateService
    {
        ISessionsIteratorService Iterator { get; }
        ISessionsFiltersService Filters { get; }
        bool IsInNewSession { get; }
        
    }
}
