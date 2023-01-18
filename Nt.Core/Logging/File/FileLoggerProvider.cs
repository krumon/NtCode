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

        private readonly IOptionsMonitor<FileLoggerOptions> _options;
        private readonly ConcurrentDictionary<EventId, FileLogger> _loggers;
        private ConcurrentDictionary<string, FileFormatter> _formatters;
        private IDisposable _optionsReloadToken;
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
            : this(options, Array.Empty<FileFormatter>()) { }

        /// <summary>
        /// Creates an instance of <see cref="ConsoleLoggerProvider"/>.
        /// </summary>
        /// <param name="options">The options to create <see cref="ConsoleLogger"/> instances with.</param>
        /// <param name="formatters">Log formatters added for <see cref="ConsoleLogger"/> insteaces.</param>
        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> options, IEnumerable<FileFormatter> formatters)
        {
            _options = options;
            _loggers = new ConcurrentDictionary<EventId, FileLogger>();
            SetFormatters(formatters);

            ReloadLoggerOptions(options.CurrentValue);
            _optionsReloadToken = _options.OnChange(ReloadLoggerOptions);

        }

        #endregion

        #region ILoggerProvider Implementation

        public ILogger CreateLogger(string categoryName) 
        {
            var loggersOptions = _options.CurrentValue.FileLog;
            
            if(loggersOptions == null)
                throw new NullReferenceException(nameof(loggersOptions));

            if(loggersOptions.Count<1)
                throw new ArgumentException(nameof(loggersOptions));

            foreach(KeyValuePair<LogMessageType,FileLoggerSettings> options in loggersOptions)
            {
                if (options.Value.FormatterName == null || _formatters.TryGetValue(options.Value.FormatterName, out FileFormatter logFormatter))
                {
                    switch (options.Value.FormatterName)
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

        private void SetFormatters(IEnumerable<FileFormatter> formatters = null)
        {
            var cd = new ConcurrentDictionary<string, FileFormatter>(StringComparer.OrdinalIgnoreCase);

            bool added = false;
            if (formatters != null)
            {
                foreach (FileFormatter formatter in formatters)
                {
                    cd.TryAdd(formatter.Name, formatter);
                    added = true;
                }
            }

            if (!added)
            {
                cd.TryAdd(FileFormatterNames.Default, new FileFormatter(FileFormatterNames.Default, new FileFormatterOptionsMonitor<FileFormatterOptions>(new FileFormatterOptions())));
                //cd.TryAdd(ConsoleFormatterNames.Systemd, new SystemdConsoleFormatter(new FormatterOptionsMonitor<ConsoleFormatterOptions>(new ConsoleFormatterOptions())));
                //cd.TryAdd(ConsoleFormatterNames.Json, new JsonConsoleFormatter(new FormatterOptionsMonitor<JsonConsoleFormatterOptions>(new JsonConsoleFormatterOptions())));
            }

            _formatters = cd;
        }

        // warning:  ReloadLoggerOptions can be called before the ctor completed,... before registering all of the state used in this method need to be initialized
        private void ReloadLoggerOptions(FileLoggerOptions options, string text = null)
        {
            var loggersOptions = options.FileLog;

            if (loggersOptions == null)
                throw new NullReferenceException(nameof(loggersOptions));

            if (loggersOptions.Count < 1)
                throw new ArgumentException(nameof(loggersOptions));

            foreach (KeyValuePair<LogMessageType, FileLoggerSettings> op in loggersOptions)
            {
                if (op.Value.FormatterName == null || _formatters.TryGetValue(op.Value.FormatterName, out FileFormatter formatter))
                {
                    switch (op.Value.FormatterName)
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
                logger.Value.Formatter = logFormatter;
            }
        }

        #endregion

    }
}
