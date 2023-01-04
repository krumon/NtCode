﻿using System;

namespace Nt.Core.Configuration
{
    /// <summary>
    /// Contains information about a file load exception.
    /// </summary>
    public class FileLoadExceptionContext
    {
        /// <summary>
        /// The <see cref="FileConfigurationProvider"/> that caused the exception.
        /// </summary>
        public FileConfigurationProvider Provider { get; set; }

        /// <summary>
        /// The exception that occurred in Load.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// If true, the exception will not be rethrown.
        /// </summary>
        public bool Ignore { get; set; }
    }
}
