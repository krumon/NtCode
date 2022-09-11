using System;

namespace Nt.Core
{
    /// <summary>
    /// Represents a partialHoliday definition.
    /// </summary>
    public class NtHoliday
    {
        /// <summary>
        /// A DateTime representing the date of the trading hours partialHoliday.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// A string which is used to describe the partialHoliday (e.g., Christmas).
        /// </summary>
        public string Description { get; set; }

    }
}
