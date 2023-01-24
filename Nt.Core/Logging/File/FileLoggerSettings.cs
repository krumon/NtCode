namespace Nt.Core.Logging.File
{
    /// <summary>
    /// The settings for a file log.
    /// </summary>
    public class FileLoggerSettings
    {
        //#region Private members

        //private const LogMessageType _messageType = LogMessageType.System;


        //#endregion


        ///// <summary>
        ///// Name of the log message formatter to use. Defaults to "*****" />.
        ///// </summary>
        //public string FormatterName { get; set; }

        ///// <summary>
        ///// The type of log message.
        ///// </summary>
        //public LogMessageType MessageType { get; set; } = _messageType;

        /// <summary>
        /// The minimum level for the log message type.
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// The directory to store the file.
        /// </summary>
        public string Directory { get; set; } 

        /// <summary>
        /// The name and extension of the file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether to display latest logs at the top of the file
        /// </summary>
        public bool LogAtTop { get; set; }

        public FileLoggerSettings(string name):this(name,LogLevel.Information,false)
        {
        }

        public FileLoggerSettings(string name, string directory):this(name,directory,LogLevel.Information,false)
        {
        }

        public FileLoggerSettings(string name, LogLevel logLevel, bool logAtTop) : this(name,null,logLevel,logAtTop) 
        {
        }

        public FileLoggerSettings(string name, string directory, LogLevel logLevel, bool logAtTop)
        {
            if (string.IsNullOrEmpty(name))
                throw new System.ArgumentNullException(nameof(name));

            Name = name;
            LogLevel = logLevel;
            LogAtTop = logAtTop;
            Directory = string.IsNullOrEmpty(directory) ? System.IO.Directory.GetCurrentDirectory() : directory;
        }
    }
}
