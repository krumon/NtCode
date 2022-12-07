using NinjaTrader.Gui.Chart;
using Nt.Core.Data;
using Nt.Core.Hosting;
using Nt.Core.Services;
using System;

namespace Nt.Scripts.Ninjascripts
{
    public class ChartDataScript : ChartDataService
    {

        public override void Configure(object[] ninjascriptObjects)
        {
            if (ninjascriptObjects == null)
                throw new ArgumentNullException(nameof(ninjascriptObjects));

            this.TryGet(ninjascriptObjects, out ChartBars chartBars);

            if (chartBars == null)
                throw new ArgumentException("For configure the chart data service is necesary a ninjatrader ChartBars object");

            TradingHoursName = chartBars.Properties.TradingHoursInstance.Name;
            InstrumentName = chartBars.Properties.Instrument;
            BarsPeriod = new BarsPeriod
            {
                PeriodType = (PeriodType)(int)chartBars.Properties.BarsPeriod.BarsPeriodType
            };
        }

        public override void DataLoaded(object[] ninjascriptObjects)
        {
        }

        public override void Dispose()
        {
        }

    }
}
