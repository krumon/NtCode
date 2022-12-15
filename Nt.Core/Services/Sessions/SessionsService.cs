using Nt.Core.Hosting;
using Nt.Core.Options;
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

        public SessionsService(ISessionsIteratorService iterator, ISessionsFiltersService filters)
        {
            _iterator = iterator ?? throw new ArgumentNullException(nameof(iterator));
            _filters = filters ?? throw new ArgumentNullException(nameof(filters));
        }

        public ISessionsIteratorService Iterator => _iterator;

        public ISessionsFiltersService Filters => _filters;

        public bool IsConfigured => Iterator.IsConfigured;

        public bool IsDataLoaded => Iterator.IsDataLoaded;

        public void Configure(object[] ninjascriptObjects)
        {
            //Iterator.Configure(ninjascriptObjects);
        }

        public void DataLoaded(object[] ninjascriptObjects)
        {
            //Iterator.DataLoaded(ninjascriptObjects);
        }

        public void Dispose()
        {
        }

        public void OnBarUpdate()
        {
            //Iterator.OnBarUpdate();
        }

        public void OnMarketData()
        {
            //Iterator.OnMarketData();
        }

        public void OnSessionUpdate()
        {
            //Iterator?.OnSessionUpdate();
        }
    }
}
