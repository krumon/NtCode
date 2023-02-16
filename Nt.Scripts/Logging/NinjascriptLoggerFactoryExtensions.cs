using Nt.Core.DependencyInjection;
using Nt.Core.Logging;
using Nt.Core.Logging.Configuration;
using Nt.Core.Logging.Options;
using System;

namespace Nt.Scripts.Logging
{
    /// <summary>
    /// Extension methods for the <see cref="NinjascriptLogger"/>.
    /// </summary>
    public static class NinjascriptLoggerFactoryExtensions
    {
        /// <summary>
        /// Adds a new file logger to the specific path
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <param name="path">The path where to write to</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static ILoggingBuilder AddNinjascriptOutput(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.AddNinjascriptFormatter<NinjascriptFormatter, NinjascriptFormatterOptions>();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, NinjascriptLoggerProvider>());
            LoggerProviderOptions.RegisterProviderOptions<NinjascriptLoggerOptions, NinjascriptLoggerProvider>(builder.Services);

            return builder;
        }

        /// <summary>
        /// Adds a new file logger to the specific path
        /// </summary>
        /// <param name="builder">The log builder to add to</param>
        /// <param name="options">The file logger options.</param>
        /// <returns>The same instance of the <see cref="ILoggingBuilder"/> for chaining.</returns>
        public static ILoggingBuilder AddNinjascriptOutput(this ILoggingBuilder builder, Action<NinjascriptLoggerOptions> options)
        {
            builder.AddNinjascriptOutput();
            builder.Services.Configure(options);

            return builder;
        }

        internal static ILoggingBuilder AddNinjascriptWithFormatter<TOptions>(this ILoggingBuilder builder, string name, Action<TOptions> configure)
            where TOptions : NinjascriptFormatterOptions
        {
            if (configure == null)
                throw new ArgumentNullException(nameof(configure));

            builder.AddNinjascriptFormatterWithName(name);
            builder.Services.Configure(configure);

            return builder;
        }
        private static ILoggingBuilder AddNinjascriptFormatterWithName(this ILoggingBuilder builder, string name) =>
            builder.AddNinjascriptOutput((NinjascriptLoggerOptions options) => options.FormatterName = name);

    }
}
