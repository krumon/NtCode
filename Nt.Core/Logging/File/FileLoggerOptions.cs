using System.Collections.Generic;

namespace Nt.Core.Logging.File
{
    /// <summary>
    /// The configuration for a <see cref="FileLogger"/>
    /// </summary>
    public class FileLoggerOptions
    {
        #region Public Properties

        //public LogLevel LogLevel { get; set; }
        public string FormatterName { get; set; }

        /// <summary>
        /// The directory to store the file.
        /// </summary>
        public string Directory { get; set; } = System.IO.Directory.GetCurrentDirectory();

        /// <summary>
        /// The name and extension of the file.
        /// </summary>
        public string Name { get; set; } = "log.txt";

        /// <summary>
        /// Whether to display latest logs at the top of the file
        /// </summary>
        public bool LogAtTop { get; set; }

        public Dictionary<string, FileLoggerSettings> Files { get; set; }
            = new Dictionary<string, FileLoggerSettings>()
            {
                ["SecureLog"] = new FileLoggerSettings("SecureLog",LogLevel.Information,true)
            };

        //public ConcurrentDictionary<LogMessageType, FileLoggerSettings> FileLogs { get; set; }
        //    = new ConcurrentDictionary<LogMessageType, FileLoggerSettings>()
        //    {
        //        //[LogMessageType.System] = new FileLoggerSettings()
        //        [0] = new FileLoggerSettings()
        //    };

        #endregion
    }
}
