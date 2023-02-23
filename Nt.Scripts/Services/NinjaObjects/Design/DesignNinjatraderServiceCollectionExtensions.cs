﻿using NinjaTrader.Gui.Chart;
using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Services.Design
{
    public static class DesignNinjatraderServiceCollectionExtensions
    {

        public static IServiceCollection AddDesignGlobalsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<IGlobalsData, DesignGlobalsData>());

            return services;
        }
        public static IServiceCollection TryAddDesignGlobalsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            try
            {
                AddDesignGlobalsData(services);
            }
            catch { }

            return services;
        }
        public static IServiceCollection AddDesignNinjascript(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<INinjascript>(new DesignNinjascript()));

            return services;
        }
        public static IServiceCollection TryAddDesignNinjascript(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            try
            {
                AddDesignNinjascript(services);
            }
            catch 
            {
            }

            return services;
        }
        public static IServiceCollection AddDesignChartBarsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
             
            services.TryAdd(ServiceDescriptor.Singleton<IChartBarsData>(new DesignChartBarsData()));

            return services;
        }
        public static IServiceCollection TryAddDesignChartBarsData(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            try
            {
                AddDesignChartBarsData(services);
            }
            catch 
            {
            }

            return services;
        }
    }
}