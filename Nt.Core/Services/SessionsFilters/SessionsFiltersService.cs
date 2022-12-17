using Nt.Core.Options;
using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Service to control the access to any session.
    /// </summary>
    public class SessionsFiltersService : ISessionsFiltersService
    {
        private readonly ISessionsIteratorService _session;
        private readonly SessionsFiltersOptions _options = new SessionsFiltersOptions(); // Default options

        public SessionsFiltersService(ISessionsIteratorService session, IOptions<SessionsFiltersOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            _session = session ?? throw new ArgumentNullException(nameof(session));
            options?.Configure(_options);
        }

        public SessionsFiltersOptions Options => _options;

        public bool Check()
        {
            Console.WriteLine("The filters is checking....");
            bool check = true;

            if (_session.IsPartialHoliday == true && _options.IncludePartialHolidays == false)
                check = false;

            return check;
        }
    }
}
