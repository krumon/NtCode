using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Nt.Core.Logging.Internal;
using Nt.Core.Options;

namespace Nt.Core.Logging.File
{
    /// <summary>
    /// Provides the ability to log to file
    /// </summary>
    [ProviderAlias("File")]
    public class FileLoggerProvider : ILoggerProvider, IDisposable
    {
        #region Private members

        private readonly ConcurrentDictionary<string, FileLogger> _loggers;
        private readonly IExternalScopeProvider _scopeProvider = NullExternalScopeProvider.Instance;

        private readonly IDisposable _optionsReloadToken;
        private readonly IDisposable _formatterReloadToken;
        private FileLoggerOptions _currentOptions;
        private FileFormatter _currentFormatter;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of <see cref="ConsoleLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="ConsoleLogger"/> instances with.</param>
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options)
            : this(options, null) { }

        /// <summary>
        /// Creates an instance of <see cref="ConsoleLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="FileLogger"/> instances with.</param>
        /// <param name="formatter">Log formatter added for <see cref="FileLogger"/> instances.</param>
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options, IOptionsMonitor<FileFormatter> formatter = null)
        {
            _loggers = new ConcurrentDictionary<string, FileLogger>();
            _currentOptions = options.CurrentValue;
            _currentFormatter = formatter == null ? new FileFormatter() : formatter.CurrentValue;

            SetFormatters(formatter);
            ReloadFileLoggerOptions(options.CurrentValue);
            _optionsReloadToken = options.OnChange(ReloadFileLoggerOptions);
            if (formatter != null)
            {
                ReloadFileLoggersFormatter(formatter.CurrentValue);
                _formatterReloadToken = formatter.OnChange(ReloadFileLoggersFormatter);
            }
        }

        #endregion

        #region ILoggerProvider Implementation

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new FileLogger(name)
            {
                Options = GetCurrentOptions(),
                Formatter = GetCurrentFormatter(),
                ScopeProvider = _scopeProvider
            });

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
            _formatterReloadToken?.Dispose();
            _loggers.Clear();
        }

        #endregion

        #region Private methods

        private FileLoggerOptions GetCurrentOptions() => _currentOptions;
        private FileFormatter GetCurrentFormatter() => _currentFormatter;

        private void SetFormatters(IOptionsMonitor<FileFormatter> formatter = null)
        {
            _currentFormatter = formatter.CurrentValue ?? new FileFormatter();

            foreach (KeyValuePair<string, FileLogger> logger in _loggers)
            {
                logger.Value.Formatter = _currentFormatter;
            }
        }

        // warning:  ReloadLoggerOptions can be called before the ctor completed,... before registering all of the state used in this method need to be initialized
        private void ReloadFileLoggerOptions(FileLoggerOptions currentOptions)
        {
            _currentOptions = currentOptions ?? new FileLoggerOptions();

            foreach (KeyValuePair<string, FileLogger> logger in _loggers)
                logger.Value.Options = GetCurrentOptions();
        }

        private void ReloadFileLoggersFormatter(FileFormatter formatter)
        {
            _currentFormatter = formatter ?? new FileFormatter();

            foreach (KeyValuePair<string, FileLogger> logger in _loggers)
                logger.Value.Formatter = GetCurrentFormatter();
        }

        #endregion

    }
}
