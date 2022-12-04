using Nt.Core.Data;

namespace Nt.Core.Services
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public abstract class ChartDataService : IChartDataService
    {

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public BarsPeriod BarsPeriod { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string InstrumentName { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string TradingHoursName { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public abstract void Configure(object[] ninjascriptObjects);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public abstract void Dispose();

    }
}
