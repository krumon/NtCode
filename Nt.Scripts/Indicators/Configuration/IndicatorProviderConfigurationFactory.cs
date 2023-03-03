using Nt.Core.Configuration;
using Nt.Core.Logging;
using System;
using System.Collections.Generic;

namespace Nt.Scripts.Indicators.Configuration
{
    internal sealed class IndicatorProviderConfigurationFactory : IIndicatorProviderConfigurationFactory
    {
        private readonly IEnumerable<IndicatorConfiguration> _configurations;

        public IndicatorProviderConfigurationFactory(IEnumerable<IndicatorConfiguration> configurations)
        {
            _configurations = configurations;
        }

        public IConfiguration GetConfiguration(Type providerType)
        {
            if (providerType == null)
                throw new ArgumentNullException(nameof(providerType));

            string fullName = providerType.FullName;
            string alias = ProviderAliasAttribute.GetAlias(providerType);
            var configurationBuilder = new ConfigurationBuilder();
            foreach (IndicatorConfiguration configuration in _configurations)
            {
                IConfigurationSection sectionFromFullName = configuration.Configuration.GetSection(fullName);
                configurationBuilder.AddConfiguration(sectionFromFullName);

                if (!string.IsNullOrWhiteSpace(alias))
                {
                    IConfigurationSection sectionFromAlias = configuration.Configuration.GetSection(alias);
                    configurationBuilder.AddConfiguration(sectionFromAlias);
                }
            }
            return configurationBuilder.Build();
        }
    }
}
