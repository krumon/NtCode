using Nt.Core.DependencyInjection;
using Nt.Core.Options;
using System;

namespace Nt.Core.Ninjascripts
{
    /// <summary>
    /// Extension methods for setting up ninjascripts services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class NinjascriptsServiceCollectionExtensions
    {

        /// <summary>
        /// Adds ninjascript services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddNinjascripts(this IServiceCollection services)
        {
            return AddNinjascripts(services, builder => { });
        }

        /// <summary>
        /// Adds ninjascript services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <param name="configure">The <see cref="INinjascriptsBuilder"/> configuration delegate.</param>
        /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
        public static IServiceCollection AddNinjascripts(this IServiceCollection services, Action<INinjascriptsBuilder> configure)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor.Singleton<INinjascriptsFactory, NinjascriptsFactory>());
            services.TryAdd(ServiceDescriptor.Singleton(typeof(INinjascripts<>), typeof(Ninjascripts<>)));

            //services.TryAddEnumerable(ServiceDescriptor.Singleton<IConfigureOptions<LoggerFilterOptions>>(
            //    new DefaultLoggerLevelConfigureOptions(LogLevel.Information)));

            configure(new NinjascriptsBuilder(services));

            return services;
        }

    }
}
