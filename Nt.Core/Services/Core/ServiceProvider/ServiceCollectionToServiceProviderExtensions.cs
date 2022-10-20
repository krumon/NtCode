using System;

namespace Nt.Core.Services
{
    /// <summary>
    /// Extension methods for building a <see cref="NinjascriptServiceProvider"/> from an <see cref="INinjascriptServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionToServiceProviderExtensions
    {

        /// <summary>
        /// Creates a <see cref="NinjascriptServiceProvider"/> containing services from the provided <see cref="INinjascriptServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="INinjascriptServiceCollection"/> containing service descriptors.</param>
        /// <returns>The <see cref="NinjascriptServiceProvider"/>.</returns>
        public static NinjascriptServiceProvider BuildServiceProvider(this INinjascriptServiceCollection services)
        {
            return BuildServiceProvider(services, NinjascriptServiceProviderOptions.Default);
        }

        /// <summary>
        /// Creates a <see cref="NinjascriptServiceProvider"/> containing services from the provided <see cref="INinjascriptServiceCollection"/>
        /// optionally enabling scope validation.
        /// </summary>
        /// <param name="services">The <see cref="INinjascriptServiceCollection"/> containing service descriptors.</param>
        /// <param name="validateScopes">
        /// <c>true</c> to perform check verifying that scoped services never gets resolved from root provider; otherwise <c>false</c>.
        /// </param>
        /// <returns>The <see cref="NinjascriptServiceProvider"/>.</returns>
        public static NinjascriptServiceProvider BuildServiceProvider(this INinjascriptServiceCollection services, bool validateScopes)
        {
            return services.BuildServiceProvider(new NinjascriptServiceProviderOptions { ValidateScopes = validateScopes });
        }

        /// <summary>
        /// Creates a <see cref="NinjascriptServiceProvider"/> containing services from the provided <see cref="IServiceCollection"/>
        /// optionally enabling scope validation.
        /// </summary>
        /// <param name="services">The <see cref="INinjascriptServiceCollection"/> containing service descriptors.</param>
        /// <param name="options">
        /// Configures various service provider behaviors.
        /// </param>
        /// <returns>The <see cref="NinjascriptServiceProvider"/>.</returns>
        public static NinjascriptServiceProvider BuildServiceProvider(this INinjascriptServiceCollection services, NinjascriptServiceProviderOptions options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return new NinjascriptServiceProvider(services, options);
        }
    }
}
