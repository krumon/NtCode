namespace Nt.Core.Services
{
    /// <summary>
    /// Defines methods that are necesary to execute when the bar updated.
    /// </summary>
    public interface IOnBarUpdateService
    {
        /// <summary>
        /// Updates the service when the bar update. The methods can be use when the bar is closed or on each tick of the bar.        
        /// </summary>
        void OnBarUpdate();
    }
}
