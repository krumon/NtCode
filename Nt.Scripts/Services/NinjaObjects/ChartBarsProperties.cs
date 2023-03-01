using NinjaTrader.Gui.Chart;
using Nt.Core.Data;
using System;
using System.Text;
using BarsPeriod = Nt.Core.Data.BarsPeriod;

namespace Nt.Scripts.Services
{
    public class ChartBarsProperties : IChartBarsData
    {
        private readonly ChartBars _chartBars;

        public BarsPeriod BarsPeriod { get; set; }
        public string InstrumentName { get; set; }
        public string TradingHoursName { get; set; }
        public bool IsConfigured { get; protected set; }
        public bool IsDataLoaded { get; protected set; }

        public ChartBarsProperties(ChartBars chartBars)
        {
            _chartBars = chartBars ?? throw new ArgumentNullException(nameof(chartBars));
            Configure();
        }

        // TODO: Delete this constructor. Is only necesary for testing in console.
        internal ChartBarsProperties()
        {
            Configure();
        }

        protected virtual void Configure()
        {
            TradingHoursName = _chartBars.Properties.TradingHoursInstance.Name;
            InstrumentName = _chartBars.Properties.Instrument.Split(' ')[0];
            BarsPeriod = new BarsPeriod
            {
                PeriodType = (PeriodType)(int)_chartBars.Properties.BarsPeriod.BarsPeriodType,
                PeriodValue = _chartBars.Properties.BarsPeriod.Value,
                MarketDataType = (MarketDataType)_chartBars.Properties.BarsPeriod.MarketDataType
            };
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
