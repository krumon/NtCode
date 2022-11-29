using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the ninjascript general data to use bay the host.
    /// </summary>
    public interface IBarsDataService
    {
        
        /// <summary>
        /// The primary instrument name.
        /// </summary>
        string InstrumentName { get; set; }

        /// <summary>
        /// The primary instrument trading hours.
        /// </summary>
        string TradingHoursName { get; set; }

    }
}
