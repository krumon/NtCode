using System;
using System.Collections.Generic;

namespace Nt.Core.Data
{
    public interface IServicesHostBuilder
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

        ///// <summary>
        ///// Set up the configuration for the builder itself. This will be used to initialize 
        ///// the <see cref="INinjascriptHostEnvironment"/> for use later in the build process. 
        ///// This can be called multiple times and the results will be additive.
        ///// </summary>
        ///// <param name="configureDelegate">The delegate for configuring the 
        ///// <see cref="INinjascriptConfigurationBuilder"/> that will be used 
        ///// to construct the <see cref="INinjascriptConfiguration"/> for the host.</param>
        ///// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        //INinjascriptHostBuilder ConfigureHostConfiguration(Action<INinjascriptConfigurationBuilder> configureDelegate);

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="INinjascriptServiceCollection"/>
        /// that will be used to construct the <see cref="INinjascriptServiceProvider"/>.</param>
        /// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        IServicesHostBuilder ConfigureServices(Action<IServiceCollection<IServiceDescriptor>> configureDelegate);

    }
}