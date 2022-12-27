using Nt.Core.Options;
using System;

namespace Nt.Core.Logging
{
    /// <summary>
    /// Options for the built-in console log formatter.
    /// </summary>
    public class ConsoleFormatterConfigureOptions : ConfigureOptions<ConsoleFormatterOptions>
    {
        public ConsoleFormatterConfigureOptions(Action<ConsoleFormatterOptions> action) : base(action)
        {
        }
    }
}
