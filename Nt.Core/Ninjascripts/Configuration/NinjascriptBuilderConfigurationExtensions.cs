using Nt.Core.DependencyInjection;

namespace Nt.Core.Ninjascripts.Configuration
{
    /// <summary>
    /// Extension methods for setting up ninjascript services in an <see cref="INinjascriptsBuilder" />.
    /// </summary>
    public static class NinjascriptsBuilderConfigurationExtensions
    {
        /// <summary>
        /// Adds services required to consume <see cref="INinjascriptsProviderConfigurationFactory"/> or <see cref="INinjascriptsProviderConfiguration{T}"/>
        /// </summary>
        /// <param name="builder">The <see cref="INinjascriptsBuilder"/> to register services on.</param>
        public static void AddConfiguration(this INinjascriptsBuilder builder)
        {
            builder.Services.TryAddSingleton<INinjascriptsProviderConfigurationFactory, NinjascriptsProviderConfigurationFactory>();
            builder.Services.TryAddSingleton(typeof(INinjascriptsProviderConfiguration<>), typeof(NinjascriptsProviderConfiguration<>));
        }
    }
}
