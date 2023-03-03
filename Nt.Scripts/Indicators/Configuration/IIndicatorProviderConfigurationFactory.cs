using Nt.Core.Configuration;
using System;

namespace Nt.Scripts.Indicators.Configuration
{
    /// <summary>
    /// Allows access to configuration section associated with indicator provider
    /// </summary>
    public interface IIndicatorProviderConfigurationFactory
    {
        /// <summary>
        /// Return configuration section associated with indicator provider
        /// </summary>
        /// <param name="providerType">The indicator provider type.</param>
        /// <returns>The <see cref="IConfiguration"/> for the given providerType.</returns>
        IConfiguration GetConfiguration(Type providerType);
    }
}
