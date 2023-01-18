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

        private IDisposable _optionsReloadToken;
        private IDisposable _formatterReloadToken;
        private readonly FileLoggerOptions _currentOptions;
        private FileFormatter _currentFormatter;
        private readonly ConcurrentDictionary<EventId, FileLogger> _loggers;
        //private ConcurrentDictionary<string, FileFormatter> _formatters;
        private IExternalScopeProvider _scopeProvider = NullExternalScopeProvider.Instance;

        private readonly IDisposable _onChangeToken;
        private FileLoggerOptions _currentConfig;

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
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options, IOptionsMonitor<FileFormatter> formatter)
        {
            _currentOptions = options.CurrentValue;
            _currentFormatter = formatter.CurrentValue;
            _loggers = new ConcurrentDictionary<EventId, FileLogger>();
            SetFormatters(formatter);

            ReloadLoggerOptions(options.CurrentValue);
            _optionsReloadToken = _currentOptions.OnChange(ReloadLoggerOptions);

        }

        #endregion

        #region ILoggerProvider Implementation

        public ILogger CreateLogger(string categoryName) 
        {
            var loggersOptions = _currentOptions.CurrentValue.FileLogs;
            
            if(loggersOptions == null)
                throw new NullReferenceException(nameof(loggersOptions));

            if(loggersOptions.Count<1)
                throw new ArgumentException(nameof(loggersOptions));

            foreach(KeyValuePair<LogMessageType,FileLoggerSettings> settings in loggersOptions)
            {
                if (settings.Value.FormatterName == null || _formatters.TryGetValue(settings.Value.FormatterName, out FileFormatter logFormatter))
                {
                    switch (settings.Value.FormatterName)
                    {
                        default:
                            logFormatter = _formatters[FileFormatterNames.Default];
                            break;
                    }
                }
            }

            return _loggers.TryGetValue(categoryName, out FileLogger logger) ?
                logger :
                _loggers.GetOrAdd(categoryName, new FileLogger(categoryName)
                {
                    Options = GetCurrentConfig(),
                    ScopeProvider = _scopeProvider,
                    Formatter = logFormatter,
                });

            _loggers.GetOrAdd(categoryName, name => new FileLogger(name, GetCurrentConfig));
        } 

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }

        #endregion

        #region Private methods

        private FileLoggerOptions GetCurrentConfig() => _currentConfig;

        private void SetFormatters(IOptionsMonitor<FileFormatter> formatter = null)
        {
            if (formatter == null)
                _currentFormatter = new FileFormatter();
        }

        // warning:  ReloadLoggerOptions can be called before the ctor completed,... before registering all of the state used in this method need to be initialized
        private void ReloadLoggerOptions(FileLoggerOptions options, string text = null)
        {
            var loggersOptions = options.FileLogs;

            if (loggersOptions == null)
                throw new NullReferenceException(nameof(loggersOptions));

            if (loggersOptions.Count < 1)
                throw new ArgumentException(nameof(loggersOptions));

            FileFormatter formatter = null;
            foreach (KeyValuePair<LogMessageType, FileLoggerSettings> settings in loggersOptions)
            {
                if (settings.Value.FormatterName == null || !_formatters.TryGetValue(settings.Value.FormatterName, out formatter))
                {
                    switch (settings.Value.FormatterName)
                    {
                        default:
                            formatter = _formatters[FileFormatterNames.Default];
                            break;
                    }
                }
            }

            foreach (KeyValuePair<EventId, FileLogger> logger in _loggers)
            {
                logger.Value.Options = options;
                logger.Value.Formatter = formatter;
            }
        }

        #endregion

    }
}
