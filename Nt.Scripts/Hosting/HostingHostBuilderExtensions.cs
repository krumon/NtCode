using Nt.Core.Attributes;
using Nt.Core.Configuration;
using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging;
using Nt.Scripts.Logging;
using Nt.Scripts.Ninjascripts;
using Nt.Scripts.Ninjascripts.Indicators;
using Nt.Scripts.Services;
using System;
using System.IO;

namespace Nt.Scripts.Hosting
{
    public static class HostingHostBuilderExtensions
    {

        /// <summary>
        /// Specify the data series to be used by the host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder"/> to configure.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder UseDataSeries(this IHostBuilder hostBuilder)
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services
                    .Configure<DataSeriesOptions>(context.Configuration.GetSection(DataSeriesOptions.Key))
                    .AddDataSeries();
            });
        }

        ///// <summary>
        ///// Specify the <see cref="Sessions"/> to be used by the host.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="IHostBuilder"/> to configure.</param>
        ///// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        //public static IHostBuilder UseSessions(this IHostBuilder hostBuilder)
        //{
        //    return hostBuilder.ConfigureNinjascript(builder => 
        //    {
        //        builder.AddSessions();
        //    });
        //    //return hostBuilder.ConfigureServices((context, services) =>
        //    //{
        //    //    services.AddSessionsIndicator();
        //    //});
        //}

        ///// <summary>
        ///// Adds a delegate for configuring the provided <see cref="ILoggingBuilder"/>. This may be called multiple times.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        ///// <param name="configureSessions">The delegate that configures the <see cref="ILoggingBuilder"/>.</param>
        ///// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        //public static IHostBuilder ConfigureSessions(this IHostBuilder hostBuilder, Action<HostBuilderContext, ISessionsBuilder> configureSessions)
        //{
        //    return hostBuilder.ConfigureServices((context, builder) => builder.AddNinjascript(b => 
        //    {
        //    b.AddSessions(builder(configureSessions));
        //    ));
        //}


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
        public static IHostBuilder NinjaConfigureDefaults(this IHostBuilder builder, object[] ninjatraderObjects)
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
                //// Default the EventLogLoggerProvider to warning or above
                //logging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Warning);

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
            .ConfigureNinjascript((context, ninjascriptBuilder) =>
            {
                ninjascriptBuilder
                    .AddConfiguration(context.Configuration.GetSection("Ninjascripts"))
                    .AddSessions(ninjatraderObjects);
            });
            //.UseDataSeries()
            //.UseSessions();

            return builder;

        }

        [UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Calling IConfiguration.GetValue is safe when the T is bool.")]
        private static bool GetReloadConfigOnChangeValue(HostBuilderContext hostingContext) => hostingContext.Configuration.GetValue("hostBuilder:reloadConfigOnChange", defaultValue: true);

    }
}
