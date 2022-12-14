using Nt.Core.Data;
using Nt.Core.DependencyInjection;
using System;
using System.Runtime.CompilerServices;

namespace Nt.Core.Services
{
    ///// <summary>
    ///// Extensions methods for add service to the <see cref="ISessionCollection"/>.
    ///// </summary>
    //public static class ServiceCollectionExtensions
    //{

    //    public static IServiceCollection AddSessionService<TImplementation>(this IServiceCollection services, Action<ISessionsBuilder> builderActions)
    //        where TImplementation : ISessionsService, new()
    //    {
    //        if (builderActions == null)
    //            throw new ArgumentNullException(nameof(builderActions));

    //        ISessionsBuilder builder = new SessionsBuilder();

    //        builderActions(builder);

    //        ISessionsService service = builder.Build<TImplementation>();

    //        services.Add(service);

    //        return services;
    //    }
    //}
}
