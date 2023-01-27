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
        private const string homeDirectory = "C:\\Users\\Usuario\\Documents\\GitHub\\NtCode";
        private const string workDirectory = "C:\\Users\\enrique.rueda\\Documents\\kike\\GitHub\\NtCode";
        
        #region Private members

        /// <summary>
        /// Represents the name of the logger.
        /// </summary>
        private readonly string _name;

        internal FileLoggerOptions Options { get; set; }
        internal BaseFileFormatter Formatter { get; set; }
        internal IExternalScopeProvider ScopeProvider { get; set; }
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

        #region Constructor

        /// <summary>
        /// Creates <see cref="FileLogger"/> default instance.
        /// </summary>
        /// <param name="name">The category name of the logger.</param>
        /// <param name="getCurrentConfig">The current configuration.</param>
        public FileLogger( string name) => _name = name;

        #endregion

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

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
            var normalizedPath = Options.Name.ToUpper();
            var normalizedDirectory = Options.Directory;

            if (!Directory.Exists(normalizedDirectory))
            {
                try
                {
                    Directory.CreateDirectory(normalizedDirectory);
                }
                catch(DirectoryNotFoundException ex1)
                {
                    if (Directory.Exists(homeDirectory))
                        normalizedDirectory = homeDirectory;
                }
                catch(UnauthorizedAccessException ex2)
                {
                    if (Directory.Exists(workDirectory))
                        normalizedDirectory = workDirectory;
                }
            }

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
                //// Ensure folder
                //if (!Directory.Exists(Options.FileLogs[0].Directory))
                //    Directory.CreateDirectory(Options.FileLogs[0].Directory);

                // Open the file
                using (var fileStream = new StreamWriter(System.IO.File.Open(Path.Combine(normalizedDirectory, normalizedPath), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)))
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
