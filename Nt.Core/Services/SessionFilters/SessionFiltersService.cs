namespace Nt.Core.Services
{
    /// <summary>
    /// Service to control the access to any session.
    /// </summary>
    public class SessionFiltersService
    {
        private readonly ISessionService _session;

        public SessionFiltersService(ISessionService session)
        {
            _session = session;
        }
    }
}
