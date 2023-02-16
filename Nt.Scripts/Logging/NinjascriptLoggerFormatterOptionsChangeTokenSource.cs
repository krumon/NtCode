using Nt.Core.Attributes;
using Nt.Core.Logging.Configuration;
using Nt.Core.Options;

namespace Nt.Scripts.Logging
{
    [UnsupportedOSPlatform("browser")]
    internal sealed class NinjascriptLoggerFormatterOptionsChangeTokenSource<TFormatter, TOptions> : ConfigurationChangeTokenSource<TOptions>
        where TFormatter : NinjascriptFormatter
        where TOptions : NinjascriptFormatterOptions
    {
        public NinjascriptLoggerFormatterOptionsChangeTokenSource(ILoggerProviderConfiguration<NinjascriptLoggerProvider> providerConfiguration)
            : base(providerConfiguration.Configuration.GetSection("FormatterOptions"))
        {
        }
    }
}
