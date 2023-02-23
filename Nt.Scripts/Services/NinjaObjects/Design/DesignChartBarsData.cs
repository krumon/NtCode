using Nt.Core.Data;

namespace Nt.Scripts.Services.Design
{
    public class DesignChartBarsData : ChartBarsData
    {
        public DesignChartBarsData() : base()
        {
        }

        protected override void Configure()
        {
            TradingHoursName = "DesignTradingHoursName";
            InstrumentName = "DesignInstrumentName";
            BarsPeriod = new BarsPeriod
            {
                PeriodType = PeriodType.Minute,
                PeriodValue = 1,
                MarketDataType = MarketDataType.Last
            };
            IsConfigured = true;
        }
    }
}
