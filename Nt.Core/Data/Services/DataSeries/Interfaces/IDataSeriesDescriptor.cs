namespace Nt.Core.Data
{
    /// <summary>
    /// Represents default implementation of <see cref="DataSeriesDescriptor"/>.
    /// </summary>
    public interface IDataSeriesDescriptor : IServiceDescriptor
    {

        /// <summary>
        /// Gets or sets the instrument key.
        /// </summary>
        InstrumentKey InstrumentKey { get; }

        /// <summary>
        /// Gets or sets the series period type.
        /// </summary>
        PeriodType PeriodType { get; set; }

        /// <summary>
        /// Gets or sets the series period value.
        /// </summary>
        int PeriodValue { get;set; }

        /// <summary>
        /// Gets or sets the trading hours key.
        /// </summary>
        TradingHoursKey TradingHoursKey { get; set; }

    }
}
