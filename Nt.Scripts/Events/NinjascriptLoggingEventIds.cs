
using Nt.Core.Logging;

namespace Nt.Scripts.Events
{
    internal static class NinjascriptLoggingEventIds
    {
        public static readonly EventId PrintToOutput = new EventId(1, nameof(PrintToOutput));
        public static readonly EventId ClearOutputWindow = new EventId(2, nameof(ClearOutputWindow));
        public static readonly EventId PrintToFile = new EventId(3, nameof(PrintToFile));
    }
}
