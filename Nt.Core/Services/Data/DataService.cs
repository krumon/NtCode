using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the ninjascript general data to use bay the host.
    /// </summary>
    public class DataService
    {
        
        /// <summary>
        /// The <see cref="TimeZoneInfo"/> configure by the user in the platform general options.
        /// </summary>
        public TimeZoneInfo UserTimeZoneInfo { get; set; }

        /// <summary>
        /// The primary instrument name.
        /// </summary>
        public string InstrumentName { get; set; }

        /// <summary>
        /// The primary instrument trading hours.
        /// </summary>
        public string TradingHoursName { get; set; }

    }
}
