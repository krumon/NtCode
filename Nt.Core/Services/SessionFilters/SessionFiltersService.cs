namespace Nt.Core.Services
{
    /// <summary>
    /// Service to control the access to any session.
    /// </summary>
    public class SessionFiltersService : ISessionFiltersService
    {
        private readonly ISessionsService _session;

        public SessionFiltersService(ISessionsService session)
        {
            _session = session;
        }
    }
}
