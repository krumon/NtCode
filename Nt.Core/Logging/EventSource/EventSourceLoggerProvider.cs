﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nt.Core.Logging.EventSource
{
    /// <summary>
    /// The provider for the <see cref="EventSourceLogger"/>.
    /// </summary>
    [ProviderAlias("EventSource")]
    public class EventSourceLoggerProvider : ILoggerProvider
    {
        private static int _globalFactoryID;

        // A small integer that uniquely identifies the LoggerFactory associated with this LoggingProvider.
        private readonly int _factoryID;

        private EventSourceLogger _loggers; // Linked list of loggers that I have created
        private readonly LoggingEventSource _eventSource;

        public EventSourceLoggerProvider(LoggingEventSource eventSource)
        {
            if (eventSource == null)
            {
                throw new ArgumentNullException(nameof(eventSource));
            }
            _eventSource = eventSource;
            _factoryID = Interlocked.Increment(ref _globalFactoryID);
        }

        /// <inheritdoc />
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers = new EventSourceLogger(categoryName, _factoryID, _eventSource, _loggers);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // Turn off any logging
            for (EventSourceLogger logger = _loggers; logger != null; logger = logger.Next)
            {
                logger.Level = LogLevel.None;
            }
        }
    }
}
