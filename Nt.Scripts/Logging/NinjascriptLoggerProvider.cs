using Nt.Core.Logging;
using Nt.Core.Logging.Console;
using Nt.Core.Logging.File;
using Nt.Core.Options;
using Nt.Scripts.Ninjascripts;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nt.Scripts.Logging
{
    /// <summary>
    /// Provides the ability to log to ninjatrader output windows.
    /// </summary>
    [ProviderAlias("Ninjascript")]
    public class NinjascriptLoggerProvider : ILoggerProvider, IDisposable
    {
        private readonly ConcurrentDictionary<string, NinjascriptLogger> _loggers;
        private readonly IDisposable _optionsReloadToken;
        private NinjascriptLoggerOptions _currentOptions;
        private ConcurrentDictionary<string, NinjascriptFormatter> _formatters;
        readonly Action<object> _ninjascriptPrintMethod;
        readonly Action _ninjascriptClearMethod;

        /// <summary>
        /// Creates an instance of <see cref="NinjascriptLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="ConsoleLogger"/> instances with.</param>
        public NinjascriptLoggerProvider(INinjascript ninjascript, IOptionsMonitor < NinjascriptLoggerOptions> options) : this(ninjascript, options, Array.Empty<NinjascriptFormatter>()) { }

        /// <summary>
        /// Creates an instance of <see cref="NinjascriptLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="FileLogger"/> instances with.</param>
        /// <param name="formatters">Log formatter added for <see cref="FileLogger"/> instances.</param>
        public NinjascriptLoggerProvider(INinjascript ninjascript, IOptionsMonitor<NinjascriptLoggerOptions> options, IEnumerable<NinjascriptFormatter> formatters)
        {
            if (ninjascript == null)
                throw new ArgumentNullException(nameof(ninjascript));
            _ninjascriptPrintMethod = ninjascript.Instance.Print;
            _ninjascriptClearMethod = ninjascript.Instance.ClearOutputWindow;

            _currentOptions = options.CurrentValue;
            _loggers = new ConcurrentDictionary<string, NinjascriptLogger>();

            SetFormatters(formatters);
            ReloadNinjascriptLoggerOptions(options.CurrentValue);
            _optionsReloadToken = options.OnChange(ReloadNinjascriptLoggerOptions);
        }

        // TODO: Delete this constructor. Is only necesary for testing in console.
        public NinjascriptLoggerProvider(IOptionsMonitor<NinjascriptLoggerOptions> options, IEnumerable<NinjascriptFormatter> formatters)
        {
            _currentOptions = options.CurrentValue;
            _loggers = new ConcurrentDictionary<string, NinjascriptLogger>();

            SetFormatters(formatters);
            ReloadNinjascriptLoggerOptions(options.CurrentValue);
            _optionsReloadToken = options.OnChange(ReloadNinjascriptLoggerOptions);
        }

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new NinjascriptLogger(name,_ninjascriptPrintMethod,_ninjascriptClearMethod)
            {
                Options = GetCurrentOptions(),
                Formatter = GetCurrentFormatter(),
            });

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
            _loggers.Clear();
        }

        private NinjascriptLoggerOptions GetCurrentOptions() => _currentOptions;
        private NinjascriptFormatter GetCurrentFormatter() 
        {
            if (_currentOptions.FormatterName == null || !_formatters.TryGetValue(_currentOptions.FormatterName, out NinjascriptFormatter ninjascriptFormatter))
                ninjascriptFormatter = _formatters[NinjascriptFormatterNames.Output];

            return ninjascriptFormatter;
        }

        private void SetFormatters(IEnumerable<NinjascriptFormatter> formatters)
        {
            var cd = new ConcurrentDictionary<string, NinjascriptFormatter>(StringComparer.OrdinalIgnoreCase);

            if (formatters != null)
            {
                foreach (NinjascriptFormatter formatter in formatters)
                {
                    cd.TryAdd(formatter.Name, formatter);
                }
            }

            if (cd.Count == 0 || !cd.ContainsKey(NinjascriptFormatterNames.Output))
            {
                cd.TryAdd(NinjascriptFormatterNames.Output, new NinjascriptOutputFormatter(new NinjascriptFormatterOptionsMonitor<NinjascriptOutputFormatterOptions>(new NinjascriptOutputFormatterOptions())));
            }

            _formatters = cd;
        }

        // warning:  ReloadLoggerOptions can be called before the ctor completed,... before registering all of the state used in this method need to be initialized
        private void ReloadNinjascriptLoggerOptions(NinjascriptLoggerOptions currentOptions)
        {
            _currentOptions = currentOptions ?? new NinjascriptLoggerOptions();

            foreach (KeyValuePair<string, NinjascriptLogger> logger in _loggers)
            {
                logger.Value.Options = GetCurrentOptions();
                logger.Value.Formatter = GetCurrentFormatter();
            }
        }
    }
}
