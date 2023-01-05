using Nt.Core.Options;

namespace Nt.Core.Logging.Console.Internal
{
    internal sealed class DefaultConsoleFormatterConfigureOptions : ConfigureOptions<SimpleConsoleFormatterOptions>
    {
        public DefaultConsoleFormatterConfigureOptions() : base(options => { options.ColorBehavior = LoggerColorBehavior.Default; })
        {
        }
    }
}
