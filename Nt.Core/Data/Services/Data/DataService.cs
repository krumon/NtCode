using System;

namespace Nt.Core.Data
{
    /// <summary>
    /// Represents the ninjascript general data to use bay the host.
    /// </summary>
    public class DataService : IRequiredService
    {
        
        public RequiredServiceType Key { get; private set; }

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
