using System.Collections.Generic;
using System;

namespace Nt.Core.Ninjascripts.Internal
{
    internal class Ninjascript : INinjascript
    {

        public NinjascriptInfo[] Ninjascripts { get; set; }
        //public MessageLogger[] MessageLoggers { get; set; }

        public Ninjascript()
        {

        }

        //public void Calculate()
        //{
        //    MessageLogger[] loggers = MessageLoggers;
        //    if (loggers == null)
        //        return;

        //    List<Exception> exceptions = null;
        //    for (int i = 0; i < loggers.Length; i++)
        //    {
        //        ref readonly MessageLogger loggerInfo = ref loggers[i];
        //        if (!loggerInfo.IsEnabled(logLevel))
        //            continue;

        //        LoggerLog(logLevel, eventId, loggerInfo.Logger, exception, formatter, ref exceptions, state);
        //    }

        //    if (exceptions != null && exceptions.Count > 0)
        //    {
        //        ThrowLoggingError(exceptions);
        //    }
        //}

        //public bool IsEnabled(LogLevel logLevel)
        //{
        //    MessageLogger[] loggers = MessageLoggers;
        //    if (loggers == null)
        //    {
        //        return false;
        //    }

        //    List<Exception> exceptions = null;
        //    int i = 0;
        //    for (; i < loggers.Length; i++)
        //    {
        //        ref readonly MessageLogger loggerInfo = ref loggers[i];
        //        if (!loggerInfo.IsEnabled(logLevel))
        //        {
        //            continue;
        //        }

        //        if (LoggerIsEnabled(logLevel, loggerInfo.Logger, ref exceptions))
        //        {
        //            break;
        //        }
        //    }

        //    if (exceptions != null && exceptions.Count > 0)
        //    {
        //        ThrowLoggingError(exceptions);
        //    }

        //    return i < loggers.Length;

        //}
        private static void ThrowLoggingError(List<Exception> exceptions)
        {
            throw new AggregateException(
                message: "An error occurred while writing to logger(s).", innerExceptions: exceptions);
        }

    }
}
