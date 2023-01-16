using Nt.Core.Options;
using System;

namespace Nt.Core.Logging.Console.Internal
{
    internal sealed class ConsoleFormatterOptionsMonitor<TOptions> : IOptionsMonitor<TOptions>
        where TOptions : SimpleConsoleFormatterOptions
    {
        private TOptions _options;
        public ConsoleFormatterOptionsMonitor(TOptions options)
        {
            _options = options;
        }

        public TOptions Get(string name) => _options;

        public IDisposable OnChange(Action<TOptions, string> listener)
        {
            return null;
        }

        public TOptions CurrentValue => _options;
    }
}
