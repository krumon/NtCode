﻿using Nt.Core.Attributes;
using Nt.Core.DependencyInjection;
using Nt.Core.Logging.Console.Internal;
using Nt.Core.Logging.Internal;
using Nt.Core.Options;
using Nt.Core.Reflection;
using System;

namespace Nt.Core.Logging.Console
{
    public static class ConsoleLoggerExtensions
    {
        internal const string TrimmingRequiresUnreferencedCodeMessage = "TOptions's dependent types may have their members trimmed. Ensure all required members are preserved.";

        /// <summary>
        /// Adds a console logger named 'Console' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        //[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode",
        //    Justification = "AddConsoleFormatter and RegisterProviderOptions are only dangerous when the Options type cannot be statically analyzed, but that is not the case here. " +
        //    "The DynamicallyAccessedMembers annotations on them will make sure to preserve the right members from the different options objects.")]
        public static ILoggingBuilder AddConsole(this ILoggingBuilder builder)
        {
            //builder.AddConfiguration();

            builder.AddConsoleFormatter<ConsoleFormatter, ConsoleFormatterOptions>();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ConsoleLoggerProvider>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<ConsoleLoggerOptions>>(new DefaultConsoleLoggerConfigureOptions()));
            //LoggerProviderOptions.RegisterProviderOptions<ConsoleLoggerOptions, ConsoleLoggerProvider>(builder.Services);

            return builder;
        }

        /// <summary>
        /// Adds a console logger named 'Console' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <param name="configure">A delegate to configure the <see cref="ConsoleLogger"/>.</param>
        public static ILoggingBuilder AddConsole(this ILoggingBuilder builder, Action<ConsoleLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddConsoleFormatter<ConsoleFormatter, ConsoleFormatterOptions>();
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, ConsoleLoggerProvider>());
            builder.Services.Configure(configure);

            return builder;
        }

        /// <summary>
        /// Add the default console log formatter named 'simple' to the factory with default properties.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        public static ILoggingBuilder AddConsole(this ILoggingBuilder builder, string name) =>
            builder.AddFormatterWithName(name);

        /// <summary>
        /// Add and configure a console log formatter named 'simple' to the factory.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <param name="configure">A delegate to configure the <see cref="ConsoleLogger"/> options for the built-in default log formatter.</param>
        public static ILoggingBuilder AddConsole(this ILoggingBuilder builder, Action<ConsoleFormatterOptions> configure, string name = "Krumon")
        {
            return builder.AddConsoleWithFormatter(name, configure);
        }

        internal static ILoggingBuilder AddConsoleWithFormatter<TOptions>(this ILoggingBuilder builder, string name, Action<TOptions> configure)
            where TOptions : ConsoleFormatterOptions
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }
            builder.AddFormatterWithName(name);
            builder.Services.Configure(configure);

            return builder;
        }

        private static ILoggingBuilder AddFormatterWithName(this ILoggingBuilder builder, string name) =>
            builder.AddConsole((ConsoleLoggerOptions options) => options.FormatterName = name);

        /// <summary>
        /// Adds a custom console logger formatter 'TFormatter' to be configured with options 'TOptions'.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        //[RequiresUnreferencedCode(TrimmingRequiresUnreferencedCodeMessage)]
        public static ILoggingBuilder AddConsoleFormatter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions>(this ILoggingBuilder builder)
            where TOptions : ConsoleFormatterOptions
            where TFormatter : BaseConsoleFormatter
        {
            //builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<BaseConsoleFormatter, TFormatter>());
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<TOptions>>(new DefaultConsoleFormatterConfigureOptions()));
            //builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IOptionsChangeTokenSource<TOptions>, ConsoleLoggerFormatterOptionsChangeTokenSource<TFormatter, TOptions>>());

            return builder;
        }

        /// <summary>
        /// Adds a custom console logger formatter 'TFormatter' to be configured with options 'TOptions'.
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to use.</param>
        /// <param name="configure">A delegate to configure options 'TOptions' for custom formatter 'TFormatter'.</param>
        //[RequiresUnreferencedCode(TrimmingRequiresUnreferencedCodeMessage)]
        public static ILoggingBuilder AddConsoleFormatter<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions>(this ILoggingBuilder builder, Action<TOptions> configure)
            where TOptions : ConsoleFormatterOptions
            where TFormatter : BaseConsoleFormatter
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddConsoleFormatter<TFormatter, TOptions>();
            builder.Services.Configure(configure);
            return builder;
        }
    }

    //internal sealed class ConsoleLoggerFormatterConfigureOptions<TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions> : ConfigureFromConfigurationOptions<TOptions>
    //    where TOptions : ConsoleFormatterOptions
    //    where TFormatter : BaseConsoleFormatter
    //{
    //    //[RequiresUnreferencedCode(ConsoleLoggerExtensions.TrimmingRequiresUnreferencedCodeMessage)]
    //    public ConsoleLoggerFormatterConfigureOptions(ILoggerProviderConfiguration<ConsoleLoggerProvider> providerConfiguration) :
    //        base(providerConfiguration.Configuration.GetSection("FormatterOptions"))
    //    {
    //    }
    //}

    //internal sealed class ConsoleLoggerFormatterOptionsChangeTokenSource<TFormatter, TOptions> : ConfigurationChangeTokenSource<TOptions>
    //    where TOptions : ConsoleFormatterOptions
    //    where TFormatter : ConsoleFormatter
    //{
    //    public ConsoleLoggerFormatterOptionsChangeTokenSource(ILoggerProviderConfiguration<ConsoleLoggerProvider> providerConfiguration)
    //        : base(providerConfiguration.Configuration.GetSection("FormatterOptions"))
    //    {
    //    }
    //}
}
