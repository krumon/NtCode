using Nt.Core.Options;

namespace Nt.Core.Logging.Internal
{
    internal sealed class FileLogFiltersConfigureOptions : IConfigureOptions<LoggerFilterOptions>
    {
        private readonly LogMessageType _logType;

        public FileLogFiltersConfigureOptions(LogMessageType logType)
        {
            _logType = logType;
        }

        public void Configure(LoggerFilterOptions options)
        {
            //foreach (LoggerFilterRule rule in _eventSource.GetFilterRules())
            //{
            //    options.Rules.Add(rule);
            //}
        }
    }
}
