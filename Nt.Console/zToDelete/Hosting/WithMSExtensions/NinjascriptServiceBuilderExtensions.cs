using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ConsoleApp;
using System;
using System.IO;
using System.Reflection;

namespace ConsoleApp
{ 
    public static class NinjascriptServiceBuilderExtensions
    {
        /// <summary>
        /// Configures a ninjascript builder in the default way.
        /// </summary>
        /// <param name="builder">The builder to configure</param>
        /// <param name="configure">The custom configuration action</param>
        /// <returns></returns>
        public static NinjascriptServiceBuilder AddDefaultConfiguration(this NinjascriptServiceBuilder builder, Action<IConfigurationBuilder> configure = null)
        {

            // Create our configuration sources
            var configurationBuilder = new ConfigurationBuilder()
                // Add environment variables
                .AddEnvironmentVariables();

            // If we are not on a mobile platform...
            //if (!builder.Environment.IsMobile)
            //{
                // Add file based configuration

                // Set base path for Json files as the startup location of the application
                configurationBuilder.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                // Add application settings json files
                configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                //configurationBuilder.AddJsonFile($"appsettings.{builder.Environment.Configuration}.json", optional: true, reloadOnChange: true);
            //}

            // Let custom configuration happen
            configure?.Invoke(configurationBuilder);

            // Inject configuration into services
            var configuration = configurationBuilder.Build();
            builder.Services.AddSingleton<IConfiguration>(configuration);

            // Set the construction Configuration
            builder.UseConfiguration(configuration);

            // Chain the construction
            return builder;
        }

        /// <summary>
        /// Configures a ninjascript builder using the provided configuration
        /// </summary>
        /// <param name="builder">The construction to configure</param>
        /// <param name="configuration">The configuration</param>
        /// <returns></returns>
        public static NinjascriptServiceBuilder AddConfiguration(this NinjascriptServiceBuilder builder, IConfiguration configuration)
        {
            // Add specific configuration
            builder.UseConfiguration(configuration);

            // Add configuration to services
            builder.Services.AddSingleton(configuration);

            // Chain the construction
            return builder;
        }

        /// <summary>
        /// Injects all of the default services used by Ninjascript for a quicker and cleaner setup
        /// </summary>
        /// <param name="builder">The construction</param>
        /// <returns></returns>
        public static NinjascriptServiceBuilder AddDefaultServices(this NinjascriptServiceBuilder builder)
        {
            // Add exception handler
            builder.AddDefaultExceptionHandler();

            // Add default logger
            builder.AddDefaultLogger();

            // Chain the construction
            return builder;
        }

        /// <summary>
        /// Injects the default logger into the ninjascript.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <returns></returns>
        public static NinjascriptServiceBuilder AddDefaultLogger(this NinjascriptServiceBuilder builder)
        {
            // Add logging as default
            builder.Services.AddLogging(options =>
            {
                // Default to debug level
                options.SetMinimumLevel(LogLevel.Debug);

                // Setup loggers from configuration
                options.AddConfiguration(builder.Configuration.GetSection("Logging"));

                // Add console logger
                options.AddConsole();

                // Add debug logger
                options.AddDebug();
            });

            // Adds a default logger so that we can get a non-generic ILogger
            // that will have the category name of "Dna"
            builder.Services.AddTransient(provider => provider.GetService<ILoggerFactory>().CreateLogger("Krumon"));

            // Chain the construction
            return builder;
        }

        /// <summary>
        /// Injects the default exception handler into the framework construction
        /// </summary>
        /// <param name="builder">The construction</param>
        /// <returns></returns>
        public static NinjascriptServiceBuilder AddDefaultExceptionHandler(this NinjascriptServiceBuilder builder)
        {
            // Bind a static instance of the BaseExceptionHandler
            builder.Services.AddSingleton<IExceptionHandler>(new BaseExceptionHandler());

            // Chain the construction
            return builder;
        }

    }
}
