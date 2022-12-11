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
            Console.WriteLine($"{nameof(ChartDataDesignScript)} is configuring...");

            TradingHoursName = "España";
            InstrumentName = "MES";
            BarsPeriod = new BarsPeriod
            {
                PeriodType = PeriodType.Minute,
                PeriodValue = 5
            };

            IsConfigured = true;

            Console.WriteLine($"{nameof(ChartDataDesignScript)} is configured.");

        }

        public override void DataLoaded(object[] ninjascriptObjects)
        {
            IsDataLoaded = true;
            Console.WriteLine($"{nameof(ChartDataDesignScript)} is configured by data loaded.");
        }

        public override void Dispose()
        {
            Console.WriteLine($"{nameof(ChartDataDesignScript)} is disposed.");
        }

        public void OnBarUpdate()
        {
            Console.WriteLine($"{nameof(ChartDataDesignScript)} is updated (when bar update).");
        }

        public void OnMarketData()
        {
            Console.WriteLine($"{nameof(ChartDataDesignScript)} is updated (when market data changed).");
        }
    }
}
