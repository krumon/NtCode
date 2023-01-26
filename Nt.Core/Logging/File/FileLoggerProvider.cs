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
        private FileLoggerOptions _currentOptions;
        private FileFormatter _formatter;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance of <see cref="ConsoleLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="ConsoleLogger"/> instances with.</param>
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options) : this(options, null) { }

        /// <summary>
        /// Creates an instance of <see cref="ConsoleLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="FileLogger"/> instances with.</param>
        /// <param name="formatter">Log formatter added for <see cref="FileLogger"/> instances.</param>
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options, FileFormatter formatter)
        {
            _currentOptions = options.CurrentValue;
            _loggers = new ConcurrentDictionary<string, FileLogger>();

            SetFormatters(formatter);
            ReloadFileLoggerOptions(options.CurrentValue);
            _optionsReloadToken = options.OnChange(ReloadFileLoggerOptions);
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
            _loggers.Clear();
        }

        #endregion

        #region Private methods

        private FileLoggerOptions GetCurrentOptions() => _currentOptions;
        private FileFormatter GetCurrentFormatter() => _formatter;
        private void SetFormatters(FileFormatter formatter = null) => ReloadFileLoggersFormatter(formatter);

        // warning:  ReloadLoggerOptions can be called before the ctor completed,... before registering all of the state used in this method need to be initialized
        private void ReloadFileLoggerOptions(FileLoggerOptions currentOptions)
        {
            _currentOptions = currentOptions ?? new FileLoggerOptions();

            foreach (KeyValuePair<string, FileLogger> logger in _loggers)
                logger.Value.Options = GetCurrentOptions();
        }

        private void ReloadFileLoggersFormatter(FileFormatter formatter)
        {
            _formatter = formatter ?? new FileFormatter();

            foreach (KeyValuePair<string, FileLogger> logger in _loggers)
                logger.Value.Formatter = GetCurrentFormatter();
        }

        #endregion

    }
}
