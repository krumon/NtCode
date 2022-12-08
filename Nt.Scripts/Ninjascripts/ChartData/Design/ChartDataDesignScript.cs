using Nt.Core.Data;
using Nt.Core.Services;
using System;

namespace Nt.Scripts.Ninjascripts.Design
{
    /// <summary>
    /// Design object to use for testing.
    /// </summary>
    public class ChartDataDesignScript : ChartDataScript, IOnBarUpdateService, IOnMarketDataService
    {
        public override void Configure(object[] ninjascriptObjects)
        {
            Console.WriteLine("CahartDataScript is configuring...");

            TradingHoursName = "España";
            InstrumentName = "MES";
            BarsPeriod = new BarsPeriod
            {
                PeriodType = PeriodType.Minute,
                PeriodValue = 5
            };

            Console.WriteLine("CahartDataScript is configured.");

        }

        public override void DataLoaded(object[] ninjascriptObjects)
        {
            Console.WriteLine("CahartDataScript is configured by data loaded.");
        }

        public override void Dispose()
        {
            Console.WriteLine("CahartDataScript is disposed.");
        }

        public void OnBarUpdate()
        {
            Console.WriteLine("CahartDataScript is updated (when bar update).");
        }

        public void OnMarketData()
        {
            Console.WriteLine("CahartDataScript is updated (when market data changed).");
        }
    }
}
