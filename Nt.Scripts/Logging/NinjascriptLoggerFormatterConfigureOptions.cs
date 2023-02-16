using Nt.Core.Attributes;
using Nt.Core.DependencyInjection;
using Nt.Core.Logging.Configuration;
using Nt.Core.Options;

namespace Nt.Scripts.Logging
{
    [UnsupportedOSPlatform("browser")]
    internal sealed class NinjascriptLoggerFormatterConfigureOptions<TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions> : ConfigureFromConfigurationOptions<TOptions>
        where TFormatter : NinjascriptFormatter
        where TOptions : NinjascriptFormatterOptions
    {
        public NinjascriptLoggerFormatterConfigureOptions(ILoggerProviderConfiguration<NinjascriptLoggerProvider> providerConfiguration) :
            base(providerConfiguration.Configuration.GetSection("FormatterOptions"))
        {
        }
    }
}
