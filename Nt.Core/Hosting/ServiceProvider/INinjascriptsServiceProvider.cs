using System;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Defines an engine to gets a service object.
    /// </summary>
    public interface INinjascriptsServiceProvider
    {

        /// <summary>
        /// Gets the service object with the specific type.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <returns>The object of the type service or null if there aren't object.</returns>
        object GetService(Type serviceType);

    }
}
