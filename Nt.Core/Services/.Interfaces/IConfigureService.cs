namespace Nt.Core.Services
{
    public interface IConfigureService
    {
        /// <summary>
        /// Method to configure the service in the ninjascript.
        /// </summary>
        void Configure(object script);
    }
}
