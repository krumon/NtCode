using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Services
{
    public static class SessionsServiceCollectionExtensions
    {

        public static IServiceCollection AddSessions(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ISessionsIterator, SessionsIterator>();
            //services.AddSingleton<ISessionsFiltersService, SessionsFiltersScript>();
            //services.AddSingleton<ISessionsService, SessionsScript>();

            return services;
        }
    }
}
