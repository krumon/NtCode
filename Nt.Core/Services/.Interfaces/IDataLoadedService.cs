namespace Nt.Core.Services
{
    public interface IDataLoadedService
    {
        /// <summary>
        /// Method to configure the service in the ninjascript when the data is loaded.
        /// </summary>
        void DataLoaded(object[] ninjascriptObjects);
    }
}
