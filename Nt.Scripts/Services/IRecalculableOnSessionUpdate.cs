namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents the services that are recalculable when the session is updated.
    /// </summary>
    public interface IRecalculableOnSessionUpdate
    {
        /// <summary>
        /// Method that recalculates the service when the session is updated.        
        /// </summary>
        void OnSessionUpdate();
        
    }
}
