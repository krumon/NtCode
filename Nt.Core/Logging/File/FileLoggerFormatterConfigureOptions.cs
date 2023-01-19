using Nt.Core.Attributes;
using Nt.Core.DependencyInjection;
using Nt.Core.Logging.Configuration;
using Nt.Core.Options;

namespace Nt.Core.Logging.File
{
    [UnsupportedOSPlatform("browser")]
    internal sealed class FileLoggerFormatterConfigureOptions<TFormatter, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TOptions> : ConfigureFromConfigurationOptions<TOptions>
        where TOptions : FileFormatterOptions
        where TFormatter : FileFormatter
    {
        [RequiresUnreferencedCode(ConsoleLoggerFormatterExtensions.TrimmingRequiresUnreferencedCodeMessage)]
        public FileLoggerFormatterConfigureOptions(ILoggerProviderConfiguration<FileLoggerProvider> providerConfiguration) :
            base(providerConfiguration.Configuration.GetSection("FormatterOptions"))
        {
        }
    }
}
