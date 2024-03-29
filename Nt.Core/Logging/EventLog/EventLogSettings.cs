﻿using System;

namespace Nt.Core.Logging.EventLog
{
    /// <summary>
    /// Settings for <see cref="EventLogLogger"/>.
    /// </summary>
    public class EventLogSettings
    {
        private IEventLog _eventLog;

        /// <summary>
        /// Name of the event log. If <c>null</c> or not specified, "Application" is the default.
        /// </summary>
        public string LogName { get; set; }

        /// <summary>
        /// Name of the event log source. If <c>null</c> or not specified, ".NET Runtime" is the default.
        /// </summary>
        public string SourceName { get; set; }

        /// <summary>
        /// Name of the machine having the event log. If <c>null</c> or not specified, local machine is the default.
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// The function used to filter events based on the log level.
        /// </summary>
        public Func<string, LogLevel, bool> Filter { get; set; }

        internal IEventLog EventLog
        {
            get => _eventLog ?? CreateDefaultEventLog();

            // For unit testing purposes only.
            set => _eventLog = value;
        }

        private IEventLog CreateDefaultEventLog()
        {
            string logName = string.IsNullOrEmpty(LogName) ? "Application" : LogName;
            string machineName = string.IsNullOrEmpty(MachineName) ? "." : MachineName;
            string sourceName = string.IsNullOrEmpty(SourceName) ? ".NET Runtime" : SourceName;
            int? defaultEventId = null;

            if (string.IsNullOrEmpty(SourceName))
            {
                sourceName = ".NET Runtime";
                defaultEventId = 1000;
            }

#if NETSTANDARD
            Debug.Assert(RuntimeInformation.IsOSPlatform(OSPlatform.Windows));
#endif
            return new WindowsEventLog(logName, machineName, sourceName) { DefaultEventId = defaultEventId };
        }
    }
}
