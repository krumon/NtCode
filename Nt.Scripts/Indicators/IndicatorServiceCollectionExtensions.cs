using Nt.Core.DependencyInjection;
using Nt.Core.Logging;
using Nt.Core.Options;
using Nt.Scripts.Indicators.Internal;
using System;

namespace Nt.Scripts.Indicators
{
    /// <summary>
    /// Extension methods for setting up logging services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class IndicatorServiceCollectionExtensions
    {
        /// <summary>
        /// Adds indicator services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddIndicators(this IServiceCollection services)
        {
            return AddIndicators(services, builder => { });
        }

        /// <summary>
        /// Adds indicator services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configure">The <see cref="ILoggingBuilder"/> configuration delegate.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddIndicators(this IServiceCollection services, Action<IIndicatorBuilder> configure)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<IIndicatorFactory, IndicatorFactory>());
            services.TryAdd(ServiceDescriptor.Singleton(typeof(IIndicator<>), typeof(Indicator<>)));

            services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<IndicatorFilterOptions>>(
                new DefaultIndicatorFilterConfigureOptions(LogLevel.Information)));

            configure(new IndicatorBuilder(services));

            return services;
        }
    }
}
