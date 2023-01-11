using Nt.Core.Attributes;
using Nt.Core.Logging.Console.Internal;
using Nt.Core.Options;
using System;
using System.Collections.Concurrent;

namespace Nt.Core.Logging.Console
{
    [UnsupportedOSPlatform("browser")]
    [ProviderAlias("ColorConsole")]
    public sealed class ColorConsoleLoggerProvider : ILoggerProvider
    {

        #region Private members

        private readonly IDisposable _onChangeToken;
        private ColorConsoleLoggerConfiguration _currentConfig;
        private readonly ConcurrentDictionary<string, ColorConsoleLogger> _loggers =
            new ConcurrentDictionary<string, ColorConsoleLogger>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Constructors

        public ColorConsoleLoggerProvider(
            IOptionsMonitor<ColorConsoleLoggerConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        #endregion

        #region Implementation methods

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new ColorConsoleLogger(name, GetCurrentConfig));

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }

        #endregion

        #region Private methods

        private ColorConsoleLoggerConfiguration GetCurrentConfig() => _currentConfig;

        #endregion

    }
}
