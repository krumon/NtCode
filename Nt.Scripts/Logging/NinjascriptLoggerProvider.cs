using Nt.Core.Logging;
using Nt.Core.Logging.Console;
using Nt.Core.Logging.File;
using Nt.Core.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Nt.Scripts.Logging
{
    /// <summary>
    /// Provides the ability to log to file
    /// </summary>
    [ProviderAlias("Ninjascript")]
    public class NinjascriptLoggerProvider : ILoggerProvider, IDisposable
    {
        #region Private members

        private readonly ConcurrentDictionary<string, NinjascriptLogger> _loggers;
        private readonly IExternalScopeProvider _scopeProvider = null; // NullExternalScopeProvider.Instance;

        private readonly IDisposable _optionsReloadToken;
        private NinjascriptLoggerOptions _currentOptions;
        private ConcurrentDictionary<string, NinjascriptFormatter> _formatters;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of <see cref="NinjascriptLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="ConsoleLogger"/> instances with.</param>
        public NinjascriptLoggerProvider(IOptionsMonitor<NinjascriptLoggerOptions> options) : this(options, Array.Empty<NinjascriptFormatter>()) { }

        /// <summary>
        /// Creates an instance of <see cref="NinjascriptLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="FileLogger"/> instances with.</param>
        /// <param name="formatters">Log formatter added for <see cref="FileLogger"/> instances.</param>
        public NinjascriptLoggerProvider(IOptionsMonitor<NinjascriptLoggerOptions> options, IEnumerable<NinjascriptFormatter> formatters)
        {
            _currentOptions = options.CurrentValue;
            _loggers = new ConcurrentDictionary<string, NinjascriptLogger>();

            SetFormatters(formatters);
            ReloadNinjascriptLoggerOptions(options.CurrentValue);
            _optionsReloadToken = options.OnChange(ReloadNinjascriptLoggerOptions);
        }

        #endregion

        #region ILoggerProvider Implementation

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new NinjascriptLogger(name)
            {
                Options = GetCurrentOptions(),
                Formatter = GetCurrentFormatter(),
                ScopeProvider = _scopeProvider
            });

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
            _loggers.Clear();
        }

        #endregion

        #region Private methods

        private NinjascriptLoggerOptions GetCurrentOptions() => _currentOptions;
        // TODO: Devuelvo un valor por defecto. El usuario debería poder elegir el tipo de formato
        private NinjascriptFormatter GetCurrentFormatter() => null; // _formatters[FileFormatterNames.Normal];
        private void SetFormatters(IEnumerable<NinjascriptFormatter> formatters)
        {
            var cd = new ConcurrentDictionary<string, NinjascriptFormatter>(StringComparer.OrdinalIgnoreCase);

            bool added = false;
            if (formatters != null)
            {
                foreach (NinjascriptFormatter formatter in formatters)
                {
                    cd.TryAdd(formatter.Name, formatter);
                    added = true;
                }
            }

            if (!added)
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

        #endregion

    }
}
