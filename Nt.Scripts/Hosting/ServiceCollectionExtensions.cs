﻿using Nt.Core.DependencyInjection;
using Nt.Core.Services;
using Nt.Scripts.Ninjascripts;
using Nt.Scripts.Ninjascripts.Design;
using System;

namespace Nt.Scripts
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddSessions<TService>(this IServiceCollection services, Action<ISessionsBuilder> action)
        {
            ISessionsBuilder builder = new SessionsBuilder(services);
            action?.Invoke(builder);
            builder.Build();
            services.AddSingleton<ISessionsIteratorService, SessionsIteratorDesignScript>();
            services.AddSingleton<ISessionsFiltersService, SessionsFiltersScript>();
            services.AddSingleton<ISessionsService, SessionsScript>();
            return services;
        }
    }
}