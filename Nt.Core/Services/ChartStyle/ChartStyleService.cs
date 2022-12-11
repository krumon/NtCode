using System;
using System.Threading;
using System.Threading.Tasks;

namespace Nt.Core.Services
{
    /// <summary>
    /// Represents the ninjatrader chart style service.
    /// </summary>
    public class ChartStyleService : IChartStyleService
    {
        private readonly IChartDataService _chartDataService;

        private string _tradingHoursName;
        private string _instrumentName;

        public ChartStyleService(IChartDataService chartDataService)
        {
            _chartDataService = chartDataService;   
        }

        public bool IsConfigured => throw new NotImplementedException();

        public bool IsDataLoaded => throw new NotImplementedException();

        public void Configure(object[] ninjascriptObjects)
        {
            _instrumentName = _chartDataService.InstrumentName;
        }

        public void DataLoaded(object[] ninjascriptObjects)
        {
            _tradingHoursName = _chartDataService.TradingHoursName;
        }

        public void Dispose()
        {
        }

        public void OnBarUpdate()
        {
        }

        public void OnMarketData()
        {
        }
    }
}
