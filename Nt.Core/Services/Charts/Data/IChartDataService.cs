using Nt.Core.Data;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the ninjatrader chart bars general data to use by the ninjascript objects.
    /// </summary>
    public interface IChartDataService
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
