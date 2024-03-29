﻿using Nt.Core.Logging.Internal;
using System;
using System.Diagnostics;

namespace Nt.Core.Logging.Debug
{
    /// <summary>
    /// A logger that writes messages in the debug output window only when a debugger is attached.
    /// </summary>
    internal sealed class DebugLogger : ILogger
    {
        private readonly string _name;

        /// <summary>
        /// Initializes a new instance of the <see cref="DebugLogger"/> class.
        /// </summary>
        /// <param name="name">The name of the logger.</param>
        public DebugLogger(string name)
        {
            _name = name;
        }

        /// <inheritdoc />
        public IDisposable BeginScope<TState>(TState state)
        {
            return NullScope.Instance;
        }

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel)
        {
            // If the filter is null, everything is enabled
            // unless the debugger is not attached
            return Debugger.IsAttached && logLevel != LogLevel.None;
        }

        /// <inheritdoc />
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            if (formatter == null)
                throw new ArgumentNullException(nameof(formatter));

            string message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
                return;

            message = $"{logLevel}: {message}";

            if (exception != null)
                message += Environment.NewLine + Environment.NewLine + exception;

            DebugWriteLine(message, _name);
        }

        private void DebugWriteLine(string message, string name)
        {
            System.Diagnostics.Debug.WriteLine(message, category: name);
        }
    }
}
