﻿using Nt.Core.Attributes;
using Nt.Core.Logging.Abstractions;
using Nt.Core.Logging.Internal;
using System;
using System.IO;

namespace Nt.Core.Logging.Console
{
    [UnsupportedOSPlatform("browser")]
    internal sealed class ConsoleLogger : ILogger
    {
        private readonly string _name;
        private readonly ConsoleLoggerProcessor _queueProcessor;

        internal ConsoleLogger(string name, ConsoleLoggerProcessor loggerProcessor)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _queueProcessor = loggerProcessor;
        }

        internal ConsoleFormatter Formatter { get; set; }
        internal IExternalScopeProvider ScopeProvider { get; set; }
        internal ConsoleLoggerOptions Options { get; set; }

        [ThreadStatic]
        private static StringWriter t_stringWriter;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            if (t_stringWriter == null) t_stringWriter= new StringWriter();
            LogEntry<TState> logEntry = new LogEntry<TState>(logLevel, _name, eventId, state, exception, formatter);
            Formatter.Write(in logEntry, ScopeProvider, t_stringWriter);

            var sb = t_stringWriter.GetStringBuilder();
            if (sb.Length == 0)
            {
                return;
            }
            string computedAnsiString = sb.ToString();
            sb.Clear();
            if (sb.Capacity > 1024)
            {
                sb.Capacity = 1024;
            }
            _queueProcessor.EnqueueMessage(new LogMessageEntry(computedAnsiString, logAsError: logLevel >= Options.LogToStandardErrorThreshold));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public IDisposable BeginScope<TState>(TState state) => ScopeProvider?.Push(state) ?? NullScope.Instance;
    }
}
