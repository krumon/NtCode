using Nt.Core.Data;
using Nt.Core.Hosting;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the properties and methods to create a default implementation of <see cref="ChartDataService"/>.
    /// </summary>
    public interface IChartDataService : IHostedService
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
