using Nt.Core.Logging.Abstractions;
using Nt.Core.Logging.Internal;
using System;
using System.Collections.Concurrent;
using System.IO;

namespace Nt.Core.Logging.File
{
    /// <summary>
    /// A logger that writes the logs to file
    /// </summary>
    internal class FileLogger : ILogger
    {
        #region Private members

        /// <summary>
        /// Represents the name of the logger.
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Represents the action to configure the logger.
        /// </summary>
        private readonly Func<FileLoggerOptions> _getCurrentConfig;

        internal FileFormatter Formatter { get; set; }
        internal IExternalScopeProvider ScopeProvider { get; set; }
        internal FileLoggerOptions Options { get; set; }
        [ThreadStatic]
        private static StringWriter t_stringWriter;

        #endregion

        #region Static Properties

        /// <summary>
        /// A list of file locks based on path
        /// </summary>
        protected static ConcurrentDictionary<string, object> FileLocks = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// The lock to lock the list of locks
        /// </summary>
        protected static object FileLockLock = new object();

        #endregion

        #region Protected Members

        ///// <summary>
        ///// The category for this logger
        ///// </summary>
        //protected readonly string _categoryName;

        ///// <summary>
        ///// The file path to write to
        ///// </summary>
        //protected readonly string _filePath;

        ///// <summary>
        ///// The directory the file is in
        ///// </summary>
        //protected readonly string _directory;

        ///// <summary>
        ///// The configuration to use
        ///// </summary>
        //protected FileLoggerOptions _options;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates <see cref="FileLogger"/> default instance.
        /// </summary>
        /// <param name="name">The category name of the logger.</param>
        /// <param name="getCurrentConfig">The current configuration.</param>
        public FileLogger(
            string name,
            Func<FileLoggerOptions> getCurrentConfig) =>
            (_name, _getCurrentConfig) = (name, getCurrentConfig);

        ///// <summary>
        ///// Default constructor
        ///// </summary>
        ///// <param name="categoryName">The category for this logger</param>
        ///// <param name="filePath">The file path to write to</param>
        ///// <param name="configuration">The configuration to use</param>
        //public FileLogger(string categoryName, string filePath, FileLoggerOptions configuration)
        //{
        //    // Get absolute path
        //    filePath = Path.GetFullPath(filePath);

        //    // Set members
        //    _categoryName = categoryName;
        //    _filePath = filePath;
        //    _directory = Path.GetDirectoryName(filePath);
        //    _options = configuration;
        //}

        #endregion

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            if (t_stringWriter == null) t_stringWriter = new StringWriter();
            LogEntry<TState> logEntry = new LogEntry<TState>(logLevel, _name, eventId, state, exception, formatter);
            Formatter.Write(in logEntry, ScopeProvider, t_stringWriter);

            var sb = t_stringWriter.GetStringBuilder();
            if (sb.Length == 0)
            {
                return;
            }
            string computedAnsiString = sb.ToString();
            sb.Clear();
            if (sb.Capacity > 1024)
            {
                sb.Capacity = 1024;
            }
            // Normalize path
            // TODO: Make use of configuration base path
            var normalizedPath = Options.FilePath.ToUpper();

            var fileLock = default(object);

            // Double safety even though the FileLocks should be thread safe
            lock (FileLockLock)
            {
                // Get the file lock based on the absolute path
                fileLock = FileLocks.GetOrAdd(normalizedPath, path => new object());
            }

            // Lock the file
            lock (fileLock)
            {
                // Ensure folder
                if (!Directory.Exists(Options.Directory))
                    Directory.CreateDirectory(Options.Directory);

                // Open the file
                using (var fileStream = new StreamWriter(System.IO.File.Open(Options.FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)))
                {
                    // Go to end
                    fileStream.BaseStream.Seek(0, SeekOrigin.End);

                    // NOTE: Ignore logToTop in configuration as not efficient for files on OS

                    // Write the message to the file
                    fileStream.Write(computedAnsiString);
                }
            }
        }

        /// <summary>
        /// Enabled if the log level is the same or greater than the configuration
        /// </summary>
        /// <param name="logLevel">The log level to check against</param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            // Enabled if the log level is greater or equal to what we want to log
            return logLevel != LogLevel.None;
        }

        /// <summary>
        /// File loggers are not scoped so this is always null
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="state"></param>
        /// <returns></returns>
        public IDisposable BeginScope<TState>(TState state) => ScopeProvider?.Push(state) ?? NullScope.Instance;

    }
}
