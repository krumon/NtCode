using Nt.Core.DependencyInjection;
using Nt.Core.Hosting;
using Nt.Core.Logging.Configuration;
using Nt.Core.Logging.File;
using System;
using System.IO;

namespace Nt.Core.Logging
{
    /// <summary>
    /// Extension methods for the <see cref="FileLogger"/>.
    /// </summary>
    public static class FileLoggerExtensions
    {
        /// <summary>
        /// Adds a new file logger to the specific path
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder) => AddFileLogger(builder, Directory.GetCurrentDirectory());

        /// <summary>
        /// Adds a new file logger to the specific path
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <param name="path">The path where to write to</param>
        /// <param name="options">The file logger options.</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, string path)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <FileLoggerOptions, FileLoggerProvider>(builder.Services);

            return builder;
        }

        /// <summary>
        /// Adds a new file logger to the specific path
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <param name="path">The path where to write to</param>
        /// <param name="options">The file logger options.</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static ILoggingBuilder AddFileLogger(this ILoggingBuilder builder, string path, Action<FileLoggerOptions> configure)
        {
            builder.AddFileLogger();
            builder.Services.Configure(configure);

            return builder;
        }

        /// <summary>
        /// Injects a file logger into the ninjascript service.
        /// </summary>
        /// <param name="builder">The builder</param>
        /// <param name="logPath">The path of the file to log to</param>
        /// <param name="logTop">Whether to display latest logs at the top of the file</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static IHostBuilder AddFileLogger(this IHostBuilder builder, string logPath = "log.txt", bool logTop = true)
        {
            //// Make use of AddLogging extension
            //builder.Services.AddLogging(options =>
            //{
            //    // Add file logger
            //    options.AddFile(logPath, new FileLoggerOptions { LogAtTop = logTop });
            //});

            // Chain the construction
            return builder;
        }
    }
}
