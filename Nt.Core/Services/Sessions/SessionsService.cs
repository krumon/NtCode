using Nt.Core.Hosting;
using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Service to control the access to any session.
    /// </summary>
    public class SessionsService : ISessionsService, IHostedService, IOnBarUpdateService, IOnMarketDataService, IOnSessionUpdateService
    {
        private readonly ISessionsIteratorService _iterator;
        private readonly ISessionsFiltersService _filters;
        public bool IsInNewSession => _iterator.IsSessionUpdated;
        public SessionsService(ISessionsIteratorService iterator, ISessionsFiltersService filters)
        {
            _iterator = iterator ?? throw new ArgumentNullException(nameof(iterator));
            _filters = filters ?? throw new ArgumentNullException(nameof(filters));
        }

        public ISessionsIteratorService Iterator => _iterator;
        public ISessionsFiltersService Filters => _filters;
        public bool IsConfigured => true;
        public bool IsDataLoaded => true;
        public virtual void Configure(object[] ninjascriptObjects) { }
        public virtual void DataLoaded(object[] ninjascriptObjects) { }
        public virtual void Dispose() { }
        public virtual void OnBarUpdate()
        {
            Iterator.OnBarUpdate();
            if (Filters.IsEnabled)
            {

            }
        }
        public virtual void OnMarketData()
        {
            Iterator?.OnMarketData();
            if (Filters.IsEnabled)
            {

            }
        }
        public virtual void OnSessionUpdate()
        {
            Iterator.OnSessionUpdate();
            Filters.OnSessionUpdate();
        }
    }
}
