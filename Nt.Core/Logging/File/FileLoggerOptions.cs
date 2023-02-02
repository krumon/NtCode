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
        public string FileName { get; set; } = "log.txt";

        /// <summary>
        /// Whether to display latest logs at the top of the file
        /// </summary>
        public bool LogAtTop { get; set; }

        /// <summary>
        /// Indicates if the directory doesn't exist or cannot create, store the log file in the current directory.
        /// </summary>
        public bool EnsureExistDirectory { get; set; }

        public Dictionary<string, FileLoggerSettings> Files { get; set; }
            = new Dictionary<string, FileLoggerSettings>()
            {
                ["SecureLog"] = new FileLoggerSettings("SecureLog",LogLevel.Information,true)
            };

        #endregion
    }
}
