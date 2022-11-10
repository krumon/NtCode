namespace Nt.Core.Data
{

    /// <summary>
    /// Defines an engine to gets a service object.
    /// </summary>
    public interface IServiceProvider
    {

        /// <summary>
        /// Gets the service object with the specific type.
        /// </summary>
        /// <param name="key">The key of the ninjascript.</param>
        /// <returns>The object of the ninjascript service or null if there aren't object.</returns>
        object GetService(string key);

    }
}
