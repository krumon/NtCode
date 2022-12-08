using ConsoleApp.Internal;
using System;

namespace ConsoleApp
{
    /// <summary>
    /// Defines an engine to gets a service object.
    /// </summary>
    public interface INinjascriptServiceProvider
    {

        /// <summary>
        /// Gets the service object with the specific type.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <returns>The object of the type service or null if there aren't object.</returns>
        object GetService(Type serviceType);

    }
}
