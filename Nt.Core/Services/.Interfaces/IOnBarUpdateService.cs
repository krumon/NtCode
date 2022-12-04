namespace Nt.Core.Services
{
    public interface IOnBarUpdateService
    {
        /// <summary>
        /// Updates the service when the bar update. The methods can be use when the bar is closed or on each tick of the bar.        
        /// </summary>
        void OnBarUpdate();
    }
}
