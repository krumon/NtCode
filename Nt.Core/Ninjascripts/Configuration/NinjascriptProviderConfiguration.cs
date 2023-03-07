using Nt.Core.Configuration;

namespace Nt.Core.Ninjascripts.Configuration
{
    internal sealed class NinjascriptsProviderConfiguration<T> : INinjascriptsProviderConfiguration<T>
    {
        public NinjascriptsProviderConfiguration(INinjascriptsProviderConfigurationFactory providerConfigurationFactory)
        {
            Configuration = providerConfigurationFactory.GetConfiguration(typeof(T));
        }

        public IConfiguration Configuration { get; }
    }
}
