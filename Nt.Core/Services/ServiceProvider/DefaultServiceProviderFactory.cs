
namespace Nt.Core.Services
{
    /// <summary>
    /// Default implementation of <see cref="IServiceProviderFactory{TContainerBuilder}"/>.
    /// </summary>
    public class DefaultServiceProviderFactory : IServiceProviderFactory<INinjascriptServiceCollection>
    {
        //private readonly ServiceProviderOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultServiceProviderFactory"/> class
        /// with default options.
        /// </summary>
        //public DefaultServiceProviderFactory() : this(ServiceProviderOptions.Default)
        //{

        //}

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultServiceProviderFactory"/> class
        /// with the specified <paramref name="options"/>.
        /// </summary>
        /// <param name="options">The options to use for this instance.</param>
        //public DefaultServiceProviderFactory(ServiceProviderOptions options)
        //{
        //    if (options == null)
        //    {
        //        throw new ArgumentNullException(nameof(options));
        //    }

        //    _options = options;
        //}

        /// <inheritdoc />
        public INinjascriptServiceCollection CreateBuilder(INinjascriptServiceCollection services)
        {
            return services;
        }

        /// <inheritdoc />
        public INinjascriptServiceProvider CreateServiceProvider(INinjascriptServiceCollection containerBuilder)
        {
            return default;
            //return containerBuilder.BuildServiceProvider(_options);
        }
    }
}