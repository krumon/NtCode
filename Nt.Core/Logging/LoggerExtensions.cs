using Kr.Core.Extensions;
using Nt.Core.Logging.Internal;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Nt.Core.Logging
{
    /// <summary>
    /// Extensions for <see cref="ILogger"/> loggers
    /// </summary>
    public static class LoggerExtensions
    {
        private static readonly Func<FormattedLogValues, Exception, string> _messageFormatter = MessageFormatter;
        private static readonly Func<SourceLogValues, Exception, string> _messageSourceFormatter = MessageSourceFormatter;

        //------------------------------------------DEBUG------------------------------------------//

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogDebug(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogDebug(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, eventId, exception, message, args);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogDebug(0, "Processing request from {Address}", address)</example>
        public static void LogDebug(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, eventId, message, args);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogDebug(exception, "Error while processing request from {Address}", address)</example>
        public static void LogDebug(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, exception, message, args);
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogDebug("Processing request from {Address}", address)</example>
        public static void LogDebug(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Debug, message, args);
        }

        //------------------------------------------TRACE------------------------------------------//

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogTrace(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogTrace(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, eventId, exception, message, args);
        }

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogTrace(0, "Processing request from {Address}", address)</example>
        public static void LogTrace(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, eventId, message, args);
        }

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogTrace(exception, "Error while processing request from {Address}", address)</example>
        public static void LogTrace(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, exception, message, args);
        }

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogTrace("Processing request from {Address}", address)</example>
        public static void LogTrace(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Trace, message, args);
        }

        //------------------------------------------INFORMATION------------------------------------------//

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogInformation(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, eventId, exception, message, args);
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation(0, "Processing request from {Address}", address)</example>
        public static void LogInformation(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, eventId, message, args);
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation(exception, "Error while processing request from {Address}", address)</example>
        public static void LogInformation(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, exception, message, args);
        }

        /// <summary>
        /// Formats and writes an informational log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogInformation("Processing request from {Address}", address)</example>
        public static void LogInformation(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Information, message, args);
        }

        //------------------------------------------WARNING------------------------------------------//

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogWarning(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogWarning(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, eventId, exception, message, args);
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogWarning(0, "Processing request from {Address}", address)</example>
        public static void LogWarning(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, eventId, message, args);
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogWarning(exception, "Error while processing request from {Address}", address)</example>
        public static void LogWarning(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, exception, message, args);
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogWarning("Processing request from {Address}", address)</example>
        public static void LogWarning(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Warning, message, args);
        }

        //------------------------------------------ERROR------------------------------------------//

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogError(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogError(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, eventId, exception, message, args);
        }

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogError(0, "Processing request from {Address}", address)</example>
        public static void LogError(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, eventId, message, args);
        }

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogError(exception, "Error while processing request from {Address}", address)</example>
        public static void LogError(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, exception, message, args);
        }

        /// <summary>
        /// Formats and writes an error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogError("Processing request from {Address}", address)</example>
        public static void LogError(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Error, message, args);
        }

        //------------------------------------------CRITICAL------------------------------------------//

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogCritical(this ILogger logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, eventId, exception, message, args);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, "Processing request from {Address}", address)</example>
        public static void LogCritical(this ILogger logger, EventId eventId, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, eventId, message, args);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(exception, "Error while processing request from {Address}", address)</example>
        public static void LogCritical(this ILogger logger, Exception exception, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, exception, message, args);
        }

        /// <summary>
        /// Formats and writes a critical log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical("Processing request from {Address}", address)</example>
        public static void LogCritical(this ILogger logger, string message, params object[] args)
        {
            logger.Log(LogLevel.Critical, message, args);
        }

        //------------------------------------------LOG------------------------------------------//

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Log(this ILogger logger, LogLevel logLevel, string message, params object[] args)
        {
            logger.Log(logLevel, 0, null, message, args);
        }

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, string message, params object[] args)
        {
            logger.Log(logLevel, eventId, null, message, args);
        }

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Log(this ILogger logger, LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            logger.Log(logLevel, 0, exception, message, args);
        }

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void Log(this ILogger logger, LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(logLevel, eventId, new FormattedLogValues(message, args), exception, _messageFormatter);
        }

        //------------------------------------------TRACE SOURCE------------------------------------------//

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogTraceSource(
            this ILogger logger,
            EventId eventId,
            Exception exception,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Trace, eventId, exception, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, "Processing request from {Address}", address)</example>
        public static void LogTraceSource(
            this ILogger logger,
            EventId eventId,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Trace, eventId, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(exception, "Error while processing request from {Address}", address)</example>
        public static void LogTraceSource(
            this ILogger logger,
            Exception exception,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Trace, exception, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a trace log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical("Processing request from {Address}", address)</example>
        public static void LogTraceSource(
            this ILogger logger,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Trace, message, args.Prepend(origin, filePath, lineNumber));
        }

        //------------------------------------------DEBUG SOURCE------------------------------------------//

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogDebugSource(
            this ILogger logger,
            EventId eventId,
            Exception exception,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Debug, eventId, exception, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, "Processing request from {Address}", address)</example>
        public static void LogDebugSource(
            this ILogger logger,
            EventId eventId,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Debug, eventId, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(exception, "Error while processing request from {Address}", address)</example>
        public static void LogDebugSource(
            this ILogger logger,
            Exception exception,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Debug, exception, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a debug log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical("Processing request from {Address}", address)</example>
        public static void LogDebugSource(
            this ILogger logger,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Debug, message, args.Prepend(origin, filePath, lineNumber));
        }

        //------------------------------------------INFORMATION SOURCE------------------------------------------//

        /// <summary>
        /// Formats and writes a information log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogInformationSource(
            this ILogger logger,
            EventId eventId,
            Exception exception,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Information, eventId, exception, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a information log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, "Processing request from {Address}", address)</example>
        public static void LogInformationSource(
            this ILogger logger,
            EventId eventId,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Information, eventId, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a information log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(exception, "Error while processing request from {Address}", address)</example>
        public static void LogInformationSource(
            this ILogger logger,
            Exception exception,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Information, exception, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a information log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical("Processing request from {Address}", address)</example>
        public static void LogInformationSource(
            this ILogger logger,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Information, message, args.Prepend(origin, filePath, lineNumber));
        }

        //------------------------------------------WARNING SOURCE------------------------------------------//

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogWarningSource(
            this ILogger logger,
            EventId eventId,
            Exception exception,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Warning, eventId, exception, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, "Processing request from {Address}", address)</example>
        public static void LogWarningSource(
            this ILogger logger,
            EventId eventId,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Warning, eventId, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(exception, "Error while processing request from {Address}", address)</example>
        public static void LogWarningSource(
            this ILogger logger,
            Exception exception,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Warning, exception, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a warning log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical("Processing request from {Address}", address)</example>
        public static void LogWarningSource(
            this ILogger logger,
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Warning, message, args.Prepend(origin, filePath, lineNumber));
        }


        //------------------------------------------ERROR SOURCE------------------------------------------//

        /// <summary>
        /// Formats and writes a error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, exception, "Error while processing request from {Address}", address)</example>
        public static void LogErrorSource(
            this ILogger logger,
            EventId eventId, 
            Exception exception, 
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Error, eventId, exception, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(0, "Processing request from {Address}", address)</example>
        public static void LogErrorSource(
            this ILogger logger, 
            EventId eventId, 
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Error, eventId, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical(exception, "Error while processing request from {Address}", address)</example>
        public static void LogErrorSource(
            this ILogger logger, 
            Exception exception, 
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Error, exception, message, args.Prepend(origin, filePath, lineNumber));
        }

        /// <summary>
        /// Formats and writes a error log message.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="message">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <example>logger.LogCritical("Processing request from {Address}", address)</example>
        public static void LogErrorSource(
            this ILogger logger, 
            string message,
            [CallerMemberName] string origin = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0,
            params object[] args)
        {
            logger.LogSource(LogLevel.Error, message, args.Prepend(origin, filePath, lineNumber));
        }

        //------------------------------------------LOG SOURCE------------------------------------------//

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogSource(this ILogger logger, LogLevel logLevel, string message, params object[] args)
        {
            logger.LogSource(logLevel, 0, null, message, args);
        }

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogSource(this ILogger logger, LogLevel logLevel, EventId eventId, string message, params object[] args)
        {
            logger.LogSource(logLevel, eventId, null, message, args);
        }

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">Format string of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogSource(this ILogger logger, LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            logger.LogSource(logLevel, 0, exception, message, args);
        }

        /// <summary>
        /// Formats and writes a log message at the specified log level.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to write to.</param>
        /// <param name="logLevel">Entry will be written on this level.</param>
        /// <param name="eventId">The event id associated with the log.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">String of the log message.</param>
        /// <param name="args">An object array that contains zero or more objects to source format.</param>
        public static void LogSource(this ILogger logger, LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args)
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(logLevel, eventId, new SourceLogValues(message, args), exception, _messageSourceFormatter);
        }

        //------------------------------------------Scope------------------------------------------//

        /// <summary>
        /// Formats the message and creates a scope.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> to create the scope in.</param>
        /// <param name="messageFormat">Format string of the log message in message template format. Example: <c>"User {User} logged in from {Address}"</c></param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>A disposable scope object. Can be null.</returns>
        /// <example>
        /// using(logger.BeginScope("Processing request from {Address}", address))
        /// {
        /// }
        /// </example>
        public static IDisposable BeginScope(
            this ILogger logger,
            string messageFormat,
            params object[] args)
        {
            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            return logger.BeginScope(new FormattedLogValues(messageFormat, args));
        }

        //------------------------------------------HELPERS------------------------------------------//

        private static string MessageFormatter(FormattedLogValues state, Exception error)
        {
            return state.ToString();
        }

        private static string MessageSourceFormatter(SourceLogValues state, Exception error)
        {
            return state.ToString();
        }
    }
}
