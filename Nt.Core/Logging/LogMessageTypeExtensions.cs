namespace Nt.Core.Logging
{
    /// <summary>
    /// Reserved file names for the built-in file loggers.
    /// </summary>
    public static class LogMessageTypeExtensions
    {
        /// <summary>
        /// Converts <see cref="LogMessageType"/> to name./>
        /// </summary>
        /// <param name="logType">The type of log.</param>
        /// <returns>The log name.</returns>
        /// <exception cref="System.Exception">The type doesn't exist.</exception>
        public static string ToName(this LogMessageType logType)
        {
            switch (logType)
            {
                case LogMessageType.Boot:
                    return "Boot";
                case LogMessageType.System:
                    return "System";
                case LogMessageType.Secure:
                    return "Secure";
                default:
                    throw new System.Exception($"{nameof(logType)} is not a {typeof(LogMessageType)}.");
            }
        }
        /// <summary>
        /// Converts <see cref="LogMessageType"/> to log file name./>
        /// </summary>
        /// <param name="logType">The type of log.</param>
        /// <returns>The log name.</returns>
        /// <exception cref="System.Exception">The type doesn't exist.</exception>
        public static string ToFileName(this LogMessageType logType)
        {
            switch (logType)
            {
                case LogMessageType.Boot:
                    return "bootlog.txt";
                case LogMessageType.System:
                    return "syslog.txt";
                case LogMessageType.Secure:
                    return "securelog.txt";
                default:
                    throw new System.Exception($"{nameof(logType)} is not a {typeof(LogMessageType)}.");
            }
        }

        /// <summary>
        /// Converts <see cref="LogMessageType"/> to <see cref="EventId"/> instance./>
        /// </summary>
        /// <param name="logType">The type of log.</param>
        /// <returns>The <see cref="EventId"/> instance.</returns>
        /// <exception cref="System.Exception">The type doesn't exist.</exception>
        public static EventId ToEventId(this LogMessageType logType)
        {
            switch (logType)
            {
                case LogMessageType.Boot:
                    return new EventId(2,"Boot Event");
                case LogMessageType.System:
                    return new EventId(0, "System Event");
                case LogMessageType.Secure:
                    return new EventId(3, "Secure Event");
                default:
                    throw new System.Exception($"{nameof(logType)} is not a {typeof(LogMessageType)}.");
            }
        }
    }
}
