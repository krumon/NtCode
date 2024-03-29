﻿namespace Nt.Core.Logging.Console
{
    /// <summary>
    /// Determines when to use color when logging messages.
    /// </summary>
    public enum LoggerColorBehavior
    {
        /// <summary>
        /// Use the default color behavior, enabling color except when the console output
        /// is redirected.
        /// </summary>
        /// <remarks>Enables color except when the console output is redirected.</remarks>
        Default,
        /// <summary>
        /// Enable color for logging
        /// </summary>
        Enabled,
        /// <summary>
        /// Disable color for logging
        /// </summary>
        Disabled
    }
}
