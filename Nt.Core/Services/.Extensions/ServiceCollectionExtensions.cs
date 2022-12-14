using Nt.Core.Data;
using Nt.Core.DependencyInjection;
using System;
using System.Runtime.CompilerServices;

namespace Nt.Core.Services
{
    /// <summary>
    /// Extensions methods for add service to the <see cref="ISessionCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddSessionService<TImplementation>(this IServiceCollection services, Action<ISessionBuilder> builderActions)
            where TImplementation : ISessionService, new()
        {
            if (builderActions == null)
                throw new ArgumentNullException(nameof(builderActions));

            ISessionBuilder builder = new SessionBuilder();

            builderActions(builder);

            ISessionService service = builder.Build<TImplementation>();

            services.Add(service);

            return services;
        }
    }
}
