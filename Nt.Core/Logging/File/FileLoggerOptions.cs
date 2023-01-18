using System.Collections.Concurrent;

namespace Nt.Core.Logging.File
{
    /// <summary>
    /// The configuration for a <see cref="FileLogger"/>
    /// </summary>
    public class FileLoggerOptions
    {
        #region Public Properties

        public ConcurrentDictionary<LogMessageType, FileLoggerSettings> FileLogs { get; set; }
            = new ConcurrentDictionary<LogMessageType, FileLoggerSettings>()
            {
                [LogMessageType.System] = new FileLoggerSettings()
            };

        #endregion
    }
}
