namespace Nt.Core.Logging.File
{
    /// <summary>
    /// The settings for a file log.
    /// </summary>
    public class FileLoggerSettings
    {
        #region Private members

        private const LogMessageType _messageType = LogMessageType.System;


        #endregion

        #region Public Properties

        /// <summary>
        /// Name of the log message formatter to use. Defaults to "*****" />.
        /// </summary>
        public string FormatterName { get; set; }

        /// <summary>
        /// The type of log message.
        /// </summary>
        public LogMessageType MessageType { get; set; } = _messageType;

        /// <summary>
        /// The minimum level for the log message type.
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        /// <summary>
        /// The directory to store the file.
        /// </summary>
        public string Directory { get; set; } = System.IO.Directory.GetCurrentDirectory();

        /// <summary>
        /// The name and extension of the file.
        /// </summary>
        public string FileName { get; set; } = _messageType.ToFileName();

        /// <summary>
        /// Whether to log the time as part of the message
        /// </summary>
        public bool LogTime { get; set; }

        /// <summary>
        /// Whether to display latest logs at the top of the file
        /// </summary>
        public bool LogAtTop { get; set; }

        #endregion
    }
}
