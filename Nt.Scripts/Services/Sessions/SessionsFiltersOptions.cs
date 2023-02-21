﻿namespace Nt.Scripts.Services
{
    /// <summary>
    /// <see cref="SessionsFilters"/> services options.
    /// </summary>
    public class SessionsFiltersOptions
    {

        public bool IncludePartialHolidays { get; set; } = true;
        public bool IncludeHolidays { get; set; } = false;
        public bool ExcludeHistoricalData { get; set; } = false;

    }
}
