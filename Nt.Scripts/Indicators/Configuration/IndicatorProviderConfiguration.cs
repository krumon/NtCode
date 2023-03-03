using Nt.Core.Configuration;

namespace Nt.Scripts.Indicators.Configuration
{
    internal sealed class IndicatorProviderConfiguration<T> : IIndicatorProviderConfiguration<T>
    {
        public IndicatorProviderConfiguration(IIndicatorProviderConfigurationFactory providerConfigurationFactory)
        {
            Configuration = providerConfigurationFactory.GetConfiguration(typeof(T));
        }

        public IConfiguration Configuration { get; }
    }
}
