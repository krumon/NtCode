
namespace Nt.Core.Services
{
    /// <summary>
    /// Provides an extension point for creating a container specific builder and an <see cref="INinjascriptServiceProvider"/>.
    /// </summary>
    /// <typeparam name="TContainerBuilder"></typeparam>
    public interface IServiceProviderFactory<TContainerBuilder> // where TContainerBuilder : notnull
    {
        /// <summary>
        /// Creates a container builder from an <see cref="INinjascriptServiceCollection"/>.
        /// </summary>
        /// <param name="services">The collection of services.</param>
        /// <returns>A container builder that can be used to create an <see cref="INinjascriptServiceProvider"/>.</returns>
        TContainerBuilder CreateBuilder(INinjascriptServiceCollection services);

        /// <summary>
        /// Creates an <see cref="INinjascriptServiceProvider"/> from the container builder.
        /// </summary>
        /// <param name="containerBuilder">The container builder.</param>
        /// <returns>An <see cref="INinjascriptServiceProvider"/>.</returns>
        INinjascriptServiceProvider CreateServiceProvider(TContainerBuilder containerBuilder);

    }
}
