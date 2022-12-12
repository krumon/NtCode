using Nt.Core.DependencyInjection;
using Nt.Core.Services;
using System;
using IServiceProvider = Nt.Core.DependencyInjection.IServiceProvider;

namespace Nt.Core.Hosting
{
    public interface IHostBuilder
    {

        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="IHost"/></returns>
        IHost Build();

        /// <summary>
        /// Adds host options to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="optionsDelegate">The delegate for configuring the <see cref="IHostService"/>.
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder ConfigureHostOptions(Action<HostOptions> optionsDelegate);

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureServicesDelegate">The delegate for configuring the <see cref="IServiceCollection"/> that will be used
        /// to construct the <see cref="IServiceProvider"/> for the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder ConfigureServices(Action<IServiceCollection> configureServicesDelegate);

        /// <summary>
        /// Adds session services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureSessionsDelegate">The delegate for configuring the <see cref="ISessionService"/> that will be used by the host.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        IHostBuilder ConfigureSessions(Action<ISessionBuilder> configureSessionsDelegate);

    }
}