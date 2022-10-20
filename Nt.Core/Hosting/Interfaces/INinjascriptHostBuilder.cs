using Nt.Core.Configuration;
using Nt.Core.Services;
using System;
using System.Collections.Generic;

namespace Nt.Core.Hosting
{
    public interface INinjascriptHostBuilder
    {

        /// <summary>
        /// A central location for sharing state between components during the host building process.
        /// </summary>
        IDictionary<object, object> Properties { get; }

        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="INinjascriptHost"/></returns>
        INinjascriptHost Build();

        /// <summary>
        /// Set up the configuration for the builder itself. This will be used to initialize 
        /// the <see cref="INinjascriptHostEnvironment"/> for use later in the build process. 
        /// This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the 
        /// <see cref="INinjascriptConfigurationBuilder"/> that will be used 
        /// to construct the <see cref="INinjascriptConfiguration"/> for the host.</param>
        /// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        INinjascriptHostBuilder ConfigureHostConfiguration(Action<INinjascriptConfigurationBuilder> configureDelegate);

        /// <summary>
        /// Sets up the configuration for the remainder of the build process and application.
        /// This can be called multiple times and the results will be additive. The results
        /// will be available at <see cref="NinjascriptHostBuilderContext.Configuration"/>
        /// for subsequent operations, as well as in <see cref="INinjascriptHost.Services"/>.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="INinjascriptConfigurationBuilder"/>
        /// that will be used to construct the <see cref="INinjascriptConfiguration"/> for the application.</param>
        /// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        INinjascriptHostBuilder ConfigureAppConfiguration(Action<NinjascriptHostBuilderContext, INinjascriptConfigurationBuilder> configureDelegate);

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="INinjascriptServiceCollection"/>
        /// that will be used to construct the <see cref="INinjascriptServiceProvider"/>.</param>
        /// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        INinjascriptHostBuilder ConfigureServices(Action<NinjascriptHostBuilderContext, INinjascriptServiceCollection> configureDelegate);

    }
}