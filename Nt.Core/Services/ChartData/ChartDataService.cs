using Nt.Core.Data;
using System.Text;

namespace Nt.Core.Services
{
    /// <inheritdoc/>
    public abstract class ChartDataService : IChartDataService
    {

        public BarsPeriod BarsPeriod { get; set; }
        public string InstrumentName { get; set; }
        public string TradingHoursName { get; set; }
        public bool IsConfigured { get; protected set; }
        public bool IsDataLoaded { get; protected set; }
        public abstract void Configure(object[] ninjascriptObjects);
        public virtual void DataLoaded(object[] ninjascriptObjects) => IsDataLoaded = IsConfigured;
        public abstract void Dispose();
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Instrument: {InstrumentName} ");
            builder.Append($"| TradingHours: {TradingHoursName} ");
            builder.Append($"| Price: {BarsPeriod.MarketDataType} ");
            builder.Append($"| Interval: {BarsPeriod.PeriodType} ");
            builder.Append($"| Value: {BarsPeriod.PeriodValue} ");
            return builder.ToString();
        }
    }
}
