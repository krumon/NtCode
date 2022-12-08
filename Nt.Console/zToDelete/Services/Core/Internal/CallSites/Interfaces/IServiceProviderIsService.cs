using System;

namespace ConsoleApp
{
    internal interface IServiceProviderIsService
    {

        /// <summary>
        /// Determines if the specified service type is available from the <see cref="INinjascriptServiceProvider"/>.
        /// </summary>
        /// <param name="serviceType">An object that specifies the type of service object to test.</param>
        /// <returns>True if the specified service is a available, false if it is not.</returns>
        bool IsService(Type serviceType);

    }
}