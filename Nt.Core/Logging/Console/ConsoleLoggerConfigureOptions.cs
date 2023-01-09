using Nt.Core.Options;
using System;

namespace Nt.Core.Logging.Console
{
    internal class ConsoleLoggerConfigureOptions : ConfigureOptions<ConsoleLoggerOptions>
    {
        public ConsoleLoggerConfigureOptions(Action<ConsoleLoggerOptions> action) : base(action)
        {
        }
    }
}
