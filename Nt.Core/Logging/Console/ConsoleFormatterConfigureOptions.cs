using Nt.Core.Options;
using System;

namespace Nt.Core.Logging
{
    /// <summary>
    /// Options for the built-in console log formatter.
    /// </summary>
    public class ConsoleFormatterConfigureOptions : ConfigureOptions<SimpleConsoleFormatterOptions>
    {
        public ConsoleFormatterConfigureOptions(Action<SimpleConsoleFormatterOptions> action) : base(action)
        {
        }
    }
}
