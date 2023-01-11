using Nt.Core.DependencyInjection;
using Nt.Core.Logging.Configuration;
using Nt.Core.Logging.Console;
using System;

namespace Nt.Core.Logging
{
    /// <summary>
    /// Builder extensions methods for <see cref="ColorConsoleLogger"/>.
    /// </summary>
    public static class ColorConsoleLoggerExtensions
    {

        public static ILoggingBuilder AddColorConsoleLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, ColorConsoleLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <ColorConsoleLoggerConfiguration, ColorConsoleLoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddColorConsoleLogger(
            this ILoggingBuilder builder,
            Action<ColorConsoleLoggerConfiguration> configure)
        {
            builder.AddColorConsoleLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
