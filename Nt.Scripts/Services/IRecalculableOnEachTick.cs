namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents the services that are recalculable on each tick.
    /// </summary>
    public interface IRecalculableOnEachTick
    {
        /// <summary>
        /// Method that recalculates the service on each tick.        
        /// </summary>
        void OnMarketData();
        
    }
}
