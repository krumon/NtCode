using Nt.Core.DependencyInjection;
using Nt.Scripts.Ninjascripts;
using System;

namespace Nt.Scripts.Indicators
{
    public static class SessionsNinjascriptBuilderExtensions
    {
        /// <summary>
        /// Adds logging services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder AddSessions(this INinjascriptBuilder builder)
        {
            return AddSessions(builder, config => { });
        }

        /// <summary>
        /// Adds logging services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configure">The <see cref="ILoggingBuilder"/> configuration delegate.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static INinjascriptBuilder AddSessions(this INinjascriptBuilder builder, Action<ISessionsBuilder> configure)
        {
            //services.TryAdd(ServiceDescriptor.Singleton<ISessionsFactory, LoggerFactory>());
            //services.TryAdd(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));

            //services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<LoggerFilterOptions>>(
            //    new DefaultLoggerLevelConfigureOptions(LogLevel.Information)));

            builder.Services.AddSingleton<ISessionsIndicator, SessionsIndicator>();
            configure(new SessionsBuilder(builder.Services));

            return builder;
        }

        //public static IServiceCollection AddSessionsIndicator(this ISessionsBuilder builder)
        //{
        //    if (builder == null)
        //        throw new ArgumentNullException(nameof(services));

        //    builder.AddConfiguration();

        //    builder.AddNinjatraderLoggerFormatter<OutputWindowFormatter, OutputWindowFormatterOptions>();

        //    builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, NinjatraderLoggerProvider>());
        //    LoggerProviderOptions.RegisterProviderOptions<NinjatraderLoggerOptions, NinjatraderLoggerProvider>(builder.Services);

        //    //services.AddSessionsIterator();
        //    //services.AddSessionsFilters();
        //    services.AddSingleton<ISessionsIndicator, SessionsIndicator>();

        //    return services;
        //}

        /// <summary>
        /// Adds <see cref="ISessionsIterator"/> service to the <see cref="IServiceCollection"/> container.
        /// </summary>
        /// <param name="services">The service container.</param>
        /// <returns>The <see cref="IServiceCollection"/> to continua adding services.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="IServiceCollection"/> cannot be null.</exception>
        public static IServiceCollection AddSessionsIterator(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ISessionsIterator, SessionsIterator>();

            return services;
        }

        /// <summary>
        /// Adds <see cref="ISessionsFilters"/> service to the <see cref="IServiceCollection"/> container.
        /// </summary>
        /// <param name="services">The service container.</param>
        /// <returns>The <see cref="IServiceCollection"/> to continue adding services.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="IServiceCollection"/> cannot be null.</exception>
        public static IServiceCollection AddSessionsFilters(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ISessionsFilters, SessionsFilters>();

            return services;
        }
    }
}
