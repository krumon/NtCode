namespace Nt.Core.Data
{

    /// <summary>
    /// Defines an engine to gets a service object.
    /// </summary>
    public interface IHostedService
    {

        /// <summary>
        /// Gets the type of the ninjascript service.
        /// </summary>
        NinjascriptServiceType Key { get; }

    }
}
