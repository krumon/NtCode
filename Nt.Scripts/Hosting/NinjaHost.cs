using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Scripts.Services;
using System;

namespace Nt.Scripts.Hosting
{
    public static class NinjaHost
    {

        private static IHost _host;

        /// <summary>
        /// Gets <see cref="IHost"/> container.
        /// </summary>
        public static IHost Host => _host;

        /// <summary>
        /// Gets <see cref="ISessionsIterator"/> service.
        /// </summary>
        public static ISessionsIterator SessionsIterator => _host?.Services.GetService<ISessionsIterator>();

        /// <summary>
        /// Gets <see cref="ISessionsFilters"/> service.
        /// </summary>
        public static ISessionsFilters SessionsFilters => _host?.Services.GetService<ISessionsFilters>();

        /// <summary>
        /// Gets <see cref="ILogger{TCategoryName}"/> service.
        /// </summary>
        /// <typeparam name="T">The category type.</typeparam>
        /// <returns>The <see cref="ILogger{TCategoryName}"/> service.</returns>
        public static ILogger Logger<T>() => _host?.Services.GetService<ILogger<T>>();

        /// <summary>
        /// Create a <see cref="NinjaHost"/> with the configure host.
        /// </summary>
        /// <param name="host"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Create(IHost host)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));                
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the returned <see cref="HostBuilder"/>:
        ///   <list type="bullet">
        ///     <item><description>set the <see cref="IHostEnvironment.ContentRootPath"/> to the result of <see cref="Directory.GetCurrentDirectory()"/></description></item>
        ///     <item><description>load host <see cref="Core.Configuration.IConfiguration"/> from "DOTNET_" prefixed environment variables</description></item>
        ///     <item><description>load app <see cref="Core.Configuration.IConfiguration"/> from 'appsettings.json' and 'appsettings.[<see cref="IHostEnvironment.EnvironmentName"/>].json'</description></item>
        ///     <item><description>load app <see cref="Core.Configuration.IConfiguration"/> from User Secrets when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development' using the entry assembly</description></item>
        ///     <item><description>load app <see cref="Core.Configuration.IConfiguration"/> from environment variables</description></item>
        ///     <item><description>configure the <see cref="ILoggerFactory"/> to log to the console, debug, and event source output</description></item>
        ///     <item><description>enables scope validation on the dependency injection container when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development'</description></item>
        ///   </list>
        /// </remarks>
        /// <returns>The initialized <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateNinjaDefaultBuilder(params object[] ninjatraderObjects)
        {
            HostBuilder builder = new HostBuilder();
            return builder.NinjaConfigureDefaults(ninjatraderObjects);
        }

    }
}
