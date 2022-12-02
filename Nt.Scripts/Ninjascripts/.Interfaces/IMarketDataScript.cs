namespace Nt.Scripts.Ninjascripts
{
    public interface IMarketDataScript
    {
        /// <summary>
        /// Updates the service when the market data changes.
        /// </summary>
        void OnMarketData();
    }
}
