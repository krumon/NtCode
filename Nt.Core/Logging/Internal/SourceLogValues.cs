using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Nt.Core.Logging.Internal
{
    /// <summary>
    /// LogValues to enable formatting options supported by <see cref="string.Format(IFormatProvider, string, object?)"/>.
    /// This also enables using {NamedformatItem} in the format string.
    /// </summary>
    internal readonly struct SourceLogValues 
    {
        internal const int MaxCachedFormatters = 1024;
        private const string NullFormat = "[null]";
        private static int _count;
        //private static ConcurrentDictionary<string, LogValuesFormatter> _formatters = new ConcurrentDictionary<string, LogValuesFormatter>();
        private readonly string _formatter;
        private readonly object[] _values;
        private readonly string _originalMessage;

        public SourceLogValues(string format, params object[] values)
        {
            if (values != null && values.Length != 0 && format != null)
            {
                _formatter = string.Empty;
            }
            else
            {
                _formatter = null;
            }

            _originalMessage = format ?? NullFormat;
            _values = values;
        }

        public override string ToString()
        {
            if (_formatter == null)
            {
                return _originalMessage;
            }

            return _formatter.Format(_values);
        }
    }
}
