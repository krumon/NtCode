﻿using Nt.Core.Logging.Internal;
using System;

namespace Nt.Core.Logging
{
    /// <summary>
    /// Delegates to a new <see cref="ILogger"/> instance using the full name of the given type, created by the
    /// provided <see cref="ILoggerFactory"/>.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    public class Logger<T> : ILogger<T>
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new <see cref="Logger{T}"/>.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public Logger(ILoggerFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            _logger = factory.CreateLogger(TypeNameHelper.GetTypeDisplayName(typeof(T), includeGenericParameters: false, nestedTypeDelimiter: '.'));
        }
        bool ILogger.IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(logLevel);
        }
        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _logger.Log(logLevel, eventId, state, exception, formatter);
        }
    }
}
