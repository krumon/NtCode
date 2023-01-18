namespace Nt.Core.Logging.File
{
    /// <summary>
    /// The configuration for a <see cref="FileLogger"/>
    /// </summary>
    public class FileLoggerOptions
    {
        #region Public Properties

        ///// <summary>
        ///// Name of the log message formatter to use. Default .... />.
        ///// </summary>
        //public string FormatterName { get; set; }

        /// <summary>
        /// The minimum
        /// </summary>
        public string LogLevel { get; set; }

        /// <summary>
        /// The directory to store the file.
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// The name and extension of the file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Whether to log the time as part of the message
        /// </summary>
        public bool LogTime { get; set; }

        /// <summary>
        /// Whether to display latest logs at the top of the file
        /// </summary>
        public bool LogAtTop { get; set; }

        /// <summary>
        /// Indicates if the log level should be output as part of the log message
        /// </summary>
        public bool ShowLogLevel { get; set; }

        #endregion
    }
}
