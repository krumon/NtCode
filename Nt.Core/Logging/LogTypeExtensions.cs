namespace Nt.Core.Logging
{
    /// <summary>
    /// Reserved file names for the built-in file loggers.
    /// </summary>
    public static class LogTypeExtensions
    {
        /// <summary>
        /// Converts <see cref="LogType"/> to name./>
        /// </summary>
        /// <param name="logType">The type of log.</param>
        /// <returns>The log name.</returns>
        /// <exception cref="System.Exception">The type doesn't exist.</exception>
        public static string ToName(this LogType logType)
        {
            switch (logType)
            {
                case LogType.Boot:
                    return "Boot";
                case LogType.Message:
                    return "System";
                case LogType.Secure:
                    return "Secure";
                default:
                    throw new System.Exception($"{nameof(logType)} is not a {typeof(LogType)}.");
            }
        }
        /// <summary>
        /// Converts <see cref="LogType"/> to log file name./>
        /// </summary>
        /// <param name="logType">The type of log.</param>
        /// <returns>The log name.</returns>
        /// <exception cref="System.Exception">The type doesn't exist.</exception>
        public static string ToFileName(this LogType logType)
        {
            switch (logType)
            {
                case LogType.Boot:
                    return "bootlog.txt";
                case LogType.Message:
                    return "syslog.txt";
                case LogType.Secure:
                    return "securelog.txt";
                default:
                    throw new System.Exception($"{nameof(logType)} is not a {typeof(LogType)}.");
            }
        }
    }
}
