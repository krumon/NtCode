namespace Nt.Core
{
    /// <summary>
    /// Options to create <see cref="SessionsManager"/> object.
    /// </summary>
    public class SessionFiltersOptions
    {
        public bool IncludeHistoricalData { get; set; } = true;
        public bool IncludePartialHolidays { get; set; } = true;
        public bool IncludeLateBegin { get; set; } = true;
        public bool IncludeEarlyEnd { get; set; } = true;
        public int MinYear { get; set; } = int.MinValue;
        public int MaxYear { get; set; } = int.MaxValue;
        public int MinMonth { get; set; } = int.MinValue;
        public int MaxMonth { get; set; } = int.MaxValue;
        public int MinDay { get; set; } = int.MinValue;
        public int MaxDay { get; set; } = int.MaxValue;
    }
}
