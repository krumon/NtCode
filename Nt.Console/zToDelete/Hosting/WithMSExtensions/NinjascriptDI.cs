using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ConsoleApp;

namespace ConsoleApp
{
    /// <summary>
    /// The core services that could be available in the Dna Framework
    /// for quick and easy access anywhere in code.
    /// </summary>
    /// <example>
    /// <code>
    ///     using static Dna.FrameworkDI
    ///     
    ///     Logger.Log(Configuration["something"]);
    /// </code>
    /// </example>
    public static class NinjascriptDI
    {

        /// <summary>
        /// Gets the configuration
        /// </summary>
        public static IConfiguration Configuration => NinjascriptsService.Provider?.GetService<IConfiguration>();

        /// <summary>
        /// Gets the default logger
        /// </summary>
        public static ILogger Logger => NinjascriptsService.Provider?.GetService<ILogger>();

        /// <summary>
        /// Gets the logger factory for creating loggers
        /// </summary>
        public static ILoggerFactory LoggerFactory => NinjascriptsService.Provider?.GetService<ILoggerFactory>();

        /// <summary>
        /// Gets the framework environment
        /// </summary>
        public static INinjascriptEnvironment FrameworkEnvironment => NinjascriptsService.Provider?.GetService<INinjascriptEnvironment>();

        /// <summary>
        /// Gets the framework exception handler
        /// </summary>
        public static IExceptionHandler ExceptionHandler => NinjascriptsService.Provider?.GetService<IExceptionHandler>();
    }
}
