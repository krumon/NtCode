using Nt.Core.Options;
using Nt.Core.Services;

namespace Nt.Scripts.Ninjascripts
{
    public class SessionsFiltersScript : SessionsFiltersService
    {
        public SessionsFiltersScript(ISessionsIteratorService session, IConfigureOptions<SessionsFiltersOptions> options) : base(session, options)
        {
        }
    }
}
