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
        private readonly ConcurrentDictionary<string, FileLogger> _loggers;
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
            _loggers = new ConcurrentDictionary<string, FileLogger>();
            SetFormatters(formatters);

            ReloadLoggerOptions(options.CurrentValue);
            _optionsReloadToken = _options.OnChange(ReloadLoggerOptions);

        }

        #endregion

        #region ILoggerProvider Implementation

        public ILogger CreateLogger(string categoryName) 
        {
            if (_options.CurrentValue.FormatterName == null || !_formatters.TryGetValue(_options.CurrentValue.FormatterName, out FileFormatter logFormatter))
            {
#pragma warning disable CS0618
                //switch (_options.CurrentValue.FormatterName)
                //{
                //    case ConsoleLoggerFormat.Systemd:
                //        logFormatter = _formatters[ConsoleFormatterNames.Systemd];
                //        break;
                //    default:
                //        logFormatter = _formatters[ConsoleFormatterNames.Simple];
                //        break;
                //}
#pragma warning restore CS0618

                if (_options.CurrentValue.FormatterName == null)
                {
                    UpdateFormatterOptions(logFormatter, _options.CurrentValue);
                }
            }

            return _loggers.TryGetValue(name, out ConsoleLogger logger) ?
                logger :
                _loggers.GetOrAdd(name, new ConsoleLogger(name, _messageQueue)
                {
                    Options = _options.CurrentValue,
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
                //cd.TryAdd(ConsoleFormatterNames.Simple, new SimpleConsoleFormatter(new FormatterOptionsMonitor<SimpleConsoleFormatterOptions>(new SimpleConsoleFormatterOptions())));
                //cd.TryAdd(ConsoleFormatterNames.Systemd, new SystemdConsoleFormatter(new FormatterOptionsMonitor<ConsoleFormatterOptions>(new ConsoleFormatterOptions())));
                //cd.TryAdd(ConsoleFormatterNames.Json, new JsonConsoleFormatter(new FormatterOptionsMonitor<JsonConsoleFormatterOptions>(new JsonConsoleFormatterOptions())));
            }

            _formatters = cd;
        }

        // warning:  ReloadLoggerOptions can be called before the ctor completed,... before registering all of the state used in this method need to be initialized
        private void ReloadLoggerOptions(FileLoggerOptions options, string text = null)
        {
            if (options.FormatterName == null || !_formatters.TryGetValue(options.FormatterName, out FileFormatter logFormatter))
            {
                switch (options.FormatterName)
                {
                    case "": // ConsoleLoggerFormat.Systemd:
                        logFormatter = _formatters[""];
                        break;
                    default:
                        logFormatter = _formatters[""];
                        break;
                }
                if (options.FormatterName == null)
                {
                    UpdateFormatterOptions(logFormatter, options);
                }
            }

            foreach (KeyValuePair<string, FileLogger> logger in _loggers)
            {
                logger.Value.Options = options;
                logger.Value.Formatter = logFormatter;
            }
        }

        private void UpdateFormatterOptions(FileFormatter formatter, FileLoggerOptions deprecatedFromOptions)
        {
            // kept for deprecated apis:
            if (formatter is FileFormatter defaultFormatter)
            {
                defaultFormatter.FormatterOptions = new FileFormatterOptions()
                {
                    //IncludeScopes = deprecatedFromOptions.IncludeScopes,
                    //TimestampFormat = deprecatedFromOptions.TimestampFormat,
                    //UseUtcTimestamp = deprecatedFromOptions.UseUtcTimestamp,
                };
            }
            //else
            //if (formatter is SystemdConsoleFormatter systemdFormatter)
            //{
            //    systemdFormatter.FormatterOptions = new ConsoleFormatterOptions()
            //    {
            //        IncludeScopes = deprecatedFromOptions.IncludeScopes,
            //        TimestampFormat = deprecatedFromOptions.TimestampFormat,
            //        UseUtcTimestamp = deprecatedFromOptions.UseUtcTimestamp,
            //    };
            //}
        }


        #endregion

    }
}
