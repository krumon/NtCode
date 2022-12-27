using Nt.Core.Options;

namespace Nt.Core.Logging.Console.Internal
{
    internal sealed class DefaultConsoleLoggerConfigureOptions : ConfigureOptions<ConsoleLoggerOptions>
    {
        public DefaultConsoleLoggerConfigureOptions() : base(options => { })
        {
        }
    }
}
