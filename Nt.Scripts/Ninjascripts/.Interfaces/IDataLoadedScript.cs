namespace Nt.Scripts.Ninjascripts
{
    public interface IDataLoadedScript
    {
        /// <summary>
        /// Method to configure the service in the ninjascript when the data is loaded.
        /// </summary>
        /// <param name="ninjascriptObjects">The ninjascripts objects necesary to configure the host services.</param>
        void DataLoaded(params object[] ninjascriptObjects);
    }
}
