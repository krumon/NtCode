using System;

namespace Nt.Scripts.Services
{
    /// <summary>
    /// Service to control the access to any session.
    /// </summary>
    public class Sessions : ISessions
    {
        private readonly ISessionsIterator _iterator;
        private readonly ISessionsFilters _filters;
        public bool IsInNewSession => _iterator.IsSessionUpdated;
        public Sessions(ISessionsIterator iterator, ISessionsFilters filters)
        {
            _iterator = iterator ?? throw new ArgumentNullException(nameof(iterator));
            _filters = filters ?? throw new ArgumentNullException(nameof(filters));
        }

        public ISessionsIterator Iterator => _iterator;
        public ISessionsFilters Filters => _filters;
        public bool IsConfigured => true;
        public bool IsDataLoaded => true;
        public virtual void Configure(object[] ninjascriptObjects) { }
        public virtual void DataLoaded(object[] ninjascriptObjects) { }
        public virtual void Dispose() { }
        public virtual void OnBarUpdate()
        {
            //Iterator.OnBarUpdate();
            //if (Filters.IsEnabled)
            //{

            //}
        }
        public virtual void OnMarketData()
        {
            //Iterator?.OnMarketData();
            //if (Filters.IsEnabled)
            //{

            //}
        }
        public virtual void OnSessionUpdate()
        {
            //Iterator.OnSessionUpdate();
            //Filters.OnSessionUpdate();
        }
    }
}
