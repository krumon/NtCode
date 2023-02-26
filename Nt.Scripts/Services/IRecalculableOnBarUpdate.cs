namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents the services that are recalculable when the price is updated.
    /// </summary>
    public interface IRecalculableOnBarUpdate
    {
        /// <summary>
        /// Method that recalculates the service when the price is updated.        
        /// </summary>
        void OnBarUpdate();
        
    }
}
