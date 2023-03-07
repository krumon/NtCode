using Nt.Core.Hosting;
using System;

namespace Nt.Core.Ninjascripts
{
    public static class NinjascriptsHostBuilderExtensions
    {

        /// <summary>
        /// Adds a delegate for configuring the provided <see cref="INinjascriptsBuilder"/>. This may be called multiple times.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <param name="configureNinjascripts">The delegate that configures the <see cref="ILoggingBuilder"/>.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder ConfigureNinjascripts(this IHostBuilder hostBuilder, Action<HostBuilderContext, INinjascriptsBuilder> configureNinjascripts)
        {
            return hostBuilder.ConfigureServices((context, collection) => collection.AddNinjascripts(builder => configureNinjascripts(context, builder)));
        }

        /// <summary>
        /// Adds a delegate for configuring the provided <see cref="INinjascriptsBuilder"/>. This may be called multiple times.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <param name="configureLogging">The delegate that configures the <see cref="INinjascriptsBuilder"/>.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder ConfigureNinjascripts(this IHostBuilder hostBuilder, Action<INinjascriptsBuilder> configureLogging)
        {
            return hostBuilder.ConfigureServices((context, collection) => collection.AddNinjascripts(builder => configureLogging(builder)));
        }

    }
}
