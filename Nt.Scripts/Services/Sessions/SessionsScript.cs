using Nt.Core.Services;

namespace Nt.Scripts.Ninjascripts
{
    public class SessionsScript : SessionsService
    {

        #region Constructors

        public SessionsScript(ISessionsIteratorService iterator, ISessionsFiltersService filters) : base(iterator, filters)
        {
        }

        #endregion
    }
}
