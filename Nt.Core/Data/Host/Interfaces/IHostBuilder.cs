using System;

namespace Nt.Core.Data
{
    public interface IHostBuilder
    {

        /// <summary>
        /// Run the given actions to initialize the host. This can only be called once.
        /// </summary>
        /// <returns>An initialized <see cref="INinjascriptHost"/></returns>
        IHostService Build();

        /// <summary>
        /// Adds services to the container. This can be called multiple times and the results will be additive.
        /// </summary>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="INinjascriptServiceCollection"/>
        /// that will be used to construct the <see cref="INinjascriptServiceProvider"/>.</param>
        /// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        IHostBuilder ConfigureHostOptions(Action<HostOptions> optionsDelegate);

    }
}