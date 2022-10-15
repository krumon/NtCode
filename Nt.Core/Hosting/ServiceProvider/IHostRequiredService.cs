using System;

namespace Nt.Core.Hosting
{
    /// <summary>
    /// Defines an engine to gets a service object.
    /// </summary>
    public interface IHostRequiredService
    {

        /// <summary>
        /// Gets service of type serviceType from the <see cref="INinjascriptServiceProvider"/> implementing this interface.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <returns>A service object of type serviceType. Throws an exception if the System.IServiceProvider cannot create the object.</returns>
        object GetRequiredService(Type serviceType);

    }
}
