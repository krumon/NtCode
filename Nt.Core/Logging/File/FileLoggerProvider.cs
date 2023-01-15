using System;
using System.Collections.Concurrent;
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

        private readonly IDisposable _onChangeToken;
        private FileLoggerOptions _currentConfig;
        private readonly ConcurrentDictionary<string, FileLogger> _loggers =
            new ConcurrentDictionary<string, FileLogger>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Protected Members

        ///// <summary>
        ///// The path to log to
        ///// </summary>
        //protected string _filePath;

        ///// <summary>
        ///// The configuration to use when creating loggers
        ///// </summary>
        //protected readonly FileLoggerOptions _options;

        ///// <summary>
        ///// Keeps track of the loggers already created
        ///// </summary>
        //private readonly ConcurrentDictionary<string, FileLogger> loggers = new ConcurrentDictionary<string, FileLogger>();

        #endregion

        #region Constructor

        public FileLoggerProvider(IOptionsMonitor<FileLoggerOptions> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        ///// <summary>
        ///// Default constructor
        ///// </summary>
        ///// <param name="path">The path to log to</param>
        ///// <param name="options">The configuration to use</param>
        //public FileLoggerProvider(string path, FileLoggerOptions options)
        //{
        //    // Set the configuration
        //    _options = options;

        //    // Set the path
        //    _filePath = path;
        //}

        #endregion

        #region ILoggerProvider Implementation

        public ILogger CreateLogger(string categoryName) =>
            _loggers.GetOrAdd(categoryName, name => new FileLogger(name, GetCurrentConfig));

        public void Dispose()
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }

        ///// <summary>
        ///// Creates a file logger based on the category name
        ///// </summary>
        ///// <param name="categoryName">The category name of this logger</param>
        ///// <returns></returns>
        //public ILogger CreateLogger(string categoryName)
        //{
        //    // Get or create the logger for this category
        //    return loggers.GetOrAdd(categoryName, name => new FileLogger(name, _filePath, _options));
        //}

        #endregion

        #region Private methods

        private FileLoggerOptions GetCurrentConfig() => _currentConfig;

        #endregion

    }
}
