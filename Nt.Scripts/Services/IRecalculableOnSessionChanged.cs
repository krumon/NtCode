namespace Nt.Scripts.Services
{
    /// <summary>
    /// Represents the services that are recalculable when the session changed.
    /// </summary>
    public interface IRecalculableOnSessionChanged
    {
        /// <summary>
        /// Method that recalculates the service when the session changed.        
        /// </summary>
        /// <param name="args">Argiments of the session changed event.</param>
        void OnSessionChanged(SessionChangedEventArgs args);
        
    }
}
