using Nt.Core.Logging.Abstractions;
using Nt.Core.Options;
using System;
using System.Globalization;
using System.IO;

namespace Nt.Core.Logging.File
{
    public class FileFormatter: IDisposable
    {
        private IDisposable _optionsReloadToken;
        internal FileFormatterOptions FormatterOptions { get; set; }

        public FileFormatter(string name, IOptionsMonitor<FileFormatterOptions> options)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ReloadLoggerOptions(options.CurrentValue);
            _optionsReloadToken = options.OnChange(ReloadLoggerOptions);

        }

        /// <summary>
        /// Gets the name associated with the console log formatter.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Writes the log message to the specified TextWriter.
        /// </summary>
        /// <remarks>
        /// if the formatter wants to write colors to the console, it can do so by embedding ANSI color codes into the string
        /// </remarks>
        /// <param name="logEntry">The log entry.</param>
        /// <param name="scopeProvider">The provider of scope data.</param>
        /// <param name="textWriter">The string writer embedding ansi code for colors.</param>
        /// <typeparam name="TState">The type of the object to be written.</typeparam>
        public void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider, TextWriter textWriter)
        {
            string message = logEntry.Formatter(logEntry.State, logEntry.Exception);
            if (logEntry.Exception == null && message == null)
            {
                return;
            }
            LogLevel logLevel = logEntry.LogLevel;
            ConsoleColors logLevelColors = GetLogLevelConsoleColors(logLevel);
            string logLevelString = GetLogLevelString(logLevel);

            string timestamp = null;
            string timestampFormat = FormatterOptions.TimestampFormat;
            if (timestampFormat != null)
            {
                DateTimeOffset dateTimeOffset = GetCurrentDateTime();
                timestamp = dateTimeOffset.ToString(timestampFormat);
            }
            if (timestamp != null)
            {
                textWriter.Write(timestamp);
            }
            if (logLevelString != null)
            {
                textWriter.WriteColoredMessage(logLevelString, logLevelColors.Background, logLevelColors.Foreground);
            }
            CreateDefaultLogMessage(textWriter, logEntry, message, scopeProvider);

            // Get current time
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd hh:mm:ss");

            // Prepend log level
            var logLevelString = options.OutputLogLevel ? $"{logLevel.ToString().ToUpper()}: " : "";

            // Prepend the time to the log if desired
            var timeLogString = options.LogTime ? $"[{currentTime}] " : "";

            // Get the formatted message string
            var message = formatter(state, exception);

            // Write the message
            var output = $"{logLevelString}{timeLogString}{message}{Environment.NewLine}";

            // Normalize path
            // TODO: Make use of configuration base path
            var normalizedPath = options.FilePath.ToUpper();

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
                if (!Directory.Exists(options.Directory))
                    Directory.CreateDirectory(options.Directory);

                // Open the file
                using (var fileStream = new StreamWriter(System.IO.File.Open(options.FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite)))
                {
                    // Go to end
                    fileStream.BaseStream.Seek(0, SeekOrigin.End);

                    // NOTE: Ignore logToTop in configuration as not efficient for files on OS

                    // Write the message to the file
                    fileStream.Write(output);
                }
            }

            using (var output = new PooledByteBufferWriter(DefaultBufferSize))
            {
                //using (var writer = new Utf8JsonWriter(output, FormatterOptions.JsonWriterOptions))
                var writer = new Utf8JsonWriter(output, FormatterOptions.JsonWriterOptions);
                writer.WriteStartObject();
                var timestampFormat = FormatterOptions.TimestampFormat;
                if (timestampFormat != null)
                {
                    DateTimeOffset dateTimeOffset = FormatterOptions.UseUtcTimestamp ? DateTimeOffset.UtcNow : DateTimeOffset.Now;
                    writer.WriteString("Timestamp", dateTimeOffset.ToString(timestampFormat));
                }
                writer.WriteNumber(nameof(logEntry.EventId), eventId);
                writer.WriteString(nameof(logEntry.LogLevel), GetLogLevelString(logLevel));
                writer.WriteString(nameof(logEntry.Category), category);
                writer.WriteString("Message", message);

                if (exception != null)
                {
                    string exceptionMessage = exception.ToString();
                    if (!FormatterOptions.JsonWriterOptions.Indented)
                    {
                        exceptionMessage = exceptionMessage.Replace(Environment.NewLine, " ");
                    }
                    writer.WriteString(nameof(Exception), exceptionMessage);
                }

                if (logEntry.State != null)
                {
                    writer.WriteStartObject(nameof(logEntry.State));
                    writer.WriteString("Message", logEntry.State.ToString());
                    if (logEntry.State is IReadOnlyCollection<KeyValuePair<string, object>> stateProperties)
                    {
                        foreach (KeyValuePair<string, object> item in stateProperties)
                        {
                            WriteItem(writer, item);
                        }
                    }
                    writer.WriteEndObject();
                }
                WriteScopeInformation(writer, scopeProvider);
                writer.WriteEndObject();
                writer.Flush();
                // Make dispose because using has been deleted.
                writer.Dispose();
#if NETCOREAPP
                textWriter.Write(Encoding.UTF8.GetString(output.WrittenMemory.Span));
#else
                textWriter.Write(Encoding.UTF8.GetString(output.WrittenMemory.Span.ToArray()));
#endif
            }
            textWriter.Write(Environment.NewLine);
        }

        private static string GetLogLevelString(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return "Trace";
                case LogLevel.Debug:
                    return "Debug";
                case LogLevel.Information:
                    return "Information";
                case LogLevel.Warning:
                    return "Warning";
                case LogLevel.Error:
                    return "Error";
                case LogLevel.Critical:
                    return "Critical";
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }

        private static string ToInvariantString(object obj) => Convert.ToString(obj, CultureInfo.InvariantCulture);

        private void ReloadLoggerOptions(FileFormatterOptions options)
        {
            FormatterOptions = options;
        }

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
        }

    }
}
