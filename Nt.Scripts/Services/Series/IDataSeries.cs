using Nt.Core.Data;

namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents default implementation of any data serie.
    /// </summary>
    public interface IDataSeries
    {
        /// <summary>
        /// Gets or sets the instrument key.
        /// </summary>
        InstrumentCode InstrumentKey { get; }

        /// <summary>
        /// Gets or sets the series period type.
        /// </summary>
        PeriodType PeriodType { get; set; }

        /// <summary>
        /// Gets or sets the series period value.
        /// </summary>
        int PeriodValue { get; set; }

        /// <summary>
        /// Gets or sets the trading hours key.
        /// </summary>
        TradingHoursCode TradingHoursKey { get; set; }

    }
}
