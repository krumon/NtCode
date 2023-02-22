using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Services
{
    public static class SessionsServiceCollectionExtensions
    {

        public static IServiceCollection AddSessionsIterator(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ISessionsIterator, SessionsIterator>();

            return services;
        }
        public static IServiceCollection AddSessionsFilters(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ISessionsFilters, SessionsFilters>();

            return services;
        }
    }
}
