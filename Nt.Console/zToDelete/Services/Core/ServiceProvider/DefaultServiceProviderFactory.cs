
using System;

namespace ConsoleApp
{
    /// <summary>
    /// Default implementation of <see cref="IServiceProviderFactory{TContainerBuilder}"/>.
    /// </summary>
    public class DefaultServiceProviderFactory : IServiceProviderFactory<INinjascriptServiceCollection>
    {
        private readonly NinjascriptServiceProviderOptions _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultServiceProviderFactory"/> class
        /// with default options.
        /// </summary>
        public DefaultServiceProviderFactory() : this(NinjascriptServiceProviderOptions.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultServiceProviderFactory"/> class
        /// with the specified <paramref name="options"/>.
        /// </summary>
        /// <param name="options">The options to use for this instance.</param>
        public DefaultServiceProviderFactory(NinjascriptServiceProviderOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        /// <inheritdoc />
        public INinjascriptServiceCollection CreateBuilder(INinjascriptServiceCollection services)
        {
            return services;
        }

        /// <inheritdoc />
        public INinjascriptServiceProvider CreateServiceProvider(INinjascriptServiceCollection containerBuilder)
        {
            return containerBuilder.BuildServiceProvider(_options);
        }
    }
}