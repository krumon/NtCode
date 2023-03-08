using Nt.Core.Ninjascripts.Configuration;
using Nt.Core.Options;

namespace Nt.Core.Ninjascripts.Options
{
    /// <inheritdoc />
    public class NinjascriptProviderOptionsChangeTokenSource<TOptions, TProvider> : ConfigurationChangeTokenSource<TOptions>
    {
        public NinjascriptProviderOptionsChangeTokenSource(INinjascriptProviderConfiguration<TProvider> providerConfiguration) : base(providerConfiguration.Configuration)
        {
        }
    }
}
