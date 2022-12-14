using Nt.Core.Options;
using System;

namespace Nt.Core.Services
{
    public class SessionsFiltersOptions : BaseOptions<SessionsFiltersOptions>, IOptions<SessionsFiltersOptions>
    {

        public bool IncludePartialHolidays { get; set; } = true;
        public bool IncludeHolidays { get; set; } = false;
        public bool ExcludeHistoricalData { get; set; } = false;

        public SessionsFiltersOptions(Action<SessionsFiltersOptions> action) : base(action)
        {
        }
    }
}
