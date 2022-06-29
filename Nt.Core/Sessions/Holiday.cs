using System;

namespace NtCore
{
    /// <summary>
    /// Represents a holiday definition.
    /// </summary>
    public class Holiday
    {
        /// <summary>
        /// A DateTime representing the date of the trading hours holiday.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// A string which is used to describe the holiday (e.g., Christmas).
        /// </summary>
        public string Description { get; set; }

    }
}
