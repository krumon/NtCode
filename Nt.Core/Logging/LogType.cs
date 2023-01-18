namespace Nt.Core.Logging
{
    /// <summary>
    /// Reserved file names for the built-in file loggers.
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// Log information related to system boot status.
        /// </summary>
        Boot,
        /// <summary>
        /// Log information on how our system is working. It is a general purpose log of various services that use this log. 
        /// </summary>
        Message,
        /// <summary>
        /// Security related logs. Every time a user executes commands with a higher level, the executed command is stored inside the secure log. 
        /// If the user cannot complete a login, this action is logged by this log.
        /// </summary>
        Secure
    }
}
