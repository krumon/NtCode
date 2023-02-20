﻿//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;

//namespace ConsoleApp
//{
//    /// <summary>
//    /// Extension methods for the <see cref="FileLogger"/>.
//    /// </summary>
//    public static class FileLoggerExtensions
//    {
//        /// <summary>
//        /// Adds a new file logger to the specific path
//        /// </summary>
//        /// <param name="builder">The log builder to add to</param>
//        /// <param name="path">The path where to write to</param>
//        /// <returns></returns>
//        public static ILoggingBuilder AddFile(this ILoggingBuilder builder, string path, FileLoggerConfiguration configuration = null)
//        {
//            // Create default configuration if not provided
//            if (configuration == null)
//                configuration = new FileLoggerConfiguration();

//            // Add file log provider to builder
//            builder.AddProvider(new FileLoggerProvider(path, configuration));

//            // Return the builder
//            return builder;
//        }

//        /// <summary>
//        /// Injects a file logger into the ninjascript service.
//        /// </summary>
//        /// <param name="builder">The builder</param>
//        /// <param name="logPath">The path of the file to log to</param>
//        /// <param name="logTop">Whether to display latest logs at the top of the file</param>
//        /// <returns></returns>
//        public static NinjascriptServiceBuilder AddFileLogger(this NinjascriptServiceBuilder builder, string logPath = "log.txt", bool logTop = true)
//        {
//            // Make use of AddLogging extension
//            builder.Services.AddLogging(options =>
//            {
//                // Add file logger
//                options.AddFile(logPath, new FileLoggerConfiguration { LogAtTop = logTop });
//            });

//            // Chain the construction
//            return builder;
//        }
//    }
//}
