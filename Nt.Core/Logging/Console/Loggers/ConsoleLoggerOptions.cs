﻿using System;

namespace Nt.Core.Logging.Console
{
    /// <summary>
    /// Options for a <see cref="ConsoleLogger"/>.
    /// </summary>
    public class ConsoleLoggerOptions
    {
        /// <summary>
        /// Disables colors when <see langword="true" />.
        /// </summary>
        [Obsolete("ConsoleLoggerOptions.DisableColors has been deprecated. Use SimpleConsoleFormatterOptions.ColorBehavior instead.")]
        public bool DisableColors { get; set; }

#pragma warning disable CS0618
        private ConsoleLoggerFormat _format = ConsoleLoggerFormat.Default;
        /// <summary>
        /// Gets or sets log message format. Defaults to <see cref="ConsoleLoggerFormat.Default" />.
        /// </summary>
        [Obsolete("ConsoleLoggerOptions.Format has been deprecated. Use ConsoleLoggerOptions.FormatterName instead.")]
        public ConsoleLoggerFormat Format
        {
            get => _format;
            set
            {
                if (value < ConsoleLoggerFormat.Default || value > ConsoleLoggerFormat.Systemd)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _format = value;
            }
#pragma warning restore CS0618
        }

        /// <summary>
        /// Name of the log message formatter to use. Defaults to "simple" />.
        /// </summary>
        public string FormatterName { get; set; }

        /// <summary>
        /// Includes scopes when <see langword="true" />.
        /// </summary>
        [Obsolete("ConsoleLoggerOptions.IncludeScopes has been deprecated. Use ConsoleFormatterOptions.IncludeScopes instead.")]
        public bool IncludeScopes { get; set; }

        /// <summary>
        /// Gets or sets value indicating the minimum level of messages that would get written to <c>Console.Error</c>.
        /// </summary>
        public LogLevel LogToStandardErrorThreshold { get; set; } = LogLevel.None;

        /// <summary>
        /// Gets or sets format string used to format timestamp in logging messages. Defaults to <c>null</c>.
        /// </summary>
        [Obsolete("ConsoleLoggerOptions.TimestampFormat has been deprecated. Use ConsoleFormatterOptions.TimestampFormat instead.")]
        public string TimestampFormat { get; set; }

        /// <summary>
        /// Gets or sets indication whether or not UTC timezone should be used to for timestamps in logging messages. Defaults to <c>false</c>.
        /// </summary>
        [Obsolete("ConsoleLoggerOptions.UseUtcTimestamp has been deprecated. Use ConsoleFormatterOptions.UseUtcTimestamp instead.")]
        public bool UseUtcTimestamp { get; set; }
    }
}
