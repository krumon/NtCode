namespace Nt.Core.Services
{
    /// <summary>
    /// Build the <see cref="SessionService"/> objects.
    /// </summary>
    public class SessionBuilder : ISessionBuilder
    {

        public ISessionBuilder AddFilters()
        {
            return this;
        }

        public ISessionBuilder AddGenericSessions()
        {
            return this;
        }

        public ISessionBuilder AddCustomSessions()
        {
            return this;
        }

        public ISessionBuilder AddStats()
        {
            return this;
        }

        public T Build<T>() 
            where T : ISessionService, new() //=> new TImplementation();
        {
            var service = new T();
            // Add to ISessionService the Filters, generic sessions, custom sessions,...
            // service.Filters = _builderFilters
            return service;
        } 

    }
}
