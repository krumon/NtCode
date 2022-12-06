using Nt.Core.Data;
using System.Text;

namespace Nt.Core.Services
{
    /// <inheritdoc/>
    public abstract class ChartDataService : IChartDataService
    {

        /// <inheritdoc/>
        public BarsPeriod BarsPeriod { get; set; }

        /// <inheritdoc/>
        public string InstrumentName { get; set; }

        /// <inheritdoc/>
        public string TradingHoursName { get; set; }

        /// <inheritdoc/>
        public abstract void Configure(object[] ninjascriptObjects);

        /// <inheritdoc/>
        public abstract void DataLoaded(object[] ninjascriptObjects);

        /// <inheritdoc/>
        public abstract void Dispose();

        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Chart Instrument: {InstrumentName}");
            builder.AppendLine($"Chart Trading Hours: {TradingHoursName}");
            builder.AppendLine($"Price: {BarsPeriod.MarketDataType}");
            builder.AppendLine($"Interval: {BarsPeriod.PeriodType}");
            builder.AppendLine($"Value: {BarsPeriod.PeriodValue}");
            return builder.ToString();
        }
    }
}
