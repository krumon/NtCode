using Nt.Core.Configuration;
using Nt.Core.Logging;
using Nt.Core.Logging.Internal;
using Nt.Core.Options;
using System;

namespace Nt.Core.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up logging services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class LoggingServiceCollectionExtensions
    {
        private static bool sectionIsAdded = false;
        /// <summary>
        /// Adds logging services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddLogging(this IServiceCollection services)
        {
            return AddLogging(services, builder => { });
        }

        /// <summary>
        /// Adds logging services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddLogging(this IServiceCollection services, IConfiguration configuration)
        {
            return AddLogging(services, builder => { }, configuration);
        }

        /// <summary>
        /// Adds logging services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configure">The <see cref="ILoggingBuilder"/> configuration delegate.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddLogging(this IServiceCollection services, Action<ILoggingBuilder> configure)
            => AddLogging(services, configure, null);

        /// <summary>
        /// Adds logging services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configure">The <see cref="ILoggingBuilder"/> configuration delegate.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddLogging(this IServiceCollection services, Action<ILoggingBuilder> configure, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<ILoggerFactory, LoggerFactory>());
            services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<LoggerFilterOptions>>(
                new DefaultLoggerLevelConfigureOptions(LogLevel.Information)));

            var loggingBuilder = new LoggingBuilder(services);
            loggingBuilder
                .AddConsole()
                .AddDebug();

            if (configuration != null)
            {
                IConfigurationSection section = configuration.GetSection("Logging");
                if (section.Exists() && !sectionIsAdded)
                    loggingBuilder.AddConfiguration(section);
            }

            configure(loggingBuilder);
            //configure(new LoggingBuilder(services));
            return services;
        }
    }
}
