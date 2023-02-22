using NinjaTrader.Gui.Chart;
using Nt.Core.Data;
using System.Text;
using BarsPeriod = Nt.Core.Data.BarsPeriod;

namespace Nt.Scripts.Services
{
    public class ChartBarsData : IChartBarsData
    {
        public BarsPeriod BarsPeriod { get; set; }
        public string InstrumentName { get; set; }
        public string TradingHoursName { get; set; }
        public bool IsConfigured { get; protected set; }
        public bool IsDataLoaded { get; protected set; }

        public ChartBarsData(ChartBars chartBars)
        {
            TradingHoursName = chartBars.Properties.TradingHoursInstance.Name;
            InstrumentName = chartBars.Properties.Instrument.Split(' ')[0];
            BarsPeriod = new BarsPeriod
            {
                PeriodType = (PeriodType)(int)chartBars.Properties.BarsPeriod.BarsPeriodType,
                PeriodValue = chartBars.Properties.BarsPeriod.Value,
                MarketDataType = (MarketDataType)chartBars.Properties.BarsPeriod.MarketDataType
            };
        }

        // TODO: Delete this constructor. Is only necesary for testing in console.
        public ChartBarsData()
        {
        }

        public void Configure()
        {
            IsConfigured = true;
        }

        public void DataLoaded() => IsDataLoaded = IsConfigured;

        public void Dispose()
        {
        }

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
