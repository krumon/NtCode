using Nt.Core.Services;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Nt.Core.Hosting
{
    public static class NinjascriptHostBuilderExtensions
    {

        /// <summary>
        /// Specify the environment to be used by the host. To avoid the environment being overwritten by a default
        /// value, ensure this is called after defaults are configured.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="INinjascriptHostBuilder"/> to configure.</param>
        /// <param name="environment">The environment to host the application in.</param>
        /// <returns>The <see cref="INinjascriptHostBuilder"/>.</returns>
        public static INinjascriptHostBuilder UseEnvironment(this INinjascriptHostBuilder hostBuilder, string environment)
        {
            return default;

            //return hostBuilder.ConfigureHostConfiguration(configBuilder =>
            //{
            //    configBuilder.AddInMemoryCollection(new[]
            //    {
            //        new KeyValuePair<string, string>(HostDefaults.EnvironmentKey,
            //            environment ?? throw new ArgumentNullException(nameof(environment)))
            //    });
            //});
        }

        /// <summary>
        /// Specify the content root directory to be used by the host. To avoid the content root directory being
        /// overwritten by a default value, ensure this is called after defaults are configured.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="INinjascriptHostBuilder"/> to configure.</param>
        /// <param name="contentRoot">Path to root directory of the application.</param>
        /// <returns>The <see cref="INinjascriptHostBuilder"/>.</returns>
        public static INinjascriptHostBuilder UseContentRoot(this INinjascriptHostBuilder hostBuilder, string contentRoot)
        {
            return default;
            //return hostBuilder.ConfigureHostConfiguration(configBuilder =>
            //{
            //    configBuilder.AddInMemoryCollection(new[]
            //    {
            //        new KeyValuePair<string, string>(HostDefaults.ContentRootKey,
            //            contentRoot ?? throw new ArgumentNullException(nameof(contentRoot)))
            //    });
            //});
        }

        /// <summary>
        /// Specify the <see cref="INinjascriptServiceProvider"/> to be the default one.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="INinjascriptHostBuilder"/> to configure.</param>
        /// <param name="configure"></param>
        /// <returns>The <see cref="INinjascriptHostBuilder"/>.</returns>
        public static INinjascriptHostBuilder UseDefaultServiceProvider(this INinjascriptHostBuilder hostBuilder, Action<NinjascriptServiceProviderOptions> configure)
            => hostBuilder.UseDefaultServiceProvider((context, options) => configure(options));

        /// <summary>
        /// Specify the <see cref="INinjascriptServiceProvider"/> to be the default one.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="INinjascriptHostBuilder"/> to configure.</param>
        /// <param name="configure">The delegate that configures the <see cref="INinjascriptServiceProvider"/>.</param>
        /// <returns>The <see cref="INinjascriptHostBuilder"/>.</returns>
        public static INinjascriptHostBuilder UseDefaultServiceProvider(this INinjascriptHostBuilder hostBuilder, Action<NinjascriptHostBuilderContext, NinjascriptServiceProviderOptions> configure)
        {
            return default;
            //return hostBuilder.UseServiceProviderFactory(context =>
            //{
            //    var options = new ServiceProviderOptions();
            //    configure(context, options);
            //    return new DefaultServiceProviderFactory(options);
            //});
        }

        ///// <summary>
        ///// Adds a delegate for configuring the provided <see cref="ILoggingBuilder"/>. This may be called multiple times.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="INinjascriptHostBuilder" /> to configure.</param>
        ///// <param name="configureLogging">The delegate that configures the <see cref="ILoggingBuilder"/>.</param>
        ///// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        //public static INinjascriptHostBuilder ConfigureLogging(this INinjascriptHostBuilder hostBuilder, Action<NinjascriptHostBuilderContext, ILoggingBuilder> configureLogging)
        //{
        //    return hostBuilder.ConfigureServices((context, collection) => collection.AddLogging(builder => configureLogging(context, builder)));
        //}

        ///// <summary>
        ///// Adds a delegate for configuring the provided <see cref="ILoggingBuilder"/>. This may be called multiple times.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        ///// <param name="configureLogging">The delegate that configures the <see cref="ILoggingBuilder"/>.</param>
        ///// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        //public static INinjascriptHostBuilder ConfigureLogging(this INinjascriptHostBuilder hostBuilder, Action<ILoggingBuilder> configureLogging)
        //{
        //    return hostBuilder.ConfigureServices((context, collection) => collection.AddLogging(builder => configureLogging(builder)));
        //}

        ///// <summary>
        ///// Adds a delegate for configuring the <see cref="HostOptions"/> of the <see cref="IHost"/>.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        ///// <param name="configureOptions">The delegate for configuring the <see cref="HostOptions"/>.</param>
        ///// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        //public static INinjascriptHostBuilder ConfigureHostOptions(this INinjascriptHostBuilder hostBuilder, Action<NinjascriptHostBuilderContext, HostOptions> configureOptions)
        //{
        //    return hostBuilder.ConfigureServices(
        //        (context, collection) => collection.Configure<HostOptions>(options => configureOptions(context, options)));
        //}

        ///// <summary>
        ///// Adds a delegate for configuring the <see cref="HostOptions"/> of the <see cref="IHost"/>.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        ///// <param name="configureOptions">The delegate for configuring the <see cref="HostOptions"/>.</param>
        ///// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        //public static INinjascriptHostBuilder ConfigureHostOptions(this INinjascriptHostBuilder hostBuilder, Action<NinjascriptHostOptions> configureOptions)
        //{
        //    return hostBuilder.ConfigureServices(collection => collection.Configure(configureOptions));
        //}

        ///// <summary>
        ///// Sets up the configuration for the remainder of the build process and application. This can be called multiple times and
        ///// the results will be additive. The results will be available at <see cref="HostBuilderContext.Configuration"/> for
        ///// subsequent operations, as well as in <see cref="IHost.Services"/>.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        ///// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder"/> that will be used
        ///// to construct the <see cref="IConfiguration"/> for the host.</param>
        ///// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        //public static INinjascriptHostBuilder ConfigureAppConfiguration(this INinjascriptHostBuilder hostBuilder, Action<IConfigurationBuilder> configureDelegate)
        //{
        //    return hostBuilder.ConfigureAppConfiguration((context, builder) => configureDelegate(builder));
        //}

        ///// <summary>
        ///// Adds services to the container. This can be called multiple times and the results will be additive.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="INinjascriptHostBuilder" /> to configure.</param>
        ///// <param name="configureDelegate">The delegate for configuring the <see cref="INinjascriptServiceCollection"/>.</param>
        ///// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        //public static INinjascriptHostBuilder ConfigureServices(this INinjascriptHostBuilder hostBuilder, Action<INinjascriptServiceCollection> configureDelegate)
        //{
        //    return hostBuilder.ConfigureServices((context, collection) => configureDelegate(collection));
        //}

        ///// <summary>
        ///// Enables configuring the instantiated dependency container. This can be called multiple times and
        ///// the results will be additive.
        ///// </summary>
        ///// <typeparam name="TContainerBuilder"></typeparam>
        ///// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        ///// <param name="configureDelegate">The delegate for configuring the <typeparamref name="TContainerBuilder"/>.</param>
        ///// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        //public static INinjascriptHostBuilder ConfigureContainer<TContainerBuilder>(this INinjascriptHostBuilder hostBuilder, Action<TContainerBuilder> configureDelegate)
        //{
        //    return hostBuilder.ConfigureContainer<TContainerBuilder>((context, builder) => configureDelegate(builder));
        //}

        /// <summary>
        /// Configures an existing <see cref="INinjascriptHostBuilder"/> instance with pre-configured defaults. This will overwrite
        /// previously configured values and is intended to be called before additional configuration calls.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the <see cref="IHostBuilder"/>:
        ///     * set the <see cref="INinjascriptHostEnvironment.ContentRootPath"/> to the result of <see cref="Directory.GetCurrentDirectory()"/>
        ///     * load host <see cref="INinjascriptConfiguration"/> from "DOTNET_" prefixed environment variables
        ///     * load host <see cref="INinjascriptConfiguration"/> from supplied command line args
        ///     * load app <see cref="INinjascriptConfiguration"/> from 'appsettings.json' and 'appsettings.[<see cref="IHostEnvironment.EnvironmentName"/>].json'
        ///     * load app <see cref="INinjascriptConfiguration"/> from User Secrets when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development' using the entry assembly
        ///     * load app <see cref="INinjascriptConfiguration"/> from environment variables
        ///     * load app <see cref="INinjascriptConfiguration"/> from supplied command line args
        ///     * configure the <see cref="ILoggerFactory"/> to log to the console, debug, and event source output
        ///     * enables scope validation on the dependency injection container when <see cref="IHostEnvironment.EnvironmentName"/> is 'Development'
        /// </remarks>
        /// <param name="builder">The existing builder to configure.</param>
        /// <param name="args">The command line args.</param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static INinjascriptHostBuilder ConfigureDefaults(this INinjascriptHostBuilder builder, string[] args)
        {
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            //builder.ConfigureHostConfiguration(config =>
            //{
            //    config.AddEnvironmentVariables(prefix: "DOTNET_");
            //    if (args is { Length: > 0 })
            //    {
            //        config.AddCommandLine(args);
            //    }
            //});

//            builder.ConfigureAppConfiguration((hostingContext, config) =>
//            {
//                INinjascriptHostBuilder env = hostingContext.HostingEnvironment;
//                bool reloadOnChange = GetReloadConfigOnChangeValue(hostingContext);

//                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: reloadOnChange)
//                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: reloadOnChange);

//                if (env.IsDevelopment() && env.ApplicationName is { Length: > 0 })
//                {
//                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
//                    if (appAssembly is not null)
//                    {
//                        config.AddUserSecrets(appAssembly, optional: true, reloadOnChange: reloadOnChange);
//                    }
//                }

//                config.AddEnvironmentVariables();

//                if (args is { Length: > 0 })
//                {
//                    config.AddCommandLine(args);
//                }
//            })
//            .ConfigureLogging((hostingContext, logging) =>
//            {
//                bool isWindows =
//#if NET6_0_OR_GREATER
//                    OperatingSystem.IsWindows();
//#else
//                    RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
//#endif

//                // IMPORTANT: This needs to be added *before* configuration is loaded, this lets
//                // the defaults be overridden by the configuration.
//                if (isWindows)
//                {
//                    // Default the EventLogLoggerProvider to warning or above
//                    logging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Warning);
//                }

//                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
//#if NET6_0_OR_GREATER
//                if (!OperatingSystem.IsBrowser())
//#endif
//                {
//                    logging.AddConsole();
//                }
//                logging.AddDebug();
//                logging.AddEventSourceLogger();

//                if (isWindows)
//                {
//                    // Add the EventLogLoggerProvider on windows machines
//                    logging.AddEventLog();
//                }

//                logging.Configure(options =>
//                {
//                    options.ActivityTrackingOptions =
//                        ActivityTrackingOptions.SpanId |
//                        ActivityTrackingOptions.TraceId |
//                        ActivityTrackingOptions.ParentId;
//                });

//            })
//            .UseDefaultServiceProvider((context, options) =>
//            {
//                bool isDevelopment = context.HostingEnvironment.IsDevelopment();
//                options.ValidateScopes = isDevelopment;
//                options.ValidateOnBuild = isDevelopment;
//            });

            return builder;

            //[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Calling IConfiguration.GetValue is safe when the T is bool.")]
            //static bool GetReloadConfigOnChangeValue(HostBuilderContext hostingContext) => hostingContext.Configuration.GetValue("hostBuilder:reloadConfigOnChange", defaultValue: true);
        }

        ///// <summary>
        ///// Listens for Ctrl+C or SIGTERM and calls <see cref="IHostApplicationLifetime.StopApplication"/> to start the shutdown process.
        ///// This will unblock extensions like RunAsync and WaitForShutdownAsync.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        ///// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        ////[UnsupportedOSPlatform("android")]
        ////[UnsupportedOSPlatform("browser")]
        ////[UnsupportedOSPlatform("ios")]
        ////[UnsupportedOSPlatform("tvos")]
        //public static INinjascriptHostBuilder UseConsoleLifetime(this INinjascriptHostBuilder hostBuilder)
        //{
        //    return hostBuilder.ConfigureServices(collection => collection.AddSingleton<IHostLifetime, ConsoleLifetime>());
        //}

        ///// <summary>
        ///// Listens for Ctrl+C or SIGTERM and calls <see cref="IHostApplicationLifetime.StopApplication"/> to start the shutdown process.
        ///// This will unblock extensions like RunAsync and WaitForShutdownAsync.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="INinjascriptHostBuilder" /> to configure.</param>
        ///// <param name="configureOptions">The delegate for configuring the <see cref="ConsoleLifetime"/>.</param>
        ///// <returns>The same instance of the <see cref="INinjascriptHostBuilder"/> for chaining.</returns>
        ////[UnsupportedOSPlatform("android")]
        ////[UnsupportedOSPlatform("browser")]
        ////[UnsupportedOSPlatform("ios")]
        ////[UnsupportedOSPlatform("tvos")]
        //public static INinjascriptHostBuilder UseConsoleLifetime(this INinjascriptHostBuilder hostBuilder, Action<ConsoleLifetimeOptions> configureOptions)
        //{
        //    return hostBuilder.ConfigureServices(collection =>
        //    {
        //        collection.AddSingleton<IHostLifetime, ConsoleLifetime>();
        //        collection.Configure(configureOptions);
        //    });
        //}

        ///// <summary>
        ///// Enables console support, builds and starts the host, and waits for Ctrl+C or SIGTERM to shut down.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        ///// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the console.</param>
        ///// <returns>A <see cref="Task"/> that only completes when the token is triggered or shutdown is triggered.</returns>
        ////[UnsupportedOSPlatform("android")]
        ////[UnsupportedOSPlatform("browser")]
        ////[UnsupportedOSPlatform("ios")]
        ////[UnsupportedOSPlatform("tvos")]
        //public static Task RunConsoleAsync(this INinjascriptHostBuilder hostBuilder, CancellationToken cancellationToken = default)
        //{
        //    return hostBuilder.UseConsoleLifetime().Build().RunAsync(cancellationToken);
        //}

        ///// <summary>
        ///// Enables console support, builds and starts the host, and waits for Ctrl+C or SIGTERM to shut down.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        ///// <param name="configureOptions">The delegate for configuring the <see cref="ConsoleLifetime"/>.</param>
        ///// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the console.</param>
        ///// <returns>A <see cref="Task"/> that only completes when the token is triggered or shutdown is triggered.</returns>
        ////[UnsupportedOSPlatform("android")]
        ////[UnsupportedOSPlatform("browser")]
        ////[UnsupportedOSPlatform("ios")]
        ////[UnsupportedOSPlatform("tvos")]
        //public static Task RunConsoleAsync(this INinjascriptHostBuilder hostBuilder, Action<ConsoleLifetimeOptions> configureOptions, CancellationToken cancellationToken = default)
        //{
        //    return hostBuilder.UseConsoleLifetime(configureOptions).Build().RunAsync(cancellationToken);
        //}

    }
}
