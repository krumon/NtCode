using NinjaTrader.Gui.Chart;
using Nt.Core.Data;
using Nt.Core.Services;
using System;

namespace Nt.Scripts.Ninjascripts.Charts
{
    public class ChartData : ChartDataService
    {
        public override void Configure(object script)
        {
            if (script is ChartBars chartBars)
            {
                this.TradingHoursName = chartBars.Properties.TradingHoursInstance.Name;
                this.InstrumentName = chartBars.Properties.Instrument;
                this.BarsPeriod = new BarsPeriod
                {
                    PeriodType = (PeriodType)(int)chartBars.Properties.BarsPeriod.BarsPeriodType
                };
                return;
            }
                
            throw new Exception($"{nameof(ChartData)} cannot be configured.");

        }
    }
}
