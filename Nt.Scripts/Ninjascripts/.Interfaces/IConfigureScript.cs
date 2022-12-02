namespace Nt.Scripts.Ninjascripts
{
    public interface IConfigureScript
    {
        /// <summary>
        /// Method to configure the service in the ninjascript.
        /// </summary>
        /// <param name="ninjascriptObjects">The ninjascripts objects necesary to configure the host services.</param>
        void Configure(params object[] ninjascriptObjects);
    }
}
