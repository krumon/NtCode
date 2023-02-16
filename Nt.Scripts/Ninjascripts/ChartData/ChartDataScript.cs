using NinjaTrader.Gui.Chart;
using Nt.Core.Data;
using Nt.Core.Services;
using Nt.Scripts.Hosting;
using System;

namespace Nt.Scripts.Ninjascripts
{
    public class ChartDataScript : ChartDataService
    {

        public override void Configure(object[] ninjascriptObjects)
        {
            if (ninjascriptObjects == null)
                throw new ArgumentNullException(nameof(ninjascriptObjects));

            this.TryGetObject(ninjascriptObjects, out ChartBars chartBars);

            if (chartBars == null)
                throw new ArgumentException("For configure the chart data service is necesary a ninjatrader ChartBars object");

            TradingHoursName = chartBars.Properties.TradingHoursInstance.Name;
            InstrumentName = chartBars.Properties.Instrument.Split(' ')[0];
            BarsPeriod = new BarsPeriod
            {
                PeriodType = (PeriodType)(int)chartBars.Properties.BarsPeriod.BarsPeriodType
            };
            IsConfigured = true;
        }

        public override void Dispose()
        {
        }

    }
}
