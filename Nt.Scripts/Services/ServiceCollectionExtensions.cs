using NinjaTrader.Gui.Chart;
using NinjaTrader.NinjaScript;
using Nt.Core.DependencyInjection;
using System;

namespace Nt.Scripts.Services
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds ninjatrader objects passed by the builder.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="ninjatraderObjects">The ninjatrader objects.</param>
        /// <returns>The <see cref="IServiceCollection"/> to continue builder the host.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddNinjatraderObjects(this IServiceCollection services, object[] ninjatraderObjects)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (ninjatraderObjects == null || ninjatraderObjects.Length == 0)
                return services;

            services
                .AddNinjascript(TryGetNinjatraderObject<NinjaScriptBase>(ninjatraderObjects))
                .AddGlobalsData()
                .AddChartBarsData(TryGetNinjatraderObject<ChartBars>(ninjatraderObjects));

            return services;
        }

        /// <summary>
        /// Adds ninjatrader objects passed by the builder.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="ninjatraderObjects">The ninjatrader objects.</param>
        /// <returns>The <see cref="IServiceCollection"/> to continue builder the host.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="IServiceCollection"/> cannot be null.</exception>
        public static IServiceCollection TryAddNinjatraderObjects(this IServiceCollection services, object[] ninjatraderObjects)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services
                .TryAddNinjascript(TryGetNinjatraderObject<NinjaScriptBase>(ninjatraderObjects))
                .TryAddGlobalsData()
                .TryAddChartBarsData(TryGetNinjatraderObject<ChartBars>(ninjatraderObjects));

            return services;
        }

        /// <summary>
        /// Adds sessions services to the services container.
        /// </summary>
        /// <param name="services">The services container.</param>
        /// <returns>The <see cref="IServiceCollection"/> to continue builder the host.</returns>
        /// <exception cref="ArgumentNullException">The <see cref="IServiceCollection"/> cannot be null.</exception>
        public static IServiceCollection AddSessionsManager(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddSingleton<ISessionsManager, SessionsManager>();

            return services;
        }

        private static T TryGetNinjatraderObject<T>(object[] ninjatraderObjects)
            where T : class
        {
            T searchObject = null;
            try
            {
                if (ninjatraderObjects == null)
                    // TODO: Changes for return.
                    throw new ArgumentNullException(nameof(ninjatraderObjects));

                if (ninjatraderObjects.Length == 0)
                    // TODO: Changes for return.
                    throw new ArgumentException("The ninjascript objects array cannot be empty.");

                foreach (var o in ninjatraderObjects)
                    if (o is T t)
                    {
                        searchObject = t;
                        break;
                    }
            }
            catch
            {
            }

            return searchObject;
        }
    }
}
