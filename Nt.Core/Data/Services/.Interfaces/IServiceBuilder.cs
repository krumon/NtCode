using System;
using System.Collections.Generic;

namespace Nt.Core.Data
{
    public interface IServiceBuilder
    {

        /// <summary>
        /// A central location for sharing state between components during the host building process.
        /// </summary>
        IDictionary<object, object> Properties { get; }

        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="INinjascriptHost"/></returns>
        IServicesHost Build();

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="INinjascriptServiceCollection"/>
        /// that will be used to construct the <see cref="INinjascriptServiceProvider"/>.</param>
        /// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        IServicesHostBuilder ConfigureService(Action<IServiceCollection<IServiceDescriptor>> configureDelegate);

    }
}