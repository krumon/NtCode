﻿using System;

namespace Nt.Core.Logging
{
    /// <summary>
    /// Represents a type used to configure the logging system and create instances of
    /// <see cref="ILogger"/> from the registered <see cref="ILoggerProvider"/>.
    /// </summary>
    public interface ILoggerFactory : IDisposable
    {
        /// <summary>
        /// Creates a new <see cref="ILogger"/> instance.
        /// </summary>
        /// <param name="categoryName">The category name for messages produced by the logger.</param>
        /// <returns>The <see cref="ILogger"/>.</returns>
        ILogger CreateLogger(string categoryName);

        /// <summary>
        /// Adds an <see cref="ILoggerProvider"/> to the logging system.
        /// </summary>
        /// <param name="provider">The <see cref="ILoggerProvider"/>.</param>
        void AddProvider(ILoggerProvider provider);
    }
}
