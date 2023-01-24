﻿namespace Nt.Core.Logging.File
{
    public class FileFormatterOptions
    {

        /// <summary>
        /// Includes scopes when <see langword="true" />.
        /// </summary>
        public bool IncludeScopes { get; set; }

        /// <summary>
        /// Gets or sets the timestamp options.
        /// </summary>
        public TimestampOptions TimestampOptions { get; set; } = new TimestampOptions();

        ///// <summary>
        ///// Whether to log the time as part of the message
        ///// </summary>
        //public bool LogTime { get; set; }

        ///// <summary>
        ///// Gets or sets format string used to format timestamp in logging messages. Defaults to <c>null</c>.
        ///// </summary>
        //public string TimestampFormat { get; set; } = "yyyy-MM-DD HH:mm:ss";

        ///// <summary>
        ///// Gets or sets indication whether or not UTC timezone should be used to for timestamps in logging messages. Defaults to <c>false</c>.
        ///// </summary>
        //public bool UseUtcTimestamp { get; set; }

        /// <summary>
        /// When <see langword="true" />, the entire message gets logged in a single line.
        /// </summary>
        public bool SingleLine { get; set; }

        /// <summary>
        /// Indicates if the log level should be output as part of the log message
        /// </summary>
        public bool LogLevel { get; set; }

    }
}
