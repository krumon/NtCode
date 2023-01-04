using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using IServiceProvider = Nt.Core.DependencyInjection.IServiceProvider;

namespace Nt.Core.Hosting
{
    public interface IHostBuilder
    {

        /// <summary>
        ///  A central location for sharing state between components during the host building
        ///  process.
        /// </summary>
        IDictionary<object, object> Properties { get; }

        /// <summary>
        /// Set up the configuration for the builder itself. This will be used to initialize
        /// the Microsoft.Extensions.Hosting.IHostEnvironment for use later in the build
        /// process. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the Microsoft.Extensions.Configuration.IConfigurationBuilder
        /// that will be used to construct the Microsoft.Extensions.Configuration.IConfiguration
        /// for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate);

        /// <summary>
        /// Adds host options to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="optionsDelegate">The delegate for configuring the <see cref="IHostService"/>.
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder ConfigureHostOptions(Action<HostOptions> optionsDelegate);

        /// <summary>
        /// Sets up the configuration for the remainder of the build process and application.
        /// This can be called multiple times and the results will be additive. The results
        /// will be available at Microsoft.Extensions.Hosting.HostBuilderContext.Configuration
        /// for subsequent operations, as well as in <see cref="IHost.Services"/>.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the Microsoft.Extensions.Configuration.IConfigurationBuilder
        /// that will be used to construct the <see cref="IConfiguration"/>.
        /// for the application.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate);

        /// <summary>
        ///  Adds services to the container. This can be called multiple times and the results
        ///  will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the Microsoft.Extensions.DependencyInjection.IServiceCollection
        /// that will be used to construct the System.IServiceProvider.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate);

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureServicesDelegate">The delegate for configuring the <see cref="IServiceCollection"/> that will be used
        /// to construct the <see cref="IServiceProvider"/> for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder ConfigureServices(Action<IServiceCollection> configureServicesDelegate);

        /// <summary>
        /// Overrides the factory used to create the service provider.
        /// </summary>
        /// <typeparam name="TContainerBuilder">The type of builder.</typeparam>
        /// <param name="factory">The factory to register.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory);

        /// <summary>
        /// Overrides the factory used to create the service provider.
        /// </summary>
        /// <typeparam name="TContainerBuilder">The type of builder.</typeparam>
        /// <param name="factory">The factory to register.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory);

        /// <summary>
        /// Enables configuring the instantiated dependency container. This can be called
        /// multiple times and the results will be additive.
        /// </summary>
        /// <typeparam name="TContainerBuilder">The type of builder.</typeparam>
        /// <param name="configureDelegate">The delegate which configures the builder.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate);

        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="IHost"/></returns>
        IHost Build();

    }
}