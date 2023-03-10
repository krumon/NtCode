using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Indicators
{
    public static class SessionsServiceCollectionExtensions
    {
        public static IServiceCollection AddSessionsIndicator(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ISessionsIndicator, SessionsIndicator>();
            services.AddSessionsIterator();
            services.AddSessionsFilters();

            return services;
        }

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
