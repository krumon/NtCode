using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Services
{
    public static class DataServiceCollectionExtensions
    {

        public static IServiceCollection AddGlobalsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<IGlobalsData, GlobalsData>());

            return services;
        }

    }
}
