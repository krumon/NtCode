using Nt.Core.Data;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the ninjascript general data to use bay the host.
    /// </summary>
    public interface IChartDataService : IConfigureService
    {
        
        /// <summary>
        /// Gets or sets the period of the chart bars.
        /// </summary>
        BarsPeriod BarsPeriod { get; set; }

        /// <summary>
        /// The chart instrument (primary instrument) name.
        /// </summary>
        string InstrumentName { get; set; }

        /// <summary>
        /// The chart instrument (primary instrument) trading hours.
        /// The trading hours is configured by the user in "Traging Hours Template" UI.
        /// </summary>
        string TradingHoursName { get; set; }

    }
}
