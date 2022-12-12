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

        public TImplementation Build<TImplementation>(TImplementation service) 
            where TImplementation : ISessionService, new() => new TImplementation();

    }
}
