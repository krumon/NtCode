using NinjaTrader.Gui.Chart;
using Nt.Core.Data;
using Nt.Core.Services;
using System;

namespace Nt.Scripts.Ninjascripts.Charts
{
    public class ChartDataScript : ChartDataService, IChartDataScript
    {
        public void Configure(params object[] ninjascriptObjects)
        {
            TryGetNinjascriptObject<ChartBars>(out ChartBars chartBars, ninjascriptObjects);

            if (chartBars == null)
                throw new ArgumentException("For configure the chart data service is necesary a ninjatrader ChartBars object");

            TradingHoursName = chartBars.Properties.TradingHoursInstance.Name;
            InstrumentName = chartBars.Properties.Instrument;
            BarsPeriod = new BarsPeriod
            {
                PeriodType = (PeriodType)(int)chartBars.Properties.BarsPeriod.BarsPeriodType
            };
        }

        private bool TryGetNinjascriptObject<T>(out T nsObject, params object[] ninjascriptObjects)
            where T : class
        {
            object findObject = null;
            bool find = false;
            try
            {
                if (ninjascriptObjects == null)
                    throw new ArgumentNullException(nameof(ninjascriptObjects));

                if (ninjascriptObjects.Length == 0)
                    throw new ArgumentException("The ninjascript objects array cannot be empty.");

                foreach (var ninjascriptObject in ninjascriptObjects)
                    if (ninjascriptObject is T)
                    {
                        findObject = (T)ninjascriptObject;
                        find = true;
                        break;
                    }
            }
            catch
            {
            }

            nsObject = (T)findObject;

            return find;
        }
    }
}
