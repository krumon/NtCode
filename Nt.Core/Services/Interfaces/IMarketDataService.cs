namespace Nt.Core.Services
{
    public interface IMarketDataService
    {
        /// <summary>
        /// Updates the service when the market data changes.
        /// </summary>
        void OnMarketData();
    }
}
