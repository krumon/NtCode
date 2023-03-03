using Nt.Core.DependencyInjection;

namespace Nt.Scripts.Indicators.Configuration
{
    /// <summary>
    /// Extension methods for setting up indicator services in an <see cref="IIndicatorBuilder" />.
    /// </summary>
    public static class IndicatorBuilderConfigurationExtensions
    {
        /// <summary>
        /// Adds services required to consume <see cref="IIndicatorProviderConfigurationFactory"/> or <see cref="IIndicatorProviderConfiguration{T}"/>
        /// </summary>
        /// <param name="builder">The <see cref="ILoggingBuilder"/> to register services on.</param>
        public static void AddConfiguration(this IIndicatorBuilder builder)
        {
            builder.Services.TryAddSingleton<IIndicatorProviderConfigurationFactory, IndicatorProviderConfigurationFactory>();
            builder.Services.TryAddSingleton(typeof(IIndicatorProviderConfiguration<>), typeof(IndicatorProviderConfiguration<>));
        }
    }
}
