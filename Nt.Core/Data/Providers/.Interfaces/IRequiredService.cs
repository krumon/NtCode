namespace Nt.Core.Data
{
    /// <summary>
    /// Defines an engine to gets a service object.
    /// </summary>
    public interface IRequiredNinjascript
    {

        /// <summary>
        /// Gets ninjascript of key from the <see cref="IServiceProvider"/> implementing this interface.
        /// </summary>
        /// <param name="key">An object that specifies the key of ninjascript object to get.</param>
        /// <returns>A ninjascript object of string key. Throws an exception if the System.INinjascriptProvider cannot create the object.</returns>
        object GetRequiredService(string key);

    }
}
