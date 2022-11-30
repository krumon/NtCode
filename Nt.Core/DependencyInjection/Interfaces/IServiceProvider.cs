using System;
using System.Collections.Generic;

namespace Nt.Core.DependencyInjection
{

    /// <summary>
    /// Defines an engine to gets a service object.
    /// </summary>
    public interface IServiceProvider
    {

        /// <summary>
        /// Gets the service object with the specific type.
        /// </summary>
        /// <param name="serviceType">The type of the service.</param>
        /// <returns>The object of the ninjascript service or null if there aren't object.</returns>
        object GetService(Type serviceType);

        /// <summary>
        /// Gets all service objects of the specified type.
        /// </summary>
        /// <returns>The service collection that was produced.</returns>
        IEnumerable<T> GetAllServices<T>();

    }
}
