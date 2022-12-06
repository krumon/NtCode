namespace Nt.Core.Hosting
{

    /// <summary>
    /// Defines methods for objects that are managed by the host.
    /// </summary>
    public interface IHostedService : IConfigureService, IDataLoadedService
    {

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        void Dispose();

    }
}
