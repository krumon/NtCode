using System.Collections.Generic;

namespace Nt.Core.Hosting
{
    public interface INinjascriptsHostBuilder
    {

        /// <summary>
        /// A central location for sharing state between components during the host building process.
        /// </summary>
        IDictionary<object, object> Properties { get; }

        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="INinjascriptsHost"/></returns>
        INinjascriptsHost Build();

        //
        // Resumen:
        //     Set up the configuration for the builder itself. This will be used to initialize
        //     the Microsoft.Extensions.Hosting.IHostEnvironment for use later in the build
        //     process. This can be called multiple times and the results will be additive.
        //
        // Parámetros:
        //   configureDelegate:
        //     The delegate for configuring the Microsoft.Extensions.Configuration.IConfigurationBuilder
        //     that will be used to construct the Microsoft.Extensions.Configuration.IConfiguration
        //     for the host.
        //
        // Devuelve:
        //     The same instance of the Microsoft.Extensions.Hosting.IHostBuilder for chaining.
        //INinjascriptHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate);

        //
        // Resumen:
        //     Sets up the configuration for the remainder of the build process and application.
        //     This can be called multiple times and the results will be additive. The results
        //     will be available at Microsoft.Extensions.Hosting.HostBuilderContext.Configuration
        //     for subsequent operations, as well as in Microsoft.Extensions.Hosting.IHost.Services.
        //
        // Parámetros:
        //   configureDelegate:
        //     The delegate for configuring the Microsoft.Extensions.Configuration.IConfigurationBuilder
        //     that will be used to construct the Microsoft.Extensions.Configuration.IConfiguration
        //     for the application.
        //
        // Devuelve:
        //     The same instance of the Microsoft.Extensions.Hosting.IHostBuilder for chaining.
        //INinjascriptHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate);

        //
        // Resumen:
        //     Adds services to the container. This can be called multiple times and the results
        //     will be additive.
        //
        // Parámetros:
        //   configureDelegate:
        //     The delegate for configuring the Microsoft.Extensions.DependencyInjection.IServiceCollection
        //     that will be used to construct the System.IServiceProvider.
        //
        // Devuelve:
        //     The same instance of the Microsoft.Extensions.Hosting.IHostBuilder for chaining.
        //INinjascriptHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate);

    }
}