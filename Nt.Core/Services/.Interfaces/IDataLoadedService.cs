namespace Nt.Core.Services
{
    public interface IDataLoadedService
    {
        /// <summary>
        /// Method to configure the service in the ninjascript when the data is loaded.
        /// </summary>
        /// <param name="ninjascriptObjects">The ninjascripts objects necesary to configure the host services.</param>
        void DataLoaded(params object[] ninjascriptObjects);
    }
}
