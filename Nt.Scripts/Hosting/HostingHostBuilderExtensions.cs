﻿using Nt.Core.Attributes;
using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Scripts.Logging;
using Nt.Scripts.MasterScripts;
using Nt.Scripts.Ninjascripts;
using Nt.Scripts.Ninjascripts.Indicators;
using System.IO;

namespace Nt.Scripts.Hosting
{
    public static class HostingHostBuilderExtensions
    {

        /// <summary>
        /// Configures an existing <see cref="IHostBuilder"/> instance with pre-configured defaults. This will overwrite
        /// previously configured values and is intended to be called before additional configuration calls.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the <see cref="IHostBuilder"/>:
        ///     * set the <see cref="IHostEnvironment.ContentRootPath"/> to the result of <see cref="Directory.GetCurrentDirectory()"/>
        ///     * load host <see cref="IConfiguration"/> from "DOTNET_" prefixed environment variables
        ///     * load host <see cref="IConfiguration"/> from supplied command line args
        ///     * load app <see cref="IConfiguration"/> from 'appsettings.json' and 'appsettings.[<see cref="IHostEnvironment.EnvironmentName"/>].json'
        ///     * load app <see cref="IConfiguration"/> from User Secrets when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development' using the entry assembly
        ///     * load app <see cref="IConfiguration"/> from environment variables
        ///     * load app <see cref="IConfiguration"/> from supplied command line args
        ///     * configure the <see cref="ILoggerFactory"/> to log to the console, debug, and event source output
        ///     * enables scope validation on the dependency injection container when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development'
        /// </remarks>
        /// <param name="builder">The existing builder to configure.</param>
        /// <param name="ninjatraderObjects">The command line args.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder NinjaHostConfigureDefaults<T>(this IHostBuilder builder, T ninjaScript, object[] ninjatraderObjects)
        {
            builder.UseContentRoot(NinjaTrader.Core.Globals.UserDataDir);
            builder.ConfigureHostConfiguration(config =>
            {
                config.AddJsonFile("launchsettings.json", optional: true, reloadOnChange: false);
            });

            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                IHostEnvironment env = hostingContext.Environment;
                bool reloadOnChange = GetReloadConfigOnChangeValue(hostingContext);

                config
                    .AddJsonFile("ntsettings.json", optional: true, reloadOnChange: reloadOnChange)
                    .AddJsonFile($"ntsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: reloadOnChange);
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging
                    .AddConfiguration(hostingContext.Configuration.GetSection("Logging"))
                    .AddOutputWindowLogger()
                    .Configure(options =>
                    {
                        options.ActivityTrackingOptions =
                            ActivityTrackingOptions.SpanId |
                            ActivityTrackingOptions.TraceId |
                            ActivityTrackingOptions.ParentId;
                    });
            })
            .UseDefaultServiceProvider((context, options) =>
            {
                bool isDevelopment = context.Environment.IsDevelopment();
                options.ValidateScopes = isDevelopment;
                options.ValidateOnBuild = isDevelopment;
            })
            .ConfigureServices(services =>
            {
                services.AddSingleton(typeof(T),ninjaScript);
            })
            .ConfigureMasterScripts(context => { })
            .ConfigureNinjascript((context, ninjascriptBuilder) =>
            {
                ninjascriptBuilder
                    .AddConfiguration(context.Configuration.GetSection("Ninjascripts"))
                    .AddSessions(ninjatraderObjects);

            });
            
            return builder;

        }

        [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Calling IConfiguration.GetValue is safe when the T is bool.")]
        private static bool GetReloadConfigOnChangeValue(HostBuilderContext hostingContext) => hostingContext.Configuration.GetValue("hostBuilder:reloadConfigOnChange", defaultValue: true);

    }
}
