﻿using Nt.Core.Logging.Console;

namespace Nt.Core.Logging
{
    /// <summary>
    /// Options for the built-in console log formatter.
    /// </summary>
    public abstract class BaseConsoleFormatterOptions
    {
        public BaseConsoleFormatterOptions() { }

        /// <summary>
        /// Includes scopes when <see langword="true" />.
        /// </summary>
        public bool IncludeScopes { get; set; }

        /// <summary>
        /// Gets or sets format string used to format timestamp in logging messages. Defaults to <c>null</c>.
        /// </summary>
        public string TimestampFormat { get; set; }

        /// <summary>
        /// Gets or sets indication whether or not UTC timezone should be used to for timestamps in logging messages. Defaults to <c>false</c>.
        /// </summary>
        public bool UseUtcTimestamp { get; set; }

    }
}