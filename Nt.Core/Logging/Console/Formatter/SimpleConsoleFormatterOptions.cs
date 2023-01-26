﻿namespace Nt.Core.Logging.Console
{
    /// <summary>
    /// Options for the built-in default console log formatter.
    /// </summary>
    public class SimpleConsoleFormatterOptions : ConsoleFormatterOptions
    {
        public SimpleConsoleFormatterOptions() { }

        /// <summary>
        /// Determines when to use color when logging messages.
        /// </summary>
        public LoggerColorBehavior ColorBehavior { get; set; }

        /// <summary>
        /// When <see langword="true" />, the entire message gets logged in a single line.
        /// </summary>
        public bool SingleLine { get; set; }
    }
}
