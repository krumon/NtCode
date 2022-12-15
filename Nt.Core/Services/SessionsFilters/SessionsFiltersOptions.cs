namespace Nt.Core.Services
{
    public class SessionsFiltersOptions //: BaseOptions<SessionsFiltersOptions>, IOptions<SessionsFiltersOptions>
    {

        public bool IncludePartialHolidays { get; set; } = true;
        public bool IncludeHolidays { get; set; } = false;
        public bool ExcludeHistoricalData { get; set; } = false;

    }
}
